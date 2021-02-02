using System;

namespace AntSim
{
    /// <summary>
    /// Basisklasse fuer alle Bildschirmobjekte
    /// </summary>
    class ScreenObject
    {
        protected static readonly Random _Random = new Random();

        public int PositionX { get; protected set; }
        public int PositionY { get; protected set; }
        protected char Symbol { get; set; }

        public ScreenObject()
        {
            Console.CursorVisible = false;
        }

        protected virtual char GetSymbol()
        {
            return Symbol;
        }

        public void Initialize(World world)
        {
            SetPositionX(world);
            SetPositionY(world);
            SetColors();
            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(GetSymbol());
        }

        protected virtual void SetColors()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void SetPositionX(World world)
        {
            double PositionXDoubleValue = _Random.NextDouble() * world.BorderRight;
            PositionX = Convert.ToInt32(PositionXDoubleValue);
            if (PositionX < world.BorderLeft)
            {
                PositionX = world.BorderLeft;
            }
            else if (PositionX >= world.BorderRight)
            {
                PositionX = world.BorderRight;
            }
        }

        private void SetPositionY(World world)
        {
            double PositionYDoubleValue = _Random.NextDouble() * world.BorderBottom;
            PositionY = Convert.ToInt32(PositionYDoubleValue);
            if (PositionY < world.BorderTop)
            {
                PositionY = world.BorderTop;
            }
            else if (PositionY >= world.BorderBottom)
            {
                PositionY = world.BorderBottom;
            }
        }
    }
}