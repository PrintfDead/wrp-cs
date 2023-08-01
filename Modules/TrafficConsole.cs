using System.Diagnostics;
using WashingtonRP.Structures;
using System.IO;
using SampSharp.GameMode.Definitions;

namespace WashingtonRP.Modules
{
    public class TrafficConsole
    {
        public ProcessStartInfo psi;
        public Process process;
        public int line = 0;

        public TrafficConsole()
        {
            psi = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            process = Process.Start(psi);
        }

        public void AddUser(Player player)
        {
            StreamWriter sw = process.StandardInput;
            StreamReader sr = process.StandardOutput;

            sw.NewLine = "\n";
            while(player.ConnectionStatus == ConnectionStatus.Connected)
            {
                sw.WriteLine($"Name: {player.Name} | IP: {player.IP} | Time: {player.ConnectedTime}");
            }

            sr.Close();
        }
    }
}