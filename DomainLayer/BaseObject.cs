using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer.Validation;

namespace DomainLayer {
    public abstract class BaseObject : IValidate {
        public int Id { get; set; }
        public string Name { get; set; }

        public abstract bool Validate();
    }

    public abstract class BaseParentObject : BaseObject, IValidateChildren {
        public abstract List<BaseObject> Children { get; }
        
        public override bool Validate() {
            return ValidateChildren();
        }

        public bool ValidateChildren() {
            return Children.Aggregate(true, (current, child) => current &= child.Validate());
        }
    }
}
