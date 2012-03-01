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

    public abstract class BaseParentObject : BaseObject, IHaveChildren {
        public abstract List<BaseObject> Children { get; }
        readonly IValidateChildren childValidator;

        public BaseParentObject(IValidateChildren childValidator) {
            this.childValidator = childValidator;
        }

        public bool ValidateChildren() {
            return childValidator.ValidateChildren(Children);
        }
    }
}
