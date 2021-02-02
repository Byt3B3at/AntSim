using System;

namespace AntSim
{
    /// <summary>
    /// Food erbt die Klasse ScreenObject
    /// </summary>
    class Food : ScreenObject
    {
        public int Mass { get; set; }

        public Food()
        {
            Symbol = '*';
            Mass = 3;
        }

        protected override void SetColors()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}