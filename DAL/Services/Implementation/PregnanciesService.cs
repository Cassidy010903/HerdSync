using DAL.Configuration.Database;
using DAL.Models.Base.History;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services.Implementation
{
    public class PregnanciesService(HerdsyncDBContext context) : IPregnanciesService
    {
        public async Task ArchiveAndDeletePregnancyAsync(Guid id, string deletedBy)
        {
            var entity = await context.Pregnancies.FindAsync(id);
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

            context.PregnanciesHistory.Add(history);
            context.Pregnancies.Remove(entity);

            await context.SaveChangesAsync();
        }
    }
}
