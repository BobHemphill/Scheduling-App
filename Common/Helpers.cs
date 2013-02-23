using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    public static class BitwiseDayOfWeekHelper {
        public static BitwiseDayOfWeek Convert(DayOfWeek day) {
            switch (day) {
                case DayOfWeek.Sunday:
                    return BitwiseDayOfWeek.Sunday;
                case DayOfWeek.Monday:
                    return BitwiseDayOfWeek.Monday;
                case DayOfWeek.Tuesday:
                    return BitwiseDayOfWeek.Tuesday;
                case DayOfWeek.Wednesday:
                    return BitwiseDayOfWeek.Wednesday;
                case DayOfWeek.Thursday:
                    return BitwiseDayOfWeek.Thursday;
                case DayOfWeek.Friday:
                    return BitwiseDayOfWeek.Friday;
                case DayOfWeek.Saturday:
                    return BitwiseDayOfWeek.Saturday;
                default:
                    throw new ArgumentException(String.Format("BitwiseDayOfWeekHelper:Convert - Argument supplied not a valid DayOfWeek: {0}", day));
            }
        }
    }
}
