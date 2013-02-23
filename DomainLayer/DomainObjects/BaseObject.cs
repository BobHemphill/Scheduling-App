using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer.Validation;

namespace DomainLayer {
    public abstract class BaseObject{
        public int Id { get; set; }
        public string Name { get; set; }

        public abstract bool IsValid { get; }
    }
}
