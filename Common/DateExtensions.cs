using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    public class DateExtensions {
        public static DateTime Max(DateTime first, DateTime second) {
            if (Comparer<DateTime>.Default.Compare(first, second) > 0)
                return first;
            return second;
        }

        public static DateTime Min(DateTime first, DateTime second) {
            if (Comparer<DateTime>.Default.Compare(first, second) < 0)
                return first;
            return second;
        }
    }
}
