using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DomainLayer {
    public static class DomainFactory {
        public static CalendarYear CreateCalendarYear() {
            var today = DateTime.Today;
            var year = new CalendarYear(today.Year);
            year.Blocks.Add(CreateBlock(today, today.AddMonths(3).AddDays(-1)));
            year.Blocks.Add(CreateBlock(today.AddMonths(3), today.AddMonths(6).AddDays(-1)));
            year.Blocks.Add(CreateBlock(today.AddMonths(6), today.AddMonths(9).AddDays(-1)));
            year.Blocks.Add(CreateBlock(today.AddMonths(9), today.AddMonths(12).AddDays(-1)));
            AssignBaseObjectProperties(year);
            return year;
        }

        public static Block CreateBlock(DateTime startDate, DateTime endDate) {
            var block = new Block { StartDate = startDate, EndDate = endDate };
            foreach (var rotationType in Enum.GetValues(typeof(RotationTypes))) {
                block.Rotations.Add(CreateRotation((RotationTypes)rotationType));
            }
            AssignBaseObjectProperties(block);
            return block;
        }

        public static Rotation CreateRotation(RotationTypes rotationType) {
            var rotation = new Rotation { RotationType = rotationType };
            AssignBaseObjectProperties(rotation);
            return rotation;
        }


        public static void AssignBaseObjectProperties(BaseObject obj){
            obj.Id = 1;
            obj.Name = Guid.NewGuid().ToString();
        }
    }
}
