using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP.Commands;
using WashingtonRP.Structures;
using SampSharp.GameMode.SAMP;
using WashingtonRP.Modules;
using WashingtonRP.Modules.Utils;
using SampSharp.Streamer.Entities;
using SampSharp.GameMode.World;
using System.Collections.Generic;

namespace WashingtonRP.Commands
{
    public class PlayerCommands
    {
        public static List<Floor> Floor { get; set; } = new List<Floor>();

        [Command("health")]
        public static void SayCommand(Player player, Player to_player, float health)
        {
            to_player.Health = health;

            player.SendClientMessage($"{player.pName} le seteo la vida a ${to_player.pName} en {health}");
        }

        [Command("inv", "inventario", "bag", "mochila")]
        public static void InventoryCommand(Player player)
        {
            if (player.pCrack)
            {
                player.SendClientMessage("* No puedes ver el inventario en este estado");
                return;
            }

            var color = new Color(255, 240, 149);

            var dialog = new ListDialog("Inventario", "Sacar", "Cerrar");
            dialog.AddItem($"{Color.White}(ID: {player.Inventory.Slot1.ID}) {color}{player.Inventory.Slot1.Name}");
            dialog.AddItem($"{Color.White}(ID: {player.Inventory.Slot2.ID}) {color}{player.Inventory.Slot2.Name}");
            dialog.AddItem($"{Color.White}(ID: {player.Inventory.Slot3.ID}) {color}{player.Inventory.Slot3.Name}");
            dialog.AddItem($"{Color.White}(ID: {player.Inventory.Slot4.ID}) {color}{player.Inventory.Slot4.Name}");
            dialog.AddItem($"{Color.White}(ID: {player.Inventory.Slot5.ID}) {color}{player.Inventory.Slot5.Name}");
            dialog.Response += Dialogs.TakeInventoryDialog_Response;
            dialog.Show(player);
        }

        [Command("sacar")]
        public static void TakeInventoryCommand(Player player, int pos)
        {
            if (player.pCrack)
            {
                player.SendClientMessage("* No puedes ver el inventario en este estado");
                return;
            }

            if (pos > 5 || pos < 0)
            {
                player.SendClientMessage("* Este slot no existe");
            }

            Objects.TakeItem(player, pos);
        }

        [Command("soltar", "tirar")]
        public static void DropObjectCommand(Player player)
        {
            if (player.RightHand == Items.Vacio)
            {
                player.SendClientMessage("* No tienes ningun objeto en tu mano derecha");
                return;
            }

            if (player.InAnyVehicle)
            {
                player.SendClientMessage("* No puedes hacer esto dentro de un vehiculo");
            }

            var label = new TextLabel($"{Color.White}({player.RightHand.ID}) {Color.Purple}{player.RightHandAmount} {Color.Yellow}{player.RightHand.Name}", Color.White, new SampSharp.GameMode.Vector3(), 5.0f);

            Floor.Add(new Structures.Floor
            {
                Item = player.RightHand,
                ItemAmount = player.RightHandAmount,
                Position = player.Position,
                Interior = player.Interior,
                VirtualWorld = player.VirtualWorld,
                Label = label
            });

            Message.PutChatAme(player, $"solto {player.RightHand.Name} en el suelo");

            player.RightHand = Items.Vacio;
            player.RightHandAmount = 0;
            player.RemoveAttachedObject(7);
        }

        [Command("agarrar", "levantar", "recoger")]
        public static void TakeItemCommand(Player player)
        {
            if (player.RightHand != Items.Vacio)
            {
                player.SendClientMessage("* Tienes que tener la mano derecha vacia.");
                return;
            }

            Floor objeto = null;

            Floor.ForEach((x) =>
            {
                if (player.IsInRangeOfPoint(3.2f, x.Position))
                {
                    objeto = x;
                }
            });

            if (objeto == null)
            {
                player.SendClientMessage("* No estas cerca de ningun objeto");
                return;
            }

            player.RightHand = objeto.Item;
            player.RightHandAmount = objeto.ItemAmount;
            Objects.PutObject(player, objeto.Item, 1);

            objeto.Label.Dispose();
            Floor.Remove(objeto);

            Message.PutChatAme(player, $"recogio {player.RightHand.Name} del suelo");
        }

        [Command("mano", "cambiarmano")]
        public static void ChangeHandCommand(Player player)
        {
            if (player.RightHand == Items.Vacio && player.LeftHand == Items.Vacio)
            {
                player.SendClientMessage("* No tienes ningun objeto en tus manos");
                return;
            }

            if (player.RightHand == Items.Vacio)
            {
                player.RightHand = player.LeftHand;
                player.RightHandAmount = player.LeftHandAmount;
                player.LeftHand = Items.Vacio;
                player.LeftHandAmount = 0;
                player.RemoveAttachedObject(7);
                player.RemoveAttachedObject(8);

                if (player.RightHand.IDGun > 0)
                {
                    if (player.RightHandAmount > 0)
                    {
                        player.GiveWeapon(Objects.GetWeapon(player.RightHand.IDGun), player.RightHandAmount);
                    }
                }

                Message.PutChatAme(player, $"cambia {player.RightHand.Name} de mano");

                Objects.PutObject(player, player.RightHand, 1);
            } 
            else if (player.LeftHand == Items.Vacio)
            {
                player.LeftHand = player.RightHand;
                player.LeftHandAmount = player.RightHandAmount;
                player.RightHand = Items.Vacio;
                player.RightHandAmount = 0;
                player.ResetWeapons();
                player.RemoveAttachedObject(7);
                player.RemoveAttachedObject(8);

                Message.PutChatAme(player, $"cambia {player.LeftHand.Name} de mano");

                Objects.PutObject(player, player.LeftHand, 2);
            }
            else if (player.LeftHand != Items.Vacio && player.RightHand != Items.Vacio)
            {
                Objects.PutObject(player, player.RightHand, 2);
                Objects.PutObject(player, player.LeftHand, 1);

                var mano1 = player.RightHand;
                var mano2 = player.LeftHand;
                int amount1 = player.RightHandAmount;
                int amount2 = player.LeftHandAmount;

                player.RightHand = mano2;
                player.RightHandAmount = amount2;
                player.LeftHand = mano1;
                player.LeftHandAmount = amount1;

                Message.PutChatAme(player, $"cambia {player.LeftHand.Name} de mano");
            }
            else
            {
                player.SendClientMessage("* Ocurrio un error, comunicate con @justprintf. Se expecifico.");
                return;
            }
        }

        [Command("guardar")]
        public static void SaveInventoryCommand(Player player)
        {
            if (player.pCrack)
            {
                player.SendClientMessage("* No puedes ver el inventario en este estado");
                return;
            }

            if (player.RightHand == Items.Vacio)
            {
                player.SendClientMessage("* No tienes ningun objeto en tu mano derecha");
                return;
            }

            if (/*player.RightHand.Size == TypeSize.Medium ||*/ player.RightHand.Size == TypeSize.Big)
            {
                player.SendClientMessage("* Este objeto es demsiado grande para guardarlo en el inventario");
                return;
            }

            if (player.Inventory.Slot1 == Items.Vacio)
            {
                player.Inventory.Slot1 = player.RightHand;
                player.Inventory.SlotAmount1 = player.RightHandAmount;
                

                Message.PutChatAme(player, $"guardo {player.RightHand.Name} en sus bolsillos");
                player.RemoveAttachedObject(7);

                if (player.RightHand.IDGun > 0)
                {
                    player.ResetWeapons();
                }

                player.RightHand = Items.Vacio;
                player.RightHandAmount = 0;
            } 
            else if (player.Inventory.Slot2 == Items.Vacio)
            {
                player.Inventory.Slot2 = player.RightHand;
                player.Inventory.SlotAmount2 = player.RightHandAmount;

                Message.PutChatAme(player, $"guardo {player.RightHand.Name} en sus bolsillos");
                player.RemoveAttachedObject(7);

                if (player.RightHand.IDGun > 0)
                {
                    player.ResetWeapons();
                }

                player.RightHand = Items.Vacio;
                player.RightHandAmount = 0;
            } 
            else if (player.Inventory.Slot3 == Items.Vacio)
            {
                player.Inventory.Slot3 = player.RightHand;
                player.Inventory.SlotAmount3 = player.RightHandAmount;

                Message.PutChatAme(player, $"guardo {player.RightHand.Name} en sus bolsillos");
                player.RemoveAttachedObject(7);

                if (player.RightHand.IDGun > 0)
                {
                    player.ResetWeapons();
                }

                player.RightHand = Items.Vacio;
                player.RightHandAmount = 0;
            }
            else if (player.Inventory.Slot4 == Items.Vacio)
            {
                player.Inventory.Slot4 = player.RightHand;
                player.Inventory.SlotAmount4 = player.RightHandAmount;

                Message.PutChatAme(player, $"guardo {player.RightHand.Name} en sus bolsillos");
                player.RemoveAttachedObject(7);

                if (player.RightHand.IDGun > 0)
                {
                    player.ResetWeapons();
                }

                player.RightHand = Items.Vacio;
                player.RightHandAmount = 0;
            }
            else if (player.Inventory.Slot5 == Items.Vacio)
            {
                player.Inventory.Slot5 = player.RightHand;
                player.Inventory.SlotAmount5 = player.RightHandAmount;

                Message.PutChatAme(player, $"guardo {player.RightHand.Name} en sus bolsillos");
                player.RemoveAttachedObject(7);

                if (player.RightHand.IDGun > 0)
                {
                    player.ResetWeapons();
                }

                player.RightHand = Items.Vacio;
                player.RightHandAmount = 0;
            }
            else
            {
                player.SendClientMessage("* No tienes espacio en tus bolsillos");
                Message.PutChatAme(player, $"intento guardar {player.RightHand.Name} pero no lo logro");
                return;
            }
        }
    }
}
