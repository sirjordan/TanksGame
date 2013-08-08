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

        static void Main()
        {
            // set the encoding to print the extendet ASCII table
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);

            Battlefield.SetBattlefield();
            Tank playerTank = new Tank(Battlefield.FieldHeight - 3, Battlefield.FieldWidth / 2);
            

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
                        // shoot a missle
                    }
                }
            }
            // game over things
        }
    }
}
