using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DomainLayer.Validation;

namespace DomainLayer {
    public class Block : BaseParentObject {
        public CalendarYear Year { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        List<Rotation> rotations = new List<Rotation>();
        public List<Rotation> Rotations { get { return rotations; } set { rotations = value; } }
        public override List<BaseObject> Children { get { return Rotations.Cast<BaseObject>().ToList(); } }

        public Block():this(new ChildValidator()) {

        }

        public Block(IValidateChildren childValidator) : base(childValidator) {

        }

        public override bool Validate() {
            var allRotationsValid = ValidateChildren();
            if (!allRotationsValid) { return false; }

            foreach (var type in System.Enum.GetValues(typeof(RotationTypes))) {
                if (!Rotations.Exists(r => r.RotationType == (RotationTypes)type)) return false;
            }

            return true;
        }
    }
}
