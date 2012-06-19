using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer.Validation;

namespace DomainLayer {
    public abstract class BaseObject{
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public abstract class BaseObject<T> : BaseObject, IValidate {
        public abstract bool Validate(T validationParams);
        public abstract T ValidationParams { get; }

        public virtual bool Validate() { return Validate(ValidationParams); }
    }

    public abstract class BaseParentObject<T, U> : BaseObject<T>, IHaveChildren<U> {
        public abstract List<BaseObject<U>> Children { get; }
        
        readonly IValidateChildren<U> childValidator;

        public BaseParentObject(IValidateChildren<U> childValidator) {
            this.childValidator = childValidator;
        }

        public abstract U ChildValidationParams { get; }
        public bool ValidateChildren(U childValidationParams) {
            return childValidator.ValidateChildren(Children, childValidationParams);
        }
    }
}
