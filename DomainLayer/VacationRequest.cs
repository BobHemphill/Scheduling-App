using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
  public class VacationRequest {
    public long Id { get; set; }

    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime RequestedDate { get; set; }

    public bool Granted { get; set; }
  }
}
