using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using NUnit.Framework;
using DomainLayer;
using Assignment;
using Rhino.Mocks;

namespace AssignmentTests {
    [TestFixture]
    public class ResidentSelectorTest {
        [Test]
        public void Select() {
            var residents = new List<Resident> { new Resident(), new Resident() };

            Assert.AreEqual(residents.First(), new ResidentSelector().Select(residents, new Shift(DateTime.Today, DateTime.Today, DateTime.Today)));
        }

        [Test]
        public void Constraints() {
            Assert.IsTrue(new ResidentSelector().Constraints.Any(c => c is MinimumTimeOffConstraint));
        }

        [Test]
        public void Select_UsesConstraints() {
            var mockery = new MockRepository();
            var constraint1 = mockery.StrictMock<IResidentConstraint>();
            var constraint2 = mockery.StrictMock<IResidentConstraint>();
            var selector = new ResidentSelector();
            selector.Constraints.Clear();
            selector.Constraints.Add(constraint1);
            selector.Constraints.Add(constraint2);

            var residents = new List<Resident> { new Resident() };
            var shift = new Shift(DateTime.Today, DateTime.Today, DateTime.Today);
            using (mockery.Record()) {
                Expect.Call(constraint1.Assignable(residents.First(), shift)).Return(true);
                Expect.Call(constraint2.Assignable(residents.First(), shift)).Return(true);
            }
            using (mockery.Playback()) {
                Assert.AreEqual(residents.First(), selector.Select(residents, shift));
            }
        }

        [Test]
        public void Select_Finds_FirstToSatisfyAllConstraints() {
            var mockery = new MockRepository();
            var constraint1 = mockery.StrictMock<IResidentConstraint>();
            var constraint2 = mockery.StrictMock<IResidentConstraint>();
            var selector = new ResidentSelector();
            selector.Constraints.Clear();
            selector.Constraints.Add(constraint1);
            selector.Constraints.Add(constraint2);

            var residents = new List<Resident> { new Resident(), new Resident(), new Resident() };
            var shift = new Shift(DateTime.Today, DateTime.Today, DateTime.Today);
            using (mockery.Record()) {
                SetupResult.For(constraint1.Assignable(residents.First(), shift)).Return(false);
                SetupResult.For(constraint2.Assignable(residents.First(), shift)).Return(true);

                SetupResult.For(constraint1.Assignable(residents.Skip(1).First(), shift)).Return(true);
                SetupResult.For(constraint2.Assignable(residents.Skip(1).First(), shift)).Return(false);

                SetupResult.For(constraint1.Assignable(residents.Skip(2).First(), shift)).Return(true);
                SetupResult.For(constraint2.Assignable(residents.Skip(2).First(), shift)).Return(true);
            }
            using (mockery.Playback()) {
                Assert.AreEqual(residents.Skip(2).First(), selector.Select(residents, shift));
            }
        }

        [Test]
        public void Select_Finds_Resident_Associated_With_RotationShift_First() {
            var rotationShift = new RotationShift(BitwiseDayOfWeek.Monday | BitwiseDayOfWeek.Tuesday, TimeConstants.StartOfDay12HourDay, TimeConstants.EndOfDay12HourDay);
            var shifts = new ShiftFactory().Create(new List<RotationShift> { rotationShift }, new DateTime(2012, 12, 3), new DateTime(2012, 12, 4)).ToList();
            var residents = new List<Resident> { new Resident(), new Resident() };

            shifts.First().Resident = residents.Skip(1).First();

            var selector = new ResidentSelector();
            selector.Constraints.Clear();

            Assert.AreEqual(residents.Skip(1).First(), selector.Select(residents, shifts.Skip(1).First()));
        }

        [Test]
        public void Select_Finds_Resident_With_Least_Number_Of_Shifts() {
            var residents = new List<Resident> { new Resident(), new Resident() };
            var shift = new Shift(DateTime.Today, DateTime.Today, DateTime.Today);
            residents.First().Shifts.Add(shift);

            var selector = new ResidentSelector();
            selector.Constraints.Clear();

            Assert.AreEqual(residents.Skip(1).First(), selector.Select(residents, shift));
        }
    }
}
