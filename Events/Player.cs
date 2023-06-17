using SampSharp.GameMode;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using System;
using WashingtonRP.Structures;
using WashingtonRP.Modules;

namespace WashingtonRP.Events
{
    [PooledType]
    public class Player : PlayerData
    {
        public override void OnConnected(EventArgs e)
        {
            base.OnConnected(e);

            if (IsNPC) return;

            Functions.ClearPlayerVars(this);

            string washingtonText = $"{Color.Blue}Was{Color.Red}h{Color.White}i{Color.Red}n{Color.White}g{Color.Red}t{Color.White}o{Color.Red}n {Color.White}Roleplay";

            SendClientMessage(Color.White, $"¡Hola! Bienvenido a {washingtonText} disfruta de este servidor.");
            SendClientMessage(Color.White, $"Contactate con @justprintf por discord si encuentras algun error.{Color.White}");
        }

        public override void OnDisconnected(DisconnectEventArgs e)
        {
            base.OnDisconnected(e);

            if (!state_on)
                Functions.SaveCharacter(this);
        }

        public override void OnSpawned(SpawnEventArgs e)
        {
            base.OnSpawned(e);
            if (IsNPC) return;
            ToggleSpectating(false);

            Health = pHealth;
            Armour = pChaleco;
            Skin = pSkin;

            Position = Positions;
            Rotation = new Vector3(pRotation);
            Interior = pInterior;
            VirtualWorld = pVirtualWorld;
        }

        public override void OnRequestClass(RequestClassEventArgs e)
        {
            base.OnRequestClass(e);

            if (!state_on)
            {
                state_on = true;
                ToggleSpectating(true);
                //PlayAudioStream("https://dl42.y2hub.cc/file/youtubeBPUeVfVu0Sw128.mp3?fn=Natanael%20Cano-%20Doble%20Vaso%20%5BSERIE%20VGLY%5D.mp3");

                var dialog = new ListDialog("Selecciona una opcion.", "Siguiente", "Salir");
                dialog.AddItem("Iniciar Sesion");
                dialog.AddItem("Registrarse");
                dialog.AddItem("Notas de version");
                dialog.Response += Dialogs.SelectDialog_Response;
                dialog.Show(this);
            }
        }

        public override void OnText(TextEventArgs e)
        {
            base.OnText(e);

            if (Crack)
            {
                SendClientMessage(Color.Yellow, "* No puedes hablar en este estado");
            }

            Color color1 = new Color(230, 230, 230);
            Color color2 = new Color(200, 200, 200);
            Color color3 = new Color(170, 170, 170);
            Color color4 = new Color(140, 140, 140);
            Color color5 = new Color(110, 110, 110);

            Functions.SendMessageProx((float)30.0, this, $"{Functions.GetNameCharacter(this)} dice: {e.Text}", color1, color2, color3, color4, color5);
        }

        public override void OnGiveDamage(DamageEventArgs e)
        {
            base.OnGiveDamage(e);

            SendClientMessage("El jugador: %s, recibio damage (%f)", pName, e.Amount);
        }
        public override void OnTakeDamage(DamageEventArgs e)
        {
            base.OnTakeDamage(e);

           // SendClientMessage("El jugador: %s, take damage (%f)", pName, e.Amount);
        }
    }
}