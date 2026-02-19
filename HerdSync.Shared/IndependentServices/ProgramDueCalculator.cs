using HerdSync.Shared.Enums.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerdSync.Shared.IndependentServices
{
    public static class ProgramDueCalculator
    {
        public static DateTime? NextDue(DateTime? lastRunUtc, RepeatType repeat)
        {
            if (repeat == RepeatType.OnceOff) return null;
            var last = lastRunUtc ?? DateTime.MinValue;
            return repeat switch
            {
                RepeatType.Monthly => last.AddMonths(1),
                RepeatType.Quarterly => last.AddMonths(3),
                RepeatType.BiAnnual => last.AddMonths(6),
                RepeatType.Annual => last.AddYears(1),
                _ => null
            };
        }

        public static bool IsDueInMonth(DateTime nowLocal, DateTime? nextDueUtc, TimeZoneInfo tz)
        {
            if (nextDueUtc is null) return false;
            var localNext = TimeZoneInfo.ConvertTimeFromUtc(nextDueUtc.Value, tz);
            return localNext.Year == nowLocal.Year && localNext.Month == nowLocal.Month;
        }
    }

}
