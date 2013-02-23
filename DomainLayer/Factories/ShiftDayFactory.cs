using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
    public interface IShiftDayFactory {
        IEnumerable<Shift> CreateShiftsForDay(DateTime day, IEnumerable<RotationShift> rotationShifts);
    }

    public class ShiftDayFactory : IShiftDayFactory {
        public IEnumerable<Shift> CreateShiftsForDay(DateTime day, IEnumerable<RotationShift> rotationShifts) {
            return rotationShifts.Where(rs => rs.IsAssingingRequired).Select(rs => {
                                                                                        var shift = new Shift(day, rs.StartTime, rs.EndTime, rs.MultipleDayDecorator);
                                                                                        rs.Shifts.Add(shift);
                                                                                        return shift;
                                                                                    });
        }
    }
}
