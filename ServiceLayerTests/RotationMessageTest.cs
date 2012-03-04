using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer;
using Common;
using ServiceLayer.Messages;

namespace ServiceLayerTests {
    [TestFixture]
    public class RotationMessageTest {
        [Test]
        public void GenerateMessage() {
            var rotation = new Rotation {
                Id = 1,
                Name = "Name",
                RotationType = RotationTypes.Rotation3
            };
            var rotationMessage = new RotationMessage(rotation);

            ServiceTestHelper.AssertDomainMessageProperties(rotation, rotationMessage);
            Assert.AreEqual(rotation.RotationType, rotationMessage.RotationType);
        }
    }
}
