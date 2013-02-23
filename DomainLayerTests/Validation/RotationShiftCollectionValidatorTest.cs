using System;
using System.Collections.Generic;
using NUnit.Framework;
using DomainLayer.Validation;
using DomainLayer;
using Common;

namespace DomainLayerTests.Validation {
    [TestFixture]
    public class RotationShiftCollectionValidatorTest {
        [TestCase(BitwiseDayOfWeek.Sunday)]
        [TestCase(BitwiseDayOfWeek.Saturday)]
        public void Validate_Weekend(BitwiseDayOfWeek testDay) {
            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift>()));
            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { new RotationShift(Common.BitwiseDayOfWeek.Monday, TimeConstants.StartOfDay, TimeConstants.EndOfDay) }));
            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { new RotationShift(testDay, TimeConstants.StartOfDay.AddMinutes(1), TimeConstants.EndOfDay) }));
            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { new RotationShift(testDay, TimeConstants.StartOfDay, TimeConstants.EndOfDay.AddMinutes(-1)) }));

            Assert.IsTrue(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { new RotationShift(testDay, TimeConstants.StartOfDay, TimeConstants.EndOfDay) }));
            Assert.IsTrue(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { new RotationShift(TimeConstants.Weekend, TimeConstants.StartOfDay, TimeConstants.EndOfDay) }));
            Assert.IsTrue(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { 
                new RotationShift(TimeConstants.Weekend, TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddMinutes(1)),
                new RotationShift(TimeConstants.Weekend, TimeConstants.StartOfDay.AddMinutes(2), TimeConstants.EndOfDay)
            }));

            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { 
                new RotationShift(TimeConstants.Weekend, TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddMinutes(1)),
                new RotationShift(TimeConstants.Weekend, TimeConstants.StartOfDay.AddMinutes(3), TimeConstants.EndOfDay)
            }));
        }

        [TestCase(BitwiseDayOfWeek.Monday)]
        [TestCase(BitwiseDayOfWeek.Tuesday)]
        [TestCase(BitwiseDayOfWeek.Wednesday)]
        [TestCase(BitwiseDayOfWeek.Thursday)]
        [TestCase(BitwiseDayOfWeek.Friday)]
        public void Validate_WeekDay(BitwiseDayOfWeek testDay) {
            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift>()));
            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { new RotationShift(Common.BitwiseDayOfWeek.Sunday, TimeConstants.StartOfDay, TimeConstants.EndOfDay) }));
            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { new RotationShift(testDay, TimeConstants.StartOfDay.AddMinutes(1), TimeConstants.EndOfDay) }));
            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { new RotationShift(testDay, TimeConstants.StartOfDay, TimeConstants.EndOfDay.AddMinutes(-1)) }));

            Assert.IsTrue(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { new RotationShift(testDay, TimeConstants.StartOfDay, TimeConstants.EndOfDay) }));
            Assert.IsTrue(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { new RotationShift(TimeConstants.Weekday, TimeConstants.StartOfDay, TimeConstants.EndOfDay) }));
            Assert.IsTrue(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { 
                new RotationShift(TimeConstants.Weekday, TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddMinutes(1)),
                new RotationShift(TimeConstants.Weekday, TimeConstants.StartOfDay.AddMinutes(2), TimeConstants.EndOfDay)
            }));

            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(testDay, new List<RotationShift> { 
                new RotationShift(TimeConstants.Weekday, TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddMinutes(1)),
                new RotationShift(TimeConstants.Weekday, TimeConstants.StartOfDay.AddMinutes(3), TimeConstants.EndOfDay)
            }));
        }

        [Test]
        public void ValidateOvernight_BeginningOfDay() {
            var sundayNightShift = new RotationShift(Common.BitwiseDayOfWeek.Sunday, TimeConstants.EndOfDay.AddMinutes(-1), TimeConstants.StartOfDay.AddMinutes(1), multipleDayDecorator: new OvernightMultipleDayDecorator());
            var mondayShift = new RotationShift(Common.BitwiseDayOfWeek.Monday, TimeConstants.StartOfDay.AddMinutes(2), TimeConstants.EndOfDay);

            Assert.IsFalse(new RotationShiftCollectionValidator().ValidateDay(Common.BitwiseDayOfWeek.Sunday, new List<RotationShift> { sundayNightShift }));
            Assert.IsTrue(new RotationShiftCollectionValidator().ValidateDay(Common.BitwiseDayOfWeek.Monday, new List<RotationShift> { sundayNightShift, mondayShift }));
        }
    }

    [TestFixture]
    public class OvernightMultipleDayDecoratorTest {
        [Test]
        public void AdjustApplicableDays() {
            var decorator = new OvernightMultipleDayDecorator();

            var today = DateTime.Today;
            for (int i = 0; i < 7; i++) {
                var testDay = BitwiseDayOfWeekHelper.Convert(today.DayOfWeek);
                var tomorrowTestDay = BitwiseDayOfWeekHelper.Convert(today.AddDays(1).DayOfWeek);

                Assert.AreEqual(testDay, decorator.AdjustApplicableDays(testDay) & testDay);
                Assert.AreEqual(tomorrowTestDay, decorator.AdjustApplicableDays(testDay) & tomorrowTestDay);

                today = today.AddDays(1);
            }
        }

        [Test]
        public void AdjustDayStartTime_SameDay() {
            var decorator = new OvernightMultipleDayDecorator();

            var startTime = TimeConstants.StartOfDay.AddMinutes(1);
            var adjustedStartTime = decorator.AdjustDayStartTime(startTime, BitwiseDayOfWeek.Monday, BitwiseDayOfWeek.Monday);

            Assert.AreEqual(startTime, adjustedStartTime);
        }

        [Test]
        public void AdjustDayStartTime_NextDay() {
            var decorator = new OvernightMultipleDayDecorator();

            var startTime = TimeConstants.StartOfDay.AddMinutes(1);
            var adjustedStartTime = decorator.AdjustDayStartTime(startTime, BitwiseDayOfWeek.Tuesday, BitwiseDayOfWeek.Monday);

            Assert.AreEqual(TimeConstants.StartOfDay, adjustedStartTime);
        }

        [Test]
        public void AdjustDayEndTime_SameDay() {
            var decorator = new OvernightMultipleDayDecorator();

            var endTime = TimeConstants.EndOfDay.AddMinutes(-1);
            var adjustedEndTime = decorator.AdjustDayEndTime(endTime, BitwiseDayOfWeek.Monday, BitwiseDayOfWeek.Monday);

            Assert.AreEqual(TimeConstants.EndOfDay, adjustedEndTime);
        }

        [Test]
        public void AdjustDayEndTime_NextDay() {
            var decorator = new OvernightMultipleDayDecorator();

            var endTime = TimeConstants.EndOfDay.AddMinutes(-1);
            var adjustedEndTime = decorator.AdjustDayEndTime(endTime, BitwiseDayOfWeek.Tuesday, BitwiseDayOfWeek.Monday);

            Assert.AreEqual(endTime, adjustedEndTime);
        }
    }
}
