using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common;

namespace CommonTests {
    [TestFixture]
    public class ActionListTest {
        class mockObject { public int Item; }

        [Test]
        public void OnAdd() {
            var list = new ActionList<mockObject>();
            list.OnAdd += new EventHandler<ActionArgs<mockObject>>((i, args) => { args.Item.Item = 2; });

            var mock = new mockObject { Item = 1 };
            list.Add(mock);
            Assert.AreEqual(2, mock.Item);
        }

        [Test]
        public void OnAdd_Range() {
            var list = new ActionList<mockObject>();
            list.OnAdd += new EventHandler<ActionArgs<mockObject>>((i, args) => { args.Item.Item = 2; });

            var mock1 = new mockObject { Item = 1 };
            var mock2 = new mockObject { Item = 1 };
            list.AddRange(new List<mockObject>{mock1, mock2});
            Assert.AreEqual(2, mock1.Item);
            Assert.AreEqual(2, mock2.Item);
        }
    }
}
