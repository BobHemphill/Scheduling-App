using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DomainLayer.Validation {
    public class RotationShiftCollectionValidator : ICollectionValidator<RotationShift> {
        public bool Validate(IEnumerable<RotationShift> rotationShifts) {
            foreach (BitwiseDayOfWeek testDay in System.Enum.GetValues(typeof(BitwiseDayOfWeek))) {
                if (testDay == BitwiseDayOfWeek.None) continue;
                if (!ValidateDay(testDay, rotationShifts)) return false;
            }
            return true;
        }

        public bool ValidateDay(BitwiseDayOfWeek testDay, IEnumerable<RotationShift> rotationShifts) {
            var filteredRotationShifts = rotationShifts.Where(rs => (rs.AdjustedDay & testDay) == testDay);
            if (filteredRotationShifts.Count() < 1) return false;
            
            var orderedRotationShifts = filteredRotationShifts.OrderBy(rs => rs.AdjustedStartTime(testDay)).ThenBy(rs => rs.AdjustedEndTime(testDay));
            if (!ValidateOrderedStartAndEnd(testDay, orderedRotationShifts)) return false;
            return ValidateOrderedInclusion(testDay, orderedRotationShifts);
        }


        bool ValidateOrderedStartAndEnd(BitwiseDayOfWeek testDay, IEnumerable<RotationShift> orderedRotationShifts) {
            var startTime = orderedRotationShifts.First().AdjustedStartTime(testDay);
            var endTime = orderedRotationShifts.Last().AdjustedEndTime(testDay);
            return (startTime.Hour == 0 && startTime.Minute == 0 && endTime.Hour == 23 && endTime.Minute == 59);
        }

        bool ValidateOrderedInclusion(BitwiseDayOfWeek testDay, IEnumerable<RotationShift> orderedRotationShifts) {
            var nextHour = 0;
            var nextMinute = 0;

            foreach (var rotationShift in orderedRotationShifts) {
                var startTime = rotationShift.AdjustedStartTime(testDay);
                if (!(startTime.Hour == nextHour && startTime.Minute == nextMinute)) return false;
                var nextTime = rotationShift.AdjustedEndTime(testDay).AddMinutes(1);
                nextHour = nextTime.Hour;
                nextMinute = nextTime.Minute;
            }
            return true;
        }
    }
}
