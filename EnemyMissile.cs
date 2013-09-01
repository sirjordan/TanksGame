using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tanks
{
    public class EnemyMissile
    {
        // Fields
        int speed;
        Tank.direction missleDirection;
        bool launched;
        Tank currentTank;
        protected int missileTop;
        protected int missileLeft;

        // Constructor
        public EnemyMissile(Tank.direction direction, int missileLeft, int missileTop)
        {
            //this.currentTank = tank;
            this.missleDirection = direction;
            this.missileLeft = missileLeft;
            this.missileTop = missileTop;
            //Launch();
        }

        // Methods 
        public void Launch()
        {
            if (missileLeft > 1 && missileLeft < Battlefield.FieldWidth - 1 && missileTop > 3
                && missileTop < Battlefield.FieldHeight - 1)
            {
                if (missleDirection.Equals(Tank.direction.down))
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
                else if (missleDirection.Equals(Tank.direction.left))
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
                else if (missleDirection.Equals(Tank.direction.right))
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
                else    // direction UP
                {
                    missileLeft += 1;
                    missileTop -= 3;
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
                        else
                        {
                            break;
                        }
                    }
                }

                // finaly
                Battlefield.Add(missileTop, missileLeft, ' ');
                Console.SetCursorPosition(missileLeft, missileTop);
                Console.Write(' ');
            }
        }

        // draw the missle
        void DrawMissile()
        {
            Thread.Sleep(35);
            Console.ForegroundColor = ConsoleColor.Blue;
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
                if (GameEngine.playerTank.IsInPosition(missileTop, missileLeft))
                {
                    // delete tank
                    GameEngine.playerTank.life--;
                    Tank.Delete(GameEngine.playerTank.leftPos, GameEngine.playerTank.topPos);
                    GameEngine.playerTank.leftPos = 20;
                    GameEngine.playerTank.topPos = 37;
                    GameEngine.playerTank.PlaceTank(20, 37);
                    Console.SetCursorPosition(Battlefield.FieldWidth + 6, 19);
                    Console.Write("LIVES: " + GameEngine.playerTank.life);
                    if (GameEngine.playerTank.life <= 0)
                    {                       
                        GameEngine.GameOver();                       
                    }
                    
                    break;
                }
            }
        }
    }
}
