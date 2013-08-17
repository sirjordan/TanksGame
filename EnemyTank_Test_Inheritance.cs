using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace Tanks
{
    public class EnemyTank_Test_Inheritance : Tank
    {
        // <Enemy constructor>
        public EnemyTank_Test_Inheritance(int row, int col)
            : base(row, col)
        {
            
        }

        // <Methods>
        // Enemy AI movement
        // Overrided Move() method
        public void Move()
        {
            Random ran = new Random();
            direction d = RandomDirection();
            int moves = ran.Next(1, 51);
            int speed = 100;
            if (d == direction.right)
            {
                for (int i = 0; i < moves; i++)
                {
                    if (!body.Equals(bodyRight)) // if direction is not Right
                    {
                        body = bodyRight;
                    }
                    else if (this.leftPos < Battlefield.FieldWidth - 4)
                    {
                        Delete(leftPos, topPos); // delete the last position of the tank
                        if (FreeCells(d))
                        {
                            leftPos++; // the topLeft left
                        }
                    }
                    PlaceTank(leftPos, topPos); // draw the tank
                    Thread.Sleep(speed);
                }

            }
            else if (d == direction.down)
            {
                for (int i = 0; i < moves; i++)
                {
                    if (!body.Equals(bodyDown))
                    {
                        body = bodyDown;
                    }
                    else if (this.topPos < Battlefield.FieldHeight - 4)
                    {
                        Delete(leftPos, topPos);
                        if (FreeCells(d))
                        {
                            topPos++;
                        }
                    }
                    PlaceTank(leftPos, topPos);
                    Thread.Sleep(speed);
                }

            }
            else if (d == direction.left)
            {
                for (int i = 0; i < moves; i++)
                {
                    if (!body.Equals(bodyLeft))
                    {
                        body = bodyLeft;
                    }
                    else if (this.leftPos > 1)
                    {
                        Delete(leftPos, topPos);
                        if (FreeCells(d))
                        {
                            leftPos--;
                        }
                    }
                    PlaceTank(leftPos, topPos);
                    Thread.Sleep(speed);
                }

            }
            else // if direction is UP
            {
                for (int i = 0; i < moves; i++)
                {
                    if (!body.Equals(bodyUp))
                    {
                        body = bodyUp;
                    }
                    else if (this.topPos > 1)
                    {
                        Delete(leftPos, topPos);
                        if (FreeCells(d))
                        {
                            topPos--;
                        }
                    }
                    PlaceTank(leftPos, topPos);
                    Thread.Sleep(speed);
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

    }
}
