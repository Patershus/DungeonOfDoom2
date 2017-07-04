using DungeonsOfDoom.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Game
    {
        Player player;
        readonly Room[,] world = new Room[20,5];
        Random random = new Random();
        static int level = 1;
        //static int monsterCount;

        public void Play(int currentLevel)
        {

            CreatePlayer();
            CreateWorld();

            //Medelande vid start av spel.
            TextUtils.Animate("Welcome to Dungeons Of Doom...");
            TextUtils.Animate("Press any key to start.");
            Console.ReadKey(true);

            //Spel loop
            do
            {
                Console.Clear();
            Console.WriteLine($"Level: {currentLevel}");
                DisplayStats();
                DisplayWorld();
                CheckIfEmpty();                
                AskForMovement();
                DidIWin();
            } while (player.Health > 0);


            GameOver();
        }

        private void DidIWin()
        {
            if (Monster.MonsterCount == 0)
            {
                Console.WriteLine("You win!");
                Thread.Sleep(1000);
                Console.ReadKey(true);
                level += 1;
                Play(level);
            }
        }

        private void CheckIfEmpty()
        {
            if (world[player.X, player.Y].Monster != null)
            {
                Console.WriteLine(player.Fight(world[player.X, player.Y].Monster));
                
                world[player.X, player.Y].Monster = null;
                Monster.MonsterCount--;
            }
            else if (world[player.X, player.Y].Item != null)
            {              
                    string OutputString = world[player.X, player.Y].Item.Use(player);
                    world[player.X, player.Y].Item = null;
                    Console.WriteLine(OutputString);          
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
            //monsterCount = 0;
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    Room room = world[x, y];
                    if (player.X == x && player.Y == y)
                        Console.Write($"{player.MapChar}");
                    else if (room.Monster != null)
                    {
                        Console.Write($"{room.Monster.MapChar}");
                        //monsterCount += 1;
                    }
                    else if (room.Item != null)
                        Console.Write($"{room.Item.MapChar}");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
          
        }

        //Spelet är över, startar ett nytt spel
        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game over...");
            Console.ReadKey();
            level = 1;
            Play(level);
        }

        //Skapar världen
        private void CreateWorld()
        {
            //world = new Room[20, 5];
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    world[x, y] = new Room();

                    if (player.X != x || player.Y != y)
                    {
                        //Lägger till monster
                        if (RandomUtils.TestPercentage(3))
                            world[x, y].Monster = new Ogre(x,y);
                        if (RandomUtils.TestPercentage(15))
                            world[x, y].Monster = new Orc(x,y);

                        //Lägger till items
                        if (RandomUtils.TestPercentage(5))
                            world[x, y].Item = new Sword("Rusty Sword", 2);
                        if (RandomUtils.TestPercentage(0.1))
                            world[x, y].Item = new Sword("Sword AF Doom", 100000000);
                        if (RandomUtils.TestPercentage(5))
                            world[x, y].Item = new Potion( 10, "Healing potion");
                        if (RandomUtils.TestPercentage(5))
                            world[x, y].Item = new Portal("Portal");
                    }
                }
            }
        }
        //Skapar en instans av Player med slumpad startposition.
        private void CreatePlayer()
        {
            //int playerStartX = random.Next(0, 20);
            int playerStartX = RandomUtils.RandomNumber(0, 19);
            int playerStartY = RandomUtils.RandomNumber(0,4);
            player = new Player(30, playerStartX, playerStartY, 5);
        }
    }
}
