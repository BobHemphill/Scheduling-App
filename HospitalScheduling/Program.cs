using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalScheduling {
  class Program {
    static void Main(string[] args) {
      new Scheduler().Schedule();

      Console.ReadLine();
    }
  }
}
