using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer;

namespace DomainLayerTests {
  [TestFixture]
  public class ResidentTest {
    [Test]
    public void Name() {
      var resident = new Resident { FirstName = "Bob", LastName = "Hemphill", ResidentYear = ResidentYears.Intern };
      Assert.AreEqual("Hemphill, Bob(Intern)", resident.ToString());
    }
  }
}
