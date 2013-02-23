using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using DomainLayer;
using Assignment;
using Common;

namespace AssignmentTests {
    [TestFixture]
    public class RotationAssignerTest {
        [Test]
        public void Assign() {
            var mockery = new MockRepository();
            var shiftFactory = mockery.StrictMock<IShiftFactory>();
            var shiftAssigner = mockery.StrictMock<IAssign<Shift>>();

            var block = new Block { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) };
            var rotations = new List<Rotation> { new Rotation { Block = block } };
            rotations.First().RotationShifts.Add(new RotationShift(Common.BitwiseDayOfWeek.None, DateTime.Today, DateTime.Today));

            var residents = new List<Resident> { new Resident() };
            var shifts = new List<Shift> { new Shift(DateTime.Today, DateTime.Today, DateTime.Today) };
            var assignedResidents = new List<Resident>();

            using (mockery.Record()) {
                Expect.Call(shiftFactory.Create(Arg<ActionList<RotationShift>>.Matches(l => rotations.First().RotationShifts.TrueForAll(iL => l.Contains(iL))), Arg<DateTime>.Is.Equal(block.StartDate), Arg<DateTime>.Is.Equal(block.EndDate))).Return(shifts);
                Expect.Call(shiftAssigner.Assign(shifts, residents)).Return(new AssignmentResult(residents));
            }
            using (mockery.Playback()) {
                Assert.IsTrue(new RotationAssigner(shiftFactory, shiftAssigner).Assign(rotations, residents).Assigned);
            }
        }

        [Test]
        public void Assign_MultipleRotations() {
            var mockery = new MockRepository();
            var shiftFactory = mockery.StrictMock<IShiftFactory>();
            var shiftAssigner = mockery.StrictMock<IAssign<Shift>>();

            var block = new Block { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) };
            var rotation1 = new Rotation { Block = block };
            rotation1.RotationShifts.Add(new RotationShift(Common.BitwiseDayOfWeek.None, DateTime.Today, DateTime.Today));
            var rotation2 = new Rotation { Block = block };
            rotation2.RotationShifts.Add(new RotationShift(Common.BitwiseDayOfWeek.None, DateTime.Today, DateTime.Today));
            var rotations = new List<Rotation> { rotation1, rotation2 };


            var residents = new List<Resident> { new Resident(), new Resident() };
            var rotation1Shifts = new List<Shift> { new Shift(DateTime.Today, DateTime.Today, DateTime.Today) };
            var rotation2Shifts = new List<Shift> { new Shift(DateTime.Today, DateTime.Today, DateTime.Today) };

            using (mockery.Record()) {
                Expect.Call(shiftFactory.Create(Arg<ActionList<RotationShift>>.Matches(l => rotation1.RotationShifts.TrueForAll(iL => l.Contains(iL))), Arg<DateTime>.Is.Equal(block.StartDate), Arg<DateTime>.Is.Equal(block.EndDate))).Return(rotation1Shifts);
                Expect.Call(shiftFactory.Create(Arg<ActionList<RotationShift>>.Matches(l => rotation2.RotationShifts.TrueForAll(iL => l.Contains(iL))), Arg<DateTime>.Is.Equal(block.StartDate), Arg<DateTime>.Is.Equal(block.EndDate))).Return(rotation2Shifts);

                Expect.Call(shiftAssigner.Assign(Arg<IEnumerable<Shift>>.Matches(l => rotation1Shifts.TrueForAll(iL => l.Contains(iL))), Arg<IEnumerable<Resident>>.Matches(l => residents.TrueForAll(iL => l.Contains(iL))))).Return(new AssignmentResult(new List<Resident> { residents.First() }));
                Expect.Call(shiftAssigner.Assign(Arg<IEnumerable<Shift>>.Matches(l => rotation2Shifts.TrueForAll(iL => l.Contains(iL))), Arg<IEnumerable<Resident>>.Matches(l => residents.Skip(1).ToList().TrueForAll(iL => l.Contains(iL))))).Return(new AssignmentResult(new List<Resident> { residents.Skip(1).First() }));
            }
            using (mockery.Playback()) {
                var result = new RotationAssigner(shiftFactory, shiftAssigner).Assign(rotations, residents);

                residents.ForEach(r => Assert.IsTrue(result.AssignedResidents.Contains(r)));
            }
        }
    }
}
