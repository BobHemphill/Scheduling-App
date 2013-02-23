using NUnit.Framework;
using DomainLayer;

namespace DomainLayerTests {
  [TestFixture]
  public class ResidentTest {
    [Test]
    public void Name() {
      var resident = new Resident { FirstName = "Bob", LastName = "Hemphill", ResidentYear = ResidentYears.PGY_1 };
      Assert.AreEqual("Hemphill, Bob(PGY_1)\n", resident.ToString());
    }
  }
}
