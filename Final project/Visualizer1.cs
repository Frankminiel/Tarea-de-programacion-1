using System;
namespace Renta_de_Juegos.UI
{
    public static class Visuals
    {

        public static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        public static void ShowBanner(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("========================================");
            Console.WriteLine($"            {text}");
            Console.WriteLine("========================================");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
        public static void ShowWeAllCanIntro()
        {
            Console.Clear();
            Console.CursorVisible = false;

            string[] logo =
            {
            "██╗    ██╗███████╗     █████╗ ██╗     ██╗      ██████╗ █████╗ ██╗     ██╗",
            "██║    ██║██╔════╝    ██╔══██╗██║     ██║     ██╔════╝██╔══██╗██║     ██║",
            "██║ █╗ ██║█████╗      ███████║██║     ██║     ██║     ███████║██║     ██║",
            "██║███╗██║██╔══╝      ██╔══██║██║     ██║     ██║     ██╔══██║██║     ██║",
            "╚███╔███╔╝███████╗    ██║  ██║███████╗███████╗╚██████╗██║  ██║███████╗███████╗",
            " ╚══╝╚══╝ ╚══════╝    ╚═╝  ╚═╝╚══════╝╚══════╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚══════╝"
        };

            Console.ForegroundColor = ConsoleColor.Cyan;

            foreach (var line in logo)
            {
                Console.WriteLine(line);
                Thread.Sleep(60);  // Animación suave
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n               A place where we all can play ");
            Thread.Sleep(800);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPress ANY key to start...");
            Console.ReadKey();

            Console.CursorVisible = true;
            Console.Clear();

        }
    }
}