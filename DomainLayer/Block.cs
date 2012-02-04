using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public class Block : BaseObject
    {
        public CalendarYear Year { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override bool Validate(){return true;}
    }
}
