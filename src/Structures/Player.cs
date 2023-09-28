using SampSharp.GameMode;
using SampSharp.GameMode.World;
using System.Collections.Generic;

namespace WashingtonRP.Structures
{
    public class Player : BasePlayer
    {
        // Account Data
        public int aID { get; set; }
        public string aName { get; set; }
        public string aEmail { get; set; }
        public string aPassword { get; set; }
        public string aIP { get; set; }

        // Player Data
        public int pID { get; set; }
        public string pName { get; set; }
        public Vector3 pPositions { get; set; }
        public float pRotation { get; set; }
        public int pInterior { get; set; }
        public int pVirtualWorld { get; set; }
        public float pHealth { get; set; }
        public float pChaleco { get; set; }
        public int pSkin { get; set; }
        public bool pCrack { get; set; }
        public Admin pAdmin { get; set; }

        // Inventory data
        public List<Hand> Hand { get; set; }
        public List<Wrist> Wrist { get; set; }
        public Inventories Inventory { get; set; }

        // Temp Data
        public bool InLogin { get; set; } = false;
        public List<int> charactersID { get; set; } = new List<int>();
        public bool InTaserReload { get; set; } = false;

        // Anti Cheat
        public int InBalas { get; set; } = 0;

    }
}
