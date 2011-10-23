using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer {
  public class ResidentYearsCount {
    public int Interns { get; set; }
    public int Senior1s { get; set; }
    public int Senior2s { get; set; }
    public int Chiefs { get; set; }
    public int Anys { get; set; }

    public ResidentYearsCount() : this(1,1,1,1,1) {}

    public ResidentYearsCount(int i, int s1, int s2, int c, int a) {
      Interns = i;
      Senior1s = s1;
      Senior2s = s2;
      Chiefs = c;
      Anys = a;
    }

    public RotationResidents this[ResidentYears index] {
      get {
        switch(index) {
          case ResidentYears.Intern:
            return new RotationResidents(Interns);
          case ResidentYears.Senior1:
            return new RotationResidents(Senior1s);
          case ResidentYears.Senior2:
            return new RotationResidents(Senior2s);
          case ResidentYears.Chief:
            return new RotationResidents(Chiefs);
          case ResidentYears.Any:
            return new RotationResidents(Anys);
          default:
            throw new IndexOutOfRangeException();
        }
      }
    }
  }

  public class Rotation {
    public long Id { get; set; }
    public string Name { get; set; }

    public decimal Weight { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Dictionary<ResidentYears, RotationResidents> Residents { get; set; }

    public Rotation(string name, DateTime startDate, DateTime endDate, ResidentYearsCount requiredCounts=null) {
      Name = name;
      StartDate = startDate;
      EndDate = endDate;

      Residents = new Dictionary<ResidentYears, RotationResidents>();
      if(requiredCounts != null) {
        foreach(ResidentYears index in Enum.GetValues(typeof(ResidentYears))) {
          Residents.Add(index, requiredCounts[index]);
        }
      }
    }

    bool isScheduled = false;
    public bool IsScheduled { get { return isScheduled; } }

    public override string ToString() {
      string ret =  String.Format("{0} - ({1}-{2})", Name, StartDate, EndDate);
      
      foreach(KeyValuePair<ResidentYears, RotationResidents> kvp in Residents) {
        foreach(Resident r in kvp.Value.Residents) {
          ret += String.Format("\n\t{0}", r);
        }
      }
      ret += "\n";
      return ret;
    }

    public void Schedule(List<Resident> residents) {
      foreach(KeyValuePair<ResidentYears, RotationResidents> kvp in Residents) {
        if(residents.Any(r => r.ResidentYear == kvp.Key)) {
          kvp.Value.Residents.Add(residents.First(r => r.ResidentYear == kvp.Key));
        }
      }
      isScheduled = true;
    }
  }
}
