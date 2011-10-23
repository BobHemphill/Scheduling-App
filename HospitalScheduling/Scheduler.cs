using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer;

namespace HospitalScheduling {
  public class Scheduler {
    List<Resident> residents;
    List<Rotation> rotations;

    public void Schedule() {
      CreateResidents();
      CreateRotations();

      while(rotations.All(r => !r.IsScheduled)) {
        var rotation = rotations.First(r => !r.IsScheduled);
        rotation.Schedule(residents);
      }

      DisplaySchedule();
    }

    private void DisplaySchedule() {
      foreach(var resident in residents) {
        Console.WriteLine(resident);
      }
      Console.WriteLine();
      foreach(var rotation in rotations) {
        Console.WriteLine(rotation);
      }
    }

    private void CreateResidents() {
      residents = new List<Resident>();
      residents.Add(new Resident { FirstName = "Bob", LastName = "Hemphill", ResidentYear = ResidentYears.Intern });
      residents.Add(new Resident { FirstName = "Beth", LastName = "Elston", ResidentYear = ResidentYears.Senior1 });
      residents.Add(new Resident { FirstName = "Fakey", LastName = "McFakerson", ResidentYear = ResidentYears.Senior2 });
      residents.Add(new Resident { FirstName = "Cheif", LastName = "Wiggum", ResidentYear = ResidentYears.Chief });
    }

    private void CreateRotations() {
      rotations = new List<Rotation>();
      rotations.Add(new Rotation("ER", (DateTime.Today.AddDays(-7)), DateTime.Today, new ResidentYearsCount()));
      rotations.Add(new Rotation("Red", (DateTime.Today.AddDays(-7)), DateTime.Today, new ResidentYearsCount()));
      rotations.Add(new Rotation("Purple", (DateTime.Today.AddDays(-7)), DateTime.Today, new ResidentYearsCount()));
      rotations.Add(new Rotation("Yellow", (DateTime.Today.AddDays(-7)), DateTime.Today, new ResidentYearsCount()));
    }
  }
}
