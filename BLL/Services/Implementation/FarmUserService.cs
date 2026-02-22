using AutoMapper;
using DAL.Models.Authentication;
using DAL.Repositories;
using HerdSync.Shared.DTO.Authentication;
using Microsoft.Extensions.Logging;

namespace BLL.Services.Implementation
{
    public class FarmUserService(IMapper mapper, IFarmUserRepository repository, ILogger<FarmUserService> logger) : IFarmUserService
    {
        public async Task<IEnumerable<FarmUserDTO>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<FarmUserDTO>>(entities);
        }

        public async Task<FarmUserDTO?> GetByIdAsync(Guid farmUserId)
        {
            var entity = await repository.GetByIdAsync(farmUserId);
            return entity == null ? null : mapper.Map<FarmUserDTO>(entity);
        }

        public async Task<FarmUserDTO> CreateAsync(FarmUserDTO farmUserDTO)
        {
            var entity = mapper.Map<FarmUserModel>(farmUserDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var created = await repository.AddAsync(entity);
            logger.LogInformation("Created new farm user with ID {FarmUserId}", created.UserId);
            return mapper.Map<FarmUserDTO>(created);
        }

        public async Task<FarmUserDTO> UpdateAsync(FarmUserDTO farmUserDTO)
        {
            var entity = mapper.Map<FarmUserModel>(farmUserDTO);
            if (entity == null)
                throw new InvalidOperationException("Mapping from DTO to entity failed.");
            var updated = await repository.UpdateAsync(entity);
            logger.LogInformation("Updated farm user with ID {FarmUserId}", updated.UserId);
            return mapper.Map<FarmUserDTO>(updated);
        }

        public async Task SoftDeleteAsync(Guid farmUserId)
        {
            await repository.SoftDeleteAsync(farmUserId);
            logger.LogInformation("Soft deleted farm user with ID {FarmUserId}", farmUserId);
        }
    }
}