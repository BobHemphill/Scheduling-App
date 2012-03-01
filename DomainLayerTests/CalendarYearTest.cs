using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer;
using Rhino.Mocks;
using DomainLayer.Validation;

namespace DomainLayerTests {
    [TestFixture]
    public class CalendarYearTest {
        [Test]
        public void ValidateFullYearCoverage() {
            var mockery = new MockRepository();
            var childValidator = mockery.StrictMock<IValidateChildren>();
            var year = new CalendarYear(2011, childValidator);

            Assert.IsFalse(year.Validate());
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 12, 31) });
            
            using (mockery.Record()) {
                SetupResult.For(childValidator.ValidateChildren(null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                Assert.IsTrue(year.Validate());
            }
        }

        [Test]
        public void ValidateFullYearCoverage_MultipleBlocks() {
            var mockery = new MockRepository();
            var childValidator = mockery.StrictMock<IValidateChildren>();
            var year = new CalendarYear(2011, childValidator);

            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 6, 30) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 7, 1), EndDate = new DateTime(2011, 12, 31) });
            using (mockery.Record()) {
                SetupResult.For(childValidator.ValidateChildren(null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                Assert.IsTrue(year.Validate());
            }
        }

        [Test]
        public void ValidateFullYearCoverage_FullYear_Valid() {
            var mockery = new MockRepository();
            var childValidator = mockery.StrictMock<IValidateChildren>();
            var year = new CalendarYear(2011, childValidator);

            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 1, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 2, 1), EndDate = new DateTime(2011, 2, 28) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 3, 1), EndDate = new DateTime(2011, 3, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 4, 1), EndDate = new DateTime(2011, 4, 30) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 5, 1), EndDate = new DateTime(2011, 5, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 6, 1), EndDate = new DateTime(2011, 6, 30) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 7, 1), EndDate = new DateTime(2011, 7, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 8, 1), EndDate = new DateTime(2011, 8, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 9, 1), EndDate = new DateTime(2011, 9, 30) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 10, 1), EndDate = new DateTime(2011, 10, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 11, 1), EndDate = new DateTime(2011, 11, 30) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 12, 1), EndDate = new DateTime(2011, 12, 31) });
            using (mockery.Record()) {
                SetupResult.For(childValidator.ValidateChildren(null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                Assert.IsTrue(year.Validate());
            }
        }

        [Test]
        public void ValidateFullYearCoverage_FullYear_Invalid() {
            var mockery = new MockRepository();
            var childValidator = mockery.StrictMock<IValidateChildren>();
            var year = new CalendarYear(2011, childValidator);

            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 1, 30) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 2, 1), EndDate = new DateTime(2011, 2, 28) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 3, 1), EndDate = new DateTime(2011, 3, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 4, 1), EndDate = new DateTime(2011, 4, 30) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 5, 1), EndDate = new DateTime(2011, 5, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 6, 1), EndDate = new DateTime(2011, 6, 30) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 7, 1), EndDate = new DateTime(2011, 7, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 8, 1), EndDate = new DateTime(2011, 8, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 9, 1), EndDate = new DateTime(2011, 9, 30) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 10, 1), EndDate = new DateTime(2011, 10, 31) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 11, 1), EndDate = new DateTime(2011, 11, 30) });
            year.Blocks.Add(new Block { StartDate = new DateTime(2011, 12, 1), EndDate = new DateTime(2011, 12, 31) });
            using (mockery.Record()) {
                SetupResult.For(childValidator.ValidateChildren(null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                Assert.IsFalse(year.Validate());
            }
        }

        [Test]
        public void All_Blocks_Valid() {
            var mockery = new MockRepository();
            var block = mockery.PartialMock<Block>();
            block.StartDate = new DateTime(2011, 1, 1);
            block.EndDate = new DateTime(2011, 12, 31);

            using (mockery.Record()) {
                SetupResult.For(block.Validate()).Return(true);
            }
            using (mockery.Playback()) {
                var year = new CalendarYear(2011);
                year.Blocks.Add(block);

                Assert.IsTrue(year.Validate());
            }
        }

        [Test]
        public void All_Blocks_Not_Valid() {
            var mockery = new MockRepository();
            var childValidator = mockery.StrictMock<IValidateChildren>();
            
            var year = new CalendarYear(2011, childValidator);

            using (mockery.Record()) {
                SetupResult.For(childValidator.ValidateChildren(null)).IgnoreArguments().Return(false);
            }
            using (mockery.Playback()) {
                Assert.IsFalse(year.Validate());
            }
        }
    }
}
