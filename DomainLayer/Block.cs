using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
    public class Block : BaseParentObject {
        public CalendarYear Year { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        List<Rotation> rotations = new List<Rotation>();
        public List<Rotation> Rotations { get { return rotations; } set { rotations = value; } }
        public override List<BaseObject> Children { get { return Rotations.Cast<BaseObject>().ToList(); } }

        public override bool Validate() {
            var allRotationsValid = base.Validate();
            if (!allRotationsValid) { return false; }

            return true;
        }
    }
}
