using SampSharp.GameMode.Display;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.SAMP.Commands;
using WashingtonRP.Structures;

namespace WashingtonRP.Commands
{
    public class PlayerCommands
    {
        [Command("health")]
        public static void SayCommand(PlayerData player, PlayerData to_player, float health)
        {
            to_player.Health = health;

            player.SendClientMessage($"{player.pName} le seteo la vida a ${to_player.pName} en {health}");
        }

        [Command("inv", "inventario", "bag", "mochila")]
        public static void InventoryCommand(PlayerData player)
        {
            var dialog = new ListDialog("Inventario", "Cerrar");
            dialog.AddItem($"(ID: {player.Inventory.Slot1.ID}) {player.Inventory.Slot1.Name}");
            dialog.AddItem($"(ID: {player.Inventory.Slot2.ID}) {player.Inventory.Slot2.Name}");
            dialog.AddItem($"(ID: {player.Inventory.Slot3.ID}) {player.Inventory.Slot3.Name}");
            dialog.AddItem($"(ID: {player.Inventory.Slot4.ID}) {player.Inventory.Slot4.Name}");
            dialog.AddItem($"(ID: {player.Inventory.Slot5.ID}) {player.Inventory.Slot5.Name}");
            dialog.Show(player);
        }
    }
}
