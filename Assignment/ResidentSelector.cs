using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer;

namespace Assignment {
    public interface IResidentSelector {
        Resident Select(IEnumerable<Resident> residents, Shift shift);
    }

    public class ResidentSelector : IResidentSelector {
        readonly List<IResidentConstraint> constraints;
        public List<IResidentConstraint> Constraints { get { return constraints; } }

        public ResidentSelector() {
            constraints = new List<IResidentConstraint>();
            constraints.Add(new MinimumTimeOffConstraint());
        }

        public Resident Select(IEnumerable<Resident> residents, Shift shift) {
            return residents.OrderBy(residentsAssociatedWithShift(shift)).ThenBy(r => r.Shifts.Count).ThenBy(r => r.SortableId).First(r => Constraints.TrueForAll(c => c.Assignable(r, shift)));
        }

        static Func<Resident, int> residentsAssociatedWithShift(Shift shift) {
            return r => shift.AssociatedShifts.Any(s => s.Resident != null && s.Resident == r) ? -1 : 0;
        }
    }
}
