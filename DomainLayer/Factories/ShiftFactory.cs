using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace DomainLayer {
    public interface IShiftFactory {
        IEnumerable<Shift> Create(IEnumerable<RotationShift> rotationShifts, DateTime startDate, DateTime endDate);
    }

    public class ShiftFactory : IShiftFactory {
        readonly IShiftDayFactory shiftDayFactory;

        public IEnumerable<Shift> Create(IEnumerable<RotationShift> rotationShifts, DateTime startDate, DateTime endDate) {
            var shifts = new List<Shift>();
            var loopDate = startDate;
            do {
                var dayOfWeek = BitwiseDayOfWeekHelper.Convert(loopDate.DayOfWeek);
                shifts.AddRange(shiftDayFactory.CreateShiftsForDay(loopDate, rotationShifts.Where(rs=> (rs.Day&dayOfWeek)==dayOfWeek)));
                loopDate = loopDate.AddDays(1);
            } while (loopDate <= endDate);
            return shifts;
        }

        public ShiftFactory() : this(new ShiftDayFactory()) { }
        public ShiftFactory(IShiftDayFactory shiftDayFactory) {
            this.shiftDayFactory = shiftDayFactory;
        }
    }
}
