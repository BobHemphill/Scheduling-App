using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Common;

namespace DomainLayer.Validation {
    public class RotationCollectionValidator : ICollectionValidator<Rotation> {
        public bool Validate(IEnumerable<Rotation> rotations) {
            foreach (var type in System.Enum.GetValues(typeof(RotationTypes))) {
                if (!rotations.Any(r => r.RotationType == (RotationTypes)type)) return false;
            }
            return true;
        }
    }
}
