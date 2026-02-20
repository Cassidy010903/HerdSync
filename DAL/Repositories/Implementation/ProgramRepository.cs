using DAL.Configuration.Database;
using DAL.Models;
using HerdSync.Shared.Enums.Data;

namespace DAL.Services.Implementation
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly HerdsyncDBContext _db;

        public ProgramRepository(HerdsyncDBContext db) => _db = db;

        public async Task<prg_Program> CreateAsync(
            string name,
            IEnumerable<(AgeGroupEnum? age, GenderEnum? gender, AnimalTypeEnum? species, IEnumerable<Guid> treatmentIds)> rules)
        {
            var program = new prg_Program { Id = Guid.NewGuid(), ProgramName = name };

            foreach (var (age, gender, species, tids) in rules)
            {
                var instr = new ins_Instruction_Lookup
                {
                    Id = Guid.NewGuid(),
                    Program = program,
                    TargetGroup = age,
                    TargetGender = gender,
                    TargetSpecies = species
                };

                int i = 0;
                foreach (var tid in tids)
                {
                    instr.Treatments.Add(new itr_Instruction_Treatment
                    {
                        Id = Guid.NewGuid(),
                        Instruction = instr,
                        TreatmentId = tid,
                        SortOrder = i++
                    });
                }

                program.Instructions.Add(instr);
            }

            _db.ProgramOld.Add(program);
            await _db.SaveChangesAsync();
            return program;
        }

        public Task<List<prg_Program>> ListAsync() =>
            _db.ProgramOld
                .Include(p => p.Instructions)
                .ThenInclude(i => i.Treatments)
                .ToListAsync();

        public Task<prg_Program?> GetAsync(Guid programId) =>
            _db.ProgramOld
                .Include(p => p.Instructions)
                .ThenInclude(i => i.Treatments)
                .ThenInclude(it => it.Treatment)
                .FirstOrDefaultAsync(p => p.Id == programId);
    }
}