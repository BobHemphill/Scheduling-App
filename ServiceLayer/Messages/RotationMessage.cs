using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainLayer;
using Common;

namespace ServiceLayer.Messages {
    public class RotationMessage : DomainMessage {
        public RotationTypes RotationType { get; set; }

        public RotationMessage(Rotation rotation)
            : base(rotation) {

        }

        protected override void GenerateMessage(BaseObject obj) {
            var rotation = obj as Rotation;
            RotationType = rotation.RotationType;
        }
    }
}
