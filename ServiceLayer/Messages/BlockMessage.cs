using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer;

namespace ServiceLayer.Messages {
    public class BlockMessage : DomainMessage {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<RotationMessage> Rotations { get; set; }

        public BlockMessage(Block block)
            : base(block) {

        }

        protected override void GenerateMessage(BaseObject obj) {
            var block = obj as Block;
            StartDate = block.StartDate;
            EndDate = block.EndDate;

            Rotations = new List<RotationMessage>();
            foreach (var rotation in block.Rotations) {
                Rotations.Add(new RotationMessage(rotation));
            }
        }
    }
}
