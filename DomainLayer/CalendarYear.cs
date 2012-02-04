using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer.Validation;

namespace DomainLayer {
    public class CalendarYear : BaseObject {
        int Year { get; set; }
        bool IsLeapYear { get { return false; } }

        List<Block> blocks = new List<Block>();
        public List<Block> Blocks { get { return blocks; } set { blocks = value; } }

        public CalendarYear(int year) {
            Year = year;
        }

        public override bool Validate() {
            if (Blocks.Count < 1) { return false; }
            else {
                var allBlocksValid = Blocks.Aggregate(true, (current, block) => current &= block.Validate());
                if (!allBlocksValid) { return false; }

                var orderedBlocks = Blocks.OrderBy(i => i.StartDate).ThenBy(i => i.EndDate);
                var min = orderedBlocks.First();
                var max = orderedBlocks.Last();
                var minMax = min.StartDate == new DateTime(Year, 1, 1) && max.EndDate == new DateTime(Year, 12, 31);

                if (!minMax) { return minMax; }

                DateTime? nextBlock = null;
                foreach (Block block in orderedBlocks) {
                    if (nextBlock != null) {
                        if (block.StartDate != nextBlock.Value) { return false; }
                    }
                    nextBlock = block.EndDate.AddDays(1);
                }
                return true;
            }
        }
    }
}
