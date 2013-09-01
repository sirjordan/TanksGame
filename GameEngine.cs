using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Tanks
{
    public class GameEngine
    {

        public static int enemyTankCount = 5;
        public static int difficult = 2;   // more is harder
        static int gameSpeed = 60;  // small is fast
        public static bool runGame = true;
        public static List<EnemyTank> enemyTanks = new List<EnemyTank>();
        public static Tank playerTank;
        public static List<string> highScores = new List<string>(5);
        public static string playerName;

        // test enemyTank
        //static EnemyTank enemyTank1 = new EnemyTank(1, Battlefield.FieldWidth - 5);

        static void Main()
        {
            // set the encoding to print the extendet ASCII table
            //Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("T A N K S\n");
            Console.WriteLine("Created By: HomerSimpson Team\n");
            Console.Write("Enter Player's Name: ");
            playerName = Console.ReadLine();
            Console.Clear();

            // Generate player tank
            playerTank = new Tank(36, 20, "green");
            // Create a number of enemies based on the difficult
            enemyTanks = EnemyTank.GenerateEnemyTanks(difficult);

            // Create a battlefield playing grid
            Battlefield.SetBattlefield();

            Thread enemyTanksTh = new Thread(new ThreadStart(RunEnemyTank));
            Thread playerTankTh = new Thread(new ThreadStart(Run));
            enemyTanksTh.Start();
            playerTankTh.Start();
            enemyTanksTh.Join();
            playerTankTh.Join();

            //Battlefield.Add(40, 30, 'a');
        }

        // gameRun
        static void Run()
        {
            while (runGame)
            {
                // move the tank
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        playerTank.Move(Tank.direction.left);
                    }
                    if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        playerTank.Move(Tank.direction.right);
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        playerTank.Move(Tank.direction.down);
                    }
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        playerTank.Move(Tank.direction.up);
                    }
                    if (keyInfo.Key == ConsoleKey.Spacebar) // and if there is no missle already
                    {
                        // TODO: Thread does't work properly, maybe it is couse it is nestet thread..??
                        Missile newMissile = new Missile(playerTank);

                        Thread missileThread = new Thread(new ThreadStart(newMissile.Launch));
                        missileThread.Start();
                        missileThread.Join();
                    }
                    
                }
            }
        }

        // enemy Tanks infiniteLoops , this is just for testing
        // this method need to be upgraded :D
        public static void RunEnemyTank()
        {
            while (runGame)
            {
                List<Thread> threadList = new List<Thread>();                
                for (int i = 0; i < enemyTanks.Count; i++)
                {
                    threadList.Add(new Thread(new ThreadStart(enemyTanks[i].Move)));
                    threadList[i].Start();                    
                }

                foreach (var thread in threadList)
                {
                    thread.Join();
                }
            }
            
        }

        // game over things
        public static void GameOver()
        {
            // TODO: Display other players stats and score thru txt file and place the player if is the top list
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(Battlefield.FieldWidth / 2 , Battlefield.FieldHeight / 2 - 10);
            Console.WriteLine("GAME OVER !");
            Console.SetCursorPosition(Battlefield.FieldWidth / 2, Battlefield.FieldHeight / 2 + 1 - 10);
            Console.WriteLine("Your Score is {0}", GameEngine.playerTank.TanksKilled);
            runGame = false;

            HightScore();
            // exit all therads
            Environment.Exit(0);
        }

        // Get and display hight scores, if the player score is higher it fits
        public static void HightScore()
        {
            try
            {
                using (StreamReader read = new StreamReader(@"..\..\HighScores.txt")) 
                {
                    string line = string.Empty;
                    while (line != null)
                    {
                        line = read.ReadLine();
                        if (line != null)
                        {
                            string[] separated = line.Split(' ');
                            highScores.Add(separated[0] + " " + separated[1]);
                        }
                    }
                }

                // Place the current player
                for (int i = 0; i < highScores.Count; i++)
                {
                    string[] currentPlayerInList = highScores[i].Split(' ');

                    if (playerTank.TanksKilled > int.Parse(currentPlayerInList[0]))
                    {
                        // move down the lower players
                        for (int j = highScores.Count - 1; j > i;  j--)
                        {
                            highScores[j] = highScores[j - 1];
                        }

                        highScores[i] = playerTank.TanksKilled + " " + playerName;    // TODO: Here should be tre player name
                        break;
                    }
                }

                // move into the .txt file
                using (StreamWriter write = new StreamWriter(@"..\..\HighScores.txt", false))
                {
                    for (int i = 0; i < highScores.Count; i++)
                    {
                        write.WriteLine(highScores[i]);
                    }
                }

                // displayng the current played games
                // and print the hightscore result
                Console.ForegroundColor = ConsoleColor.Green;
                int vert = Battlefield.FieldHeight / 2 + 3;
                Console.SetCursorPosition(Battlefield.FieldWidth / 2, vert - 10);
                Console.WriteLine("High Scores:");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                for (int i = 0; i < highScores.Count; i++)
                {
                    Console.SetCursorPosition(Battlefield.FieldWidth / 2, vert + i + 1 - 10);
                    Console.Write(highScores[i].ToString());
                }
                // the press any key thing
                Console.WriteLine();
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine("Text file 'HighScores.txt' Not found!");
            }
        }
    }
}
