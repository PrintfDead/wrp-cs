using System.Collections.Generic;
using System.Numerics;

namespace WashingtonRP.Structures
{
    public class Hand
    {
        public bool Position { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
    }

    public class Wrist
    {
        public bool Position { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
    }

    public class Slot
    {
        public int ID { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
    }
    public class Inventories
    {
        public int ID { get; set; }
        public List<Slot> Slot { get; set; }

        public void TakeSlot(int id)
        {
            Slot.ForEach(x =>
            {
                if (x.ID == id)
                {
                    x.Item = Items.Vacio;
                    x.Amount = 0;
                }
            });
        }

        public Slot GetSlot(int id)
        {
            var list = new Slot
            {
                ID = -1,
                Item = Items.Vacio,
                Amount = 0
            };

            Slot.ForEach(x =>
            {
                if (x.ID == id)
                {
                    list = x;
                }
            });

            if (list.ID != -1)
            {
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
