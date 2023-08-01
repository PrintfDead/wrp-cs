using System;
using System.Collections.Generic;
using System.Text;

namespace WashingtonRP.Structures.Models
{
    public class StoresModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; }
        public int Interior { get; set; }
        public int VirtualWorld { get; set; }
        public float InteriorX { get; set; }
        public float InteriorY { get; set; }
        public float InteriorZ { get; set; }
        public float ExteriorX { get; set; }
        public float ExteriorY { get; set; }
        public float ExteriorZ { get; set; }
    }
}
