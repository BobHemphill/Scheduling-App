using System.Collections.Generic;
using System.Linq;
using DomainLayer;

namespace Assignment {
  public interface IBlockAssigner {
    BlockAssignmentResult Assign(IEnumerable<Block> blocks, IEnumerable<Resident> residents);
  }

  public class BlockAssignmentResult {
    public BlockAssignmentResult() : this(new List<Resident>()) { }
    public BlockAssignmentResult(IEnumerable<Resident> residents) {
      Residents = residents;
    }

    public IEnumerable<Resident> Residents { get; private set; }
  }

  public class BlockAssigner : IBlockAssigner {
    readonly IAssign<Rotation> assigner;

    public BlockAssigner() : this(new RotationAssigner()) { }
    public BlockAssigner(IAssign<Rotation> assigner) {
      this.assigner = assigner;
    }

    public BlockAssignmentResult Assign(IEnumerable<Block> blocks, IEnumerable<Resident> residents) {
      var tempResidents = blocks.Aggregate(residents, (current, block) => assigner.Assign(block.Rotations, current).AssignedResidents);
      return new BlockAssignmentResult(tempResidents);
    }
  }
}
