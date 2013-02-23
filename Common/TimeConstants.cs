using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    public static class TimeConstants {

        public static BitwiseDayOfWeek Weekday {
            get {
                return BitwiseDayOfWeek.Monday |
                       BitwiseDayOfWeek.Tuesday |
                       BitwiseDayOfWeek.Wednesday |
                       BitwiseDayOfWeek.Thursday |
                       BitwiseDayOfWeek.Friday;
            }
        }

        public static BitwiseDayOfWeek Weekend {
            get {
                return BitwiseDayOfWeek.Saturday |
                       BitwiseDayOfWeek.Sunday;
            }
        }

        public static BitwiseDayOfWeek AllWeek {
            get {
                return BitwiseDayOfWeek.Monday |
                       BitwiseDayOfWeek.Tuesday |
                       BitwiseDayOfWeek.Wednesday |
                       BitwiseDayOfWeek.Thursday |
                       BitwiseDayOfWeek.Friday |
                       BitwiseDayOfWeek.Saturday |
                       BitwiseDayOfWeek.Sunday;
            }
        }

        public static DateTime StartOfDay {
            get {
                return new DateTime(1900, 1, 1, 0, 0, 0, 0);
            }
        }
        public static DateTime StartOfDay12HourDay {
            get {
                return new DateTime(1900, 1, 1, 7, 0, 0, 0);
            }
        }

        public static DateTime EndOfDay {
            get {
                return new DateTime(1900, 1, 1, 23, 59, 0, 0);
            }
        }
        public static DateTime EndOfDay12HourDay {
            get {
                return new DateTime(1900, 1, 1, 13, 59, 0, 0);
            }
        }

        public static DateTime AdjustStartOfDay(DateTime day) {
            return new DateTime(day.Year, day.Month, day.Day, StartOfDay.Hour, StartOfDay.Minute, 0, 0);
        }

        public static DateTime AdjustEndOfDay(DateTime day) {
            return new DateTime(day.Year, day.Month, day.Day, EndOfDay.Hour, EndOfDay.Minute, 0, 0);
        }

        const long NSPerSecond = 10000000;
        public static long MINUTE_TICKS { get { return 60 * NSPerSecond; } }
        public static long HOUR_TICKS { get { return 60 * 60 * NSPerSecond; } }
    }
}
