using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tanks
{
    public class GameEngine
    {
        
        static int difficult = 1;   // more is harder
        static int gameSpeed = 60;  // small is fast
        static bool runGame = true;
        public static List<EnemyTank> enemyTanks = new List<EnemyTank>();
        public static Tank playerTank = new Tank(37, 20, "green");

        // test enemyTank
        //static EnemyTank enemyTank1 = new EnemyTank(1, Battlefield.FieldWidth - 5);

        static void Main()
        {
            // set the encoding to print the extendet ASCII table
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
            
            // create a number of enemies based on the difficult
            enemyTanks = EnemyTank.GenerateEnemyTanks(difficult);

            // create a battlefield playing grid
            Battlefield.SetBattlefield();

            Thread enemyTanksTh = new Thread(new ThreadStart(RunEnemyTank));
            Thread playerTankTh = new Thread(new ThreadStart(Run));
            enemyTanksTh.Start();
            playerTankTh.Start();
            enemyTanksTh.Join();
            playerTankTh.Join();

            Battlefield.Add(40, 30, 'a');
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
                if (playerTank.life <= 0)
                    break;
            }
        }

        // enemy Tanks infiniteLoops , this is just for testing
        // this method need to be upgraded :D
        static void RunEnemyTank()
        {
            while (runGame)
            {
                List<Thread> threadList = new List<Thread>();                
                for (int i = 0; i < enemyTanks.Count; i++)
                {
                    threadList.Add(new Thread(new ThreadStart(enemyTanks[i].Move)));
                    threadList[i].Start();
                    //EnemyMissile enemyMissile = new EnemyMissile(enemyTanks[i]);
                    //enemyMissile.Launch();
                }

                foreach (var thread in threadList)
                {
                    thread.Join();
                }
            }
            
        }
    }
}
