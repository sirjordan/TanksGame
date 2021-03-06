using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace Tanks
{
    public class EnemyTank : Tank
    {
        // enemy tank Fileds
        public bool isAlive;
        
        Random randomMoves = new Random();
        // <Enemy constructor>
        public EnemyTank(int row, int col, string colorTank)
            : base(row, col, colorTank)
        {
            this.isAlive = true;
        }
               
       

        private void seekPlayersTank()
        {
            if (this.leftPos == GameEngine.playerTank.leftPos
                ||
                this.leftPos == GameEngine.playerTank.leftPos+1
                ||
                this.leftPos + 1 == GameEngine.playerTank.leftPos
                )
            {
                if (this.topPos > GameEngine.playerTank.topPos)
                {
                    this.tankOrientation = direction.up;
                }
                else
                {
                    this.tankOrientation = direction.down;
                }
            }
            else if (this.topPos == GameEngine.playerTank.topPos
                || this.topPos == GameEngine.playerTank.topPos + 1
                || this.topPos + 1 == GameEngine.playerTank.topPos)
            {
                if (this.leftPos > GameEngine.playerTank.leftPos)
                {
                    this.tankOrientation = direction.left;
                }
                else
                {
                    this.tankOrientation = direction.right;
                }
            }
            else
            {
                this.tankOrientation = RandomDirection();
            }
        }
        
        // <Methods>
        // Enemy AI movement
        // Overrided Move() method
        public void Move()
        {
            
            int moves = randomMoves.Next(1, 51);    // old 51

            tankOrientation = RandomDirection();
                        
            int speed = 100;
            if (tankOrientation == direction.right)
            {
                
                for (int i = 0; i < moves && isAlive; i++)
                {
                    if (i % 4 == 1)seekPlayersTank(); // make the tank more 'stupid' (not to chase at each iteration), also helps when the tank is in front of a wall
                    if (tankOrientation != direction.right)
                        return; // i.e the tank has decided to change its direction
                    if (!body.Equals(bodyRight)) // if direction is not Right
                    {
                        body = bodyRight;
                    }
                    else if (this.leftPos < Battlefield.FieldWidth - 4)
                    {
                        Delete(leftPos, topPos); // delete the last position of the tank
                        if (FreeCells(tankOrientation))
                        {
                            leftPos++; // the topLeft left
                        }
                    }
                    PlaceTank(leftPos, topPos); // draw the tank
                    Thread.Sleep(speed);
                    if (moves % 3 == 0)
                    {
                        EnemyMissile enemyMissile = new EnemyMissile(tankOrientation, leftPos, topPos);
                        enemyMissile.Launch();
                    }
                    
                }

            }
            else if (tankOrientation == direction.down)
            {
                for (int i = 0; i < moves && isAlive; i++)
                {
                    if (i % 4 == 1) seekPlayersTank(); // make the tank more 'stupid' (not to chase at each iteration), also helps when the tank is in front of a wall
                    if (tankOrientation != direction.down)
                        return; // i.e the tank has decided to change its direction
                    if (!body.Equals(bodyDown))
                    {
                        body = bodyDown;
                    }
                    else if (this.topPos < Battlefield.FieldHeight - 4)
                    {
                        Delete(leftPos, topPos);
                        if (FreeCells(tankOrientation))
                        {
                            topPos++;
                        }
                    }
                    PlaceTank(leftPos, topPos);
                    Thread.Sleep(speed);
                    if (moves % 3 == 0)
                    {
                        EnemyMissile enemyMissile = new EnemyMissile(tankOrientation, leftPos, topPos);
                        enemyMissile.Launch();
                    }
                }

            }
            else if (tankOrientation == direction.left)
            {
                for (int i = 0; i < moves && isAlive; i++)
                {
                    if (i % 4 == 1) seekPlayersTank(); // make the tank more 'stupid' (not to chase at each iteration), also helps when the tank is in front of a wall
                    if (tankOrientation != direction.left)
                        return; // i.e the tank has decided to change its direction
                    if (!body.Equals(bodyLeft))
                    {
                        body = bodyLeft;
                    }
                    else if (this.leftPos > 1)
                    {
                        Delete(leftPos, topPos);
                        if (FreeCells(tankOrientation))
                        {
                            leftPos--;
                        }
                    }
                    PlaceTank(leftPos, topPos);
                    Thread.Sleep(speed);
                    if (moves % 3 == 0)
                    {
                        EnemyMissile enemyMissile = new EnemyMissile(tankOrientation, leftPos, topPos);
                        enemyMissile.Launch();
                    }
                }

            }
            else // if direction is UP
            {
                for (int i = 0; i < moves && isAlive; i++)
                {
                    if (i % 2 == 1) seekPlayersTank(); // make the tank more 'stupid' (not to chase at each iteration), also helps when the tank is in front of a wall
                    if (tankOrientation != direction.up)
                        return; // i.e the tank has decided to change its direction
                    if (!body.Equals(bodyUp))
                    {
                        body = bodyUp;
                    }
                    else if (this.topPos > 1)
                    {
                        Delete(leftPos, topPos);
                        if (FreeCells(tankOrientation))
                        {
                            topPos--;
                        }
                    }
                    PlaceTank(leftPos, topPos);
                    Thread.Sleep(speed);
                    if (moves % 3 == 0)
                    {
                        EnemyMissile enemyMissile = new EnemyMissile(tankOrientation, leftPos, topPos);
                        enemyMissile.Launch();
                    }
                }
            }

        }

        // EnemyTank direction
        static direction RandomDirection()
        {
            Random number = new Random();
            direction dir = direction.down;
            int num = number.Next(1, 5);
            switch (num)
            {
                case 1: dir = direction.up; break;
                case 2: dir = direction.down; break;
                case 3: dir = direction.left; break;
                case 4: dir = direction.right; break;
                default:
                    break;
            }
            return dir;
        }

        //Returns random placed enemy tanks, the number of tanks returned is based on input int 
        // TODO: Modify not to place tank over tank
        public static List<EnemyTank> GenerateEnemyTanks(int number)
        {
            List<EnemyTank> enemy = new List<EnemyTank>(number);
            Random randomCol = new Random();

            for (int i = 0; i < number; i++)
            {
                int col = randomCol.Next(2, Battlefield.FieldWidth - 4);
                enemy.Add(new EnemyTank(1, col, "red"));   // the enemy tanks appeard in the top
                                
            }

            return enemy;
        }
    }
}
