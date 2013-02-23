using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using DomainLayer;
using Common;
using DomainLayer.Validation;

namespace DomainLayerTests {
    [TestFixture]
    public class BlockTest {
        [Test]
        public void IsValid_Calls_RotationValidator() {
            var mockery = new MockRepository();
            var validator = mockery.StrictMock<ICollectionValidator<Rotation>>();

            using (mockery.Record()) {
                Expect.Call(validator.Validate(null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                Assert.IsTrue(new Block(validator).IsValid);
            }
        }

        [Test]
        public void All_Rotations_Valid() {
            var mockery = new MockRepository();
            var rotation = mockery.PartialMock<Rotation>();
            var validator = mockery.DynamicMock<ICollectionValidator<Rotation>>();

            using (mockery.Record()) {
                Expect.Call(rotation.IsValid).Return(true);
                SetupResult.For(validator.Validate(null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                var block = new Block(validator);
                block.Rotations.Add(rotation);

                Assert.IsTrue(block.IsValid);
            }
        }

        [Test]
        public void All_Rotations_InValid() {
            var mockery = new MockRepository();
            var rotation = mockery.PartialMock<Rotation>();

            using (mockery.Record()) {
                Expect.Call(rotation.IsValid).Return(false);
            }
            using (mockery.Playback()) {
                var block = new Block();
                block.Rotations.Add(rotation);

                Assert.IsFalse(block.IsValid);
            }
        }

        [Test]
        public void Block_Set_When_RotationAdded() {
            var block = new Block();
            var rotation1 = new Rotation();

            block.Rotations.Add(rotation1);
            Assert.AreEqual(block, rotation1.Block);

            var rotation2 = new Rotation();
            var rotation3 = new Rotation();
            block.Rotations.AddRange(new List<Rotation> { rotation2, rotation3 });
            Assert.AreEqual(block, rotation2.Block);
            Assert.AreEqual(block, rotation3.Block);
        }
    }
}
