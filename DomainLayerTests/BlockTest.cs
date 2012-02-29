using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using DomainLayer;

namespace DomainLayerTests {
    [TestFixture]
    public class BlockTest {
        [Test]
        public void IsValid_All_Rotations_Valid() {
            var mockery = new MockRepository();
            var rotation = mockery.PartialMock<Rotation>();           

            using (mockery.Record()) {
                SetupResult.For(rotation.Validate()).Return(true);
            }
            using (mockery.Playback()) {
                var block = new Block();
                block.Rotations.Add(rotation);

                Assert.IsTrue(block.Validate());
            }
        }

        [Test]
        public void IsValid_All_Rotations_Not_Valid() {
            var mockery = new MockRepository();
            var rotation = mockery.PartialMock<Rotation>();

            using (mockery.Record()) {
                SetupResult.For(rotation.Validate()).Return(false);
            }
            using (mockery.Playback()) {
                var block = new Block();
                block.Rotations.Add(rotation);

                Assert.IsFalse(block.Validate());
            }
        }
    }
}
