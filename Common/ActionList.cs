using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    public class ActionList<T> : List<T> {
        public event EventHandler<ActionArgs<T>> OnAdd;

        public void Add(T item) {
            if (null != OnAdd) {
                OnAdd(this, new ActionArgs<T>(item));
            }
            base.Add(item);
        }

        public void AddRange(IEnumerable<T> items) {
            items.ToList<T>().ForEach(Add);
        }
    }

    public class ActionArgs<T> : EventArgs {
        readonly T item;
        public T Item { get { return item; } }
        public ActionArgs(T item) {
            this.item = item;
        }
    }
}
