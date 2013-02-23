using System;
using System.Collections.Generic;
using Common;

namespace DomainLayer {
  public class Shift {
    readonly DateTime startTime;
    readonly DateTime endTime;

    public DateTime StartTime { get { return startTime; } }
    public DateTime EndTime { get { return endTime; } }

    public RotationShift RotationShift { get; set; }
    public Resident Resident { get; set; }
    public List<Shift> AssociatedShifts { get { return RotationShift == null ? new List<Shift>() : RotationShift.Shifts; } }

    public DateRange DateRange { get { return new DateRange(StartTime, EndTime); } }

    public Shift(DateTime date, DateTime startTime, DateTime endTime, IMultipleDayDecorator multipleDayDecorator = null) {
      this.startTime = ConstructDateFromParts(date, startTime);
      this.endTime = ApplyMultipleDayDecorator(multipleDayDecorator, ConstructDateFromParts(date, endTime));
    }

    DateTime ConstructDateFromParts(DateTime day, DateTime Time) {
      return new DateTime(day.Year, day.Month, day.Day, Time.Hour, Time.Minute, 0);
    }

    DateTime ApplyMultipleDayDecorator(IMultipleDayDecorator multipleDayDecorator, DateTime day) {
      return multipleDayDecorator != null ? multipleDayDecorator.AdjustShiftEndDay(day) : day;
    }

    public override string ToString() {
      return string.Format("{0} {1} - {2}", RotationShift, StartTime.ToString("f"), EndTime.ToString("t"));
    }
  }
}
