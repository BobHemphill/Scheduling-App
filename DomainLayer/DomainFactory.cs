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
            year.Blocks.Add(CreateBlock(today, today.AddMonths(1).AddDays(-1)));
            for (int i = 1; i < 12; i++) {
                year.Blocks.Add(CreateBlock(today.AddMonths(i), today.AddMonths(i + 1).AddDays(-1)));
            }
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

        static int id = 0;
        public static void AssignBaseObjectProperties(BaseObject obj) {
            obj.Id = id++;
            obj.Name = Guid.NewGuid().ToString();
        }
    }
}
