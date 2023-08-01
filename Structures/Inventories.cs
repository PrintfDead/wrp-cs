namespace WashingtonRP.Structures
{
    public class Inventories
    {
        public int ID { get; set; }
        public Item Slot1 { get; set; }
        public int SlotAmount1 { get; set; }
        public Item Slot2 { get; set; }
        public int SlotAmount2 { get; set; }
        public Item Slot3 { get; set; }
        public int SlotAmount3 { get; set; }
        public Item Slot4 { get; set; }
        public int SlotAmount4 { get; set; }
        public Item Slot5 { get; set; }
        public int SlotAmount5 { get; set; }

        public void TakeSlot(int id)
        {
            if (id == 1)
            {
                Slot1 = Items.Vacio;
                SlotAmount1 = 0;
            }
            else if (id == 2)
            {
                Slot2 = Items.Vacio;
                SlotAmount2 = 0;
            }
            else if (id == 3)
            {
                Slot3 = Items.Vacio;
                SlotAmount3 = 0;
            }
            else if (id == 4)
            {
                Slot4 = Items.Vacio;
                SlotAmount4 = 0;
            }
            else
            {
                Slot5 = Items.Vacio;
                SlotAmount5 = 0;
            }
        }

        public Item GetSlot(int id)
        {
            if (id == 1) return Slot1;
            else if (id == 2) return Slot2;
            else if (id == 3) return Slot3;
            else if (id == 4) return Slot4;
            else return Slot5;
        }

        public int GetSlotAmount(int id)
        {
            if (id == 1) return SlotAmount1;
            else if (id == 2) return SlotAmount2;
            else if (id == 3) return SlotAmount3;
            else if (id == 4) return SlotAmount4;
            else return SlotAmount5;
        }
    }
}
