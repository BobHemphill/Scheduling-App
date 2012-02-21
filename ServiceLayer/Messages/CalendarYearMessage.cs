using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer;

namespace ServiceLayer.Messages {
    public class CalendarYearMessage : DomainMessage {
        public int Year { get; set; }
        public List<BlockMessage> Blocks { get; set; }

        public CalendarYearMessage(CalendarYear year) : base(year) {

        }

        protected override void GenerateMessage(BaseObject obj) {
            var year = obj as CalendarYear;
            Year = year.Year;
            Blocks = new List<BlockMessage>();
            foreach (var block in year.Blocks) {
                Blocks.Add(new BlockMessage(block));
            }
        }
    }
}
