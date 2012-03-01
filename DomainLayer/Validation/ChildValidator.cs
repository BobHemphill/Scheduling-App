using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.Validation {
    public class ChildValidator : IValidateChildren {
        public bool ValidateChildren(List<BaseObject> children) {
            return children.Aggregate(true, (current, child) => current &= child.Validate());
        }
    }
}
