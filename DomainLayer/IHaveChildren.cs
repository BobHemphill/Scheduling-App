using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
    public interface IHaveChildren<T> {
        List<BaseObject<T>> Children { get; }
        bool ValidateChildren(T validationParams);
    }
}
