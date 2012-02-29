using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DomainLayer {
    public class Rotation : BaseObject {
        public RotationTypes RotationType { get; set; }
        public Rotation() { }

        public override bool Validate() {
            return true;
        }
    }
}
