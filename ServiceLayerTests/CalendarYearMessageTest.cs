using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer;
using ServiceLayer.Messages;

namespace ServiceLayerTests {
    [TestFixture]
    public class CalendarYearMessageTest {
        [Test]
        public void GenerateMessage() {
            var year = new CalendarYear(2012) { Blocks = new List<Block> { new Block { Id = 1 }, new Block { Id = 2 } } };
            var yearMessage = new CalendarYearMessage(year);

            ServiceTestHelper.AssertDomainMessageProperties(year, yearMessage);
            Assert.AreEqual(year.Year, yearMessage.Year);
            foreach (var block in year.Blocks) {
                Assert.IsTrue(yearMessage.Blocks.Exists(bm => bm.Id == block.Id));
            }
        }
    }
}
