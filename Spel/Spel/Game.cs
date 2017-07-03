using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Game
    {
        Player player;
        Room[,] world;
        Random random = new Random();

        public void Play()
        {
            CreatePlayer();
            CreateWorld();

            do
            {
                Console.Clear();
                DisplayStats();
                DisplayWorld();
                CheckIfEmpty();
                AskForMovement();
            } while (player.Health > 0);


            GameOver();
        }

        private void CheckIfEmpty()
        {
            if (world[player.X, player.Y].Item != null)
            {
                if (world[player.X, player.Y].Item.Name == "Sword")
                {
                    player.Damage += 5;
                    Console.WriteLine("You picked up a Sword!");
                    world[player.X, player.Y].Item = null;
                }
                else if (world[player.X, player.Y].Item.Name == "Potion")
                {
                    player.Health += 5;
                    Console.WriteLine("You picked up a Potion!");
                    world[player.X, player.Y].Item = null;
                }

            }
            else if (world[player.X, player.Y].Monster != null)
            {
                if (player.Damage > world[player.X, player.Y].Monster.Damage)
                {
                    player.Health -= world[player.X, player.Y].Monster.Damage / 2;
                }
                else
                {
                    player.Health -= world[player.X, player.Y].Monster.Damage;
                }

                Console.WriteLine("GAHHH!!!");
                Console.WriteLine("YOU KILLED A MONSTER!");
                world[player.X, player.Y].Monster = null;
            }


        }

        void DisplayStats()
        {
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine($"Damage: {player.Damage}");
        }

        private void AskForMovement()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            int newX = player.X;
            int newY = player.Y;
            bool isValidMove = true;

            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow: newX++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                default: isValidMove = false; break;
            }

            if (isValidMove &&
                newX >= 0 && newX < world.GetLength(0) &&
                newY >= 0 && newY < world.GetLength(1))
            {
                player.X = newX;
                player.Y = newY;

                //player.Health--;
            }
        }

        private void DisplayWorld()
        {
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    Room room = world[x, y];
                    if (player.X == x && player.Y == y)
                        Console.Write($"{player.MapChar}");
                    else if (room.Monster != null)
                        Console.Write($"{room.Monster.MapChar}");
                    else if (room.Item != null)
                        Console.Write($"{room.Item.MapChar}");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game over...");
            Console.ReadKey();
            Play();
        }

        private void CreateWorld()
        {
            world = new Room[20, 5];
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    world[x, y] = new Room();

                    if (player.X != x || player.Y != y)
                    {
                        if (random.Next(0, 100) < 10)
                            world[x, y].Monster = new Monster(30, 10);

                        if (random.Next(0, 100) < 5)
                            world[x, y].Item = new Sword();
                        if (random.Next(0, 100) < 5)
                            world[x, y].Item = new Potion(5);
                    }
                }
            }
        }

        private void CreatePlayer()
        {
            int playerStartX = random.Next(0, 19);
            int playerStartY = random.Next(0, 4);
            player = new Player(30, playerStartX, playerStartY, 5);
        }
    }
}
