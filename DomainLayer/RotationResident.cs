using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
  public class RotationResidents {
    public int Required { get; set; }
    public int Optional { get; set; }

    public List<Resident> Residents { get; set; }

    public RotationResidents(int required, int optional=0) {
      Required = required;
      Optional = optional;
      Residents = new List<Resident>();
    }

    public bool IsScheduled { get { return Residents.Count >= Required && Residents.Count <= (Required + Optional); } }
  }
}
