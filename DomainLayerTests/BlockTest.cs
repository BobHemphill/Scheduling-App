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
        public void IsValid_All_Rotations_Not_Valid() {
            var mockery = new MockRepository();
            var childValidator = mockery.StrictMock<IValidateChildren<DateRangeValidationParams>>();
            var block = new Block(childValidator);

            using (mockery.Record()) {
                Expect.Call(childValidator.ValidateChildren(null, null)).IgnoreArguments().Return(false);
            }
            using (mockery.Playback()) {
                Assert.IsFalse(block.Validate());
            }
        }

        [Test]
        public void IsValid_Includes_All_Rotation_False() {
            var mockery = new MockRepository();
            var childValidator = mockery.StrictMock<IValidateChildren<DateRangeValidationParams>>();

            var rotationList = new List<Rotation>();
            foreach (var type in System.Enum.GetValues(typeof(RotationTypes))) {
                rotationList.Add(new Rotation { RotationType = (RotationTypes)type });
            }
            rotationList.Remove(rotationList[0]);
            var block = new Block(childValidator);
            block.Rotations.AddRange(rotationList);

            using (mockery.Record()) {
                Expect.Call(childValidator.ValidateChildren(null, null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                Assert.IsFalse(block.Validate());
            }
        }

        [Test]
        public void IsValid_Includes_All_Rotation_True() {
            var mockery = new MockRepository();
            var childValidator = mockery.StrictMock<IValidateChildren<DateRangeValidationParams>>();

            var rotationList = new List<Rotation>();
            foreach (var type in System.Enum.GetValues(typeof(RotationTypes))) {
                rotationList.Add(new Rotation { RotationType = (RotationTypes)type });
            }
            var block = new Block(childValidator);
            block.Rotations.AddRange(rotationList);

            using (mockery.Record()) {
                Expect.Call(childValidator.ValidateChildren(null, null)).IgnoreArguments().Return(true);
            }
            using (mockery.Playback()) {
                Assert.True(block.Validate());
            }
        }
    }
}
