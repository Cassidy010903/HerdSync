using DAL.Configuration.Database;
using DAL.Constants;
using DAL.Models;
using DAL.Models.Base.History;
using HerdSync.Shared;
using HerdSync.Shared.DTO;
using HerdSync.Shared.Enums.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Services.Implementation
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly HerdsyncDBContext _context;

        public SpeciesRepository(HerdsyncDBContext context)
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
            var entity = await _context.Species.FindAsync(id);
            if (entity == null) return;

            var history = new SpeciesDetailHistory
            {
                HistoryId = Guid.NewGuid(),
                OriginalId = entity.Id,
                spd_Tag_Colour = entity.spd_Tag_Colour,
                spd_Weight = entity.spd_Weight,
                spd_Age = entity.spd_Age,
                spd_AgeGroup = entity.spd_AgeGroup,
                spd_Mother = entity.spd_Mother,
                spd_Father = entity.spd_Father,
                spd_Est_Years_Left = entity.spd_Est_Years_Left,
                spd_Medical_Note = entity.spd_Medical_Note,
                spd_Last_Pregnancy = entity.spd_Last_Pregnancy,
                spd_Total_Pregnancies = entity.spd_Total_Pregnancies,
                spd_Total_Offspring = entity.spd_Total_Offspring,
                spd_Branded = entity.spd_Branded,
                spd_Species = entity.spd_Species,
                spd_Gender = entity.spd_Gender,
                prg_Pregnancy_Id = entity.prg_Pregnancy_Id,
                spd_Born_Or_Buy = entity.spd_Born_Or_Buy,
                DeletedDateTime = DateTime.UtcNow,
                DeletedUser = deletedBy
            };

            _context.SpeciesHistory.Add(history);
            _context.Species.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task AddSpecies(spd_Species_Detail spd)
        {
            if (spd == null)
                throw new ArgumentNullException(nameof(spd));

            _context.Species.Add(spd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSpecies(spd_Species_Detail updated)
        {
            var existing = await _context.Species.FindAsync(updated.Id);
            if (existing == null)
                throw new InvalidOperationException("Cow not found.");

            // Update fields
            existing.spd_Number = updated.spd_Number;
            existing.spd_Tag_Colour = updated.spd_Tag_Colour;
            existing.spd_Weight = updated.spd_Weight;
            existing.spd_Age = updated.spd_Age;
            existing.spd_AgeGroup = updated.spd_AgeGroup;
            existing.spd_Mother = updated.spd_Mother;
            existing.spd_Father = updated.spd_Father;
            existing.spd_Est_Years_Left = updated.spd_Est_Years_Left;
            existing.spd_Medical_Note = updated.spd_Medical_Note;
            existing.spd_Last_Pregnancy = updated.spd_Last_Pregnancy;
            existing.spd_Total_Pregnancies = updated.spd_Total_Pregnancies;
            existing.spd_Total_Offspring = updated.spd_Total_Offspring;
            existing.spd_Branded = updated.spd_Branded;
            existing.spd_Species = updated.spd_Species;
            existing.spd_Gender = updated.spd_Gender;
            existing.prg_Pregnancy_Id = updated.prg_Pregnancy_Id;
            existing.spd_Born_Or_Buy = updated.spd_Born_Or_Buy;

            await _context.SaveChangesAsync();
        }
        public async Task<List<spd_Species_Detail>> GetAllSpeciesAsync()
        {
            return await _context.Species.ToListAsync();
        }
    }
}

