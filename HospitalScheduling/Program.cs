using System;
using System.Collections.Generic;
using System.Linq;
using Assignment;
using Common;
using DomainLayer;

namespace HospitalScheduling {
  class Program {
    static void Main(string[] args) {
      var calendar = CreateCalendar();
      var residents = CreateResidents();

      var schedule = new Scheduler().Schedule(calendar, residents);

      schedule.DisplaySchedule();
      Console.ReadLine();
    }

    static CalendarYear CreateCalendar() {
      var calendar = new CalendarYear(2013);
      calendar.Blocks.Add(CreateBlock(new DateTime(calendar.Year, 1, 1), new DateTime(calendar.Year, 1, 15)));
      return calendar;
    }

    static Block CreateBlock(DateTime startDate, DateTime endDate) {
      var block = new Block { Id = 1, Name = "TheBlock", StartDate = startDate, EndDate = endDate };
      block.Rotations.Add(CreateRotation());
      return block;
    }

    static Rotation CreateRotation() {
      var rotation = new Rotation { Id = 2, Name = "TheRotation", RotationType = RotationTypes.Rotation1 };
      var startTime = new DateTime(1, 1, 1, 7, 0, 0);
      rotation.RotationShifts.Add(new RotationShift(TimeConstants.Weekday, startTime, startTime.AddHours(12).AddMinutes(-1)));
      return rotation;
    }

    static IEnumerable<Resident> CreateResidents() {
      return new[] {
        new Resident{Id=3, FirstName = "Bob", LastName = "Hemphill", ResidentYear = ResidentYears.Chief}
      };
    }
  }
}
