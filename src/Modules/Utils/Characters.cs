using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SampSharp.Core.Hosting;
using SampSharp.GameMode;
using System;
using System.Collections.Generic;
using System.Linq;
using WashingtonRP.Structures;
using WashingtonRP.Structures.Models;

namespace WashingtonRP.Modules.Utils
{
    public class Characters
    {
        public static List<Structures.Character> GetCharacters(Player player)
        {
            WashingtonContext context = new WashingtonContext(new DbContextOptions<WashingtonContext>());
            var characters = new List<Structures.Character>();
            int count = 0;

            var character = context.Characters
                .Where(x => x.Account == player.aID)
                .Select(x => new { x.ID, x.Name })
                .ToList();

            if (character == null)
            {
                characters.Clear();
                for (int i = 0; i < 3; i++)
                {
                    characters.Add(new Structures.Character
                    {
                        ID = -1,
                        Name = "Crear Personaje"
                    });
                }

                return characters;
            }

            character.ForEach(x =>
            {
                count += 1;

                characters.Add(new Structures.Character
                {
                    ID = x.ID,
                    Name = x.Name
                });
            });

            if (count <= 2)
            {
                for (int i = 0; i < 3 - count; i++)
                {
                    characters.Add(new Structures.Character
                    {
                        ID = -1,
                        Name = "Crear Personaje"
                    });
                }
            }

            return characters;
        }

        public static void LoadCharacter(Player player, int id)
        {
            WashingtonContext context = new WashingtonContext(new DbContextOptions<WashingtonContext>());

            var character = context.Characters
                .Where(x => x.ID == id)
                .Select(x => x).ToList();
            if (character == null)
            {
                player.SendClientMessage("Ocurrio un error contactate con justPrintf. para solucionarlo!");
                player.Kick();
            }

            character.ForEach(x =>
            {
                player.pID = x.ID;
                player.pName = x.Name;
                player.pPositions = new Vector3(x.PositionX, x.PositionY, x.PositionZ);
                player.pRotation = x.Rotation;
                player.pInterior = x.Interior;
                player.pVirtualWorld = x.VirtualWorld;
                player.pHealth = x.Health;
                player.pChaleco = x.Chaleco;
                player.pSkin = x.Skin;
                player.pCrack = x.Crack == 1 ? true : false;

                player.Hand = new List<Hand>
                {
                    new Hand
                    {
                        Position = false,
                        Item = Items.GetItem(x.RightHand),
                        Amount = x.RightHandAmount
                    },

                    new Hand
                    {
                        Position = true,
                        Item = Items.GetItem(x.LeftHand),
                        Amount = x.LeftHandAmount
                    }
                };

                player.Wrist = new List<Wrist>
                {
                    new Wrist
                    {
                        Position = false,
                        Item = Items.GetItem(x.RightWrist),
                        Amount = x.RightWristAmount
                    },

                    new Wrist
                    {
                        Position = true,
                        Item = Items.GetItem(x.LeftWrist),
                        Amount = x.LeftWristAmount
                    }
                };

                switch (x.Admin)
                {
                    case -1:
                        player.pAdmin = Admin.None;
                        break;
                    case 0:
                        player.pAdmin = Admin.Helper;
                        break;
                    case 1:
                        player.pAdmin = Admin.Moderator;
                        break;
                    case 2:
                        player.pAdmin = Admin.Admin;
                        break;
                    case 3:
                        player.pAdmin = Admin.SeniorAdmin;
                        break;
                    case 4:
                        player.pAdmin = Admin.Manager;
                        break;
                }
            });

            var inventory = context.Inventories
                .Where(x => x.Character == player.pID)
                .Select(x => x).ToList();

            if (inventory == null)
            {
                player.SendClientMessage("Ocurrio un error contactate con justPrintf. para solucionarlo!");
                player.Kick();
            }

            inventory.ForEach(x =>
            {
                var list = new List<Slot>
                {
                    new Slot
                    {
                        ID = 1,
                        Item = Items.GetItem(x.Slot1),
                        Amount = x.SlotAmount1
                    },
                    new Slot
                    {
                        ID = 2,
                        Item = Items.GetItem(x.Slot2),
                        Amount = x.SlotAmount2
                    },
                    new Slot
                    {
                        ID = 3,
                        Item = Items.GetItem(x.Slot3),
                        Amount = x.SlotAmount3
                    },
                    new Slot
                    {
                        ID = 4,
                        Item = Items.GetItem(x.Slot4),
                        Amount = x.SlotAmount4
                    },
                    new Slot
                    {
                        ID = 5,
                        Item = Items.GetItem(x.Slot5),
                        Amount = x.SlotAmount5
                    }
                };

                Console.WriteLine(list);

                player.Inventory = new Inventories
                {
                    ID = x.ID,
                    Slot = list
                };
            });

            player.InLogin = false;
            player.Name = player.pName;

            player.Spawn();
        }

        public static void SaveCharacter(Player player)
        {
            WashingtonContext context = new WashingtonContext(new DbContextOptions<WashingtonContext>());

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

            var character = context.Characters
                .Where(x => x.ID == player.pID)
                .ToList();

            foreach(var x in character)
            {
                x.ID = player.pID;
                x.PositionX = (float)player.Position.X;
                x.PositionY = (float)player.Position.Y;
                x.PositionZ = (float)player.Position.Z;
                x.Rotation = (float)player.Rotation.Z;
                x.Interior = player.Interior;
                x.VirtualWorld = player.VirtualWorld;
                x.Health = player.Health;
                x.Chaleco = player.Armour;
                x.Skin = player.Skin;
                x.RightHand = player.Hand[0].Item.ID;
                x.RightHandAmount = player.Hand[0].Amount;
                x.LeftHand = player.Hand[1].Item.ID;
                x.LeftHandAmount = player.Hand[1].Amount;
                x.Crack = player.pCrack == true ? 1 : 0;
                x.Admin = admin;
            }

            context.SaveChanges();


            var inventory = context.Inventories
                .Where(x => x.Character == player.pID)
                .ToList();

            foreach(var x in inventory)
            {
                x.Character = player.pID;
                x.Slot1 = player.Inventory.Slot[0].Item.ID;
                x.SlotAmount1 = player.Inventory.Slot[0].Amount;
                x.Slot2 = player.Inventory.Slot[1].Item.ID;
                x.SlotAmount2 = player.Inventory.Slot[1].Amount;
                x.Slot3 = player.Inventory.Slot[2].Item.ID;
                x.SlotAmount3 = player.Inventory.Slot[2].Amount;
                x.Slot4 = player.Inventory.Slot[3].Item.ID;
                x.SlotAmount4 = player.Inventory.Slot[3].Amount;
                x.Slot5 = player.Inventory.Slot[4].Item.ID;
                x.SlotAmount5 = player.Inventory.Slot[4].Amount;
            }

            context.SaveChanges();
        }

        public static void CreateCharacter(Player player, string name)
        {
            WashingtonContext context = new WashingtonContext(new DbContextOptions<WashingtonContext>());
            var character = new CharacterModel
            {
                Name = name,
                Account = player.aID,
                PositionX = 1284.5775f,
                PositionY = 0f,
                PositionZ = 0f,
                Rotation = 0f,
                Interior = 0,
                VirtualWorld = 0,
                Health = 100.0f,
                Skin = 289,
                RightHand = 14,
                RightHandAmount = 1,
                LeftHand = 0,
                LeftHandAmount = 0,
                RightWrist = 0,
                RightWristAmount = 0,
                LeftWrist = 0,
                LeftWristAmount = 0,
                Crack = 0,
                Admin = 0
            };
            var newcharacter = context.Characters.Add(character);
            context.SaveChanges();

            var inventory = new InventoryModel
            {
                Character = (int)newcharacter.GetDatabaseValues()["ID"],
                Slot1 = 0,
                SlotAmount1 = 0,
                Slot2 = 0,
                SlotAmount2 = 0,
                Slot3 = 0,
                SlotAmount3 = 0,
                Slot4 = 0,
                SlotAmount4 = 0,
                Slot5 = 0,
                SlotAmount5 = 0,
            };
            var newinventory = context.Inventories.Add(inventory);
            context.SaveChanges();

            player.pID = (int)newcharacter.GetDatabaseValues()["ID"];
            player.pName = character.Name;
            player.pPositions = new Vector3(character.PositionX, character.PositionY, character.PositionZ);
            player.pRotation = character.Rotation;
            player.pInterior = character.Interior;
            player.pVirtualWorld = character.VirtualWorld;
            player.pHealth = character.Health;
            player.pChaleco = character.Chaleco;
            player.pSkin = character.Skin;
            player.pCrack = character.Crack == 0 ? false : true;

            // Inventory
            player.Inventory = new Inventories
            {
                ID = (int)newinventory.GetDatabaseValues()["ID"],
                Slot = new List<Slot>
                {
                    new Slot
                    {
                        ID = 1,
                        Item = Items.Vacio,
                        Amount = 0
                    },
                    new Slot
                    {
                        ID = 2,
                        Item = Items.Vacio,
                        Amount = 0
                    },
                    new Slot
                    {
                        ID = 3,
                        Item = Items.Vacio,
                        Amount = 0
                    },
                    new Slot
                    {
                        ID = 4,
                        Item = Items.Vacio,
                        Amount = 0
                    },
                    new Slot
                    {
                        ID = 5,
                        Item = Items.Vacio,
                        Amount = 0
                    }
                }
            };

            player.Hand = new List<Hand>
            {
                new Hand
                {
                    Position = false,
                    Item = Items.GetItem(character.RightHand),
                    Amount = character.RightHandAmount
                },

                new Hand
                {
                    Position = true,
                    Item = Items.GetItem(character.LeftHand),
                    Amount = character.LeftHandAmount
                }
            };

            player.Wrist = new List<Wrist>
            {
                new Wrist
                {
                    Position = false,
                    Item = Items.GetItem(character.RightWrist),
                    Amount = character.RightWristAmount
                },

                new Wrist
                {
                    Position = true,
                    Item = Items.GetItem(character.LeftWrist),
                    Amount = character.LeftWristAmount
                }
            };

            player.Health = player.pHealth;
            player.Armour = player.pChaleco;
            player.Skin = player.pSkin;

            player.pAdmin = Admin.None;

            SaveCharacter(player);

            player.Spawn();

            return;
        }

        public static void ClearPlayerVars(Player player)
        {
            // Account Vars
            player.aID = -1;
            player.aName = "none";
            player.aEmail = "none";
            player.aPassword = "none";
            player.aIP = "none";

            // Player Vars
            player.pID = -1;
            player.pName = "none";
            player.pPositions = new Vector3(0, 0, 0);
            player.pRotation = 0;
            player.pInterior = 0;
            player.pVirtualWorld = 0;
            player.pHealth = 0;
            player.pChaleco = 0;
            player.pSkin = 0;
            player.pCrack = false;

            // Inventory Vars
            player.Hand = null;
            player.Wrist = null;
            player.Inventory = null;
        }

        public static string GetNameCharacter(Player player)
        {
            string name = "";

            string[] split = player.pName.Split('_');

            name = split[0] + " " + split[1];

            return name;

        }

        public static string GetAdminName(Admin admin)
        {
            switch (admin)
            {
                case Admin.None: return "none";
                case Admin.Helper: return "Helper";
                case Admin.Moderator: return "Moderator";
                case Admin.Admin: return "Admin";
                case Admin.SeniorAdmin: return "Senior Admin";
                case Admin.Manager: return "Manager";
                default: return "none";
            }
        }

        public static void ClearChat(Player player)
        {
            for (byte i = 1; i <= 20; i++)
                player.SendClientMessage("");
        }
    }
}
