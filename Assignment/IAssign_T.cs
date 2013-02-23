using System.Collections.Generic;
using System.Linq;
using DomainLayer;

namespace Assignment {
    public interface IAssign<T> {
        AssignmentResult Assign(IEnumerable<T> assignments, IEnumerable<Resident> residents);
    }

    public class AssignmentResult {
        public bool Assigned { get; private set; }
        public IEnumerable<Resident> AssignedResidents { get; private set; }

        public AssignmentResult() : this(new List<Resident>()) { }
        public AssignmentResult(IEnumerable<Resident> residents) {
            Assigned = residents.Count() > 0;
            AssignedResidents = residents;
        }


        public static AssignmentResult Failure { get { return new AssignmentResult { Assigned = false }; } }
        public static AssignmentResult Success { get { return new AssignmentResult { Assigned = true }; } }
    }
}
