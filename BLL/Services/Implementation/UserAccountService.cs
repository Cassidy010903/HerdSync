using AutoMapper;
using DAL.Models.Authentication;
using DAL.Repositories;
using HerdSync.Shared.DTO.Authentication;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class UserAccountService(IMapper mapper, IUserAccountRepository repository, ILogger<UserAccountService> logger) : IUserAccountService
    {
        public async Task<IEnumerable<UserAccountDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<UserAccountDTO>>(entities);
        }

        public async Task<UserAccountDTO?> GetByIdAsync(Guid userAccountId)
        {
            var entity = await repository.GetByIdAsync(userAccountId);
            return entity == null ? null : mapper.Map<UserAccountDTO>(entity);
        }

        public async Task<UserAccountDTO> CreateAsync(UserAccountDTO userAccountDTO)
        {
            var entity = mapper.Map<UserAccountModel>(userAccountDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new user account with ID {UserAccountId}", created.UserId);
            return mapper.Map<UserAccountDTO>(created);
        }

        public async Task<UserAccountDTO> UpdateAsync(UserAccountDTO userAccountDTO)
        {
            var entity = mapper.Map<UserAccountModel>(userAccountDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated user account with ID {UserAccountId}", updated.UserId);
            return mapper.Map<UserAccountDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid userAccountId)
        {
            await repository.SoftDeleteAsync(userAccountId);
            logger.LogInformation("Soft deleted user account with ID {UserAccountId}", userAccountId);
        }
    }
}