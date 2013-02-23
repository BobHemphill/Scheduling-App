using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.Validation {
    public class BlockCollectionValidator : ICollectionValidator<Block> {
        public bool Validate(IEnumerable<Block> blocks) {
            if (blocks.Count() < 1) { return false; }
            var orderedBlocks = blocks.OrderBy(i => i.StartDate).ThenBy(i => i.EndDate);
            var min = orderedBlocks.First();
            var max = orderedBlocks.Last();
            var minMax = min.StartDate.Month == 1 && min.StartDate.Day == 1 && max.EndDate.Month == 12 && max.EndDate.Day == 31;

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
