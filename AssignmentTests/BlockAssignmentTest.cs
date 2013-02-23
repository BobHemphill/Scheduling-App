using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment;
using DomainLayer;
using NUnit.Framework;
using Rhino.Mocks;

namespace AssignmentTests {
  [TestFixture]
  public class BlockAssignmentTest {
    [Test]
    public void Assign() {
      var assigner = MockRepository.GenerateMock<IAssign<Rotation>>();

      var block = new Block();
      var rotations = new List<Rotation>() { new Rotation() };
      rotations.ForEach(block.Rotations.Add);

      var block2 = new Block();
      var rotations2 = new List<Rotation>() { new Rotation(), new Rotation() };
      rotations2.ForEach(block2.Rotations.Add);

      var residents = new List<Resident> { new Resident() };
      var residents2 = new List<Resident> { new Resident(), new Resident() };
      var assignmentResult = new AssignmentResult(residents2);
      assigner.Stub(a => a.Assign(Arg<IEnumerable<Rotation>>.Matches(l=> rotations.All(l.Contains)), Arg.Is(residents))).Return(assignmentResult);
      
      var residents3 = new List<Resident> { new Resident(), new Resident(), new Resident() };
      var assignmentResult2 = new AssignmentResult(residents3);
      assigner.Stub(a => a.Assign(Arg<IEnumerable<Rotation>>.Matches(l => rotations2.All(l.Contains)), Arg.Is(residents2))).Return(assignmentResult2);

      var result = new BlockAssigner(assigner).Assign(new List<Block> { block, block2 }, residents);

      Assert.AreEqual(residents3, result.Residents);
    }
  }
}
