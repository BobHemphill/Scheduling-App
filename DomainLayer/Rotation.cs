using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DomainLayer.Validation;

namespace DomainLayer {
    public class Rotation : BaseObject<DateRangeValidationParams> {
        public RotationTypes RotationType { get; set; }
        public Rotation() { }

        public override bool Validate(DateRangeValidationParams validationParams) {
            return true;
        }

        public override DateRangeValidationParams ValidationParams { get { throw new NotImplementedException(); } }
        public override bool Validate() {
            throw new NotImplementedException();
        }
    }
}
