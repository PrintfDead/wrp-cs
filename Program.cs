using SampSharp.Core;
using System.Threading;
using System;
using WashingtonRP.Events;

namespace WashingtonRP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new GameModeBuilder()
                .Use<GameMode>()
                .UseEncodingCodePage("cp1252")
                .Run();

            int count = 1;

            /* Console.SetCursorPosition(0, 0);
            Console.Write($"thread 1: {count} of 100\n");
            Console.Write($"thread 2: {count} of 100\n");
            Console.Write("Esperando a cargar threads\n");

            for (int i = 0; i < 50; i++) {
                count++;
                Thread.Sleep(1000);
                Console.SetCursorPosition(0, 0);
                Console.Write($"thread 1: {count} of 100");
                Console.SetCursorPosition(0, 1);
                Console.Write($"thread 2: {count} of 100");
            }*/
        }
    }
}
