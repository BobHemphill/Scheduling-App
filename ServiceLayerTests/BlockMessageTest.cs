using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainLayer;
using ServiceLayer.Messages;

namespace ServiceLayerTests {
    [TestFixture]
    public class BlockMessageTest {
        [Test]
        public void GenerateMessage() {
            var block = new Block {
                Id = 1,
                Name = "Name", 
                StartDate = DateTime.Today.AddDays(50),
                EndDate = DateTime.Today.AddDays(75),
            };
            block.Rotations.AddRange(new List<Rotation> { new Rotation { Id = 1 }, new Rotation { Id = 2 } });
            var blockMessage = new BlockMessage(block);

            ServiceTestHelper.AssertDomainMessageProperties(block, blockMessage);
            Assert.AreEqual(block.StartDate, blockMessage.StartDate);
            Assert.AreEqual(block.EndDate, blockMessage.EndDate);

            foreach (var rotation in block.Rotations) {
                Assert.IsTrue(blockMessage.Rotations.Exists(rm => rm.Id == rotation.Id));
            }
        }
    }
}
