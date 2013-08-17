using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tanks
{
    public class GameEngine
    {
        static int gameSpeed = 60;
        static bool runGame = true;
        List<Tank> enemyTanks = new List<Tank>();
        static Tank playerTank = new Tank(Battlefield.FieldHeight - 13, Battlefield.FieldWidth / 2);

        static EnemyTank enemyTank1 = new EnemyTank(1, 1);
        static EnemyTank enemyTank2 = new EnemyTank(1, Battlefield.FieldWidth - 5);

        static void Main()
        {
            // set the encoding to print the extendet ASCII table
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
            Battlefield.SetBattlefield();

            Thread enemyTanksTh = new Thread(new ThreadStart(RunEnemyTank));
            Thread playerTankTh = new Thread(new ThreadStart(Run));
            enemyTanksTh.Start();
            playerTankTh.Start();
            enemyTanksTh.Join();
            playerTankTh.Join();
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
                        Missile newMissile = new Missile(playerTank);
                    }
                }
            }
        }

        // enemy Tanks infiniteLoops , this is just for testing
        // this method need to be upgraded :D
        static void RunEnemyTank()
        {
            while (runGame)
            {
                enemyTank1.Move();
                //enemyTank2.Move();
            }
        }
    }
}
