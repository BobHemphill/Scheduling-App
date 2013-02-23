using System.Linq;
using DomainLayer;
using Common;

namespace Assignment {
    public interface IResidentConstraint {
        bool Assignable(Resident resident, Shift shift);
    }

    /// <summary>
    /// As Defined by the ACGME
    /// VI.G.4.a) Duty periods of PGY-1 residents must not exceed 16 hours in duration.
    /// VI.G.4.b) Duty periods of PGY-2 residents and above may be scheduled to a maximum of 24 hours of continuous duty in the hospital.
    /// </summary>
    public class ContinuousDutyHourConstraint : IResidentConstraint {
        public bool Assignable(Resident resident, Shift shift) {
            var maxHours = resident.MaxContinuousShiftHours;
            var shiftHours = shift.DateRange.Hours;
            if (shiftHours > maxHours) return false;

            var startConstraint = shift.EndTime.AddHours(-maxHours); // Period of 'maxHours' hours and 1 minute from end of shift
            var endConstraint = shift.StartTime.AddMinutes(-1);

            var assignedHours = resident.Shifts.Where(s => s.DateRange.Equals(new DateRange(startConstraint, endConstraint)))//shifts in 'maxHours' hour period
                                               .Select(s => new DateRange(DateExtensions.Max(s.StartTime, startConstraint), DateExtensions.Min(s.EndTime, endConstraint)))
                                               .Sum(dr => dr.Hours);

            return assignedHours + shiftHours <= maxHours;
        }
    }

    /// <summary>
    /// As Defined by the ACGME
    /// VI.G.5.a) PGY-1 residents should have 10 hours, and must have eight hours, free of duty between scheduled duty periods.
    /// VI.G.5.b) Intermediate-level residents [as defined by the Review Committee]
    ///           should have 10 hours free of duty, and must have eight hours
    ///           between scheduled duty periods. They must have at least 14
    ///           hours free of duty after 24 hours of in-house duty.
    /// </summary>
    public class MinimumTimeOffConstraint : IResidentConstraint {
        public bool Assignable(Resident resident, Shift shift) {
            var lastShift = resident.Shifts.OrderByDescending(s=>s.EndTime).FirstOrDefault();
            return lastShift == null ||
                   (new DateRange(lastShift.EndTime.AddMinutes(1), shift.StartTime.AddMinutes(-1)).Hours //Actual time between last shift and this shift
                   >= (lastShift.DateRange.Hours >= 24 ? 14 : 8)); // ACGME required time off between shifts depending on duration of last shift
        }
    }

    /// <summary>
    /// As Defined by the ACGME
    /// VI.G.6. Residents must not be scheduled for more than six consecutive nights of night float.
    /// </summary>
    public class MaximumContinuousNightFloatConstraint : IResidentConstraint {
        public bool Assignable(Resident resident, Shift shift) {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// As Defined by the ACGME
    /// VI.G.1. Duty hours must be limited to 80 hours per week, averaged over a four-week period, inclusive of all in-house call activities and all moonlighting.
    /// </summary>
    public class Average80HoursOver4WeeksConstraint : IResidentConstraint {
        public bool Assignable(Resident resident, Shift shift) {
            var shiftHours = shift.DateRange.Hours;
            var startConstraint = shift.EndTime.AddDays(-28).AddMinutes(1);
            var endConstraint = shift.StartTime.AddMinutes(-1);

            var assignedHours = resident.Shifts.Where(s => s.DateRange.Equals(new DateRange(startConstraint, endConstraint)))
                                               .Select(s => new DateRange(DateExtensions.Max(s.StartTime, startConstraint), DateExtensions.Min(s.EndTime, endConstraint)))
                                               .Sum(dr => dr.Hours);

            var resultingAverage = ((assignedHours + shiftHours) / 4m);
            return resultingAverage <= 80;
        }
    }

    /// <summary>
    /// As Defined by the ACGME
    /// VI.G.3. Residents must be scheduled for a minimum of one day free of duty every week (when averaged over four weeks). At-home call cannot be assigned on these free days.
    /// </summary>
    public class Average1DayOffAWeekOver4Weeks : IResidentConstraint {
        public bool Assignable(Resident resident, Shift shift) {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// As Defined by the ACGME
    /// VI.G.7. PGY-2 residents and above must be scheduled for in-house call no more frequently than every-third-night (when averaged over a four-week period).
    /// </summary>
    public class AverageCallFrequencyOver4WeeksConstraint : IResidentConstraint {
        public bool Assignable(Resident resident, Shift shift) {
            throw new System.NotImplementedException();
        }
    }
}
