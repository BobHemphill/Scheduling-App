using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.Validation {
    public interface ICollectionValidator<T> {
        bool Validate(IEnumerable<T> rotations);
    }
}
