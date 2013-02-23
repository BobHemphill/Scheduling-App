using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer;
using Common;
using Assignment;
using Rhino.Mocks;

namespace AssignmentTests {
    [TestFixture]
    public class ShiftAssignerTest {
        [Test]
        public void Assign() {
            var shifts = new List<Shift> { new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.EndOfDay) };
            var residents = new List<Resident> { new Resident() };

            var assignmentResult = new ShiftAssigner().Assign(shifts, residents);
            Assert.IsTrue(assignmentResult.Assigned);
            Assert.AreEqual(residents.First(), shifts.First().Resident);
            Assert.AreEqual(shifts.First(), residents.First().Shifts.First());
            Assert.IsTrue(assignmentResult.AssignedResidents.Contains(residents.First()));
        }

        [Test]
        public void Assign_OverlappingShifts_UnderStaffed() {
            var mockery = new MockRepository();
            var residentSelector = mockery.StrictMock<IResidentSelector>();

            var shifts = new List<Shift> { 
                new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.EndOfDay),
                new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.EndOfDay),
            };
            var residents = new List<Resident> { new Resident() };

            using (mockery.Record()) {
                Expect.Call(residentSelector.Select(Arg<IEnumerable<Resident>>.Is.Anything, Arg<Shift>.Is.Equal(shifts.First()))).Return(residents.First());
                Expect.Call(residentSelector.Select(Arg<IEnumerable<Resident>>.Is.Anything, Arg<Shift>.Is.Equal(shifts.Skip(1).First()))).Return(null);
            }
            using (mockery.Playback()) {
                Assert.IsFalse(new ShiftAssigner(residentSelector).Assign(shifts, residents).Assigned);
            }
        }

        [Test]
        public void Assign_OverlappingShifts() {
            var mockery = new MockRepository();
            var residentSelector = mockery.StrictMock<IResidentSelector>();

            var shifts = new List<Shift> { 
                new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.EndOfDay),
                new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.EndOfDay)
            };
            var residents = new List<Resident> { new Resident(), new Resident() };

            using (mockery.Record()) {
                Expect.Call(residentSelector.Select(Arg<IEnumerable<Resident>>.Is.Anything, Arg<Shift>.Is.Equal(shifts.First()))).Return(residents.First());
                Expect.Call(residentSelector.Select(Arg<IEnumerable<Resident>>.Is.Anything, Arg<Shift>.Is.Equal(shifts.Skip(1).First()))).Return(residents.Skip(1).First());
            }

            using (mockery.Playback()) {
                var assignmentResult = new ShiftAssigner(residentSelector).Assign(shifts, residents);
                Assert.IsTrue(assignmentResult.Assigned);
                foreach (var resident in residents) { Assert.AreEqual(1, resident.Shifts.Count()); }
                foreach (var shift in shifts) { Assert.IsNotNull(shift.Resident); }
            }
        }

        [Test]
        public void Assign_Assigns_Associated_Shifts() {
            var rotationShift1 = new RotationShift(TimeConstants.AllWeek, TimeConstants.StartOfDay12HourDay, TimeConstants.EndOfDay12HourDay);
            var rotationShift2 = new RotationShift(TimeConstants.AllWeek, TimeConstants.StartOfDay12HourDay, TimeConstants.EndOfDay12HourDay);
            var residents = new List<Resident> { new Resident(), new Resident() };

            var shifts = new ShiftFactory().Create(new List<RotationShift> { rotationShift1, rotationShift2 }, new DateTime(2012, 12, 3), new DateTime(2012, 12, 7));

            var assignmentResult = new ShiftAssigner().Assign(shifts, residents);
            Assert.IsTrue(assignmentResult.Assigned);
            foreach (var resident in residents) { Assert.AreEqual(5, resident.Shifts.Count()); }
        }
    }
}
