using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using DomainLayer;
using Common;

namespace DomainLayerTests {
    [TestFixture]
    public class ShiftFactoryTest {
        [Test]
        public void Create_ForEachDay() {
            var mockery = new MockRepository();
            var shiftDayFactory = mockery.StrictMock<IShiftDayFactory>();

            var startDate = GetMonday(DateTime.Today);
            var mondayRotationShift = new RotationShift(Common.BitwiseDayOfWeek.Monday, TimeConstants.StartOfDay, TimeConstants.EndOfDay);
            var tuesdayRotationShift = new RotationShift(Common.BitwiseDayOfWeek.Tuesday, TimeConstants.StartOfDay, TimeConstants.EndOfDay);

            var rotationShifts = new List<RotationShift>{mondayRotationShift, tuesdayRotationShift};
            using (mockery.Record()) {
                Expect.Call(shiftDayFactory.CreateShiftsForDay(Arg<DateTime>.Is.Equal(startDate), Arg<IEnumerable<RotationShift>>.List.IsIn(mondayRotationShift))).Return(new List<Shift>());
                Expect.Call(shiftDayFactory.CreateShiftsForDay(Arg<DateTime>.Is.Equal(startDate.AddDays(1)), Arg<IEnumerable<RotationShift>>.List.IsIn(tuesdayRotationShift))).Return(new List<Shift>());
            }
            using (mockery.Playback()) {
                new ShiftFactory(shiftDayFactory).Create(rotationShifts, startDate, startDate.AddDays(1));
            }
        }

        [Test]
        public void Create_ForEachDay_MutlidayRotationShift() {
            var mockery = new MockRepository();
            var shiftDayFactory = mockery.StrictMock<IShiftDayFactory>();

            var startDate = GetMonday(DateTime.Today);
            var mondayTuesdayRotationShift = new RotationShift(Common.BitwiseDayOfWeek.Monday | BitwiseDayOfWeek.Tuesday, TimeConstants.StartOfDay, TimeConstants.EndOfDay);

            var rotationShifts = new List<RotationShift> { mondayTuesdayRotationShift };
            using (mockery.Record()) {
                Expect.Call(shiftDayFactory.CreateShiftsForDay(Arg<DateTime>.Is.Equal(startDate), Arg<IEnumerable<RotationShift>>.List.IsIn(mondayTuesdayRotationShift))).Return(new List<Shift>());
                Expect.Call(shiftDayFactory.CreateShiftsForDay(Arg<DateTime>.Is.Equal(startDate.AddDays(1)), Arg<IEnumerable<RotationShift>>.List.IsIn(mondayTuesdayRotationShift))).Return(new List<Shift>());
            }
            using (mockery.Playback()) {
                new ShiftFactory(shiftDayFactory).Create(rotationShifts, startDate, startDate.AddDays(1));
            }
        }

        DateTime GetMonday(DateTime dateTime) {
            while (dateTime.DayOfWeek != DayOfWeek.Monday) { dateTime = dateTime.AddDays(1); }
            return dateTime;
        }
    }
}
