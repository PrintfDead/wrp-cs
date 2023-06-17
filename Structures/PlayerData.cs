using SampSharp.GameMode;
using SampSharp.GameMode.World;
using System.Collections.Generic;

namespace WashingtonRP.Structures
{
    public class PlayerData : BasePlayer
    {
        // Account Data
        public int aID { get; set; }
        public string aName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Ip { get; set; }

        // Player Data
        public int ID { get; set; }
        public string pName { get; set; }
        public Vector3 Positions { get; set; }
        public float pRotation { get; set; }
        public int pInterior { get; set; }
        public int pVirtualWorld { get; set; }
        public float pHealth { get; set; }
        public float pChaleco { get; set; }
        public int pSkin { get; set; }
        public bool Crack { get; set; }

        // Inventory data
        public int RightHand { get; set; }
        public int LeftHand { get; set; }
        public InventoryData Inventory { get; set; }

        // Temp Data
        public bool state_on { get; set; } = false;
        public List<int> charactersID { get; set; } = new List<int>();

    }
}
