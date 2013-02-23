using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common;

namespace CommonTests {
    [TestFixture]
    public class DateRangeTest {
        [Test]
        public void Equals() {
            var dateRange1 = new DateRange(TimeConstants.StartOfDay, TimeConstants.EndOfDay);
            //Same
            var dateRange2 = new DateRange(TimeConstants.StartOfDay, TimeConstants.EndOfDay);
            Assert.AreEqual(dateRange1, dateRange2);
            //Starts Later
            dateRange2 = new DateRange(TimeConstants.StartOfDay.AddMinutes(1), TimeConstants.EndOfDay);
            Assert.AreEqual(dateRange1, dateRange2);
            //Starts Sooner
            dateRange2 = new DateRange(TimeConstants.StartOfDay.AddMinutes(-1), TimeConstants.EndOfDay);
            Assert.AreEqual(dateRange1, dateRange2);
            //Ends Later
            dateRange2 = new DateRange(TimeConstants.StartOfDay, TimeConstants.EndOfDay.AddMinutes(1));
            Assert.AreEqual(dateRange1, dateRange2);
            //Ends Sooner
            dateRange2 = new DateRange(TimeConstants.StartOfDay, TimeConstants.EndOfDay.AddMinutes(-1));
            Assert.AreEqual(dateRange1, dateRange2);
            //Contained By
            dateRange2 = new DateRange(TimeConstants.StartOfDay.AddMinutes(-1), TimeConstants.EndOfDay.AddMinutes(1));
            Assert.AreEqual(dateRange1, dateRange2);
            //Contains
            dateRange2 = new DateRange(TimeConstants.StartOfDay.AddMinutes(1), TimeConstants.EndOfDay.AddMinutes(-1));
            Assert.AreEqual(dateRange1, dateRange2);  
            //Sooner Shift
            dateRange2 = new DateRange(TimeConstants.StartOfDay.AddMinutes(-1), TimeConstants.EndOfDay.AddMinutes(-1));
            Assert.AreEqual(dateRange1, dateRange2); 
            //Later Shift
            dateRange2 = new DateRange(TimeConstants.StartOfDay.AddMinutes(1), TimeConstants.EndOfDay.AddMinutes(1));
            Assert.AreEqual(dateRange1, dateRange2); 

            //Before
            dateRange2 = new DateRange(TimeConstants.StartOfDay.AddMinutes(-2), TimeConstants.StartOfDay.AddMinutes(-1));
            Assert.AreNotEqual(dateRange1, dateRange2); 
            //After
            dateRange2 = new DateRange(TimeConstants.EndOfDay.AddMinutes(1), TimeConstants.EndOfDay.AddMinutes(2));
            Assert.AreNotEqual(dateRange1, dateRange2); 
        }

        [Test]
        public void HashCode() {
            var dateRange1 = new DateRange(TimeConstants.StartOfDay, TimeConstants.EndOfDay);          
            var dateRange2 = new DateRange(TimeConstants.StartOfDay, TimeConstants.EndOfDay);

            Assert.AreEqual(1, new List<DateRange>{dateRange1, dateRange2}.GroupBy(t=>t, new DateRangeComparer()).Count());

            dateRange2 = new DateRange(TimeConstants.StartOfDay.AddMinutes(-2), TimeConstants.StartOfDay.AddMinutes(-1));
            Assert.AreEqual(2, new List<DateRange> { dateRange1, dateRange2 }.GroupBy(t => t, new DateRangeComparer()).Count());

            dateRange2 = new DateRange(TimeConstants.EndOfDay.AddMinutes(1), TimeConstants.EndOfDay.AddMinutes(2));
            Assert.AreEqual(2, new List<DateRange> { dateRange1, dateRange2 }.GroupBy(t => t, new DateRangeComparer()).Count());
        }

        [Test]
        public void Hours() {
           Assert.AreEqual(1m, new DateRange(TimeConstants.StartOfDay, TimeConstants.StartOfDay.AddHours(1).AddMinutes(-1)).Hours);
           Assert.AreEqual(24m, new DateRange(TimeConstants.StartOfDay, TimeConstants.EndOfDay).Hours);
           Assert.AreEqual(38m, new DateRange(TimeConstants.EndOfDay.AddHours(-38).AddMinutes(1), TimeConstants.EndOfDay).Hours);
           Assert.AreEqual(.5m, new DateRange(TimeConstants.StartOfDay.AddMinutes(30), TimeConstants.StartOfDay.AddHours(1).AddMinutes(-1)).Hours);
        }

        [Test]
        public void Minutes() {
            Assert.AreEqual(1m, new DateRange(TimeConstants.StartOfDay, TimeConstants.StartOfDay).Minutes);
        }
    }
}
