using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer;
using Common;
using DomainLayer.Validation;

namespace DomainLayerTests.Validation {
    [TestFixture]
    public class RotationCollectionValidatorTest {
        [Test]
        public void IsValid_Includes_All_Rotation() {
            var rotationList = new List<Rotation>();
            foreach (var type in System.Enum.GetValues(typeof(RotationTypes))) {
                rotationList.Add(new Rotation { RotationType = (RotationTypes)type });
            }
            Assert.IsTrue(new RotationCollectionValidator().Validate(rotationList));

            rotationList.Remove(rotationList[0]);
            Assert.IsFalse(new RotationCollectionValidator().Validate(rotationList));
        }
    }
}
