using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using DomainLayer.Validation;

namespace DomainLayerTests {
    [TestFixture]
    public class CalendarYearTest {
        [Test]
        public void IsValid_Calls_BlockValidator() {
            var mockery = new MockRepository();
            var validator = mockery.StrictMock<ICollectionValidator<Block>>();

            using (mockery.Record()) {
                Expect.Call(validator.Validate(null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                Assert.IsTrue(new CalendarYear(2011, validator).IsValid);
            }
        }

        [Test]
        public void All_Blocks_Valid() {
            var mockery = new MockRepository();
            var block = mockery.PartialMock<Block>();
            var validator = mockery.DynamicMock<ICollectionValidator<Block>>();

            using (mockery.Record()) {
                Expect.Call(block.IsValid).Return(true);
                SetupResult.For(validator.Validate(null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                var year = new CalendarYear(2011, validator);
                year.Blocks.Add(block);

                Assert.IsTrue(year.IsValid);
            }
        }

        [Test]
        public void All_Blocks_InValid() {
            var mockery = new MockRepository();
            var block = mockery.PartialMock<Block>();

            using (mockery.Record()) {
                Expect.Call(block.IsValid).Return(false);
            }
            using (mockery.Playback()) {
                var year = new CalendarYear(2011);
                year.Blocks.Add(block);

                Assert.IsFalse(year.IsValid);
            }
        }

        [Test]
        public void CalendarYear_Set_When_BlockAdded() {
            var calendarYear = new CalendarYear(DateTime.Today.Year);
            var block1 = new Block();

            calendarYear.Blocks.Add(block1);
            Assert.AreEqual(calendarYear, block1.CalendarYear);

            var block2 = new Block();
            var block3 = new Block();
            calendarYear.Blocks.AddRange(new List<Block>{block2,block3});
            Assert.AreEqual(calendarYear, block2.CalendarYear);
            Assert.AreEqual(calendarYear, block3.CalendarYear);
        }
    }
}
