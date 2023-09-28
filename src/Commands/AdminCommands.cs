using SampSharp.GameMode.SAMP.Commands;
using WashingtonRP.Modules.Permissions;
using WashingtonRP.Modules.Utils;
using WashingtonRP.Structures;
using SampSharp.GameMode;

namespace WashingtonRP.Commands
{
    public class AdminCommands
    {
        [Command("tp", PermissionChecker = typeof(ModeratorChecker))]
        public static void TPCommand(Player player, Player to_player, int x, int y, int z)
        {
            to_player.Position = new Vector3(x, y, z);
            to_player.SendClientMessage("Fuiste tpeado.");
        }

        [Command("revivir", PermissionChecker = typeof(ModeratorChecker))]
        public static void ToReviveCommand(Player player, Player to_player)
        {
            to_player.Position = to_player.pPositions;
            to_player.Rotation = new Vector3(to_player.pRotation);
            to_player.Interior = to_player.pInterior;
            to_player.VirtualWorld = to_player.pVirtualWorld;
            to_player.Health = 100.0f;
            to_player.pCrack = false;
            to_player.ClearAnimations();
            to_player.ToggleControllable(true);

            to_player.SendClientMessage("Te han revivido gay.");
        }

        [Command("limpiarmanos", PermissionChecker = typeof(ModeratorChecker))]
        public static void CleanHandsCommand(Player player, Player to_player)
        {
            to_player.Hand[0].Item = Items.Vacio;
            to_player.Hand[0].Amount = 0;

            to_player.Hand[1].Item = Items.Vacio;
            to_player.Hand[1].Amount = 0;

            to_player.RemoveAttachedObject(7);
            to_player.RemoveAttachedObject(8);

            to_player.ResetWeapons();

            player.SendClientMessage($"Manos de {to_player.pName} limpias.");
        }

        [Command("dameadmin")]
        public static void AdminCommand(Player player)
        {
            player.pAdmin = Admin.Manager;
        }

        [Command("additem", PermissionChecker = typeof(ModeratorChecker))]
        public static void AddItemCommand(Player player, int id, int amount)
        {
            if (id == 0)
            {
                player.SendClientMessage("* Este item no existe");
                return;
            }

            Item item = Items.GetItem(id);

            if (item == Items.Vacio)
            {
                player.SendClientMessage("* Este item no existe");
                return;
            }

            if (item.MaxAmount < amount)
            {
                player.SendClientMessage($"* La cantidad que quieres agregarte es mayor a el maximo permitido ({item.MaxAmount})");
                return;
            }

            if (player.Hand[0].Item != Items.Vacio && player.Hand[1].Item != Items.Vacio)
            {
                player.SendClientMessage("* Tienes ambas manos ocupadas");
                return;
            }

            if (player.Hand[0].Item == Items.Vacio)
            {
                if(item.IDGun > 0)
                {
                    if (amount > 0)
                    {
                        player.GiveWeapon(Objects.GetWeapon(item.IDGun), amount);

                        player.Hand[0].Item = item;
                        player.Hand[0].Amount = amount;
                        return;
                    }
                }

                player.Hand[0].Item = item;
                player.Hand[0].Amount = amount;

                Objects.PutObject(player, item, 1);
            } 
            else if (player.Hand[1].Item == Items.Vacio)
            {
                player.Hand[1].Item = item;
                player.Hand[1].Amount = amount;

                Objects.PutObject(player, item, 2);
            }
        }
    }
}
