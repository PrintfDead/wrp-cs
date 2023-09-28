namespace WashingtonRP.Structures
{
    public enum TypeSize
    {
        Empty = 0, Small = 1, Medium = 2, Big = 3
    }

    public class Items
    {
        public static Item Vacio = new Item(0, "Vacio", 0, 0, TypeSize.Empty, 0, 0, 0);
        public static Item Nudillera = new Item(1, "Nudillera", 331, 1, TypeSize.Small, 1, 1, 1.3f);
        public static Item PaloGolf = new Item(2, "Palo de golf", 333, 2, TypeSize.Medium, 1, 1, 0.8f);
        public static Item Porra = new Item(3, "Porra", 334, 3, TypeSize.Small, 1, 1, 3.2f);
        public static Item Navaja = new Item(4, "Navaja", 335, 4, TypeSize.Small, 1, 1, 5.2f);
        public static Item Bate = new Item(5, "Bate de Madera", 336, 5, TypeSize.Medium, 1, 1, 2.1f);
        public static Item Pala = new Item(6, "Pala", 337, 6, TypeSize.Big, 1, 1, 2.3f);
        public static Item PaloPool = new Item(7, "Palo de Pool", 338, 7, TypeSize.Big, 1, 1, 1.3f);
        public static Item Katana = new Item(8, "Katana", 339, 8, TypeSize.Medium, 1, 1, 10.0f);
        public static Item Motosierra = new Item(9, "Motosierra", 341, 9, TypeSize.Big, 1, 1, 9.2f);
        public static Item DildoPurpura = new Item(10, "Dildo purpura", 321, 10, TypeSize.Small, 1, 1, 0f);
        public static Item Dildo = new Item(11, "Dildo", 322, 11, TypeSize.Small, 1, 1, 0f);
        public static Item Flores = new Item(12, "Flores", 325, 14, TypeSize.Small, 1, 1, 0f);
        public static Item Granada = new Item(13, "Granda", 342, 16, TypeSize.Small, 1, 4, 0f);
        public static Item Gas = new Item(14, "Granada de Gas", 343, 17, TypeSize.Small, 1, 4, 0f);
        public static Item Molotov = new Item(15, "Botella Molotov", 344, 18, TypeSize.Medium, 1, 1, 0f);
        public static Item Glock17 = new Item(16, "Glock-17", 346, 22, TypeSize.Medium, 1, 15, 3.5f);
        public static Item Glock17Silenciada = new Item(17, "Glock-17 Silenciada", 347, 23, TypeSize.Big, 1, 15, 2.5f);
        public static Item P250 = new Item(18, "Sig Sauer P-250", 348, 24, TypeSize.Medium, 1, 30, 5.0f);
        public static Item Escopeta = new Item(19, "Remington 870 Express", 349, 25, TypeSize.Big, 1, 8, 60.0f);
        public static Item Lupara = new Item(20, "Lupara .615", 350, 26, TypeSize.Big, 1, 2, 18.6f);
        public static Item EscopetaCombate = new Item(21, "SPAS-12", 351, 27, TypeSize.Big, 1, 12, 19.5f);
        public static Item Uzi = new Item(22, "UZI .177 9mm", 352, 28, TypeSize.Medium, 1, 30, 1.2f);
        public static Item MP5 = new Item(23, "HK MP5 .22", 353, 29, TypeSize.Big, 1, 30, 1.5f);
        public static Item AK47 = new Item(24, "Kalashnikov Rifle 7,62mm", 355, 30, TypeSize.Big, 1, 40, 1.5f);
        public static Item M4 = new Item(25, "AR-15 5,56mm", 356, 31, TypeSize.Big, 1, 40, 4.8f);
        public static Item Tec9 = new Item(26, "TEC-DC9 9mm", 372, 32, TypeSize.Big, 1, 15, 1.3f);
        public static Item Rifle = new Item(27, "ZKB 680 C3-100 16,5mm", 357, 33, TypeSize.Big, 1, 4, 40.0f);
        public static Item Sniper = new Item(28, "CZ Slavia 631 4,5mm", 358, 34, TypeSize.Big, 1, 6, 80.0f);
        public static Item Aerosol = new Item(29, "Aerosol de pintura", 365, 41, TypeSize.Small, 1, 1, 0f);
        public static Item Extintor = new Item(30, "Extintor de fuego", 366, 42, TypeSize.Medium, 1, 1, 0f);
        public static Item Camara = new Item(31, "Camara T50", 367, 43, TypeSize.Small, 1, 1, 0f);
        public static Item Paracaidas = new Item(32, "Paracaidas", 371, 46, TypeSize.Medium, 1, 1, 0f);
        public static Item Taser = new Item(33, "Taser", 347, 23, TypeSize.Medium, 1, 1, 0f);

        public static Item Samsung = new Item(34, "Samsung", 18865, 0, TypeSize.Small, 1, 1, 0f);
        public static Item Reloj = new Item(35, "Reloj", 19039, 0, TypeSize.Small, 1, 1, 0f);
        public static Item Iphone = new Item(36, "Iphone", 18865, 0, TypeSize.Small, 1, 1, 0);

        public static Item GetItem(int id)
        {
            if (Vacio.ID == id) return Vacio;
            else if (Nudillera.ID == id) return Nudillera;
            else if (PaloGolf.ID == id) return PaloGolf;
            else if (Porra.ID == id) return Porra;
            else if (Navaja.ID == id) return Navaja;
            else if (Bate.ID == id) return Bate;
            else if (Pala.ID == id) return Pala;
            else if (PaloPool.ID == id) return PaloPool;
            else if (Katana.ID == id) return Katana;
            else if (Motosierra.ID == id) return Motosierra;
            else if (DildoPurpura.ID == id) return DildoPurpura;
            else if (Dildo.ID == id) return Dildo;
            else if (Flores.ID == id) return Flores;
            else if (Granada.ID == id) return Granada;
            else if (Molotov.ID == id) return Molotov;
            else if (Glock17.ID == id) return Glock17;
            else if (Glock17Silenciada.ID == id) return Glock17Silenciada;
            else if (P250.ID == id) return P250;
            else if (Escopeta.ID == id) return Escopeta;
            else if (Lupara.ID == id) return Lupara;
            else if (EscopetaCombate.ID == id) return EscopetaCombate;
            else if (Uzi.ID == id) return Uzi;
            else if (MP5.ID == id) return MP5;
            else if (AK47.ID == id) return AK47;
            else if (M4.ID == id) return M4;
            else if (Tec9.ID == id) return Tec9;
            else if (Rifle.ID == id) return Rifle;
            else if (Sniper.ID == id) return Sniper;
            else if (Aerosol.ID == id) return Aerosol;
            else if (Extintor.ID == id) return Extintor;
            else if (Camara.ID == id) return Camara;
            else if (Paracaidas.ID == id) return Paracaidas;
            else if (Samsung.ID == id) return Samsung;
            else if (Reloj.ID == id) return Reloj;
            else if (Iphone.ID == id) return Iphone;
            else return Vacio;
        }

        public static Item GetItem(string name)
        {
            if (Vacio.Name == name) return Vacio;
            else if (Nudillera.Name == name) return Nudillera;
            else if (PaloGolf.Name == name) return PaloGolf;
            else if (Porra.Name == name) return Porra;
            else if (Navaja.Name == name) return Navaja;
            else if (Bate.Name == name) return Bate;
            else if (Pala.Name == name) return Pala;
            else if (PaloPool.Name == name) return PaloPool;
            else if (Katana.Name == name) return Katana;
            else if (Motosierra.Name == name) return Motosierra;
            else if (DildoPurpura.Name == name) return DildoPurpura;
            else if (Dildo.Name == name) return Dildo;
            else if (Flores.Name == name) return Flores;
            else if (Granada.Name == name) return Granada;
            else if (Molotov.Name == name) return Molotov;
            else if (Glock17.Name == name) return Glock17;
            else if (Glock17Silenciada.Name == name) return Glock17Silenciada;
            else if (P250.Name == name) return P250;
            else if (Escopeta.Name == name) return Escopeta;
            else if (Lupara.Name == name) return Lupara;
            else if (EscopetaCombate.Name == name) return EscopetaCombate;
            else if (Uzi.Name == name) return Uzi;
            else if (MP5.Name == name) return MP5;
            else if (AK47.Name == name) return AK47;
            else if (M4.Name == name) return M4;
            else if (Tec9.Name == name) return Tec9;
            else if (Rifle.Name == name) return Rifle;
            else if (Sniper.Name == name) return Sniper;
            else if (Aerosol.Name == name) return Aerosol;
            else if (Extintor.Name == name) return Extintor;
            else if (Camara.Name == name) return Camara;
            else if (Paracaidas.Name == name) return Paracaidas;

            else if (Samsung.Name == name) return Samsung;
            else if (Reloj.Name == name) return Reloj;
            else if (Iphone.Name == name) return Iphone;
            else return Vacio;
        }

        public static Item GetItemByGun(int idgun)
        {
            if (Vacio.IDGun == idgun) return Vacio;
            else if (Nudillera.IDGun == idgun) return Nudillera;
            else if (PaloGolf.IDGun == idgun) return PaloGolf;
            else if (Porra.IDGun == idgun) return Porra;
            else if (Bate.IDGun == idgun) return Bate;
            else if (Pala.IDGun == idgun) return Pala;
            else if (PaloPool.IDGun == idgun) return PaloPool;
            else if (Katana.IDGun == idgun) return Katana;
            else if (Motosierra.IDGun == idgun) return Motosierra;
            else if (DildoPurpura.IDGun == idgun) return DildoPurpura;
            else if (Dildo.IDGun == idgun) return Dildo;
            else if (Flores.IDGun == idgun) return Flores;
            else if (Granada.IDGun == idgun) return Granada;
            else if (Molotov.IDGun == idgun) return Molotov;
            else if (Glock17.IDGun == idgun) return Glock17;
            else if (Glock17Silenciada.IDGun == idgun) return Glock17Silenciada;
            else if (P250.IDGun == idgun) return P250;
            else if (Escopeta.IDGun == idgun) return Escopeta;
            else if (Lupara.IDGun == idgun) return Lupara;
            else if (EscopetaCombate.IDGun == idgun) return EscopetaCombate;
            else if (Uzi.IDGun == idgun) return Uzi;
            else if (MP5.IDGun == idgun) return MP5;
            else if (AK47.IDGun == idgun) return AK47;
            else if (M4.IDGun == idgun) return M4;
            else if (Tec9.IDGun == idgun) return Tec9;
            else if (Rifle.IDGun == idgun) return Rifle;
            else if (Sniper.IDGun == idgun) return Sniper;
            else if (Aerosol.IDGun == idgun) return Aerosol;
            else if (Extintor.IDGun == idgun) return Extintor;
            else if (Camara.IDGun == idgun) return Camara;
            else if (Paracaidas.IDGun == idgun) return Paracaidas;
            else return Vacio;
        }
    }

    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Model { get; set; }
        public int IDGun { get; set; }
        public TypeSize Size { get; set; }
        public int DefaultAmount { get; set; }
        public int MaxAmount { get; set; }
        public float BonusDamage { get; set; }

        public Item(int id, string name, int model, int idgun, TypeSize saved, int defaultamount, int maxamount, float bonus)
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
