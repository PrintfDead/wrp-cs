using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP.Commands;
using WashingtonRP.Structures;
using SampSharp.GameMode.SAMP;
using WashingtonRP.Modules;
using WashingtonRP.Modules.Utils;
using SampSharp.GameMode.World;
using System.Collections.Generic;
using System;
using static System.Net.Mime.MediaTypeNames;

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
            int count = 0;
            player.Inventory.Slot.ForEach(x => 
            {
                count++;
                if (count > 5)
                {
                    Console.WriteLine("Ocurrio un error hay mas de 5 slots");
                }
                dialog.AddItem($"{Color.White}(ID: {x.Item.ID}) {color}{x.Item.Name}");
            });
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
            if (player.Hand[0].Item == Items.Vacio)
            {
                player.SendClientMessage("* No tienes ningun objeto en tu mano derecha");
                return;
            }

            if (player.InAnyVehicle)
            {
                player.SendClientMessage("* No puedes hacer esto dentro de un vehiculo");
            }

            var label = new TextLabel($"{Color.White}({player.Hand[0].Item.ID}) {Color.Purple}{player.Hand[0].Amount} {Color.Yellow}{player.Hand[0].Item.Name}", Color.White, new SampSharp.GameMode.Vector3(player.Position.X, player.Position.Y, player.Position.Z+1.5), 5.0f);
            label.Position = player.Position;
            label.VirtualWorld = player.VirtualWorld;

            Floor.Add(new Structures.Floor
            {
                Item = player.Hand[0].Item,
                ItemAmount = player.Hand[0].Amount,
                Position = player.Position,
                Interior = player.Interior,
                VirtualWorld = player.VirtualWorld,
                Label = label
            });

            Message.PutChatAme(player, $"solto {player.Hand[0].Item.Name} en el suelo");

            player.Hand[0].Item = Items.Vacio;
            player.Hand[0].Amount = 0;
            player.RemoveAttachedObject(7);
        }

        [Command("agarrar", "levantar", "recoger")]
        public static void TakeItemCommand(Player player)
        {
            if (player.Hand[0].Item != Items.Vacio)
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

            player.Hand[0].Item = objeto.Item;
            player.Hand[0].Amount = objeto.ItemAmount;
            Objects.PutObject(player, objeto.Item, 1);

            objeto.Label.Dispose();
            Floor.Remove(objeto);

            Message.PutChatAme(player, $"recogio {player.Hand[0].Item.Name} del suelo");
        }

        [Command("mano", "cambiarmano")]
        public static void ChangeHandCommand(Player player)
        {
            if (player.Hand[0].Item == Items.Vacio && player.Hand[1].Item == Items.Vacio)
            {
                player.SendClientMessage("* No tienes ningun objeto en tus manos");
                return;
            }

            if (player.Hand[0].Item == Items.Vacio)
            {
                player.Hand[0].Item = player.Hand[1].Item;
                player.Hand[0].Amount = player.Hand[1].Amount;
                player.Hand[1].Item = Items.Vacio;
                player.Hand[1].Amount = 0;
                player.RemoveAttachedObject(7);
                player.RemoveAttachedObject(8);

                if (player.Hand[0].Item.IDGun > 0)
                {
                    if (player.Hand[0].Amount > 0)
                    {
                        player.GiveWeapon(Objects.GetWeapon(player.Hand[0].Item.IDGun), player.Hand[0].Amount);
                    }
                }

                Message.PutChatAme(player, $"cambia {player.Hand[0].Item.Name} de mano");

                Objects.PutObject(player, player.Hand[0].Item, 1);
            } 
            else if (player.Hand[1].Item == Items.Vacio)
            {
                player.Hand[1].Item = player.Hand[0].Item;
                player.Hand[1].Amount = player.Hand[0].Amount;
                player.Hand[0].Item = Items.Vacio;
                player.Hand[0].Amount = 0;
                player.ResetWeapons();
                player.RemoveAttachedObject(7);
                player.RemoveAttachedObject(8);

                Message.PutChatAme(player, $"cambia {player.Hand[1].Item.Name} de mano");

                Objects.PutObject(player, player.Hand[1].Item, 2);
            }
            else if (player.Hand[1].Item != Items.Vacio && player.Hand[0].Item != Items.Vacio)
            {
                Objects.PutObject(player, player.Hand[0].Item, 2);
                Objects.PutObject(player, player.Hand[1].Item, 1);

                var mano1 = player.Hand[0].Item;
                var mano2 = player.Hand[1].Item;
                int amount1 = player.Hand[0].Amount;
                int amount2 = player.Hand[1].Amount;

                player.Hand[0].Item = mano2;
                player.Hand[0].Amount = amount2;
                player.Hand[1].Item = mano1;
                player.Hand[1].Amount = amount1;

                Message.PutChatAme(player, $"cambia {player.Hand[1].Item.Name} de mano");
            }
            else
            {
                player.SendClientMessage("* Ocurrio un error, comunicate con @justprintf. Se expecifico.");
                return;
            }
        }

        [Command("b", "ooc")]
        public static void OccCommand(Player player, string text)
        {
            Color color = new Color(200, 200, 255);
            Color colorAdmin = new Color(173, 22, 31);

            var admin = -1;

            switch (player.pAdmin)
            {
                case Admin.None:
                    admin = -1;
                    break;
                case Admin.Helper:
                    admin = 0;
                    break;
                case Admin.Moderator:
                    admin = 1;
                    break;
                case Admin.Admin:
                    admin = 2;
                    break;
                case Admin.SeniorAdmin:
                    admin = 3;
                    break;
                case Admin.Manager:
                    admin = 4;
                    break;
            }

            if (admin >= 0)
            {
                Message.SendMessageProx(25.0f, player, $"* (OOC {colorAdmin}{Characters.GetAdminName(player.pAdmin)}{color} | {player.Id}) {player.aName}: {text}", color, color, color, color, color);
                return;
            } else
            {
                Message.SendMessageProx(25.0f, player, $"* (OOC | {player.Id}) {player.aName}: {text}", color, color, color, color, color);
                return;
            }
        }

        [Command("do")]
        public static void DoCommand(Player player, string text)
        {
            Color color = new Color(204, 255, 170);

            Message.SendMessageProx(30.0f, player, $"* {Characters.GetNameCharacter(player)} (( {text} ))", color, color, color, color, color);
        }

        [Command("ame")]
        public static void AmeCommand(Player player, string text)
        {
            if (text.Length > 65)
            {
                player.SendClientMessage("* No puede contener mas de 65 caracteres.");
                return;
            }

            Message.PutChatAme(player, text);
        }

        [Command("me")]
        public static void MeCommand(Player player, string text)
        {
            Color color = new Color(208, 174, 235);

            Message.SendMessageProx(20.0f, player, $"* {Characters.GetNameCharacter(player)} {text}",color, color, color, color, color);
        }

        [Command("guardar")]
        public static void SaveInventoryCommand(Player player)
        {
            if (player.pCrack)
            {
                player.SendClientMessage("* No puedes ver el inventario en este estado");
                return;
            }

            if (player.Hand[0].Item == Items.Vacio)
            {
                player.SendClientMessage("* No tienes ningun objeto en tu mano derecha");
                return;
            }

            if (/*player.RightHand.Size == TypeSize.Medium ||*/ player.Hand[0].Item.Size == TypeSize.Big)
            {
                player.SendClientMessage("* Este objeto es demsiado grande para guardarlo en el inventario");
                return;
            }

            int count = 0;
            bool save = false;

            player.Inventory.Slot.ForEach(x =>
            {
                if (count >= 5)
                {
                    player.SendClientMessage("* No tienes espacio en el inventario");
                    Message.PutChatAme(player, $"intento guardar {player.Hand[0].Item.Name} pero no lo logro");
                    return;
                }

                if (save) return;

                if (x.Item == Items.Vacio)
                {
                    x.Item = player.Hand[0].Item;
                    x.Amount = player.Hand[0].Amount;

                    Message.PutChatAme(player, $"guardo {player.Hand[0].Item.Name} en sus bolsillos");
                    player.RemoveAttachedObject(7);

                    if (player.Hand[0].Item.IDGun > 0)
                    {
                        player.ResetWeapons();
                    }

                    player.Hand[0].Item = Items.Vacio;
                    player.Hand[0].Amount = 0;

                    save = true;

                    return;
                }
                else count++;
            });
        }
    }
}
