using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace DomainLayer {
    public enum ResidentYears {
        PGY_1,
        PGY_2,
        PGY_3,
        Chief,
    }

    public class Resident {
        public long Id { get; set; }
        public long SortableId { get { return new Random().Next(1, 100); } }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ResidentYears ResidentYear { get; set; }
        public List<VacationRequest> VacationRequests { get; set; }

        readonly ActionList<Shift> shifts;
        public ActionList<Shift> Shifts { get { return shifts; } }

        public Resident() {
            shifts = new ActionList<Shift>();
            shifts.OnAdd += new EventHandler<ActionArgs<Shift>>((list, args) => { SetLookUpValues(args.Item); });
        }

        void SetLookUpValues(Shift child) {
            child.Resident = this;
        }

        public override string ToString() {
          return String.Format("{0}, {1}({2})\n{3}", LastName, FirstName, ResidentYear, Shifts.Aggregate("", (current, s) => string.Format("{0}\n\t{1}", current, s.ToString())));
        }

        public int MaxContinuousShiftHours { get { return ResidentYear == ResidentYears.PGY_1 ? 16 : 24; } }
    }
}
