using System.Collections.Generic;
using DomainLayer;

namespace Assignment {
  public interface ICalendarAssigner {
    CalendarAssignmentResult Assign(CalendarYear calendar, IEnumerable<Resident> residents);
  }

  public class CalendarAssignmentResult {
    public CalendarAssignmentResult() : this(new List<Resident>()) { }
    public CalendarAssignmentResult(IEnumerable<Resident> residents) {
      Residents = residents;
    }
    public IEnumerable<Resident> Residents { get; private set; }
  }

  public class CalendarAssigner : ICalendarAssigner {
    readonly IBlockAssigner assigner;

    public CalendarAssigner() : this(new BlockAssigner()) { }
    public CalendarAssigner(IBlockAssigner assigner) {
      this.assigner = assigner;
    }

    public CalendarAssignmentResult Assign(CalendarYear calendar, IEnumerable<Resident> residents) {
      return new CalendarAssignmentResult(assigner.Assign(calendar.Blocks, residents).Residents);
    }
  }
}
