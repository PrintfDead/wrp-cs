using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using WashingtonRP.Structures;
using System.Collections.Generic;
using SampSharp.GameMode;
using MySql.Data.MySqlClient;
using SampSharp.GameMode.SAMP;

namespace WashingtonRP.Modules
{
    public class Functions
    {
        public static void ClearChat(PlayerData player)
        {
            for (byte i = 1; i <= 20; i++)
                player.SendClientMessage("");
        }

        public static void ClearPlayerVars(PlayerData player)
        {
            // Account Vars
            player.aID = -1;
            player.aName = "none";
            player.Email = "none";
            player.Password = "none";
            player.Salt = "none";
            player.Ip = "none";

            // Player Vars
            player.ID = -1;
            player.pName = "none";
            player.Positions = new Vector3(0, 0, 0);
            player.pRotation = 0;
            player.pInterior = 0;
            player.pVirtualWorld = 0;
            player.pHealth = 0;
            player.pChaleco = 0;
            player.pSkin = 0;

            // Inventory Vars
            player.RightHand = 0;
            player.LeftHand = 0;
            player.Inventory = null;
        }

        public static int LoadAccount(string email, PlayerData player)
        {
            var connection = new MySqlConnection("server=localhost; database=washington; user=root; password=;");
            connection.Open();
            var query = new MySqlCommand("SELECT * FROM cuentas WHERE Email = '" + email + "'", connection).ExecuteReader();

            if (!query.HasRows) return 0;

            while (query.Read()) 
            {
                player.Email = (string)query["Email"];
                player.Password = (string)query["Password"];
                player.aID = (int)query["ID"];
                player.aName = (string)query["Name"];
                player.Ip = (string)query["IP"];
            }

            query.Close();
            connection.Close();

            return 1;
        }

        public static bool CheckEmail(string email)
        {
            var connection = new MySqlConnection("server=localhost; database=washington; user=root; password=;");
            connection.Open();

            var query = new MySqlCommand("SELECT * FROM cuentas WHERE Email = '" + email + "'", connection).ExecuteReader();

            if (!query.Read()) return false;

            query.Close();
            connection.Close();

            return true;
        }

        public static bool CheckUsername(string nick)
        {
            var connection = new MySqlConnection("server=localhost; database=washington; user=root; password=;");
            connection.Open();

            var query = new MySqlCommand("SELECT * FROM cuentas WHERE Name = '" + nick + "'", connection).ExecuteReader();

            if (!query.Read()) return false;

            query.Close();
            connection.Close();

            return true;
        }

        public static string HashingPassword(string pass)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;

            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }

            return hashString;
        }

        public static bool CheckPassword(string password, PlayerData player)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            
            if (hashString == player.Password)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static List<CharacterData> GetCharacters(PlayerData player)
        {
            var connection = new MySqlConnection("server=localhost; database=washington; user=root; password=;");
            connection.Open();

            var reader = new MySqlCommand($"SELECT * FROM personajes WHERE CuentaID = '{player.aID}' ORDER BY ID LIMIT 3;", connection).ExecuteReader();
            var characters = new List<CharacterData>();
            int count = 0;

            if (!reader.HasRows)
            {
                characters.Clear();

                for (int i = 0; i < 3; i++)
                {
                    characters.Add(new CharacterData
                    {
                        ID = -1,
                        Name = "Crear Personaje"
                    });
                }

                return characters;
            }
            

            while(reader.Read())
            {
                count += 1;
                characters.Add(new CharacterData
                {
                    ID = (int)reader["ID"],
                    Name = (string)reader["Nombre_Apellido"]
                });
            }

            if (count <= 2)
            {
                for (int i = 0; i < 3 - count; i++)
                {
                    characters.Add(new CharacterData
                    {
                        ID = -1,
                        Name = "Crear Personaje"
                    });
                }
            }

            reader.Close();
            connection.Close();

            return characters;
        }

        public static void LoadCharacter(PlayerData player, int id)
        {
            var connection = new MySqlConnection("server=localhost; database=washington; user=root; password=;");
            connection.Open();

            var query = new MySqlCommand($"SELECT * FROM personajes WHERE ID = '{id}'", connection).ExecuteReader();
            if (!query.HasRows)
            {
                player.SendClientMessage("Ocurrio un error contactate con justPrintf. para solucionarlo!");
                player.Kick();
            }

            while (query.Read())
            {
                player.ID = (int)query["ID"];
                player.pName = (string)query["Nombre_Apellido"];
                player.Positions = new Vector3((float)query["PosicionX"], (float)query["PosicionY"], (float)query["PosicionZ"]);
                player.pRotation = (float)query["PosicionR"];
                player.pInterior = (int)query["Interior"];
                player.pVirtualWorld = (int)query["VirtualWorld"];
                player.pHealth = (float)query["Vida"];
                player.pChaleco = (float)query["Chaleco"];
                player.pSkin = (int)query["Skin"];
            }

            query.Close();

            var query2 = new MySqlCommand($"SELECT * FROM inventario WHERE IDCharacter = '{id}'", connection).ExecuteReader();
            if (!query2.HasRows)
            {
                player.SendClientMessage("Ocurrio un error contactate con justPrintf. para solucionarlo!");
                player.Kick();
            }

            while (query2.Read())
            {
                player.Inventory = new InventoryData
                {
                    ID = (int)query2["ID"],
                    Slot1 = ItemData.GetItem((int)query2["Slot1"]),
                    SlotAmount1 = (int)query2["SlotAmount1"],
                    Slot2 = ItemData.GetItem((int)query2["Slot2"]),
                    SlotAmount2 = (int)query2["SlotAmount2"],
                    Slot3 = ItemData.GetItem((int)query2["Slot3"]),
                    SlotAmount3 = (int)query2["SlotAmount3"],
                    Slot4 = ItemData.GetItem((int)query2["Slot4"]),
                    SlotAmount4 = (int)query2["SlotAmount4"],
                    Slot5 = ItemData.GetItem((int)query2["Slot5"]),
                    SlotAmount5 = (int)query2["SlotAmount5"],
                };
            }

            player.state_on = false;
            player.Spawn();

            connection.Close();
            query.Close();
        }

        public static void SaveCharacter(PlayerData player)
        {
            var connection = new MySqlConnection("server=localhost; database=washington; user=root; password=;");
            connection.Open();

            Console.WriteLine("Guardando datos");

            var query = new MySqlCommand($"UPDATE personajes SET PosicionX = '{(float)player.Position.X}', PosicionY = '{(float)player.Position.Y}', PosicionZ = '{(float)player.Position.Z}', PosicionR = '{(float)player.Rotation.Z}', Interior = '{player.Interior}', VirtualWorld = '{player.VirtualWorld}', Vida = '{player.pHealth}', Chaleco = '{player.pChaleco}', Skin = '{player.pSkin}', RightHand = '{player.RightHand}', LeftHand = '{player.LeftHand}' WHERE ID = '{player.ID}';", connection);
            var query2 = new MySqlCommand($"UPDATE inventario SET Slot1 = '{player.Inventory.Slot1.ID}', SlotAmount1 = '{player.Inventory.SlotAmount1}', Slot2 = '{player.Inventory.Slot2.ID}', SlotAmount2 = '{player.Inventory.SlotAmount2}', Slot3 = '{player.Inventory.Slot3.ID}', SlotAmount3 = '{player.Inventory.SlotAmount3}', Slot4 = '{player.Inventory.Slot4.ID}', SlotAmount4 = '{player.Inventory.SlotAmount4}', Slot5 = '{player.Inventory.Slot5.ID}', SlotAmount5 = '{player.Inventory.SlotAmount5}' WHERE IDCharacter = '{player.ID}';", connection);

            query.ExecuteNonQuery();
            query2.ExecuteNonQuery();

            connection.Close();
        }

        public static void TakeItem(PlayerData player, int id)
        {
            RegisterItem Slot = player.Inventory.GetSlot(id);

            if (player.RightHand == 0)
            {
                if (Slot.IDGun > 0)
                {
                    
                }
            }
        }

        public static string GetNameCharacter(PlayerData player)
        {
            string name = "";
            int count = 0;

            string[] split = player.pName.Split('_');

            foreach (var part in split)
            {
                if (count == 1)
                {
                    name += part;
                }

                name += part;
                count++;
            }

            return name;
            
        }

        public static void SendMessageProx(float radius, PlayerData player, string text, Color col1, Color col2, Color col3, Color col4, Color col5)
        {
            Vector3 position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z);

            foreach (PlayerData i in PlayerData.GetAll<PlayerData>())
            {
                if (player.VirtualWorld != i.VirtualWorld) return;

                if (i.IsInRangeOfPoint(radius / 16, position))
                {
                    SendSplitMessage(i, col1, text);
                } 
                else if (i.IsInRangeOfPoint(radius / 8, position))
                {
                    SendSplitMessage(i, col2, text);
                } 
                else if (i.IsInRangeOfPoint(radius / 4, position))
                {
                    SendSplitMessage(i, col3, text);
                } 
                else if (i.IsInRangeOfPoint(radius / 2, position))
                {
                    SendSplitMessage(i, col4, text);
                }
                else if (i.IsInRangeOfPoint(radius, position))
                {
                    SendSplitMessage(i, col5, text);
                }
            }
        }

        public static void SendSplitMessage(PlayerData player, Color color, string text)
        {
            if (text.Length > 64)
            {
                string splitText1 = text.Substring(0, 64);
                string splitText2 = text.Substring(64 - 1, 64 * 2);

                player.SendClientMessage(color, "%s...", splitText1);
                player.SendClientMessage(color, "...%s", splitText2);
            } else
            {
                player.SendClientMessage(color, text);
            }
        }

        public static void CreateCharacter(PlayerData player, string name)
        {
            var connection = new MySqlConnection("server=localhost; database=washington; user=root; password=;");
            connection.Open();

            var reader = new MySqlCommand($"INSERT INTO personajes (Nombre_Apellido, CuentaID) VALUES ('{name}', '{player.aID}');", connection);
            reader.ExecuteNonQuery();
            var readerInv = new MySqlCommand($"INSERT INTO inventario (IDCharacter, Slot1, Slot2, Slot3, Slot4, Slot5) VALUES ('{(int)reader.LastInsertedId}', '0', '0', '0', '0', '0');", connection);
            readerInv.ExecuteNonQuery();
            
            player.ID = (int)reader.LastInsertedId;
            player.pName = name;
            player.Positions = new Vector3(1284.5775f, -1329.1689f, 13.5451f);
            player.pRotation = 90.2268f;
            player.pInterior = 0;
            player.pVirtualWorld = 0;
            player.pHealth = 100.0f;
            player.pChaleco = 0;
            player.pSkin = 289;

            // Inventory
            player.Inventory = new InventoryData
            {
                ID = (int)readerInv.LastInsertedId,
                Slot1 = ItemData.Samsung,
                Slot2 = ItemData.Vacio,
                Slot3 = ItemData.Vacio,
                Slot4 = ItemData.Vacio,
                Slot5 = ItemData.Vacio
            };

            player.RightHand = 0;
            player.LeftHand = 0;

            player.Health = player.pHealth;
            player.Armour = player.pChaleco;
            player.Skin = player.pSkin;

            SaveCharacter(player);

            connection.Close();

            player.Spawn();

            return;
        }
    }
}
