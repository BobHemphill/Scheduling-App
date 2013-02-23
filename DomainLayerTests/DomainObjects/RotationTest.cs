using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using DomainLayer.Validation;
using DomainLayer;
using Common;

namespace DomainLayerTests {
    [TestFixture]
    public class RotationTest {
        [Test]
        public void IsValid_Calls_RotationShiftValidator() {
            var mockery = new MockRepository();
            var validator = mockery.StrictMock<ICollectionValidator<RotationShift>>();

            using (mockery.Record()) {
                Expect.Call(validator.Validate(null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                Assert.IsTrue(new Rotation(validator).IsValid);
            }
        }

        [Test]
        public void Rotation_Set_When_RotationShiftAdded() {
            var rotation = new Rotation();
            var rotationShift1 = new RotationShift(TimeConstants.AllWeek, TimeConstants.StartOfDay, TimeConstants.EndOfDay);

            rotation.RotationShifts.Add(rotationShift1);
            Assert.AreEqual(rotation, rotationShift1.Rotation);

            var rotationShift2 = new RotationShift(TimeConstants.AllWeek, TimeConstants.StartOfDay, TimeConstants.EndOfDay);
            var rotationShift3 = new RotationShift(TimeConstants.AllWeek, TimeConstants.StartOfDay, TimeConstants.EndOfDay);
            rotation.RotationShifts.AddRange(new List<RotationShift> { rotationShift2, rotationShift3 });
            Assert.AreEqual(rotation, rotationShift2.Rotation);
            Assert.AreEqual(rotation, rotationShift3.Rotation);
        }
    }
}
