using System;

namespace AntSim
{
    /// <summary>
    /// Queen erbt die Klasse Ant (erbt die Klasse ScreenObject)
    /// </summary>
    class Queen : Ant
    {
        private bool IsPregnant { get; set; }
        private int PregnancyDuration { get; set; }

        public Queen()
        {
            IsPregnant = true;
            PregnancyDuration = 5;
            Symbol = 'Q';
        }

        protected override char GetSymbol()
        {
            if (IsDying())
            {
                Symbol = 'q';
            }
            return Symbol;
        }

        private void Hatch()
        {
            if (PregnancyDuration > 0)
            {
                PregnancyDuration--;
                Console.Write(" ");
            }
            else
            {
                // TODO: in die Kollisionsüberprüfung integrieren
                Console.Write("o");
                IsPregnant = false;
            }
        }

        protected override void LeaveOldPosition(World world)
        {
            Console.BackgroundColor = world.BackgroundColor;
            Console.SetCursorPosition(PositionX, PositionY);
            if (IsPregnant)
            {
                Hatch();
            }
            else
            {
                Console.Write(" ");
            }
        }

        protected override void SetColors()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
    }
}