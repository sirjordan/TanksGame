using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tanks
{
    public class Missile
    {
        // Fields
        int speed;
        protected Tank.direction missleDirection;
        bool launched;
        protected Tank currentTank;
        protected int missileTop;
        protected int missileLeft;

        // <Constructors>
        public Missile(Tank tank)
        {
            this.currentTank = tank;
            this.missleDirection = tank.Direction;
            this.missileLeft = currentTank.leftPos;
            this.missileTop = currentTank.topPos;
            //Launch();
        }

        // Methods 
        public void Launch()
        {
            if (missleDirection.Equals(Tank.direction.down))
            {
                if (this.currentTank.topPos < Battlefield.FieldHeight - 3)
                {
                    missileLeft += 1;
                    missileTop += 3;
                    while (missileTop < Battlefield.FieldHeight - 1)
                    {
                        if (Battlefield.content[missileTop, missileLeft] == ' ')
                        {
                            DrawMissile();
                        }
                        else if (Battlefield.content[missileTop, missileLeft] == '*')  // if hit a Tank
                        {
                            CheckTanksForHit();
                            break;
                        }
                        else if (Battlefield.content[missileTop, missileLeft] == '-')  // if hit the host
                        {
                            GameEngine.GameOver();
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else if (missleDirection.Equals(Tank.direction.left))
            {
                if (this.currentTank.leftPos > 1)
                {
                    missileLeft -= 2;
                    missileTop += 1;
                    while (missileLeft > 1)
                    {
                        if (Battlefield.content[missileTop, missileLeft] == ' ')
                        {
                            DrawMissile();
                        }
                        else if (Battlefield.content[missileTop, missileLeft] == '*')  // if hit a Tank
                        {
                            CheckTanksForHit();
                            break;
                        }
                        else if (Battlefield.content[missileTop, missileLeft] == '-')  // if hit the host
                        {
                            GameEngine.GameOver();
                            break;
                        }
                        else  // if hit a wall
                        {
                            break;
                        }
                    }
                }
            }
            else if (missleDirection.Equals(Tank.direction.right))
            {
                if (this.currentTank.leftPos < Battlefield.FieldWidth - 1)
                {
                    missileLeft += 3;
                    missileTop += 1;
                    while (missileLeft < Battlefield.FieldWidth - 1)    // or something else
                    {
                        if (Battlefield.content[missileTop, missileLeft] == ' ')
                        {
                            DrawMissile();
                        }
                        else if (Battlefield.content[missileTop, missileLeft] == '*')  // if hit a Tank
                        {
                            CheckTanksForHit();
                            break;
                        }
                        else if (Battlefield.content[missileTop, missileLeft] == '-')  // if hit the host
                        {
                            GameEngine.GameOver();
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else    // direction UP
            {
                if (this.currentTank.topPos > 1)
                {
                    missileLeft += 1;
                    missileTop -= 1;
                    while (missileTop > 1)
                    {
                        if (Battlefield.content[missileTop, missileLeft] == ' ')
                        {
                            DrawMissile();
                        }
                        else if (Battlefield.content[missileTop, missileLeft] == '*')  // if hit a Tank
                        {
                            CheckTanksForHit();
                            break;
                        }
                        else if (Battlefield.content[missileTop, missileLeft] == '-')  // if hit the host
                        {
                            GameEngine.GameOver();
                            break;
                        }
                        else    // if hit a wall
                        {
                            GameEngine.playerTank.TanksKilled += 5;
                            Battlefield.UpdateSystemField();
                            break;
                        }
                    }
                }
            }

            // finaly
            Battlefield.Add(missileTop, missileLeft, ' ');
            Console.SetCursorPosition(missileLeft, missileTop);
            Console.Write(' ');
        }

        // draw the missle
        void DrawMissile()
        {
            Thread.Sleep(35);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(missileLeft, missileTop);
            Console.Write(' ');
            Battlefield.Add(missileTop, missileLeft, ' ');

            if (missleDirection.Equals(Tank.direction.right))
            {
                missileLeft++;
            }
            else if (missleDirection.Equals(Tank.direction.left))
            {
                missileLeft--;
            }
            else if (missleDirection.Equals(Tank.direction.down))
            {
                missileTop++;
            }
            else  // direction UP
            {
                missileTop--;
            }
            Console.SetCursorPosition(missileLeft, missileTop);
            Console.Write('*');
        }

        // check all  the tanks match the position of the missile
        private void CheckTanksForHit()
        {
            for (int i = 0; i < GameEngine.enemyTanks.Count; i++)
            {
                if (GameEngine.enemyTanks[i].IsInPosition(missileTop, missileLeft))
                { 
                    // delete tank
                    GameEngine.enemyTanks[i].isAlive = false;
                    Tank.Delete(GameEngine.enemyTanks[i].leftPos, GameEngine.enemyTanks[i].topPos);
                    GameEngine.enemyTanks.Remove(GameEngine.enemyTanks[i]);
                    Console.Write("Boom");
                    GameEngine.playerTank.TanksKilled += 500;
                    Battlefield.UpdateSystemField();
                    GameEngine.enemyTankCount--;
                    if (GameEngine.enemyTankCount > 1)
                    {                        
                        int col = 5;
                        if (GameEngine.enemyTankCount % 2 == 0)
                            col = 45;
                        GameEngine.enemyTanks.Add(new EnemyTank(1, col, "red"));
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.SetCursorPosition(Battlefield.FieldWidth + 3, 20);
                        Console.Write("Enemy Tanks: " + (GameEngine.enemyTankCount));
                    }
                    else if (GameEngine.enemyTankCount == 1)
                    {
                         Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.SetCursorPosition(Battlefield.FieldWidth + 3, 20);
                        Console.Write("Enemy Tanks: " + (GameEngine.enemyTankCount));
                    }
                    else if (GameEngine.enemyTankCount == 0)
                    {
                        //Console.ForegroundColor = ConsoleColor.DarkGreen;
                        //Console.SetCursorPosition(Battlefield.FieldWidth / 2 - 20, Battlefield.FieldHeight / 2);
                        //Console.WriteLine("YOU WIN !");
                        //Console.SetCursorPosition(Battlefield.FieldWidth / 2 - 20, Battlefield.FieldHeight / 2 + 2);
                        //Console.WriteLine("SCORE: " + GameEngine.playerTank.TanksKilled);
                        //GameEngine.runGame = false;
                        Console.SetCursorPosition(Battlefield.FieldWidth / 2, Battlefield.FieldHeight / 2 - 11);
                        Console.WriteLine("YOU WIN !");
                        GameEngine.GameOver();
                    }
                    break;
                    
                }
            }
        }
    }
}
