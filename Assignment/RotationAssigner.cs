using System.Collections.Generic;
using System.Linq;
using DomainLayer;

namespace Assignment {
    public class RotationAssigner : IAssign<Rotation> {
        IShiftFactory shiftFactory;
        IAssign<Shift> shiftAssigner;

        public RotationAssigner() : this(new ShiftFactory(), new ShiftAssigner()) { }

        public RotationAssigner(IShiftFactory shiftFactory, IAssign<Shift> shiftAssigner) {
            this.shiftFactory = shiftFactory;
            this.shiftAssigner = shiftAssigner;
        }

        public AssignmentResult Assign(IEnumerable<Rotation> rotations, IEnumerable<Resident> residents) {
            var assignedResidents = new List<Resident>();
            var residentList = residents.ToList();
            foreach (var rotation in rotations) {
                var result = shiftAssigner.Assign(shiftFactory.Create(rotation.RotationShifts, rotation.BlockStartDate, rotation.BlockEndDate), residentList);
                result.AssignedResidents.ToList().ForEach(l=>residentList.Remove(l));
                assignedResidents.AddRange(result.AssignedResidents);
            }

            return new AssignmentResult(assignedResidents);
        }
    }
}
