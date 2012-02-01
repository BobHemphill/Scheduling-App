using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public class CalendarYear
    {
        int Id { get; set; }
        int Year { get; set; }
        bool IsLeapYear { get { return false; } }

        List<Block> blocks = new List<Block>();
        public List<Block> Blocks { get { return blocks; } set { blocks = value; } }

        public CalendarYear(int year)
        {
            Year = year;
        }

        public bool Validate()
        {
            if (Blocks.Count < 1)
                return false;
            else
                return Blocks[0].StartDate == new DateTime(Year, 1, 1) && Blocks[0].EndDate == new DateTime(Year, 12, 31);
        }
    }
}
