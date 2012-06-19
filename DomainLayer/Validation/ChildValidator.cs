using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.Validation {
    public class ChildValidator<T> : IValidateChildren<T> {
        public bool ValidateChildren(List<BaseObject<T>> children, T validationParams) {
            return children.Aggregate(true, (current, child) => current &= child.Validate(validationParams));
        }
    }
}
