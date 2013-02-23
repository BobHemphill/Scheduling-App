using System;
using Assignment;
using Common;
using DomainLayer;
using NUnit.Framework;

namespace AssignmentTests {
    [TestFixture]
    public class MinimumTimeOffConstraintTest {
        [Test]
        public void Assignable_No_Prior_Shifts() {
            Assert.IsTrue(new MinimumTimeOffConstraint().Assignable(new Resident(), new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.StartOfDay)));
        }

        [Test]
        public void Assignable_Select_Correct_Last_Shift() {
            //8HRShift
            var assignedShift = new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddHours(8).AddMinutes(-2));
            var lastAssignedShift = new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddHours(8).AddMinutes(-1));
            var resident = new Resident();
            resident.Shifts.Add(assignedShift);
            resident.Shifts.Add(lastAssignedShift);

            var unAssingableShift = new Shift(DateTime.Today, TimeConstants.StartOfDay.AddHours(16).AddMinutes(-1), TimeConstants.StartOfDay.AddHours(16).AddMinutes(-1)); // 7 Hour 59 Minute break
            Assert.IsFalse(new MinimumTimeOffConstraint().Assignable(resident, unAssingableShift));
        }

        [Test]
        public void Assignable_8Hours_Between_NonCallShifts() {
            //8HRShift
            var assignedShift = new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddHours(8).AddMinutes(-1));
            var resident = new Resident();
            resident.Shifts.Add(assignedShift);

            var unAssingableShift = new Shift(DateTime.Today, TimeConstants.StartOfDay.AddHours(16).AddMinutes(-1), TimeConstants.StartOfDay.AddHours(16).AddMinutes(-1)); // 7 Hour 59 Minute break
            Assert.IsFalse(new MinimumTimeOffConstraint().Assignable(resident, unAssingableShift));

            var assingableShift = new Shift(DateTime.Today, TimeConstants.StartOfDay.AddHours(16), TimeConstants.StartOfDay.AddHours(16)); //8 Hour break
            Assert.IsTrue(new MinimumTimeOffConstraint().Assignable(resident, assingableShift));
        }

        [Test]
        public void Assignable_14Hours_Between_CallShifts() {
            //24HRShift
            var assignedShift = new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.EndOfDay);
            var resident = new Resident();
            resident.Shifts.Add(assignedShift);

            var unAssingableShift = new Shift(DateTime.Today.AddDays(1), TimeConstants.StartOfDay.AddHours(14).AddMinutes(-1), TimeConstants.StartOfDay.AddHours(14).AddMinutes(-1)); // 13 Hour 59 Minute break
            Assert.IsFalse(new MinimumTimeOffConstraint().Assignable(resident, unAssingableShift));

            var assingableShift = new Shift(DateTime.Today.AddDays(1), TimeConstants.StartOfDay.AddHours(14), TimeConstants.StartOfDay.AddHours(14)); //14 Hour break
            Assert.IsTrue(new MinimumTimeOffConstraint().Assignable(resident, assingableShift));
        }
    }
}
