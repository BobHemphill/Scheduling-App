using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
    public static class DomainFactory {
        public static CalendarYear CreateCalendarYear() {
            var today = DateTime.Today;
            var year = new CalendarYear(today.Year);
            year.Blocks.Add(CreateBlock(today, today.AddYears(1).AddDays(-1)));
            AssignBaseObjectProperties(year);
            return year;
        }

        public static Block CreateBlock(DateTime startDate, DateTime endDate) {
            var block = new Block { StartDate = startDate, EndDate = endDate };
            AssignBaseObjectProperties(block);
            return block;
        }

        public static void AssignBaseObjectProperties(BaseObject obj){
            obj.Id = 1;
            obj.Name = Guid.NewGuid().ToString();
        }
    }
}
