using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer;

namespace ServiceLayer.Messages {
    public abstract class DomainMessage {
        public int Id { get; set; }
        public string Name { get; set; }

        public DomainMessage(BaseObject obj) {
            Id = obj.Id;
            Name = obj.Name;
            GenerateMessage(obj);
        }

        protected abstract void GenerateMessage(BaseObject obj);
    }
}
