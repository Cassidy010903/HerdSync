using DAL.Configuration.Database;
using DAL.Models.Base.History;

namespace DAL.Services.Implementation
{
    public class PregnanciesRepository(HerdsyncDBContext context) : IPregnanciesRepository
    {
        public async Task ArchiveAndDeletePregnancyAsync(Guid id, string deletedBy)
        {
            var entity = await context.Pregnanciess.FindAsync(id);
            if (entity == null) return;

            var history = new PregnanciesDetailHistory
            {
                HistoryId = Guid.NewGuid(),
                OriginalId = entity.Id,
                prg_Pregnancy_Spot_Date = entity.prg_Pregnancy_Spot_Date,
                prg_Pregnancy_End_Date = entity.prg_Pregnancy_End_Date,
                prg_Notes = entity.prg_Notes,
                spd_Id = entity.spd_Id,
                DeletedDateTime = DateTime.UtcNow,
                DeletedUser = deletedBy
            };

            context.PregnanciesHistoryOld.Add(history);
            context.Pregnanciess.Remove(entity);

            await context.SaveChangesAsync();
        }
    }
}