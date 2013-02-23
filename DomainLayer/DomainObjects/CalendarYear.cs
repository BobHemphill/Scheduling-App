using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer.Validation;
using Common;

namespace DomainLayer {
    public class CalendarYear : BaseObject {
        readonly ICollectionValidator<Block> blockValidator;

        public int Year { get; set; }

        readonly ActionList<Block> blocks;
        public ActionList<Block> Blocks { get { return blocks; } }

        public CalendarYear(int year) : this(year, new BlockCollectionValidator()) { }
        public CalendarYear(int year, ICollectionValidator<Block> blockValidator) {
            Year = year;
            this.blockValidator = blockValidator;

            blocks = new ActionList<Block>();
            blocks.OnAdd += new EventHandler<ActionArgs<Block>>((list, args) => { SetLookUpValues(args.Item); });
        }

        void SetLookUpValues(Block child) {
            child.CalendarYear = this;
        }

        public override bool IsValid {
            get {
                if (!Blocks.All(b=>b.IsValid)) { return false; }
                return blockValidator.Validate(Blocks);
            }
        }
    }
}
