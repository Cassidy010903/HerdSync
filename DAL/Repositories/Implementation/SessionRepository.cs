using DAL.Configuration.Database;
using DAL.Models;
using DAL.Services;
using HerdMark.Models;
using HerdMark.Services;
using System.Text;

namespace DAL.Repositories.Implementation
{
    public class SessionRepository : ISessionRepository
    {
        private readonly HerdsyncDBContext _db;
        private readonly ReadMappingService _reads;
        private readonly ISpeciesRepository _age;
        private readonly ILogger<SessionRepository> _log;
        private readonly string _backupFolder;

        public ase_Active_Session? Current { get; private set; }

        public event Action<ast_Animal_Session_Treatment>? OnAnimalAction;

        public SessionRepository(
            HerdsyncDBContext db,
            ReadMappingService reads,
            ISpeciesRepository age,
            ILogger<SessionRepository> log)
        {
            _db = db;
            _reads = reads;
            _age = age;
            _log = log;

            _backupFolder = Path.Combine(AppContext.BaseDirectory, "Backups");
            Directory.CreateDirectory(_backupFolder);

            _reads.OnTagRead += HandleTagRead;
        }

        // Start new session
        public async Task<ase_Active_Session> StartAsync(Guid programId, string? notes = null)
        {
            Current = new ase_Active_Session
            {
                Id = Guid.NewGuid(),
                ProgramId = programId,
                Notes = notes,
                StartedUtc = DateTime.UtcNow,
                IsClosed = false
            };

            _db.ProgramRunOld.Add(Current);
            await _db.SaveChangesAsync();

            return Current;
        }

        // Resume last open session (if exists)
        public async Task<ase_Active_Session?> ResumeAsync()
        {
            Current = await _db.ProgramRunOld
                .Include(r => r.Animals)
                .ThenInclude(a => a.Treatments)
                .FirstOrDefaultAsync(r => !r.IsClosed);

            return Current;
        }

        // End session & schedule deletion of backup
        public async Task EndAsync(Guid sessionId)
        {
            var run = await _db.ProgramRunOld.FindAsync(sessionId);
            if (run is null) return;

            run.IsClosed = true;
            run.EndedUtc = DateTime.UtcNow;

            var program = await _db.ProgramOld.FindAsync(run.ProgramId);
            if (program is not null)
                program.LastRunUtc = run.EndedUtc ?? run.StartedUtc;

            await _db.SaveChangesAsync();

            Current = null;

            string filePath = Path.Combine(_backupFolder, $"session_{sessionId}.csv");
            if (File.Exists(filePath))
            {
                _ = Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromDays(7));
                    try { File.Delete(filePath); }
                    catch (Exception ex) { _log.LogWarning(ex, "Failed to delete backup {file}", filePath); }
                });
            }
        }

        // Handle tag scans with 3-way matching
        private async void HandleTagRead(HerdMarkRead read)
        {
            try
            {
                if (Current is null || Current.IsClosed) return;

                var link = await _db.SpeciesTag.FirstOrDefaultAsync(t => t.stl_Tag_Id == read.TagId);
                if (link is null) { _log.LogWarning("Tag {tag} not mapped", read.TagId); return; }

                var animal = await _db.Animals.FirstOrDefaultAsync(a => a.AnimalId == link.spd_Id);
                if (animal is null) { _log.LogWarning("No animal for spd_Id {id}", link.spd_Id); return; }

                // Parse animal attributes
                //AgeGroupEnum? ageGroup = !string.IsNullOrWhiteSpace(animal.spd_AgeGroup)
                //    ? Enum.Parse<AgeGroupEnum>(animal.spd_AgeGroup)
                //    : null;

                //GenderEnum? gender = !string.IsNullOrWhiteSpace(animal.spd_Gender)
                //    ? Enum.Parse<GenderEnum>(animal.spd_Gender)
                //    : null;

                //AnimalTypeEnum? species = !string.IsNullOrWhiteSpace(animal.spd_Species)
                //    ? Enum.Parse<AnimalTypeEnum>(animal.spd_Species)
                //    : null;

                // Load program with instructions
                var program = await _db.ProgramOld
                    .Include(p => p.Instructions)
                    .ThenInclude(i => i.Treatments)
                    .FirstAsync(p => p.Id == Current.ProgramId);

                // Find matching instruction (3-way match: age, gender, species)
                //var instr = program.Instructions.FirstOrDefault(i =>
                //    (i.TargetGroup == null || i.TargetGroup == ageGroup) &&
                //    (i.TargetGender == null || i.TargetGender == gender) &&
                //    (i.TargetSpecies == null || i.TargetSpecies == species)
                //);

                //var defaultTreatmentIds = instr?.Treatments
                //    .OrderBy(t => t.SortOrder)
                //    .Select(t => t.TreatmentId)
                //    .ToList() ?? new List<Guid>();

                //var action = new ast_Animal_Session_Treatment
                //{
                //    Id = Guid.NewGuid(),
                //    SessionId = Current.Id,
                //    spd_Id = animal.Id,
                //    TagId = read.TagId,
                //    AgeGroup = ageGroup ?? AgeGroupEnum.Adult, // default if null
                //    ScannedUtc = DateTime.UtcNow,
                //    AutoApplied = true
                //};

                //foreach (var tid in defaultTreatmentIds)
                //{
                //    action.Treatments.Add(new atr_Animal_Treatment
                //    {
                //        Id = Guid.NewGuid(),
                //        AnimalAction = action,
                //        TreatmentId = tid,
                //        IsDefault = true,
                //        AppliedUtc = DateTime.UtcNow
                //    });
                //}

                //_db.ProgramRunAnimalOld.Add(action);
                await _db.SaveChangesAsync();

                // Write to CSV backup
                //await AppendToCsvAsync(action);

                //OnAnimalAction?.Invoke(action);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "HandleTagRead failed for tag {tag}", read.TagId);
            }
        }

        // Add extra treatment per animal
        public async Task AddExtraTreatmentAsync(Guid animalActionId, Guid treatmentId, string? doseOverride = null)
        {
            var action = await _db.ProgramRunAnimalOld
                .Include(a => a.Treatments)
                .FirstOrDefaultAsync(a => a.Id == animalActionId);

            if (action is null) return;

            var treatment = new atr_Animal_Treatment
            {
                Id = Guid.NewGuid(),
                AnimalActionId = action.Id,
                TreatmentId = treatmentId,
                IsDefault = false,
                DoseOverride = doseOverride,
                AppliedUtc = DateTime.UtcNow
            };

            action.Treatments.Add(treatment);

            await _db.SaveChangesAsync();
            await AppendToCsvAsync(action); // backup update
        }

        // CSV backup writer
        private async Task AppendToCsvAsync(ast_Animal_Session_Treatment action)
        {
            string filePath = Path.Combine(_backupFolder, $"session_{action.SessionId}.csv");
            bool newFile = !File.Exists(filePath);

            var sb = new StringBuilder();

            if (newFile)
            {
                sb.AppendLine("ActionId,SessionId,TagId,AnimalId,AgeGroup,TreatmentIds,TimestampUtc");
            }

            string treatmentIds = string.Join(";", action.Treatments.Select(t => t.TreatmentId));
            sb.AppendLine($"{action.Id},{action.SessionId},{action.TagId},{action.spd_Id},{action.AgeGroup},{treatmentIds},{action.ScannedUtc:u}");

            await File.AppendAllTextAsync(filePath, sb.ToString());
        }
    }
}