using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer;

namespace DomainLayerTests
{
    [TestFixture]
    public class CalendarYearTest
    {
        [Test]
        public void ValidateFullYearCoverage()
        {
            var year = new CalendarYear(2011);

            Assert.IsFalse(year.Validate());

            year.Blocks.Add(new Block{StartDate = new DateTime(2011, 1, 1), EndDate = new DateTime(2011, 12, 31)});
            Assert.IsTrue(year.Validate());
        }
    }
}
