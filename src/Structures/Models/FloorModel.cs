using System;
using System.Collections.Generic;
using System.Text;

namespace WashingtonRP.Structures.Models
{
    public class FloorModel
    {
        public int Item { get; set; }
        public int ItemAmount { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public int VirtualWorld { get; set; }
        public int Interior { get; set; }
    }
}
