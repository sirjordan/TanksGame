using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class Tank
    {
        int life = 3;
        public enum direction { up, down, left, right };
        public int leftPos; // center of the tank
        public int topPos; // center of the tank
        private char[,] body = new char[3, 3] // regular tank dimentions, and geometry
        {
            {' ','*',' '},
            {'*','*','*'},
            {'*','*','*'}
        };
        // I KNOW it is stupid but for now it is the simplest !
        // the idea is just to rotate the array, but it cause difficulties
        // for now it is this
        private char[,] bodyUp = new char[3, 3]
        {
            {' ','*',' '},
            {'*','*','*'},
            {'*','*','*'}
        };
        private char[,] bodyDown = new char[3, 3]
        {
            {'*','*','*'},
            {'*','*','*'},
            {' ','*',' '}
        };
        private char[,] bodyRight = new char[3, 3]
        {
            {'*','*',' '},
            {'*','*','*'},
            {'*','*',' '}
        };
        private char[,] bodyLeft = new char[3, 3]
        {
            {' ','*','*'},
            {'*','*','*'},
            {' ','*','*'}
        };
        // end of stupidness

        // create a tank (constructor)
        public Tank(int row, int col)
        {
            this.leftPos = col;
            this.topPos = row;
            PlaceTank(leftPos, topPos);
        }

        // place a tank where it is
        public void PlaceTank(int lt, int tp) // left/top
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(lt - 1, tp - 1); // topleft for the tank
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // write to the battle array
                    Battlefield.Add(tp + i, lt + j, body[i, j]); // i know it is Ununderstandable, hope this works

                    // draw to the console
                    Console.SetCursorPosition(lt + j, tp + i);
                    Console.Write(body[i, j]);
                }
            }
        }

        // moving
        // first rotate the tank, than move if rotated
        // TODO: if there's a brick or sonething, not to move
        public void Move(direction d)
        {
            if (d == direction.right)
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
            }
            else if (d == direction.down)
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
            }
            else if (d == direction.left)
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
            }
            else // if direction is UP
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
            }
        }

        // rotating 90 degree clockword
        public char[,] Rotate(char[,] input, int iterations) // iterations -> how mani time to rotate
        {
            int rows = input.GetLength(0);
            int cols = input.GetLength(1);
            char[,] rotated = new char[cols, rows]; // yes it is reversed height and width for NonScquare arrays

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    rotated[i, j] = input[j, i];
                }
            }

            // recursively rotate
            for (int recrusive = 1; recrusive < iterations; recrusive++)
            {
                rotated = Rotate(rotated, 1);
            }

            return rotated;
        }

        // delete tank from the fields
        public void Delete(int lt, int tp)
        {
            Console.SetCursorPosition(lt - 1, tp - 1); // topleft for the tank
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // write to the battle array
                    Battlefield.Add(tp + i, lt + j, ' '); // i know it is Ununderstandable, hope this works

                    // draw to the console
                    Console.SetCursorPosition(lt + j, tp + i);
                    Console.Write(' ');
                }
            }
        }

        // is free the next cell to move
        bool FreeCells(direction d)
        {
            bool result = false;
            int row = topPos;
            int col = leftPos;
            if (d.Equals(direction.right))
            {
                result = Battlefield.content[row + 1, col + 4] == ' ' && Battlefield.content[row, col + 4] == ' ' &&
                    Battlefield.content[row + 2, col + 4] == ' ';
            }
            else if (d.Equals(direction.left))
            {
                result = Battlefield.content[row + 1, col - 2] == ' ' && Battlefield.content[row, col - 2] == ' ' &&
                    Battlefield.content[row + 2, col - 2] == ' ';
            }
            else if (d.Equals(direction.up))
            {
                result = Battlefield.content[row - 1, col] == ' ' && Battlefield.content[row - 1, col + 1] == ' ' &&
                    Battlefield.content[row - 1, col + 2] == ' ';
            }
            else // direction down
            {
                result = Battlefield.content[row + 3, col] == ' ' && Battlefield.content[row + 3, col + 1] == ' ' &&
                    Battlefield.content[row + 3, col + 2] == ' ';
            }
            return result;
        }

        // <Properties>
        // TODO: Properties for all the Fields
        // returns the tanks current direction
        public direction Direction
        {
            get
            {
                if (this.body.Equals(bodyDown))
                {
                    return direction.down;
                }
                else if (this.body.Equals(bodyLeft))
                {
                    return direction.left;
                }
                else if (this.body.Equals(bodyRight))
                {
                    return direction.right;
                }
                else
                {
                    return direction.up;
                }
            }
        }
    }
}
