using DAL.Configuration.Database;
using DAL.Constants;
using DAL.Models.Animal;
using HerdSync.Shared.Enums.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Services.Implementation
{
    public class AnimalRepository : ISpeciesRepository
    {
        private readonly HerdsyncDBContext _context;

        public AnimalRepository(HerdsyncDBContext context)
        {
            _context = context;
        }

        public List<GenderEnum> GetGenderOptions(AnimalTypeEnum species)
        {
            if (AnimalMetadata.AnimalMap.TryGetValue(species, out var info) && info?.GenderOptions != null)
            {
                return info.GenderOptions;
            }

            return new List<GenderEnum> { GenderEnum.Unknown };
        }

        public List<AgeGroupEnum> GetAgeGroupOptions(AnimalTypeEnum species)
        {
            if (AnimalMetadata.AnimalMap.TryGetValue(species, out var info) && info?.AgeGroup != null)
            {
                return info.AgeGroup;
            }

            return new List<AgeGroupEnum> { AgeGroupEnum.Unknown };
        }
        public async Task ArchiveAndDeleteSpeciesAsync(Guid id, string deletedBy)
        {
            var entity = await _context.Animals.FindAsync(id);
            if (entity == null) return;

            //var history = new SpeciesDetailHistory
            //{
            //    HistoryId = Guid.NewGuid(),
            //    OriginalId = entity.Id,
            //    spd_Tag_Colour = entity.spd_Tag_Colour,
            //    spd_Weight = entity.spd_Weight,
            //    spd_Age = entity.spd_Age,
            //    spd_AgeGroup = entity.spd_AgeGroup,
            //    spd_Mother = entity.spd_Mother,
            //    spd_Father = entity.spd_Father,
            //    spd_Est_Years_Left = entity.spd_Est_Years_Left,
            //    spd_Medical_Note = entity.spd_Medical_Note,
            //    spd_Last_Pregnancy = entity.spd_Last_Pregnancy,
            //    spd_Total_Pregnancies = entity.spd_Total_Pregnancies,
            //    spd_Total_Offspring = entity.spd_Total_Offspring,
            //    spd_Branded = entity.spd_Branded,
            //    spd_Species = entity.spd_Species,
            //    spd_Gender = entity.spd_Gender,
            //    prg_Pregnancy_Id = entity.prg_Pregnancy_Id,
            //    spd_Born_Or_Buy = entity.spd_Born_Or_Buy,
            //    DeletedDateTime = DateTime.UtcNow,
            //    DeletedUser = deletedBy
            //};

            //_context.SpeciesHistoryOld.Add(history);
            //_context.Species.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task AddSpecies(AnimalModel spd)
        {
            if (spd == null)
                throw new ArgumentNullException(nameof(spd));

            _context.Animals.Add(spd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSpecies(AnimalModel updated)
        {
            var existing = await _context.Animals.FindAsync(updated.AnimalId);
            if (existing == null)
                throw new InvalidOperationException("Cow not found.");

            // Update fields
            //existing.spd_Number = updated.spd_Number;
            //existing.spd_Tag_Colour = updated.spd_Tag_Colour;
            //existing.spd_Weight = updated.spd_Weight;
            //existing.spd_Age = updated.spd_Age;
            //existing.spd_AgeGroup = updated.spd_AgeGroup;
            //existing.spd_Mother = updated.spd_Mother;
            //existing.spd_Father = updated.spd_Father;
            //existing.spd_Est_Years_Left = updated.spd_Est_Years_Left;
            //existing.spd_Medical_Note = updated.spd_Medical_Note;
            //existing.spd_Last_Pregnancy = updated.spd_Last_Pregnancy;
            //existing.spd_Total_Pregnancies = updated.spd_Total_Pregnancies;
            //existing.spd_Total_Offspring = updated.spd_Total_Offspring;
            //existing.spd_Branded = updated.spd_Branded;
            //existing.spd_Species = updated.spd_Species;
            //existing.spd_Gender = updated.spd_Gender;
            //existing.prg_Pregnancy_Id = updated.prg_Pregnancy_Id;
            //existing.spd_Born_Or_Buy = updated.spd_Born_Or_Buy;

            await _context.SaveChangesAsync();
        }
        public async Task<List<AnimalModel>> GetAllSpeciesAsync()
        {
            return await _context.Animals.ToListAsync();
        }
    }

    public class AnimalRepository2(HerdsyncDBContext context, ILogger<AnimalRepository> logger) : IAnimalRepository
    {
        public async Task<IEnumerable<AnimalModel>> GetAllAsync()
        {
            return await context.Animals
                .Where(a => !a.IsDeleted)
                .ToListAsync();
        }

        public async Task<AnimalModel?> GetByIdAsync(Guid animalId)
        {
            return await context.Animals
                .FirstOrDefaultAsync(a => a.AnimalId == animalId && !a.IsDeleted);
        }

        public async Task<AnimalModel> AddAsync(AnimalModel animal)
        {
            context.Animals.Add(animal);
            await context.SaveChangesAsync();
            logger.LogInformation("Added new animal with ID {AnimalId}", animal.AnimalId);
            return animal;
        }

        public async Task<AnimalModel> UpdateAsync(AnimalModel animal)
        {
            context.Animals.Update(animal);
            await context.SaveChangesAsync();
            logger.LogInformation("Updated animal with ID {AnimalId}", animal.AnimalId);
            return animal;
        }

        public async Task SoftDeleteAsync(Guid animalId)
        {
            var entity = await context.Animals.FirstOrDefaultAsync(a => a.AnimalId == animalId);
            if (entity == null) throw new KeyNotFoundException($"Animal {animalId} not found.");
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            logger.LogInformation("Soft deleted animal with ID {AnimalId}", animalId);
        }
    }
}