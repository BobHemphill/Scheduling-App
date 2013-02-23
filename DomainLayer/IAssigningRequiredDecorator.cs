using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
    public interface IAssigningRequiredDecorator {
        bool IsAssingingRequired();
    }

    public class NoOpAssigningRequiredDecorator : IAssigningRequiredDecorator {
        public bool IsAssingingRequired() {
            return false;
        }
    }
}
