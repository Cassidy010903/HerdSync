using DAL.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IFarmInviteRepository
    {
        Task<FarmInviteModel> GetByCodeAsync(string inviteCode);
        Task<List<FarmInviteModel>> GetByFarmIdAsync(Guid farmId);
        Task AddAsync(FarmInviteModel invite);
        Task MarkUsedAsync(Guid inviteId, Guid usedByUserId);
    }
}
