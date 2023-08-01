using SampSharp.GameMode;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using System;
using WashingtonRP.Structures;
using WashingtonRP.Modules;
using WashingtonRP.Modules.Utils;

namespace WashingtonRP.Events
{
    [PooledType]
    public class Player : Structures.Player
    {
        public override void OnConnected(EventArgs e)
        {
            base.OnConnected(e);

            if (IsNPC) return;
            
            Characters.ClearPlayerVars(this);

            string washingtonText = $"{Color.Blue}Was{Color.Red}h{Color.White}i{Color.Red}n{Color.White}g{Color.Red}t{Color.White}o{Color.Red}n {Color.White}Roleplay";

            SendClientMessage(Color.White, $"¡Hola! Bienvenido a {washingtonText} disfruta de este servidor.");
            SendClientMessage(Color.White, $"Contactate con @justprintf por discord si encuentras algun error.{Color.White}");
        }

        public override void OnDisconnected(DisconnectEventArgs e)
        {  
            base.OnDisconnected(e);

            if (!InLogin)
                Characters.SaveCharacter(this);
        }

        public override void OnSpawned(SpawnEventArgs e)
        {
            base.OnSpawned(e);
            if (IsNPC) return;

            if (pCrack)
            {
                ToggleControllable(false);

                Health = 25.0f;
                Armour = 0;
                Position = pPositions;
                Rotation = new Vector3(pRotation);
                Interior = pInterior;
                VirtualWorld = pVirtualWorld;

                RemoveAttachedObject(7);
                ResetWeapons();

                if (RightHand != Items.Vacio)
                {
                    if (RightHand.IDGun > 0)
                    {
                        if (RightHandAmount > 0)
                        {
                            GiveWeapon(Objects.GetWeapon(RightHand.IDGun), RightHandAmount);
                        }
                    }

                    Objects.PutObject(this, RightHand, 1);
                }
                else if (LeftHand != Items.Vacio)
                {
                    Objects.PutObject(this, LeftHand, 2);
                }

                ApplyAnimation("WUZI", "CS_Dead_Guy", 4.0f, false, true, true, true, 0);

                return;
            }

            ToggleSpectating(false);

            Health = pHealth;
            Armour = pChaleco;
            Skin = pSkin;

            Position = pPositions;
            Rotation = new Vector3(pRotation);
            Interior = pInterior;
            VirtualWorld = pVirtualWorld;

            SetSkillLevel(SampSharp.GameMode.Definitions.WeaponSkill.Pistol, 1);
            SetSkillLevel(SampSharp.GameMode.Definitions.WeaponSkill.MicroUzi, 1);
            SetSkillLevel(SampSharp.GameMode.Definitions.WeaponSkill.SawnoffShotgun, 1);
            SetSkillLevel(SampSharp.GameMode.Definitions.WeaponSkill.PistolSilenced, 1);

            RemoveAttachedObject(7);
            ResetWeapons();

            if (RightHand != Items.Vacio)
            {
                if (RightHand.IDGun > 0)
                {
                    if (RightHandAmount > 0)
                    {
                        GiveWeapon(Objects.GetWeapon(RightHand.IDGun), RightHandAmount);
                    }
                }

                Objects.PutObject(this, RightHand, 1);
            }
            else if (LeftHand != Items.Vacio)
            {
                Objects.PutObject(this, LeftHand, 2);
            }
        }

        public override void OnRequestClass(RequestClassEventArgs e)
        {
            base.OnRequestClass(e);

            if (!InLogin)
            {
                InLogin = true;
                ToggleSpectating(true);
                Name = $"Conectando_{Id}";
                PlayAudioStream("https://dl42.y2hub.cc/file/youtubeBPUeVfVu0Sw128.mp3?fn=Natanael%20Cano-%20Doble%20Vaso%20%5BSERIE%20VGLY%5D.mp3");

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
            e.SendToPlayers = false;

            if (pCrack)
            {
                SendClientMessage(Color.Yellow, "* No puedes hablar en este estado");
                return;
            } else if (InLogin)
            {
                SendClientMessage(Color.Yellow, "* No puedes hablar si no estas logeado");
                return;
            }

            Color color1 = new Color(230, 230, 230);
            Color color2 = new Color(200, 200, 200);
            Color color3 = new Color(170, 170, 170);
            Color color4 = new Color(140, 140, 140);
            Color color5 = new Color(110, 110, 110);

            Message.SendMessageProx((float)30.0, this, $"{Characters.GetNameCharacter(this)} dice: {e.Text[0].ToString().ToUpper() + e.Text.Substring(1)}", color1, color2, color3, color4, color5);
            return;
        }

        public override void OnUpdate(PlayerUpdateEventArgs e)
        {
            base.OnUpdate(e);

            if (RightHand != Items.Vacio)
            {
                if (Objects.GetWeapon(Weapon) != RightHand.IDGun && RightHandAmount >= 1)
                {
                    if (RightHandAmount > 0 && WeaponAmmo == 0) 
                    {
                        ResetWeapons();
                        GiveWeapon(Objects.GetWeapon(RightHand.IDGun), RightHandAmount);
                    }

                    //SetArmedWeapon(Objects.GetWeapon(RightHand.IDGun));
                    return;
                }
                else if (RightHandAmount == 0)
                {
                    if (Weapon != SampSharp.GameMode.Definitions.Weapon.None)
                    {
                        Console.WriteLine("Reseteo de armas");
                        ResetWeapons();
                        //SetArmedWeapon(Objects.GetWeapon(0));
                    }
                }
            }
        }

        public override void OnWeaponShot(WeaponShotEventArgs e)
        {
            base.OnWeaponShot(e);

            var item = Items.GetItemByGun(Objects.GetWeapon(e.Weapon));

            if (item == Items.Vacio) { SendClientMessage("Ocurrio un error, comunicate con @justprintf por discord"); return; }

            if (item == Items.Vacio && Objects.GetWeapon(e.Weapon) > 0)
            {
                SendClientMessage("Esta arma no existe!");
                Kick();
            }

            if (RightHandAmount > 0)
            {
                Console.WriteLine(RightHandAmount);
                Console.WriteLine(WeaponAmmo);
                RightHandAmount--;

                if (WeaponAmmo != RightHandAmount && WeaponAmmo == 1)
                {
                    Console.WriteLine("No tiene balas");
                    RightHandAmount = 0;
                    ResetWeapons();
                    return;
                }

                if (WeaponAmmo != 0 && WeaponAmmo > RightHandAmount)
                {
                    RightHandAmount = WeaponAmmo;
                    return;
                }

                if (RightHandAmount == 0 && WeaponAmmo == 0)
                {
                    Console.WriteLine("Reseteo de armas");
                    ResetWeapons();
                    return;
                }
            }
            else
            {
                InBalas++;
                ResetWeapons();

                if (InBalas == 3)
                {
                    Objects.CleanHands(this);
                }
            }

            if (RightHand == Items.Taser && e.Weapon == Objects.GetWeapon(23))
            {
                InTaserReload = true;

                var timer = new Timer(5000, false);
                timer.Tick += (object sender, EventArgs e) =>
                {
                    InTaserReload = false;
                    return;
                };
            }
        }

        public override void OnDeath(DeathEventArgs e)
        {
            base.OnDeath(e);

            pPositions = Position;
            pRotation = Rotation.Z;
            pInterior = Interior;
            pVirtualWorld = VirtualWorld;

            pCrack = true;
        }

        public override void OnGiveDamage(DamageEventArgs e)
        {
            base.OnGiveDamage(e);
            Item weapon = Items.GetItemByGun(Objects.GetWeapon(e.Weapon));
            Player player = e.OtherPlayer as Player;

            if (weapon == Items.Vacio) return;

            if (e.BodyPart == SampSharp.GameMode.Definitions.BodyPart.Head)
            {
                player.Health = 0;
            }
            else if ((player.Health - weapon.BonusDamage) < 0)
            {
                player.Health = 0;
            }
            else
            {
                player.Health -= weapon.BonusDamage;
            }

            SendClientMessage("El jugador: %s, recibio damage (%f)", pName, e.Amount);
        }
        public override void OnTakeDamage(DamageEventArgs e)
        {
            base.OnTakeDamage(e);

           // SendClientMessage("El jugador: %s, take damage (%f)", pName, e.Amount);
        }
    }
}