using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using System;
using WashingtonRP.Structures;

namespace WashingtonRP.Events
{
    public class GameMode : WashingtonMode
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Console.WriteLine("\n----------------------------------");
            Console.WriteLine(" Blank game mode by your name here");
            Console.WriteLine("----------------------------------\n");

            
            SetGameModeText("Roleplay");

            SendRconCommand("password sexofernandez");
            SendRconCommand("mapname Washington");
            SendRconCommand("language Spanish");
            SendRconCommand("hostname [0.3DL] Washington Roleplay - v1.0 - Iniciando...");
            

            ShowPlayerMarkers(PlayerMarkersMode.Global);
            ShowNameTags(true);
            SetNameTagDrawDistance(40.0f);
            EnableStuntBonusForAll(false);
            DisableInteriorEnterExits();
            Server.SetWeather(2);
            Server.SetWorldTime(11);
            UsePlayerPedAnimations();
            //RegisterCommands();

            var timer = new Timer(2000, false);
            timer.Tick += StartServer;

            // TODO: Put logic to initialize your game mode here
        }

        private void StartServer(object sender, EventArgs e)
        {
            SendRconCommand("password 0");
            SendRconCommand("hostname [0.3DL] Washington Roleplay - v1.0");
        }
    }
}