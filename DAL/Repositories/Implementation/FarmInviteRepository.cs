using DAL.Configuration.Database;
using DAL.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementation
{
    public class FarmInviteRepository : IFarmInviteRepository
    {
        private readonly HerdsyncDBContext _context;

        public FarmInviteRepository(HerdsyncDBContext context)
        {
            _context = context;
        }

        public async Task<FarmInviteModel> GetByCodeAsync(string inviteCode)
        {
            return await _context.FarmInvites
                .Include(i => i.Farm)
                .Include(i => i.Role)
                .FirstOrDefaultAsync(i => i.InviteCode == inviteCode);
        }

        public async Task<List<FarmInviteModel>> GetByFarmIdAsync(Guid farmId)
        {
            return await _context.FarmInvites
                .Include(i => i.Role)
                .Where(i => i.FarmId == farmId)
                .OrderByDescending(i => i.ExpiresAt)
                .ToListAsync();
        }

        public async Task AddAsync(FarmInviteModel invite)
        {
            if (invite == null)
                throw new ArgumentNullException(nameof(invite));

            _context.FarmInvites.Add(invite);
            await _context.SaveChangesAsync();
        }

        public async Task MarkUsedAsync(Guid inviteId, Guid usedByUserId)
        {
            var invite = await _context.FarmInvites.FindAsync(inviteId);
            if (invite == null)
                throw new InvalidOperationException("Invite not found.");

            invite.IsUsed = true;
            invite.UsedByUserId = usedByUserId;
            await _context.SaveChangesAsync();
        }
    }
}
