using DAL.Configuration.Database;
using DAL.Models.Authentication;

namespace DAL.Repositories.Implementation
{
    public class UserRoleRepository(HerdSyncDbContext context, ILogger<UserRoleRepository> logger) : IUserRoleRepository
    {
        public async Task<IEnumerable<UserRoleModel>> GetAllAsync()
        {
            return await context.UserRoles
                .Where(u => !u.IsDeleted)
                .ToListAsync();
        }

        public async Task<UserRoleModel?> GetByCodeAsync(string code)
        {
            return await context.UserRoles
                .FirstOrDefaultAsync(u => u.RoleCode == code && !u.IsDeleted);
        }

        public async Task<UserRoleModel> AddAsync(UserRoleModel userRole)
        {
            context.UserRoles.Add(userRole);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new user role with code {RoleCode}", userRole.RoleCode);
            return userRole;
        }

        public async Task<UserRoleModel> UpdateAsync(UserRoleModel userRole)
        {
            context.UserRoles.Update(userRole);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated user role with code {RoleCode}", userRole.RoleCode);
            return userRole;
        }

        public async Task SoftDeleteAsync(string code)
        {
            var entity = await context.UserRoles.FirstOrDefaultAsync(u => u.RoleCode == code);
            if (entity == null) throw new KeyNotFoundException($"UserRole '{code}' not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted user role with code {RoleCode}", code);
        }
    }
}