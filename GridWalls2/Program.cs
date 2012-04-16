using System;

namespace GridWars2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                //Microsoft.Win32.Registry.SetValue("HKEY_CLASSES_ROOT", "Hight", 10);
                //Microsoft.Win32.Registry.GetValue("HKEY_CLASSES_ROOT", "Hight", 0);
                game.Run();
            }
        }
    }
}

