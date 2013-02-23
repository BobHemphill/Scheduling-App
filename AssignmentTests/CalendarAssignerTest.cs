using System.Collections.Generic;
using System.Linq;
using Assignment;
using DomainLayer;
using NUnit.Framework;
using Rhino.Mocks;

namespace AssignmentTests {
  [TestFixture]
  public class CalendarAssignerTest {
    [Test]
    public void Assign() {
      var assigner = MockRepository.GenerateMock<IBlockAssigner>();
      var calendar = new CalendarYear(2013) { Id = 1 };
      
      var blocks = new List<Block> {new Block(), new Block()};
      blocks.ForEach(calendar.Blocks.Add);

      var residents = new List<Resident> { new Resident() };
      var residents2 = new List<Resident> { new Resident(), new Resident() };
      var assignmentResult = new BlockAssignmentResult(residents2);
      assigner.Stub(a => a.Assign(blocks, residents)).Return(assignmentResult);

      var result = new CalendarAssigner(assigner).Assign(calendar, residents);

      Assert.AreEqual(residents2, result.Residents);
    }
  }
}
