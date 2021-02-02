using System;
using System.Collections.Generic;

namespace AntSim
{
    /// <summary>
    /// Ant erbt die Klasse ScreenObject
    /// </summary>
    class Ant : ScreenObject
    {
        private readonly string[] Directions = { "left", "right", "up", "down" };
        private readonly List<string> ListDirections = new List<string>();

        private int Age { get; set; }
        private string Direction { get; set; }
        private int MaxAge { get; set; }
        private int MaxWeight { get; set; }
        private int MidAge { get; set; }
        private int Weight { get; set; }

        public Ant()
        {
            ListDirections.AddRange(Directions);
            MaxWeight = 10;
            MidAge = 200;
            MaxAge = _Random.Next(MidAge, 300);
            Symbol = 'A';
        }

        public void Die(World world)
        {
            LeaveOldPosition(world);
        }

        // Food per ref übergeben?
        private void EatFood(Food food)
        {
            Weight += food.Mass;
            food = null;
        }

        private void GetRandomDirection(World world)
        {
            switch (_Random.Next(ListDirections.Count))
            {
                case 0:
                    PlanMove(ListDirections[0], world);
                    break;
                case 1:
                    PlanMove(ListDirections[1], world);
                    break;
                case 2:
                    PlanMove(ListDirections[2], world);
                    break;
                case 3:
                    PlanMove(ListDirections[3], world);
                    break;
            }
        }

        protected override char GetSymbol()
        {
            if (IsDying())
            {
                Symbol = '_';
            }
            return Symbol;
        }

        protected bool IsDying()
        {
            return (Age > MidAge) && (Age < MaxAge);
        }

        public bool IsHungry()
        {
            return Weight < MaxWeight;
        }

        public bool IsVital()
        {
            return Age < MaxAge;
        }

        protected virtual void LeaveOldPosition(World world)
        {
            Console.BackgroundColor = world.BackgroundColor;
            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(" ");
        }

        public void Move(World world)
        {
            LeaveOldPosition(world);
            SetColors();
            GetRandomDirection(world);
            SetNewPosition();
            Age++;
        }

        private void PlanMove(string direction, World world)
        {
            bool BorderLeft = (PositionX - 1) <= world.BorderLeft;
            bool BorderRight = (PositionX + 1) >= world.BorderRight;
            bool BorderTop = (PositionY - 1) <= world.BorderTop;
            bool BorderBottom = (PositionY + 1) >= world.BorderBottom;

            switch (direction)
            {
                case "left":
                    Direction = "left";
                    if (BorderLeft)
                    {
                        Direction = "right";
                        break;
                    }
                    else
                    {
                        ScreenObject screenObjectLeft = world.GetScreenObjectOnNextPosition(PositionX - 1, PositionY);
                        if (screenObjectLeft != null)
                        {
                            if (screenObjectLeft.GetType() == typeof(Food))
                            {
                                EatFood((Food)screenObjectLeft);
                                break;
                            }
                            else
                            {
                                Direction = "right";
                                break;
                            }
                        }
                    }
                    break;
                case "right":
                    Direction = "right";
                    if (BorderRight)
                    {
                        Direction = "left";
                        break;
                    }
                    else
                    {
                        ScreenObject screenObjectRight = world.GetScreenObjectOnNextPosition(PositionX + 1, PositionY);
                        if (screenObjectRight != null)
                        {
                            if (screenObjectRight.Equals(typeof(Food)))
                            {
                                EatFood((Food)screenObjectRight);
                                break;
                            }
                            else if (!BorderLeft)
                            {
                                Direction = "left";
                                break;
                            }
                        }
                    }
                    break;
                case "up":
                    Direction = "up";
                    if (BorderTop)
                    {
                        Direction = "down";
                        break;
                    }
                    else
                    {
                        ScreenObject screenObjectUp = world.GetScreenObjectOnNextPosition(PositionX, PositionY - 1);
                        if (screenObjectUp != null)
                        {
                            if (screenObjectUp.Equals(typeof(Food)))
                            {
                                EatFood((Food)screenObjectUp);
                                break;
                            }
                            else if (!BorderBottom)
                            {
                                Direction = "down";
                                break;
                            }
                        }
                    }
                    break;
                case "down":
                    Direction = "down";
                    if (BorderBottom)
                    {
                        Direction = "up";
                        break;
                    }
                    else
                    {
                        ScreenObject screenObjectDown = world.GetScreenObjectOnNextPosition(PositionX, PositionY + 1);
                        if (screenObjectDown != null)
                        {
                            if (screenObjectDown.Equals(typeof(Food)))
                            {
                                EatFood((Food)screenObjectDown);
                                break;
                            }
                            else if (!BorderTop)
                            {
                                Direction = "up";
                                break;
                            }
                        }
                    }
                    break;
            }
        }

        protected override void SetColors()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Red;
        }

        private void SetNewPosition()
        {
            switch (Direction)
            {
                case "left":
                    Console.SetCursorPosition(--PositionX, PositionY);
                    break;
                case "right":
                    Console.SetCursorPosition(++PositionX, PositionY);
                    break;
                case "up":
                    Console.SetCursorPosition(PositionX, --PositionY);
                    break;
                case "down":
                    Console.SetCursorPosition(PositionX, ++PositionY);
                    break;
            }
            Console.Write(GetSymbol());
        }
    }
}