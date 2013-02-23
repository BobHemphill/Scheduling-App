using System;
using Common;

namespace DomainLayer {
    public interface IMultipleDayDecorator {
        DateTime AdjustShiftEndDay(DateTime day);
        BitwiseDayOfWeek AdjustApplicableDays(BitwiseDayOfWeek day);
        DateTime AdjustDayStartTime(DateTime startTime, BitwiseDayOfWeek today, BitwiseDayOfWeek shiftDay);
        DateTime AdjustDayEndTime(DateTime endTime, BitwiseDayOfWeek today, BitwiseDayOfWeek shiftDay);
    }

    public class OvernightMultipleDayDecorator : IMultipleDayDecorator {
        public DateTime AdjustShiftEndDay(DateTime day) {
            return day.AddDays(1);
        }

        public BitwiseDayOfWeek AdjustApplicableDays(BitwiseDayOfWeek day) {
            var result = BitwiseDayOfWeek.None;
            if ((day & BitwiseDayOfWeek.Sunday) == BitwiseDayOfWeek.Sunday) result |= BitwiseDayOfWeek.Sunday | BitwiseDayOfWeek.Monday;
            if ((day & BitwiseDayOfWeek.Monday) == BitwiseDayOfWeek.Monday) result |= BitwiseDayOfWeek.Monday | BitwiseDayOfWeek.Tuesday;
            if ((day & BitwiseDayOfWeek.Tuesday) == BitwiseDayOfWeek.Tuesday) result |= BitwiseDayOfWeek.Tuesday | BitwiseDayOfWeek.Wednesday;
            if ((day & BitwiseDayOfWeek.Wednesday) == BitwiseDayOfWeek.Wednesday) result |= BitwiseDayOfWeek.Wednesday | BitwiseDayOfWeek.Thursday;
            if ((day & BitwiseDayOfWeek.Thursday) == BitwiseDayOfWeek.Thursday) result |= BitwiseDayOfWeek.Thursday | BitwiseDayOfWeek.Friday;
            if ((day & BitwiseDayOfWeek.Friday) == BitwiseDayOfWeek.Friday) result |= BitwiseDayOfWeek.Friday | BitwiseDayOfWeek.Saturday;
            if ((day & BitwiseDayOfWeek.Saturday) == BitwiseDayOfWeek.Saturday) result |= BitwiseDayOfWeek.Saturday | BitwiseDayOfWeek.Sunday;

            return result;
        }

        public DateTime AdjustDayStartTime(DateTime startTime, BitwiseDayOfWeek today, BitwiseDayOfWeek shiftDay) {
            return today == (shiftDay & today) ? startTime : TimeConstants.AdjustStartOfDay(startTime); //Shifts that do not start today start at StartOfDay
        }


        public DateTime AdjustDayEndTime(DateTime endTime, BitwiseDayOfWeek today, BitwiseDayOfWeek shiftDay) {
            return today == (shiftDay & today) ? TimeConstants.AdjustEndOfDay(endTime) : endTime; //Shifts that start today end at EndOfDay
        }
    }
}
