using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using Common;

namespace Assignment {
    public class ShiftAssigner : IAssign<Shift> {
        IResidentSelector residentSelector;

        public ShiftAssigner() : this(new ResidentSelector()) { }
        public ShiftAssigner(IResidentSelector residentSelector) {
            this.residentSelector = residentSelector;
        }

        public AssignmentResult Assign(IEnumerable<Shift> shifts, IEnumerable<Resident> residents) {
            var assignedResidents = new List<Resident>();

            foreach (var shiftGroup in shifts.GroupBy(s => s.DateRange, new DateRangeComparer())) {
                var residentList = residents.ToList();//enforces residents not double assigned during a specific date range
                foreach (var shift in shiftGroup) {
                    if (shift.Resident != null) continue;//Already assigned

                    var resident = residentSelector.Select(residentList, shift);
                    if (resident == null) return AssignmentResult.Failure;

                    resident.Shifts.Add(shift);
                    residentList.Remove(resident);
                    if (!assignedResidents.Contains(resident)) {
                        assignedResidents.Add(resident);
                    }
                }
            }

            return new AssignmentResult(assignedResidents);
        }
    }
}
