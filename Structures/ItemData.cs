using System;
using System.Collections.Generic;
using System.Text;

namespace WashingtonRP.Structures
{
    public enum TypeSize
    {
        Empty = 0, Small = 1, Medium = 2, Big = 3
    }

    public class ItemData
    {
        public static RegisterItem Vacio = new RegisterItem(0, "Vacio", 0, 0, TypeSize.Empty, 0, 0, 0);
        public static RegisterItem Nudillera = new RegisterItem(1, "Nudillera", 331, 1, TypeSize.Small, 1, 1, (float)1.3);
        public static RegisterItem PaloGolf = new RegisterItem(2, "Palo de golf", 333, 2, TypeSize.Medium, 1, 1, (float)0.8);
        public static RegisterItem Porra = new RegisterItem(3, "Porra", 334, 3, TypeSize.Small, 1, 1, (float)3.2);
        public static RegisterItem Navaja = new RegisterItem(4, "Navaja", 335, 4, TypeSize.Small, 1, 1, (float)5.2);
        public static RegisterItem Cuchillo = new RegisterItem(5, "Cuchillo de Cocina", 335, 4, TypeSize.Small, 1, 1, (float)3.1);
        public static RegisterItem Bate = new RegisterItem(6, "Bate de Madera", 336, 5, TypeSize.Medium, 1, 1, (float)2.1);
        public static RegisterItem BateMetalico = new RegisterItem(7, "Bate Metalico", 337, 5, TypeSize.Medium, 1, 1, (float)5.4);
        public static RegisterItem Samsung = new RegisterItem(8, "Samsung", 18865, 0, TypeSize.Small, 1, 1, 0);

        public static RegisterItem GetItem(int id)
        {
            if (Vacio.ID == id) return Vacio;
            else if (Nudillera.ID == id) return Nudillera;
            else if (PaloGolf.ID == id) return PaloGolf;
            else if (Porra.ID == id) return Porra;
            else if (Navaja.ID == id) return Navaja;
            else if (Cuchillo.ID == id) return Cuchillo;
            else if (Bate.ID == id) return Bate;
            else if (BateMetalico.ID == id) return BateMetalico;
            else return Vacio;
        }

        public static RegisterItem GetItem(string name)
        {
            if (Vacio.Name == name) return Vacio;
            else if (Nudillera.Name == name) return Nudillera;
            else if (PaloGolf.Name == name) return PaloGolf;
            else if (Porra.Name == name) return Porra;
            else if (Navaja.Name == name) return Navaja;
            else if (Cuchillo.Name == name) return Cuchillo;
            else if (Bate.Name == name) return Bate;
            else if (BateMetalico.Name == name) return BateMetalico;
            else return Vacio;
        }

        public static RegisterItem GetItemByGun(int idgun)
        {
            if (Vacio.IDGun == idgun) return Vacio;
            else if (Nudillera.IDGun == idgun) return Nudillera;
            else if (PaloGolf.IDGun == idgun) return PaloGolf;
            else if (Porra.IDGun == idgun) return Porra;
            else if (Navaja.IDGun == idgun) return Navaja;
            else if (Cuchillo.IDGun == idgun) return Cuchillo;
            else if (Bate.IDGun == idgun) return Bate;
            else if (BateMetalico.IDGun == idgun) return BateMetalico;
            else return Vacio;
        }
    }

    public class RegisterItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Model { get; set; }
        public int IDGun { get; set; }
        public TypeSize Size { get; set; }
        public int DefaultAmount { get; set; }
        public int MaxAmount { get; set; }
        public float BonusDamage { get; set; }

        public RegisterItem(int id, string name, int model, int idgun, TypeSize saved, int defaultamount, int maxamount, float bonus)
        {
            ID = id;
            Name = name;
            Model = model;
            IDGun = idgun;
            Size = saved;
            DefaultAmount = defaultamount;
            MaxAmount = maxamount;
            BonusDamage = bonus;
        }
    }
}
