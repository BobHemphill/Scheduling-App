using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer.Validation;

namespace DomainLayer {
    public class CalendarYear : BaseParentObject<EmptyValidationParams, EmptyValidationParams> {
        public int Year { get; set; }

        List<Block> blocks = new List<Block>();
        public List<Block> Blocks { get { return blocks; } set { blocks = value; } }
        public override List<BaseObject<EmptyValidationParams>> Children { get { return Blocks.Cast<BaseObject<EmptyValidationParams>>().ToList(); } }

        public CalendarYear(int year)
            : this(year, new ChildValidator<EmptyValidationParams>()) {
        }

        public CalendarYear(int year, IValidateChildren<EmptyValidationParams> validator)
            : base(validator) {
            Year = year;
        }
        public override EmptyValidationParams ValidationParams { get { return new EmptyValidationParams(); } }
        public override EmptyValidationParams ChildValidationParams { get { return new EmptyValidationParams(); } }
        public override bool Validate(EmptyValidationParams validationParams) {
            if (Blocks.Count < 1) { return false; }
            else {
                var allBlocksValid = ValidateChildren(ChildValidationParams);
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
