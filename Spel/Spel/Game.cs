using Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DoD;

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
            ThreadStart temp = new ThreadStart(PlayMusic);
            Thread musicThread = new Thread(temp);

            musicThread.IsBackground = true;

            
            CreatePlayer();
            CreateWorld();
            
            //Medelande vid start av spel.
            TextUtils.Animate("Welcome to Dungeons Of Doom...");
            TextUtils.Animate("Press any key to start.");
            Console.ReadKey(true);
            //musicThread.Start();
            //Spel loop
            do
            {
                Console.Clear();
                Console.WriteLine($"Level: {currentLevel}");
                DisplayStats();
                DisplayWorld();
                CheckIfEmpty();                
                DisplayBackpack();
                AskForMovement();
                DidIWin();
            } while (player.Health > 0);


            GameOver();
        }

        /// <summary>
        /// Prints the current backpack
        /// </summary>
        private void DisplayBackpack()
        {
            if (player.Backpack!=null)
            {
                foreach (IBackpackable item in player.Backpack)
                {
                    Console.WriteLine(item);
                }
            }
           
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

        /// <summary>
        /// Controlls if there is a Monster or an item in the position of the player
        /// </summary>
        private void CheckIfEmpty()
        {
            if (world[player.X, player.Y].Monster != null)
            {
                FightMonster();            
            }
            else if (world[player.X, player.Y].Item != null)
            {
                UseItemInRoom();                     
            }

        }

        /// <summary>
        /// Use the item in the new room
        /// </summary>
        private void UseItemInRoom()
        {
            string OutputString = world[player.X, player.Y].Item.Use(player);
            world[player.X, player.Y].Item = null;
            Console.WriteLine(OutputString);
        }

        /// <summary>
        /// Fight the monster in the room
        /// </summary>
        private void FightMonster()
        {
            Console.WriteLine(player.Fight(world[player.X, player.Y].Monster));
            world[player.X, player.Y].Monster = null;
            Monster.MonsterCount--;
        }

        /// <summary>
        /// Writes the stats of the player in the Console
        /// </summary>
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

                //if (world[player.X,player.Y].Item!=null)
                //{
                //    player.Backpack.Add(world[player.X, player.Y].Item);
                //    //world[player.X, player.Y].Item = null;
                //}
            }
        }

        /// <summary>
        /// Prints the game world
        /// </summary>
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

        
        /// <summary>
        /// Skapar världen
        /// </summary>
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
                        if (RandomUtils.TestPercentage(2+level))
                            world[x, y].Monster = new Ogre(x,y);
                        if (RandomUtils.TestPercentage(10+level))
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
        /// <summary>
        /// Plays the music
        /// </summary>
        private void PlayMusic()
        {
            //MArio
            //Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(375); Console.Beep(392, 125); Thread.Sleep(375); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(375); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125); Thread.Sleep(1125); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125);

            //-----------Tetris

            Console.Beep(658, 125);

            Console.Beep(1320, 500);

            Console.Beep(990, 250);

            Console.Beep(1056, 250);

            Console.Beep(1188, 250);

            Console.Beep(1320, 125);

            Console.Beep(1188, 125);

            Console.Beep(1056, 250);

            Console.Beep(990, 250);

            Console.Beep(880, 500);

            Console.Beep(880, 250);

            Console.Beep(1056, 250);

            Console.Beep(1320, 500);

            Console.Beep(1188, 250);

            Console.Beep(1056, 250);

            Console.Beep(990, 750);

            Console.Beep(1056, 250);

            Console.Beep(1188, 500);

            Console.Beep(1320, 500);

            Console.Beep(1056, 500);

            Console.Beep(880, 500);

            Console.Beep(880, 500);

            Thread.Sleep(250);

            Console.Beep(1188, 500);

            Console.Beep(1408, 250);

            Console.Beep(1760, 500);

            Console.Beep(1584, 250);

            Console.Beep(1408, 250);

            Console.Beep(1320, 750);

            Console.Beep(1056, 250);

            Console.Beep(1320, 500);

            Console.Beep(1188, 250);

            Console.Beep(1056, 250);

            Console.Beep(990, 500);

            Console.Beep(990, 250);

            Console.Beep(1056, 250);

            Console.Beep(1188, 500);

            Console.Beep(1320, 500);

            Console.Beep(1056, 500);

            Console.Beep(880, 500);

            Console.Beep(880, 500);

            Thread.Sleep(500);

            Console.Beep(1320, 500);

            Console.Beep(990, 250);

            Console.Beep(1056, 250);

            Console.Beep(1188, 250);

            Console.Beep(1320, 125);

            Console.Beep(1188, 125);

            Console.Beep(1056, 250);

            Console.Beep(990, 250);

            Console.Beep(880, 500);

            Console.Beep(880, 250);

            Console.Beep(1056, 250);

            Console.Beep(1320, 500);

            Console.Beep(1188, 250);

            Console.Beep(1056, 250);

            Console.Beep(990, 750);

            Console.Beep(1056, 250);

            Console.Beep(1188, 500);

            Console.Beep(1320, 500);

            Console.Beep(1056, 500);

            Console.Beep(880, 500);

            Console.Beep(880, 500);

            Thread.Sleep(250);

            Console.Beep(1188, 500);

            Console.Beep(1408, 250);

            Console.Beep(1760, 500);

            Console.Beep(1584, 250);

            Console.Beep(1408, 250);

            Console.Beep(1320, 750);

            Console.Beep(1056, 250);

            Console.Beep(1320, 500);

            Console.Beep(1188, 250);

            Console.Beep(1056, 250);

            Console.Beep(990, 500);

            Console.Beep(990, 250);

            Console.Beep(1056, 250);

            Console.Beep(1188, 500);

            Console.Beep(1320, 500);

            Console.Beep(1056, 500);

            Console.Beep(880, 500);

            Console.Beep(880, 500);

            Thread.Sleep(500);

            Console.Beep(660, 1000);

            Console.Beep(528, 1000);

            Console.Beep(594, 1000);

            Console.Beep(495, 1000);

            Console.Beep(528, 1000);

            Console.Beep(440, 1000);

            Console.Beep(419, 1000);

            Console.Beep(495, 1000);

            Console.Beep(660, 1000);

            Console.Beep(528, 1000);

            Console.Beep(594, 1000);

            Console.Beep(495, 1000);

            Console.Beep(528, 500);

            Console.Beep(660, 500);

            Console.Beep(880, 1000);

            Console.Beep(838, 2000);

            Console.Beep(660, 1000);

            Console.Beep(528, 1000);

            Console.Beep(594, 1000);

            Console.Beep(495, 1000);

            Console.Beep(528, 1000);

            Console.Beep(440, 1000);

            Console.Beep(419, 1000);

            Console.Beep(495, 1000);

            Console.Beep(660, 1000);

            Console.Beep(528, 1000);

            Console.Beep(594, 1000);

            Console.Beep(495, 1000);

            Console.Beep(528, 500);

            Console.Beep(660, 500);

            Console.Beep(880, 1000);

            Console.Beep(838, 2000);
        }
    }
}
