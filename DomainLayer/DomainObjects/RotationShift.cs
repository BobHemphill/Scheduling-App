using System;
using Common;

namespace DomainLayer {
  public class RotationShift {
    public Rotation Rotation { get; set; }
    public DateTime RotationStartDate { get { return Rotation.BlockStartDate; } }
    public DateTime RotationEndDate { get { return Rotation.BlockEndDate; } }

    readonly BitwiseDayOfWeek day;
    readonly DateTime startTime;
    readonly DateTime endTime;
    readonly IAssigningRequiredDecorator assigningRequiredDecorator;
    readonly IMultipleDayDecorator multipleDayDecorator;

    public BitwiseDayOfWeek Day { get { return day; } }
    public DateTime StartTime { get { return startTime; } }
    public DateTime EndTime { get { return endTime; } }
    public bool IsAssingingRequired { get { return assigningRequiredDecorator != null ? assigningRequiredDecorator.IsAssingingRequired() : true; } }
    public IMultipleDayDecorator MultipleDayDecorator { get { return multipleDayDecorator; } }

    readonly ActionList<Shift> shifts;
    public ActionList<Shift> Shifts { get { return shifts; } }

    public BitwiseDayOfWeek AdjustedDay {
      get {
        return multipleDayDecorator != null
        ? multipleDayDecorator.AdjustApplicableDays(day)
        : day;
      }
    }

    public DateTime AdjustedStartTime(BitwiseDayOfWeek dayOfStart) {
      return MultipleDayDecorator != null
          ? MultipleDayDecorator.AdjustDayStartTime(StartTime, dayOfStart, Day)
          : StartTime;
    }

    public DateTime AdjustedEndTime(BitwiseDayOfWeek dayOfStart) {
      return MultipleDayDecorator != null
          ? MultipleDayDecorator.AdjustDayEndTime(EndTime, dayOfStart, Day)
          : EndTime;
    }

    public RotationShift(BitwiseDayOfWeek day, DateTime startTime, DateTime endTime, IAssigningRequiredDecorator assigningRequiredDecorator = null, IMultipleDayDecorator multipleDayDecorator = null) {
      this.day = day;
      this.startTime = new DateTime(1, 1, 1, startTime.Hour, startTime.Minute, 0, 0);
      this.endTime = new DateTime(1, 1, 1, endTime.Hour, endTime.Minute, 0, 0);
      this.assigningRequiredDecorator = assigningRequiredDecorator;
      this.multipleDayDecorator = multipleDayDecorator;

      shifts = new ActionList<Shift>();
      shifts.OnAdd += new EventHandler<ActionArgs<Shift>>((list, args) => { SetLookUpValues(args.Item); });
    }

    void SetLookUpValues(Shift child) {
      child.RotationShift = this;
    }

    public override string ToString() {
      return string.Format("{0}", Rotation);
    }
  }

  //public class WeekdayRotationShift : RotationShift {
  //    public WeekdayRotationShift(DateTime startTime, DateTime endTime) : base(TimeConstants.Weekday, startTime, endTime) { }
  //}

  //public class NoOpRotationShift : RotationShift {
  //    public NoOpRotationShift(BitwiseDayOfWeek day, DateTime startTime, DateTime endTime) : base(day, startTime, endTime, new NoOpAssigningRequiredDecarator()) { }
  //}

  //public class NightShift {

  //}
}
