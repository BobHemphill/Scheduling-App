using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
    public interface IHaveChildren {
        List<BaseObject> Children { get; }
        bool ValidateChildren();
    }
}
