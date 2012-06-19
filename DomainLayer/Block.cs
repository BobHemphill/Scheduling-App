using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DomainLayer.Validation;

namespace DomainLayer {
    public class Block : BaseParentObject<EmptyValidationParams, DateRangeValidationParams> {
        public CalendarYear Year { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        List<Rotation> rotations = new List<Rotation>();
        public List<Rotation> Rotations { get { return rotations; } set { rotations = value; } }
        public override List<BaseObject<DateRangeValidationParams>> Children { get { return Rotations.Cast<BaseObject<DateRangeValidationParams>>().ToList(); } }

        public Block()
            : this(new ChildValidator<DateRangeValidationParams>()) {

        }

        public Block(IValidateChildren<DateRangeValidationParams> childValidator)
            : base(childValidator) {

        }

        public override EmptyValidationParams ValidationParams { get { return new EmptyValidationParams(); } }
        public override DateRangeValidationParams ChildValidationParams { get { return new DateRangeValidationParams(StartDate, EndDate); } }
        public override bool Validate(EmptyValidationParams validationParams) {
            var allRotationsValid = ValidateChildren(ChildValidationParams);
            if (!allRotationsValid) { return false; }

            foreach (var type in System.Enum.GetValues(typeof(RotationTypes))) {
                if (!Rotations.Exists(r => r.RotationType == (RotationTypes)type)) return false;
            }

            return true;
        }
    }
}
