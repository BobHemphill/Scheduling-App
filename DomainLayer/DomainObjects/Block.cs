using System;
using System.Linq;
using Common;
using DomainLayer.Validation;

namespace DomainLayer {
  public class Block : BaseObject {
    readonly ICollectionValidator<Rotation> rotationValidator;
    public CalendarYear CalendarYear { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    readonly ActionList<Rotation> rotations;
    public ActionList<Rotation> Rotations { get { return rotations; } }

    public Block() : this(new RotationCollectionValidator()) { }
    public Block(ICollectionValidator<Rotation> rotationValidator) {
      this.rotationValidator = rotationValidator;

      rotations = new ActionList<Rotation>();
      rotations.OnAdd += new EventHandler<ActionArgs<Rotation>>((list, args) => { SetLookUpValues(args.Item); });
    }

    void SetLookUpValues(Rotation child) {
      child.Block = this;
    }

    public override bool IsValid {
      get {
        if (!Rotations.All(b => b.IsValid)) { return false; }
        return rotationValidator.Validate(Rotations);
      }
    }

    public override string ToString() {
      return string.Format("{0}({1} - {2})", Name, StartDate.ToString("d"), EndDate.ToString("d"));
    }
  }
}
