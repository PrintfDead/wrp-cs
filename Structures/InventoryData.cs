using System;
using System.Collections.Generic;
using System.Text;

namespace WashingtonRP.Structures
{
    public class InventoryData
    {
        public int ID { get; set; }
        public RegisterItem Slot1 { get; set; }
        public int SlotAmount1 { get; set; }
        public RegisterItem Slot2 { get; set; }
        public int SlotAmount2 { get; set; }
        public RegisterItem Slot3 { get; set; }
        public int SlotAmount3 { get; set; }
        public RegisterItem Slot4 { get; set; }
        public int SlotAmount4 { get; set; }
        public RegisterItem Slot5 { get; set; }
        public int SlotAmount5 { get; set; }

        public RegisterItem GetSlot(int id)
        {
            if (Slot1.ID == id) return Slot1;
            else if (Slot2.ID == id) return Slot2;
            else if (Slot3.ID == id) return Slot3;
            else if (Slot4.ID == id) return Slot4;
            else return Slot5;
        }
    }
}
