using System;
using System.Collections.Generic;
using DomainLayer;

namespace Assignment {
  public class Schedule {
    public CalendarAssignmentResult AssignmentResult { get; set; }

    public void DisplaySchedule() {
      foreach (var resident in AssignmentResult.Residents) {
        Console.WriteLine(resident);
      }
    }
  }

  public class Scheduler {
    readonly ICalendarAssigner assigner;

    public Scheduler() : this(new CalendarAssigner()) { }
    public Scheduler(ICalendarAssigner assigner) {
      this.assigner = assigner;
    }

    public Schedule Schedule(CalendarYear calendar, IEnumerable<Resident> residents) {
      return new Schedule{AssignmentResult = assigner.Assign(calendar, residents)};
    }
  }
}
