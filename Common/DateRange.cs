using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    public class DateRange {
        public DateTime Start;
        public DateTime End;
        public decimal Minutes { get { return ((decimal)(End.Ticks - Start.Ticks) + (decimal)TimeConstants.MINUTE_TICKS) / TimeConstants.MINUTE_TICKS; } }
        public decimal Hours { get { return ((decimal)(End.Ticks - Start.Ticks) + (decimal)TimeConstants.MINUTE_TICKS) / TimeConstants.HOUR_TICKS; } }

        public DateRange(DateTime start, DateTime end) {
            if (end < start) throw new ArgumentNullException();
            this.Start = start;
            this.End = end;
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            if (!(obj is DateRange)) return false;

            var that = obj as DateRange;
            return !((that.End < this.Start) || (that.Start > this.End));
        }
    }

    public class DateRangeComparer : IEqualityComparer<DateRange> {
        public bool Equals(DateRange x, DateRange y) {
            return x.Equals(y);
        }

        public int GetHashCode(DateRange obj) {
            return 1;
        }
    }
}
