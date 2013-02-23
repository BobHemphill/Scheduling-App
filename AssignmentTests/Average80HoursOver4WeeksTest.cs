using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer;
using Common;
using Assignment;

namespace AssignmentTests {
    [TestFixture]
    public class Average80HoursOver4WeeksTest {
        [Test]
        public void Assignable_AlreadyOverAverage() {
            var week1Shifts = Create80Hour1MinuteWorkWeek(DateTime.Today);
            var week2Shifts = Create80Hour1MinuteWorkWeek(DateTime.Today.AddDays(7));
            var week3Shifts = Create80Hour1MinuteWorkWeek(DateTime.Today.AddDays(14));
            var week4Shifts = Create80Hour1MinuteWorkWeek(DateTime.Today.AddDays(21));

            var resident = new Resident();
            resident.Shifts.AddRange(week1Shifts.Union(week2Shifts.Union(week3Shifts.Union(week4Shifts))));

            var unassignableShift = new Shift(DateTime.Today.AddDays(25), TimeConstants.StartOfDay, TimeConstants.StartOfDay);
            Assert.IsFalse(new Average80HoursOver4WeeksConstraint().Assignable(resident, unassignableShift));
        }

        [Test]
        public void Assignable_28DaysLater() {
            var week1Shifts = Create80HourWorkWeek(DateTime.Today);
            var week2Shifts = Create80HourWorkWeek(DateTime.Today.AddDays(7));
            var week3Shifts = Create80HourWorkWeek(DateTime.Today.AddDays(14));
            var week4Shifts = Create80HourWorkWeek(DateTime.Today.AddDays(21));

            var resident = new Resident();
            resident.Shifts.AddRange(week1Shifts.Union(week2Shifts.Union(week3Shifts.Union(week4Shifts))));

            var unassignableShift = new Shift(DateTime.Today.AddDays(28), TimeConstants.StartOfDay, TimeConstants.StartOfDay);
            Assert.IsTrue(new Average80HoursOver4WeeksConstraint().Assignable(resident, unassignableShift));
        }

        [Test]
        public void Assignable_ShiftWouldResultInMoreThan80Average() {
            var week1Shifts = Create80Hour1MinuteWorkWeek(DateTime.Today);
            var week2Shifts = Create80Hour1MinuteWorkWeek(DateTime.Today.AddDays(7));
            var week3Shifts = Create80Hour1MinuteWorkWeek(DateTime.Today.AddDays(14));
            var week4Shifts = Create80Hour1MinuteWorkWeek(DateTime.Today.AddDays(21));

            var resident = new Resident();
            resident.Shifts.AddRange(week1Shifts.Union(week2Shifts.Union(week3Shifts)));

            foreach (var assignableShift in week4Shifts.Take(3)) {
                Assert.IsTrue(new Average80HoursOver4WeeksConstraint().Assignable(resident, assignableShift));
                resident.Shifts.Add(assignableShift);
            }
            Assert.IsFalse(new Average80HoursOver4WeeksConstraint().Assignable(resident, week4Shifts.Skip(3).First()));
        }

        IEnumerable<Shift> Create80Hour1MinuteWorkWeek(DateTime startOfWeek) {
            return new List<Shift>(){
                new Shift(startOfWeek, TimeConstants.StartOfDay, TimeConstants.EndOfDay),//24 Hour Shift
                new Shift(startOfWeek.AddDays(1), TimeConstants.StartOfDay, TimeConstants.EndOfDay),//24 Hour Shift
                new Shift(startOfWeek.AddDays(2), TimeConstants.StartOfDay, TimeConstants.EndOfDay),//24 Hour Shift
                new Shift(startOfWeek.AddDays(3), TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddHours(8)),//8 Hour 1 Minute Shift
            };
        }

        IEnumerable<Shift> Create80HourWorkWeek(DateTime startOfWeek) {
            return new List<Shift>(){
                new Shift(startOfWeek, TimeConstants.StartOfDay, TimeConstants.EndOfDay),//24 Hour Shift
                new Shift(startOfWeek.AddDays(1), TimeConstants.StartOfDay, TimeConstants.EndOfDay),//24 Hour Shift
                new Shift(startOfWeek.AddDays(2), TimeConstants.StartOfDay, TimeConstants.EndOfDay),//24 Hour Shift
                new Shift(startOfWeek.AddDays(3), TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddHours(8).AddMinutes(-1)),//8 Hour Shift
            };
        }
    }
}
