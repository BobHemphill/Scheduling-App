using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common;
using DomainLayer;

namespace DomainLayerTests {
    [TestFixture]
    public class ShiftDayFactoryTest {
        [Test]
        public void CreateShiftsForDay_ForAllRotationShifts() {
            BitwiseDayOfWeek shiftDay = BitwiseDayOfWeekHelper.Convert(DateTime.Today.DayOfWeek);
            var todayShifts = new List<RotationShift>{ 
                new RotationShift(shiftDay, TimeConstants.StartOfDay, TimeConstants.EndOfDay),
                new RotationShift(shiftDay, TimeConstants.StartOfDay, TimeConstants.EndOfDay),                
            };

            var shifts = new ShiftDayFactory().CreateShiftsForDay(DateTime.Today, todayShifts);
            Assert.AreEqual(2, shifts.Count());
            Assert.IsTrue(shifts.All(s => IsDateSame(DateTime.Today, TimeConstants.StartOfDay, s.StartTime) && IsDateSame(DateTime.Today, TimeConstants.EndOfDay, s.EndTime)));
        }

        [Test]
        public void CreateShiftsForDay_SkipsNoOps() {
            BitwiseDayOfWeek shiftDay = BitwiseDayOfWeekHelper.Convert(DateTime.Today.DayOfWeek);
            var noOpShifts = new List<RotationShift>{ 
                new RotationShift(shiftDay, TimeConstants.StartOfDay, TimeConstants.EndOfDay, new NoOpAssigningRequiredDecorator()),
            };

            var shifts = new ShiftDayFactory().CreateShiftsForDay(DateTime.Today, noOpShifts);
            Assert.AreEqual(0, shifts.Count());
        }

        [Test]
        public void CreateShiftsForDay_GeneratesOvernight() {
            BitwiseDayOfWeek shiftDay = BitwiseDayOfWeekHelper.Convert(DateTime.Today.DayOfWeek);
            var noOpShifts = new List<RotationShift>{ 
                new RotationShift(shiftDay, TimeConstants.StartOfDay, TimeConstants.EndOfDay, multipleDayDecorator: new OvernightMultipleDayDecorator()),
            };

            var shifts = new ShiftDayFactory().CreateShiftsForDay(DateTime.Today, noOpShifts);
            Assert.AreEqual(1, shifts.Count());
            Assert.IsTrue(shifts.All(s => IsDateSame(DateTime.Today, TimeConstants.StartOfDay, s.StartTime) && IsDateSame(DateTime.Today.AddDays(1), TimeConstants.EndOfDay, s.EndTime)));
        }

        [Test]
        public void CreateShiftsForDay_Overnight_MultiDay() {
            BitwiseDayOfWeek shiftDay = BitwiseDayOfWeekHelper.Convert(DateTime.Today.DayOfWeek);
            var noOpShifts = new List<RotationShift>{ 
                new RotationShift(shiftDay, TimeConstants.StartOfDay, TimeConstants.EndOfDay, multipleDayDecorator: new OvernightMultipleDayDecorator()),
            };

            var shifts = new ShiftDayFactory().CreateShiftsForDay(DateTime.Today, noOpShifts);
            Assert.AreEqual(1, shifts.Count());
            Assert.IsTrue(shifts.All(s => IsDateSame(DateTime.Today, TimeConstants.StartOfDay, s.StartTime) && IsDateSame(DateTime.Today.AddDays(1), TimeConstants.EndOfDay, s.EndTime)));
        }

        bool IsDateSame(DateTime expectedDate, DateTime expectedTime, DateTime actualDateTime) {
            return expectedDate.Year == actualDateTime.Year && expectedDate.Month == actualDateTime.Month && expectedDate.Day == actualDateTime.Day &&
                expectedTime.Hour == actualDateTime.Hour && expectedTime.Minute == actualDateTime.Minute;
        }
    }
}
