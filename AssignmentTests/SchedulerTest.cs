using System.Collections.Generic;
using Assignment;
using DomainLayer;
using NUnit.Framework;
using Rhino.Mocks;

namespace AssignmentTests {
  [TestFixture]
  public class SchedulerTest {
    [Test]
    public void Schedule() {
      var assigner = MockRepository.GenerateMock<ICalendarAssigner>();

      var assignmentResult = new CalendarAssignmentResult();
      var calendar = new CalendarYear(2013){Id = 1};
      var residents = new List<Resident>();

      assigner.Stub(a => a.Assign(calendar, residents)).Return(assignmentResult);

      var result = new Scheduler(assigner).Schedule(calendar, residents);

      Assert.AreEqual(assignmentResult, result.AssignmentResult);
    }
  }
}
