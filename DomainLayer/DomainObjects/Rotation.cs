using System;
using Common;
using DomainLayer.Validation;

namespace DomainLayer {
  public class Rotation : BaseObject {
    public Block Block { get; set; }
    public DateTime BlockStartDate { get { return Block.StartDate; } }
    public DateTime BlockEndDate { get { return Block.EndDate; } }

    readonly ICollectionValidator<RotationShift> rotationShiftValidator;
    public RotationTypes RotationType { get; set; }

    readonly ActionList<RotationShift> rotationShifts;
    public ActionList<RotationShift> RotationShifts { get { return rotationShifts; } }

    public Rotation() : this(new RotationShiftCollectionValidator()) { }
    public Rotation(ICollectionValidator<RotationShift> rotationShiftValidator) {
      this.rotationShiftValidator = rotationShiftValidator;

      rotationShifts = new ActionList<RotationShift>();
      rotationShifts.OnAdd += new EventHandler<ActionArgs<RotationShift>>((list, args) => { SetLookUpValues(args.Item); });
    }

    void SetLookUpValues(RotationShift child) {
      child.Rotation = this;
    }

    public override bool IsValid {
      get { return rotationShiftValidator.Validate(RotationShifts); }
    }

    public override string ToString() {
      return string.Format("{0} {1}", Block, Name);
    }
  }
}
