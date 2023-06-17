﻿using MySql.Data.MySqlClient;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using System;
using System.Text.RegularExpressions;
using WashingtonRP.Structures;

namespace WashingtonRP.Modules
{
    public class Dialogs
    {
        public static void SelectDialog_Response(object sender, DialogResponseEventArgs e)
        {
            var player = e.Player as PlayerData;

            if (e.DialogButton == DialogButton.Left)
            {
                if (e.ListItem == 0)
                {
                    var loginDialog = new InputDialog("Login", $"{Color.White}Introduce tu {Color.IndianRed}email {Color.White}para identificarte en la base de datos", false, "Aceptar", "Cancelar");
                    loginDialog.Response += LoginDialog_Response;
                    loginDialog.Show(player);

                    return;
                }
                else if (e.ListItem == 1)
                {
                    var registerDialog = new InputDialog("Register", $"{Color.White}Introduce tu {Color.IndianRed}email {Color.White}para registrarte en la base de datos", false, "Aceptar", "Cancelar");
                    registerDialog.Response += RegisterDialog_Response;
                    registerDialog.Show(player);
                    return;
                }
                else if (e.ListItem == 2)
                {
                    var versionNotes = new MessageDialog("Notas de version", "", "Volver");

                    return;
                }
            }
            else
            {
                player.Kick();
            }
        }

        public static void RegisterDialog_Response(object sender, DialogResponseEventArgs e)
        {
            PlayerData player = e.Player as PlayerData;
            if (e.DialogButton == DialogButton.Left)
            {
                if (e.InputText.Length <= 0)
                {
                    var registerDialog = new InputDialog("Register", $"{Color.Red}Error: {Color.White}Tienes que introducir un email\n{Color.White}Introduce tu {Color.IndianRed}email {Color.White}para registrarte en la base de datos", false, "Aceptar", "Cancelar");
                    registerDialog.Response += RegisterDialog_Response;
                    registerDialog.Show(player);
                    return;
                }

                if (!Functions.IsValidEmail(e.InputText))
                {
                    var registerDialog = new InputDialog("Register", $"{Color.Red}Error: {Color.White}Este email no es valido\n{Color.White}Introduce tu {Color.IndianRed}email {Color.White}para registrarte en la base de datos", false, "Aceptar", "Cancelar");
                    registerDialog.Response += RegisterDialog_Response;
                    registerDialog.Show(player);
                    return;
                }

                if (Functions.CheckEmail(e.InputText))
                {
                    player.SendClientMessage("Este email ya fue registrado!");

                    var registerDialog = new InputDialog("Register", $"{Color.Red}Error: {Color.White}Este email ya se encuentra registrado\n{Color.White}Introduce tu {Color.IndianRed}email {Color.White}para registrarte en la base de datos", false, "Aceptar", "Cancelar");
                    registerDialog.Response += RegisterDialog_Response;
                    registerDialog.Show(player);
                    return;
                }

                player.Email = e.InputText;
                var passwordDialog = new InputDialog("Register", $"{Color.White}Introduce tu ${Color.LightBlue}contraseña {Color.White}para registrarte en la base de datos", true, "Aceptar", "Cancelar");
                passwordDialog.Response += Register2Dialog_Response;
                passwordDialog.Show(player);
            }
            else
            {
                var dialog = new ListDialog("Selecciona una opcion.", "Siguiente", "Salir");
                dialog.AddItem("Iniciar Sesion");
                dialog.AddItem("Registrarse");
                dialog.AddItem("Notas de version");
                dialog.Response += SelectDialog_Response;
                dialog.Show(player);
            }
        }

        public static void Register2Dialog_Response(object sender, DialogResponseEventArgs e)
        {
            PlayerData player = e.Player as PlayerData;

            if (e.DialogButton == DialogButton.Left)
            {
                if (e.InputText.Length <= 0)
                {
                    var passwordDialog = new InputDialog("Register", $"{Color.Red}Error: {Color.White}Esta contraseña no es correcta\n{Color.White}Introduce tu {Color.LightBlue}contraseña {Color.White}para registrarte en la base de datos", true, "Aceptar", "Cancelar");
                    passwordDialog.Response += Register2Dialog_Response;
                    passwordDialog.Show(player);
                    return;
                }
                else if (e.InputText.Length < 8)
                {
                    var passwordDialog = new InputDialog("Register", $"{Color.Red}Error: {Color.White}Minimo 8 caracteres\n{Color.White}Introduce tu {Color.LightBlue}contraseña {Color.White}para registrarte en la base de datos", true, "Aceptar", "Cancelar");
                    passwordDialog.Response += Register2Dialog_Response;
                    passwordDialog.Show(player);
                    return;
                }

                Regex regex = new Regex(@"^([A-Z][a-z][\w]+)");
                Match match = regex.Match(e.InputText);

                if (!match.Success)
                {
                    var passwordDialog = new InputDialog("Register", $"{Color.Red}Error: {Color.White}El primer caracter debe ser mayuscula. (EJ: Washington_123)\n{Color.White}Introduce tu {Color.LightBlue}contraseña {Color.White}para registrarte en la base de datos", true, "Aceptar", "Cancelar");
                    passwordDialog.Response += Register2Dialog_Response;
                    passwordDialog.Show(player);
                    return;
                }

                player.Password = Functions.HashingPassword(e.InputText);

                var registerDialog = new InputDialog("Register", $"{Color.White}Introduce un {Color.LightBlue}nombre de usuario {Color.White}para registrarte en la base de datos", false, "Aceptar", "Cancelar");
                registerDialog.Response += Register3Dialog_Response;
                registerDialog.Show(player);
            }
            else
            {
                player.SendClientMessage("Vete a chupar pene!");
                player.Kick();
            }
        }

        public static void Register3Dialog_Response(object sender, DialogResponseEventArgs e)
        {
            var player = e.Player as PlayerData;

            if (e.DialogButton == DialogButton.Left)
            {
                if (e.InputText.Length <= 0)
                {
                    var registerDialog = new InputDialog("Register", $"{Color.Red}Error: {Color.White}Debes introducir un nombre de usuario\n{Color.White}Introduce un {Color.LightBlue}nombre de usuario {Color.White}para registrarte en la base de datos", true, "Aceptar", "Cancelar");
                    registerDialog.Response += Register3Dialog_Response;
                    registerDialog.Show(player);
                    return;
                }

                if (e.InputText.Length > 24)
                {
                    var registerDialog = new InputDialog("Register", $"{Color.Red}Error: {Color.White}Maximo 24 caracteres\n{Color.White}Introduce un {Color.LightBlue}nombre de usuario {Color.White}para registrarte en la base de datos", true, "Aceptar", "Cancelar");
                    registerDialog.Response += Register3Dialog_Response;
                    registerDialog.Show(player);
                    return;
                }

                if (Functions.CheckUsername(e.InputText))
                {
                    var registerDialog = new InputDialog("Register", $"{Color.Red}Error: {Color.White}Este nombre de usuario ya esta registrado\n{Color.White}Introduce un {Color.LightBlue}nombre de usuario {Color.White}para registrarte en la base de datos", true, "Aceptar", "Cancelar");
                    registerDialog.Response += Register3Dialog_Response;
                    registerDialog.Show(player);
                    return;
                }

                player.aName = (string)e.InputText;
                player.Ip = player.IP;

                var connection = new MySqlConnection("server=localhost; database=washington; user=root; password=;");
                connection.Open();

                var query = new MySqlCommand($"INSERT INTO cuentas (Name, Password, Email, IP) VALUES ('{player.aName}', '{player.Password}', '{player.Email}', '{player.Ip}');", connection);
                query.ExecuteNonQuery();

                connection.Close();

                player.aID = (int)query.LastInsertedId;

                Console.WriteLine(">>> Se inserto la cuenta en la base de datos");

                Functions.ClearChat(player);

                var characters = Functions.GetCharacters(player);

                var dialog = new ListDialog("Selecciona un Personaje.", "Siguiente", "Salir");

                characters.ForEach(character =>
                {
                    player.charactersID.Add(character.ID);
                    dialog.AddItem($"({character.ID}) {character.Name}");
                    Console.WriteLine(">>> Character added");
                });

                dialog.Response += SelectCharacterDialog_Response;
                dialog.Show(player);
            }
        }

        public static void LoginDialog_Response(object sender, DialogResponseEventArgs e)
        {
            PlayerData player = e.Player as PlayerData;

            if (e.DialogButton == DialogButton.Left)
            {
                if (e.InputText.Length <= 0)
                {
                    var loginDialog = new InputDialog("Login", $"{Color.Red}Error: {Color.White}Debes introducir un email\n{Color.White}Introduce tu {Color.IndianRed}email {Color.White}para identificarte en la base de datos", false, "Aceptar", "Cancelar");
                    loginDialog.Response += LoginDialog_Response;
                    loginDialog.Show(player);
                    return;
                }

                if (!Functions.IsValidEmail(e.InputText))
                {
                    var loginDialog = new InputDialog("Login", $"{Color.Red}Error: {Color.White}Este email no es valido\n{Color.White}Introduce tu {Color.IndianRed}email {Color.White}para identificarte en la base de datos", false, "Aceptar", "Cancelar");
                    loginDialog.Response += LoginDialog_Response;
                    loginDialog.Show(player);
                    return;
                }

                if (!Functions.CheckEmail(e.InputText))
                {
                    var loginDialog = new InputDialog("Login", $"{Color.Red}Error: {Color.White}Este email no se encuentra registrado\n{Color.White}Introduce tu {Color.IndianRed}email {Color.White}para identificarte en la base de datos", false, "Aceptar", "Cancelar");
                    loginDialog.Response += LoginDialog_Response;
                    loginDialog.Show(player);
                    return;
                }

                Functions.LoadAccount(e.InputText, player);

                var passwordDialog = new InputDialog("Login", $"{Color.White}Introduce tu {Color.LightBlue}contraseña {Color.White}para identificarte en la base de datos", true, "Aceptar", "Cancelar");
                passwordDialog.Response += PasswordDialog_Response;
                passwordDialog.Show(player);
            }
            else
            {
                var dialog = new ListDialog("Selecciona una opcion.", "Siguiente", "Salir");
                dialog.AddItem("Iniciar Sesion");
                dialog.AddItem("Registrarse");
                dialog.AddItem("Notas de version");
                dialog.Response += SelectDialog_Response;
                dialog.Show(player);
            }
        }

        public static void PasswordDialog_Response(object sender, DialogResponseEventArgs e)
        {
            PlayerData player = e.Player as PlayerData;

            if (e.DialogButton == DialogButton.Left)
            {
                if (e.InputText.Length <= 0)
                {
                    var passwordDialog = new InputDialog("Login", $"{Color.Red}Error: {Color.White}Debes introducir una contraseña\n{Color.White}Introduce tu {Color.LightBlue}contraseña {Color.White}para registrarte en la base de datos", true, "Aceptar", "Cancelar");
                    passwordDialog.Response += PasswordDialog_Response;
                    passwordDialog.Show(player);
                    return;
                }

                Functions.LoadAccount(e.InputText, player);

                if (!Functions.CheckPassword(e.InputText, player))
                {
                    var passwordDialog = new InputDialog("Login", $"{Color.Red}Error: {Color.White}Esta contraseña no se encuentra registrada\n{Color.White}Introduce tu {Color.LightBlue}contraseña {Color.White}para registrarte en la base de datos", true, "Aceptar", "Cancelar");
                    passwordDialog.Response += PasswordDialog_Response;
                    passwordDialog.Show(player);
                    return;
                }

                Functions.ClearChat(player);

                var characters = Functions.GetCharacters(player);

                var dialog = new ListDialog("Selecciona un Personaje.", "Siguiente", "Salir");

                characters.ForEach(character =>
                {

                    if (character.ID == -1)
                    {
                        player.charactersID.Add(-1);
                        dialog.AddItem($"{character.Name}");
                    }
                    else
                    {
                        player.charactersID.Add(character.ID);
                        dialog.AddItem($"({character.ID}) {character.Name}");
                    }
                });
                dialog.Response += SelectCharacterDialog_Response;
                dialog.Show(player);
            }
            else
            {
                var passwordDialog = new InputDialog("Login", $"{Color.White}Introduce tu ${Color.LightBlue}contraseña {Color.White}para identificarte en la base de datos", true, "Aceptar", "Cancelar");
                passwordDialog.Response += PasswordDialog_Response;
                passwordDialog.Show(player);
            }
        }

        public static void TakeInventoryDialog_Response(object sender, DialogResponseEventArgs e)
        {
            PlayerData player = e.Player as PlayerData;

            if (e.DialogButton == DialogButton.Left)
            {
                if (e.ListItem == 0)
                {
                    if (player.Inventory.Slot1.ID == ItemData.Vacio.ID)
                    {
                        player.SendClientMessage("No tienes nada en este slot");
                        return;
                    }

                    Functions.TakeItem(player, 1);
                } 
                else if (e.ListItem == 1)
                {
                    if (player.Inventory.Slot2.ID == ItemData.Vacio.ID)
                    {
                        player.SendClientMessage("No tienes nada en este slot");
                        return;
                    }

                    Functions.TakeItem(player, 2);
                }
                else if (e.ListItem == 2)
                {
                    if (player.Inventory.Slot3.ID == ItemData.Vacio.ID)
                    {
                        player.SendClientMessage("No tienes nada en este slot");
                        return;
                    }

                    Functions.TakeItem(player, 3);
                }
                else if (e.ListItem == 3)
                {
                    if (player.Inventory.Slot4.ID == ItemData.Vacio.ID)
                    {
                        player.SendClientMessage("No tienes nada en este slot");
                        return;
                    }

                    Functions.TakeItem(player, 4);
                }
                else if (e.ListItem == 4)
                {
                    if (player.Inventory.Slot5.ID == ItemData.Vacio.ID)
                    {
                        player.SendClientMessage("No tienes nada en este slot");
                        return;
                    }

                    Functions.TakeItem(player, 5);
                }
            }
        }

        public static void SelectCharacterDialog_Response(object sender, DialogResponseEventArgs e)
        {
            PlayerData player = e.Player as PlayerData;

            if (e.DialogButton == DialogButton.Left)
            {
                if (e.ListItem == 0)
                {
                    var id = player.charactersID[0];

                    if (id == -1)
                    {
                        var characterCreateDialog = new InputDialog("Creacion de Personaje", "Introduce un nombre y apellido\nFormato: Nombre_Apellido", false, "Aceptar", "Cancelar");
                        characterCreateDialog.Response += CreateCharacterDialog_Response;
                        characterCreateDialog.Show(player);
                    }
                    else
                    {
                        Functions.LoadCharacter(player, id);
                    }
                }
                if (e.ListItem == 1)
                {
                    var id = player.charactersID[1];

                    if (id == -1)
                    {
                        var characterCreateDialog = new InputDialog("Creacion de Personaje", "Introduce un nombre y apellido\nFormato: Nombre_Apellido", false, "Aceptar", "Cancelar");
                        characterCreateDialog.Response += CreateCharacterDialog_Response;
                        characterCreateDialog.Show(player);
                    }
                    else
                    {
                        Functions.LoadCharacter(player, id);
                    }
                }
                if (e.ListItem == 2)
                {
                    var id = player.charactersID[2];

                    if (id == -1)
                    {
                        var characterCreateDialog = new InputDialog("Creacion de Personaje", "Introduce un nombre y apellido\nFormato: Nombre_Apellido", false, "Aceptar", "Cancelar");
                        characterCreateDialog.Response += CreateCharacterDialog_Response;
                        characterCreateDialog.Show(player);
                    }
                    else
                    {
                        Functions.LoadCharacter(player, id);
                    }
                }
            }
            else
            {
                player.SendClientMessage("Vete a chupar pene!");
                player.Kick();
            }
        }

        public static void CreateCharacterDialog_Response(object sender, DialogResponseEventArgs e)
        {
            PlayerData player = e.Player as PlayerData;

            if (e.DialogButton == DialogButton.Left)
            {
                if (e.InputText.Length <= 0)
                {
                    player.SendClientMessage("Nombre de personaje incorrecto!");

                    var characterCreateDialog = new InputDialog("Creacion de Personaje", "Introduce un nombre y apellido\nFormato: Nombre_Apellido", false, "Aceptar", "Cancelar");
                    characterCreateDialog.Response += CreateCharacterDialog_Response;
                    characterCreateDialog.Show(player);
                    return;
                }

                Regex regex = new Regex(@"^([A-Z][a-z]+)_([A-Z][a-z]+)");
                Match match = regex.Match(e.InputText);

                if (!match.Success)
                {
                    player.SendClientMessage("Nombre de personaje incorrecto!");
                    var characterCreateDialog = new InputDialog("Creacion de Personaje", "Introduce un nombre y apellido\nFormato: Nombre_Apellido", false, "Aceptar", "Cancelar");
                    characterCreateDialog.Response += CreateCharacterDialog_Response;
                    characterCreateDialog.Show(player);
                    return;
                }

                Console.WriteLine(player.Email);

                Functions.CreateCharacter(player, e.InputText);
                player.charactersID.Clear();
            }
            else
            {
                player.Kick();
            }
        }
    }
}
