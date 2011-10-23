using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
  public enum ResidentYears{
    Intern,
    Senior1,
    Senior2,
    Chief,
    Any
  }

  public class Resident {
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ResidentYears ResidentYear { get; set; }
    public List<VacationRequest> VacationRequests { get; set; }
    public List<Rotation> Rotations { get; set; }

    public override string ToString() {
      return String.Format("{0}, {1}({2})", LastName, FirstName, ResidentYear);
    }
  }
}
