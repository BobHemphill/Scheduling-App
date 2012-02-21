using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer;
using ServiceLayer.Messages;
using NUnit.Framework;

namespace ServiceLayerTests {
    public static class ServiceTestHelper {
        public static void AssertDomainMessageProperties(BaseObject obj, DomainMessage message) {
            Assert.AreEqual(obj.Id, message.Id);
            Assert.AreEqual(obj.Name, message.Name);
        }
    }
}
