using AutoMapper;
using DAL.Models.Authentication;
using DAL.Repositories;
using HerdSync.Shared.DTO.Authentication;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class UserRoleService(IMapper mapper, IUserRoleRepository repository, ILogger<UserRoleService> logger) : IUserRoleService
    {
        public async Task<IEnumerable<UserRoleDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<UserRoleDTO>>(entities);
        }

        public async Task<UserRoleDTO?> GetByIdAsync(string code)
        {
            var entity = await repository.GetByCodeAsync(code);
            return entity == null ? null : mapper.Map<UserRoleDTO>(entity);
        }

        public async Task<UserRoleDTO> CreateAsync(UserRoleDTO userRoleDTO)
        {
            var entity = mapper.Map<UserRoleModel>(userRoleDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new user role with code {RoleCode}", created.RoleCode);
            return mapper.Map<UserRoleDTO>(created);
        }

        public async Task<UserRoleDTO> UpdateAsync(UserRoleDTO userRoleDTO)
        {
            var entity = mapper.Map<UserRoleModel>(userRoleDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated user role with code {RoleCode}", updated.RoleCode);
            return mapper.Map<UserRoleDTO>(updated);
        }

        public async Task SoftDeleteAsync(string code)
        {
            await repository.SoftDeleteAsync(code);
            logger.LogInformation("Soft deleted user role with code {RoleCode}", code);
        }
    }
}