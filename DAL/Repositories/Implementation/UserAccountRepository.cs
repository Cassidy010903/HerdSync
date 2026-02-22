using DAL.Configuration.Database;
using DAL.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.Implementation
{
    public class UserAccountRepository(HerdsyncDBContext context, ILogger<UserAccountRepository> logger) : IUserAccountRepository
    {
        public async Task<IEnumerable<UserAccountModel>> GetAllAsync()
        {
            return await context.UserAccounts
                .Where(u => !u.IsDeleted)
                .ToListAsync();
        }

        public async Task<UserAccountModel?> GetByIdAsync(Guid userAccountId)
        {
            return await context.UserAccounts
                .FirstOrDefaultAsync(u => u.UserId == userAccountId && !u.IsDeleted);
        }

        public async Task<UserAccountModel> AddAsync(UserAccountModel userAccount)
        {
            context.UserAccounts.Add(userAccount);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new user account with ID {UserAccountId}", userAccount.UserId);
            return userAccount;
        }

        public async Task<UserAccountModel> UpdateAsync(UserAccountModel userAccount)
        {
            context.UserAccounts.Update(userAccount);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated user account with ID {UserAccountId}", userAccount.UserId);
            return userAccount;
        }

        public async Task SoftDeleteAsync(Guid userAccountId)
        {
            var entity = await context.UserAccounts.FirstOrDefaultAsync(u => u.UserId == userAccountId);
            if (entity == null) throw new KeyNotFoundException($"UserAccount {userAccountId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted user account with ID {UserAccountId}", userAccountId);
        }
    }
}