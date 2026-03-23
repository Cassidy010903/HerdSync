using BCrypt.Net;
using BLL.Services.Authorization;
using DAL.Configuration.Database;
using DAL.Models.Authentication;
using DAL.Models.Farm;
using DAL.Repositories;
using HerdSync.Shared.DTO.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services.Authorization.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly HerdsyncDBContext _context;
        private readonly IFarmInviteRepository _inviteRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            HerdsyncDBContext context,
            IFarmInviteRepository inviteRepository,
            ILogger<AuthService> logger)
        {
            _context = context;
            _inviteRepository = inviteRepository;
            _logger = logger;
        }

        // ─────────────────────────────────────────────
        // REGISTER OWNER
        // ─────────────────────────────────────────────
        public async Task<AuthResultDTO> RegisterOwnerAsync(RegisterOwnerDTO dto)
        {
            if (await _context.UserAccounts.AnyAsync(u => u.Username == dto.Username))
                return Fail("Username is already taken.");

            if (await _context.Farms.AnyAsync(f => f.FarmName == dto.FarmName))
                return Fail("A farm with that name already exists.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Create user
                var user = new UserAccountModel
                {
                    UserId = Guid.NewGuid(),
                    Username = dto.Username,
                    DisplayName = dto.DisplayName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    PasswordHash = HashPassword(dto.Password),
                    IsActive = true,
                    IsSystemAdmin = false
                };
                _context.UserAccounts.Add(user);
                await _context.SaveChangesAsync();

                // 2. Create farm
                var farm = new FarmModel
                {
                    FarmId = Guid.NewGuid(),
                    FarmName = dto.FarmName,
                    OwnerUserId = user.UserId
                };
                _context.Farms.Add(farm);
                await _context.SaveChangesAsync();

                // 3. Link user to farm as owner
                var farmUser = new FarmUserModel
                {
                    FarmUserId = Guid.NewGuid(),
                    FarmId = farm.FarmId,
                    UserId = user.UserId,
                    RoleCode = "OWNR",
                    IsActive = true
                };
                _context.FarmUsers.Add(farmUser);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                _logger.LogInformation("New farm owner registered: {Username}, Farm: {FarmName}", user.Username, farm.FarmName);

                return new AuthResultDTO
                {
                    Success = true,
                    UserId = user.UserId,
                    Username = user.Username,
                    DisplayName = user.DisplayName,
                    FarmId = farm.FarmId,
                    FarmName = farm.FarmName,
                    RoleCode = "OWNR",
                    IsSystemAdmin = false
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "RegisterOwnerAsync failed for {Username}", dto.Username);
                return Fail("Registration failed. Please try again.");
            }
        }

        // ─────────────────────────────────────────────
        // REGISTER INVITED USER
        // ─────────────────────────────────────────────
        public async Task<AuthResultDTO> RegisterInvitedAsync(RegisterInvitedDTO dto)
        {
            // 1. Validate invite
            var invite = await _inviteRepository.GetByCodeAsync(dto.InviteCode);

            if (invite == null)
                return Fail("Invalid invite code.");

            if (invite.IsUsed)
                return Fail("This invite code has already been used.");

            if (invite.ExpiresAt < DateTime.UtcNow)
                return Fail("This invite code has expired.");

            // 2. Check username availability
            if (await _context.UserAccounts.AnyAsync(u => u.Username == dto.Username))
                return Fail("Username is already taken.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 3. Create user
                var user = new UserAccountModel
                {
                    UserId = Guid.NewGuid(),
                    Username = dto.Username,
                    DisplayName = dto.DisplayName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    PasswordHash = HashPassword(dto.Password),
                    IsActive = true,
                    IsSystemAdmin = false
                };
                _context.UserAccounts.Add(user);
                await _context.SaveChangesAsync();

                // 4. Link user to farm with the role from the invite
                var farmUser = new FarmUserModel
                {
                    FarmUserId = Guid.NewGuid(),
                    FarmId = invite.FarmId,
                    UserId = user.UserId,
                    RoleCode = invite.AssignedRoleCode,
                    IsActive = true
                };
                _context.FarmUsers.Add(farmUser);
                await _context.SaveChangesAsync();

                // 5. Mark invite as used
                await _inviteRepository.MarkUsedAsync(invite.InviteId, user.UserId);

                await transaction.CommitAsync();
                _logger.LogInformation("Invited user registered: {Username}, Farm: {FarmId}, Role: {Role}",
                    user.Username, invite.FarmId, invite.AssignedRoleCode);

                return new AuthResultDTO
                {
                    Success = true,
                    UserId = user.UserId,
                    Username = user.Username,
                    DisplayName = user.DisplayName,
                    FarmId = invite.FarmId,
                    FarmName = invite.Farm?.FarmName,
                    RoleCode = invite.AssignedRoleCode,
                    IsSystemAdmin = false
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "RegisterInvitedAsync failed for {Username}", dto.Username);
                return Fail("Registration failed. Please try again.");
            }
        }

        // ─────────────────────────────────────────────
        // LOGIN
        // ─────────────────────────────────────────────
        public async Task<AuthResultDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null || !user.IsActive)
                return Fail("Invalid username or password.");

            if (!VerifyPassword(dto.Password, user.PasswordHash))
                return Fail("Invalid username or password.");

            // System admin — no farm context needed
            if (user.IsSystemAdmin)
            {
                return new AuthResultDTO
                {
                    Success = true,
                    UserId = user.UserId,
                    Username = user.Username,
                    DisplayName = user.DisplayName,
                    FarmId = null,
                    FarmName = null,
                    RoleCode = "ADMN",
                    IsSystemAdmin = true
                };
            }

            // Regular user — get their farm + role
            var farmUser = await _context.FarmUsers
                .Include(fu => fu.Farm)
                .FirstOrDefaultAsync(fu => fu.UserId == user.UserId && fu.IsActive);

            if (farmUser == null)
                return Fail("No active farm association found for this user.");

            return new AuthResultDTO
            {
                Success = true,
                UserId = user.UserId,
                Username = user.Username,
                DisplayName = user.DisplayName,
                FarmId = farmUser.FarmId,
                FarmName = farmUser.Farm?.FarmName,
                RoleCode = farmUser.RoleCode,
                IsSystemAdmin = false
            };
        }

        // ─────────────────────────────────────────────
        // INVITE MANAGEMENT
        // ─────────────────────────────────────────────
        public async Task<InviteResultDTO> CreateInviteAsync(CreateInviteDTO dto)
        {
            try
            {
                var invite = new FarmInviteModel
                {
                    InviteId = Guid.NewGuid(),
                    FarmId = dto.FarmId,
                    InviteCode = GenerateInviteCode(),
                    AssignedRoleCode = dto.AssignedRoleCode,
                    ExpiresAt = DateTime.UtcNow.AddHours(dto.ExpiryHours),
                    IsUsed = false
                };

                await _inviteRepository.AddAsync(invite);
                _logger.LogInformation("Invite created for Farm {FarmId}, Role {Role}, Code {Code}",
                    dto.FarmId, dto.AssignedRoleCode, invite.InviteCode);

                return new InviteResultDTO
                {
                    Success = true,
                    InviteCode = invite.InviteCode,
                    ExpiresAt = invite.ExpiresAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateInviteAsync failed for Farm {FarmId}", dto.FarmId);
                return new InviteResultDTO { Success = false, ErrorMessage = "Failed to create invite." };
            }
        }

        public async Task<List<InviteResultDTO>> GetActiveInvitesAsync(Guid farmId)
        {
            var invites = await _inviteRepository.GetByFarmIdAsync(farmId);

            return invites
                .Where(i => !i.IsUsed && i.ExpiresAt > DateTime.UtcNow)
                .Select(i => new InviteResultDTO
                {
                    Success = true,
                    InviteCode = i.InviteCode,
                    ExpiresAt = i.ExpiresAt
                })
                .ToList();
        }

        // ─────────────────────────────────────────────
        // PRIVATE HELPERS
        // ─────────────────────────────────────────────
        private static byte[] HashPassword(string plainPassword)
        {
            string hashed = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            return Encoding.UTF8.GetBytes(hashed);
        }

        private static bool VerifyPassword(string plainPassword, byte[] storedHash)
        {
            string hashString = Encoding.UTF8.GetString(storedHash);
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashString);
        }

        private static string GenerateInviteCode()
        {
            // e.g. "A3F9K2" — 6 uppercase alphanumeric
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789"; // removed ambiguous chars: 0/O, 1/I
            return new string(Enumerable.Range(0, 6)
                .Select(_ => chars[RandomNumberGenerator.GetInt32(chars.Length)])
                .ToArray());
        }

        private static AuthResultDTO Fail(string message) =>
            new AuthResultDTO { Success = false, ErrorMessage = message };
    }
}