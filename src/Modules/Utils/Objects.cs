using Microsoft.EntityFrameworkCore;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using System;
using System.Linq;
using WashingtonRP.Structures;

namespace WashingtonRP.Modules.Utils
{
    public class Objects
    {
        public static void TakeItem(Player player, int id)
        {
            Item Slot = player.Inventory.GetSlot(id).Item;
            int SlotAmount = player.Inventory.GetSlot(id).Amount;

            if (Slot == Items.Vacio)
            {
                player.SendClientMessage("* Este slot se encuentra vacio");
                return;
            }

            if (player.Hand[0].Item == Items.Vacio)
            {
                Console.WriteLine($"{Slot.Name}");
                if (Slot.IDGun > 0)
                {
                    if (SlotAmount > 0)
                    {
                        player.GiveWeapon(GetWeapon(Slot.IDGun), SlotAmount);
                    }
                }
                player.Hand[0].Item = Slot;
                player.Hand[0].Amount = SlotAmount;

                PutObject(player, player.Hand[0].Item, 1);

                Console.WriteLine("Colocando y sacando item del inventario");

                player.Inventory.TakeSlot(id);
                Message.PutChatAme(player, $"saco {player.Hand[0].Item.Name} de sus bolsillos");
            }
            else if (player.Hand[1].Item == Items.Vacio)
            {
                Console.WriteLine("Buscando item");
                player.Hand[1].Item = Slot;
                player.Hand[1].Amount = SlotAmount;

                PutObject(player, player.Hand[1].Item, 2);

                Console.WriteLine("Colocando y sacando item del inventario");

                player.Inventory.TakeSlot(id);

                Message.PutChatAme(player, $"saco {player.Hand[1].Item.Name} de sus bolsillos");
            }
            else
            {
                player.SendClientMessage("* No tienes ninguna mano vacia");
            }
        }

        public static void PutObject(Player player, Item item, int place)
        {
            switch (place)
            {
                case 1: // mano derecha
                    switch (item.ID)
                    {
                        case int n when n >= 1 && n <= 33:
                            player.SetAttachedObject(7, item.Model, Bone.RightHand, new Vector3(), new Vector3(), new Vector3(), 0, 0); // Weapons
                            break;
                        case 34:
                            player.SetAttachedObject(7, item.Model, Bone.RightHand, new Vector3(0.093999, 0.012000, -0.010999), new Vector3(-81.199974, 10.000000, 179.099945), new Vector3(0.990999, 0.829000, 0.903000), 0, 0);
                            break;
                        case 35:
                            player.SetAttachedObject(7, item.Model, Bone.RightHand, new Vector3(0.093999, 0.012000, -0.010999), new Vector3(-81.199974, 10.000000, 179.099945), new Vector3(0.990999, 0.829000, 0.903000), 0, 0);
                            break;
                        case 36:
                            player.SetAttachedObject(7, item.Model, Bone.RightHand, new Vector3(0.093999, 0.012000, -0.010999), new Vector3(-81.199974, 10.000000, 179.099945), new Vector3(0.990999, 0.829000, 0.903000), 0, 0);
                            break;
                    }
                    break;
                case 2: // mano izquierda
                    switch (item.ID)
                    {
                        case 1:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.024999, 0.000000, -0.005999), new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 2:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(-0.040000, 0.067000, -0.003000), new Vector3(-29.899999, 152.000000, 176.000030), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 3:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(-0.115999, 0.113999, 0.087999), new Vector3(0.000000, 44.500000, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 4:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(-0.007000, 0.060999, -0.013000), new Vector3(156.600051, 9.100000, -14.800011), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 5:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.135999, 0.030999, 0.053999), new Vector3(-17.900001, 153.800018, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 6:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.041999, 0.051999, -0.080000), new Vector3(-27.899986, 155.800018, -167.499938), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 7:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.156999, 0.092000, 0.071999), new Vector3(-25.099998, 172.899963, -19.899995), new Vector3(1f, 1f, 1f), 0, 0);
                            break; 
                        case 8:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.058000, 0.008000, -0.109000), new Vector3(-27.999998, 155.100036, 172.699966), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 9:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.000000, 0.078999, -0.009000), new Vector3(155.599990, 7.200000, 4.800000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 10:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.015000, 0.041000, -0.068000), new Vector3(-25.599971, 162.900054, -175.300018), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 11:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.023999, 0.052999, -0.041999), new Vector3(157.899978, 2.499999, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 12:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.176000, 0.000000, -0.011000), new Vector3(-23.399999, 160.599990, -6.399995), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 13:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.019000, 0.047000, -0.021000), new Vector3(164.499984, 0.000000, -17.599994), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 14:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.034000, 0.063000, -0.037999), new Vector3(164.500000, 14.599996, -4.700009), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 15:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.022999, 0.039000, -0.125999), new Vector3(165.700012, 0.000000, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 16:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.033000, 0.057999, -0.017999), new Vector3(146.299987, 14.999998, -4.700002), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 17:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.050999, 0.038000, -0.021999), new Vector3(144.500015, 9.100000, -11.600002), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 18:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.041000, 0.038000, -0.026999), new Vector3(173.899963, 0.000000, 1.799999), new Vector3(1f, 1f, 1f), 0, 0 );
                            break;
                        case 19:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.000000, 0.089000, 0.000000), new Vector3(153.500015, 10.000001, 4.800000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 20:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.040000, 0.026000, 0.011000), new Vector3(-156.300003, 0.000000, 2.600000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 21:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.023000, 0.048999, -0.013000), new Vector3(172.200012, 14.400005, 0.599999), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 22:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.024999, 0.048000, -0.012000), new Vector3(172.000076, -3.099988, -2.099991), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 23:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.005000, 0.065000, -0.016000), new Vector3(162.399963, 9.599999, 8.400000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 24:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.057999, 0.076999, -0.023999), new Vector3(162.799942, 14.399999, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 25:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.043999, 0.028999, -0.038000), new Vector3(167.499969, 15.999998, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 26:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.048000, 0.049999, 0.011000), new Vector3(-172.600006, -2.299995, 5.999999), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 27:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(-0.043000, 0.065999, -0.044999), new Vector3(167.499954, 17.399999, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 28:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(-0.018000, 0.085000, 0.033000), new Vector3(167.399978, 12.200000, -0.299997), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 29:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.036999, 0.026999, -0.079999), new Vector3(152.899993, 14.900004, -0.400000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 30:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.004000, 0.061999, -0.063999), new Vector3(126.599990, 33.099998, 2.200000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 31:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.018000, 0.062999, 0.074999), new Vector3(-109.400039, 0.000000, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 32:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.018000, 0.062999, 0.074999), new Vector3(-109.400039, 0.000000, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break;
                        case 33:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.050999, 0.038000, -0.021999), new Vector3(144.500015, 9.100000, -11.600002), new Vector3(1f, 1f, 1f), 0, 0);
                            break; // Finish Weapon
                        case 34:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.056000, 0.000000, 0.007000), new Vector3(-97.100006, 0.000000, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break; // Samsung
                        case 35:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.056000, 0.000000, 0.007000), new Vector3(-97.100006, 0.000000, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break; // Reloj
                        case 36:
                            player.SetAttachedObject(8, item.Model, Bone.LeftHand, new Vector3(0.056000, 0.000000, 0.007000), new Vector3(-97.100006, 0.000000, 0.000000), new Vector3(1f, 1f, 1f), 0, 0);
                            break; // Iphone

                    }
                    break;
            }
        }

        public static int GetWeapon(Weapon id)
        {
            switch (id)
            {
                case Weapon.Brassknuckle:
                    return 1;
                case Weapon.Golfclub:
                    return 2;
                case Weapon.Nitestick:
                    return 3;
                case Weapon.Knife:
                    return 4;
                case Weapon.Bat:
                    return 5;
                case Weapon.Shovel:
                    return 6;
                case Weapon.Poolstick:
                    return 7;
                case Weapon.Katana:
                    return 8;
                case Weapon.Chainsaw:
                    return 9;
                case Weapon.Dildo:
                    return 11;
                case Weapon.Vibrator:
                    return 12;
                case Weapon.SilverVibrator:
                    return 13;
                case Weapon.Flower:
                    return 14;
                case Weapon.Cane:
                    return 15;
                case Weapon.Grenade:
                    return 16;
                case Weapon.Teargas:
                    return 17;
                case Weapon.Moltov:
                    return 18;
                case Weapon.Colt45:
                    return 22;
                case Weapon.Silenced:
                    return 23;
                case Weapon.Deagle:
                    return 24;
                case Weapon.Shotgun:
                    return 25;
                case Weapon.Sawedoff:
                    return 26;
                case Weapon.CombatShotgun:
                    return 27;
                case Weapon.Uzi:
                    return 28;
                case Weapon.MP5:
                    return 29;
                case Weapon.AK47:
                    return 30;
                case Weapon.M4:
                    return 31;
                case Weapon.Tec9:
                    return 32;
                case Weapon.Rifle:
                    return 33;
                case Weapon.Sniper:
                    return 34;
                case Weapon.Spraycan:
                    return 41;
                case Weapon.FireExtinguisher:
                    return 42;
                case Weapon.Parachute:
                    return 43;
                case Weapon.Camera:
                    return 44;
                default:
                    return 0;
            }
        }

        public static void CleanHands(Player player)
        {
            WashingtonContext context = new WashingtonContext(new DbContextOptions<WashingtonContext>());

            var character = context.Characters
                .Where(x => x.ID == player.pID)
                .ToList();

            player.Hand[0].Item = Items.Vacio;
            player.Hand[0].Amount = 0;

            player.Hand[1].Item = Items.Vacio;
            player.Hand[1].Amount = 0;

            player.RemoveAttachedObject(7);
            player.RemoveAttachedObject(8);

            player.ResetWeapons();

            foreach (var x in character)
            {          
                x.RightHand = player.Hand[0].Item.ID;
                x.RightHandAmount = player.Hand[0].Amount;
                x.LeftHand = player.Hand[1].Item.ID;
                x.LeftHandAmount = player.Hand[1].Amount;
            }

            context.SaveChanges();
        }

        public static Weapon GetWeapon(int id)
        {
            switch (id)
            {
                case 0:
                    return Weapon.None;
                case 1:
                    return Weapon.Brassknuckle;
                case 2:
                    return Weapon.Golfclub;
                case 3:
                    return Weapon.Nitestick;
                case 4:
                    return Weapon.Knife;
                case 5:
                    return Weapon.Bat;
                case 6:
                    return Weapon.Shovel;
                case 7:
                    return Weapon.Poolstick;
                case 8:
                    return Weapon.Katana;
                case 9:
                    return Weapon.Chainsaw;
                case 11:
                    return Weapon.Dildo;
                case 12:
                    return Weapon.Vibrator;
                case 13:
                    return Weapon.SilverVibrator;
                case 14:
                    return Weapon.Flower;
                case 15:
                    return Weapon.Cane;
                case 16:
                    return Weapon.Grenade;
                case 17:
                    return Weapon.Teargas;
                case 18:
                    return Weapon.Moltov;
                case 22:
                    return Weapon.Colt45;
                case 23:
                    return Weapon.Silenced;
                case 24:
                    return Weapon.Deagle;
                case 25:
                    return Weapon.Shotgun;
                case 26:
                    return Weapon.Sawedoff;
                case 27:
                    return Weapon.CombatShotgun;
                case 28:
                    return Weapon.Uzi;
                case 29:
                    return Weapon.MP5;
                case 30:
                    return Weapon.AK47;
                case 31:
                    return Weapon.M4;
                case 32:
                    return Weapon.Tec9;
                case 33:
                    return Weapon.Rifle;
                case 34:
                    return Weapon.Sniper;
                case 41:
                    return Weapon.Spraycan;
                case 42:
                    return Weapon.FireExtinguisher;
                case 43:
                    return Weapon.Parachute;
                case 44:
                    return Weapon.Camera;
                default:
                    return Weapon.None;
            }
        }
    }
}
