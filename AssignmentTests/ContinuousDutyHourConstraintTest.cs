using System;
using NUnit.Framework;
using DomainLayer;
using Common;
using Assignment;

namespace AssignmentTests {
    [TestFixture]
    public class ContinuousDutyHourConstraintTest {
        [TestCase(ResidentYears.PGY_2)]
        [TestCase(ResidentYears.PGY_3)]
        [TestCase(ResidentYears.Chief)]
        public void Assignable_AlreadyAssigned24_NotPGY1(ResidentYears residentYears) {
            //24HRShift
            var assignedShift = new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.EndOfDay);
            var resident = new Resident { ResidentYear = residentYears };
            resident.Shifts.Add(assignedShift);
            Assert.AreEqual(24, assignedShift.DateRange.Hours);

            var unAssingableShift = new Shift(DateTime.Today.AddDays(1), TimeConstants.StartOfDay, TimeConstants.StartOfDay); //1 minute shift
            Assert.IsFalse(new ContinuousDutyHourConstraint().Assignable(resident, unAssingableShift));

            var assingableShift = new Shift(DateTime.Today.AddDays(1), TimeConstants.StartOfDay.AddMinutes(1), TimeConstants.StartOfDay.AddMinutes(1)); //1 minute shift non conitinous
            Assert.IsTrue(new ContinuousDutyHourConstraint().Assignable(resident, assingableShift));
        }

        [TestCase(ResidentYears.PGY_2)]
        [TestCase(ResidentYears.PGY_3)]
        [TestCase(ResidentYears.Chief)]
        public void Assignable_ShiftWouldResultInMoreThan24_NotPGY1(ResidentYears residentYears) {
            //23.5HRShift
            var assignedShift = new Shift(DateTime.Today, TimeConstants.StartOfDay.AddMinutes(30), TimeConstants.EndOfDay);
            var resident = new Resident { ResidentYear = residentYears };
            resident.Shifts.Add(assignedShift);

            //31 minute shift
            var unAssingableShift = new Shift(DateTime.Today.AddDays(1), TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddMinutes(30));
            Assert.IsFalse(new ContinuousDutyHourConstraint().Assignable(resident, unAssingableShift));

            //30 minute shift
            var assingableShift = new Shift(DateTime.Today.AddDays(1), TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddMinutes(29));
            Assert.IsTrue(new ContinuousDutyHourConstraint().Assignable(resident, assingableShift));
        }

        [Test]
        public void Assignable_AlreadyAssigned16_PGY1() {
            //16HRShift
            var assignedShift = new Shift(DateTime.Today, TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddHours(16).AddMinutes(-1));
            var resident = new Resident { ResidentYear = ResidentYears.PGY_1 };
            resident.Shifts.Add(assignedShift);
            Assert.AreEqual(16, assignedShift.DateRange.Hours);

            var unAssingableShift = new Shift(DateTime.Today, TimeConstants.StartOfDay.AddHours(16), TimeConstants.StartOfDay.AddHours(16)); //1 minute shift
            Assert.IsFalse(new ContinuousDutyHourConstraint().Assignable(resident, unAssingableShift));

            var assingableShift = new Shift(DateTime.Today, TimeConstants.StartOfDay.AddHours(16).AddMinutes(1), TimeConstants.StartOfDay.AddHours(16).AddMinutes(1)); //1 minute shift non conitinous
            Assert.IsTrue(new ContinuousDutyHourConstraint().Assignable(resident, assingableShift));
        }

        [Test]
        public void Assignable_ShiftWouldResultInMoreThan16_PGY1() {
            //15.5HRShift
            var assignedShift = new Shift(DateTime.Today, TimeConstants.StartOfDay.AddMinutes(30), TimeConstants.StartOfDay.AddHours(16).AddMinutes(-1));
            var resident = new Resident { ResidentYear = ResidentYears.PGY_1 };
            resident.Shifts.Add(assignedShift);

            //31 minute shift
            var unAssingableShift = new Shift(DateTime.Today, TimeConstants.StartOfDay.AddHours(16), TimeConstants.StartOfDay.AddHours(16).AddMinutes(30));
            Assert.IsFalse(new ContinuousDutyHourConstraint().Assignable(resident, unAssingableShift));

            //30 minute shift
            var assingableShift = new Shift(DateTime.Today, TimeConstants.StartOfDay.AddHours(16), TimeConstants.StartOfDay.AddHours(16).AddMinutes(29));
            Assert.IsTrue(new ContinuousDutyHourConstraint().Assignable(resident, assingableShift));
        }
    }
}
