using System;

namespace AntSim
{
    /// <summary>
    /// Flower erbt die Klasse ScreenObject
    /// </summary>
    class Flower : ScreenObject
    {
        public Flower()
        {
            Symbol = 'Y';
        }

        protected override void SetColors()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
    }
}
