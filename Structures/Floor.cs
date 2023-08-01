using SampSharp.GameMode;
using SampSharp.GameMode.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace WashingtonRP.Structures
{
    public class Floor
    {
        public Item Item { get; set; }
        public int ItemAmount { get; set; }
        public Vector3 Position { get; set; }
        public int VirtualWorld { get; set; }
        public int Interior { get; set; }
        public TextLabel Label { get; set; }
    }
}
