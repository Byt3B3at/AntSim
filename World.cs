using System;
using System.Collections.Generic;
using System.Threading;

namespace AntSim
{
    /// <summary>
    /// Klasse der Welt, auf der alle Bildschirmobjekte platziert werden
    /// </summary>
    class World
    {
        public readonly ConsoleColor BackgroundColor = ConsoleColor.Green;
        public readonly int BorderBottom = Console.WindowHeight;
        public readonly int BorderLeft = Console.WindowLeft;
        public readonly int BorderRight = Console.WindowWidth;
        public readonly int BorderTop = Console.WindowTop;
        private readonly List<Ant> ListAnts = new List<Ant>();
        private readonly List<Flower> ListFlowers = new List<Flower>();
        private readonly List<Food> ListFood = new List<Food>();
        private readonly List<ScreenObject> ListScreenObjects = new List<ScreenObject>();
        // TODO: MatchField innerhalb der Konsole (= 50, 211 (Fullscreen))
        private readonly int[,] MatchField = { { 0, 25 }, { 0, 150 } };

        public World()
        {
            GetFlowers();
            GetFood();
            GetAnts();
            Draw();
            MoveAnts();
        }

        private void BuryAnt(Ant ant)
        {
            ListAnts.Remove(ant);
            ListScreenObjects.Remove(ant);
            ant = null;
        }

        /// <summary>
        /// Male Bildschirmobjekte
        /// </summary>
        private void Draw()
        {
            // Male Hintergrund
            Console.BackgroundColor = BackgroundColor;
            Console.Clear();

            // Male Blumen
            foreach (Flower _Flower in ListFlowers)
            {
                _Flower.Initialize(this);
                ListScreenObjects.Add(_Flower);
            }

            // Male Essen
            foreach (Food _Food in ListFood)
            {
                _Food.Initialize(this);
                ListScreenObjects.Add(_Food);
            }

            // Male Ameisen (inkl. Königin)
            foreach (Ant _Ant in ListAnts)
            {
                _Ant.Initialize(this);
                ListScreenObjects.Add(_Ant);
            }
        }

        private List<Ant> GetAnts()
        {
            ListAnts.Add(new Queen());
            int Amount = 35;
            for (int Ants = 0; Ants < Amount; Ants++)
            {
                ListAnts.Add(new Ant());
            }
            return ListAnts;
        }

        private List<Flower> GetFlowers()
        {
            int Amount = 35;
            for (int Flowers = 0; Flowers < Amount; Flowers++)
            {
                ListFlowers.Add(new Flower());
            }
            return ListFlowers;
        }

        private List<Food> GetFood()
        {
            int Amount = 35;
            for (int Food = 0; Food < Amount; Food++)
            {
                ListFood.Add(new Food());
            }
            return ListFood;
        }

        /// <summary>
        /// Prüfe, ob das nächste Feld frei ist
        /// </summary>
        public ScreenObject GetScreenObjectOnNextPosition(int posX, int posY)
        {
            foreach (ScreenObject screenObject in ListScreenObjects.ToArray())
            {
                if (screenObject.PositionX == posX && screenObject.PositionY == posY)
                {
                    return screenObject;
                }
            }
            return null;
        }

        /// <summary>
        /// Bewege alle Ameisen (inkl. Königin)
        /// </summary>
        private void MoveAnts()
        {
            // TODO: Check, ob alle Ant-Objekte gelöscht wurden
            while (ListAnts.Count > 0)
            {
                foreach (Ant _Ant in ListAnts.ToArray())
                {
                    if (_Ant.IsVital())
                    {
                        _Ant.Move(this);
                    }
                    else
                    {
                        _Ant.Die(this);
                        BuryAnt(_Ant);
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}