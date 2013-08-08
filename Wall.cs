using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class Wall
    {
        // every wall has a:
        string direction;
        int bricksCount;
        char[] content;
        int topPosition;
        int leftPosition;
        public static char brick = (char)177;

        // construct the wall
        public Wall(string direction, int lenght, int top, int left)
        {
            this.content = new char[lenght];
            this.bricksCount = lenght;
            this.direction = direction;
            this.topPosition = top;
            this.leftPosition = left;
            ConstructWall();
        }

        // fill the array e.g. the content of the wall
        // and add it to the large playing grid
        private void ConstructWall()
        {
            for (int i = 0; i < this.bricksCount; i++)
            {
                content[i] = brick;   // another brick in the wall ( e.g. a Brick)
            }
        }

        // displayng the wall, drawing himself
        // 1. draw a part of the wall
        // 2. set the huge grid with the content that is drawing
        public void Draw()
        {
            int currentTop = this.topPosition;
            int currentLeft = this.leftPosition;
            Console.ForegroundColor = ConsoleColor.DarkRed;

            if (direction == "hor")
            {
                // draw horizontaly - left -> right
                for (int i = 0; i < bricksCount; i++)
                {
                    Console.SetCursorPosition(currentLeft, currentTop);
                    Console.Write(content[i]);
                    Battlefield.Add(currentTop, currentLeft, content[i]);
                    currentLeft++;  
                }
            }
            else
            {
                // draw verticaly - top -> bottom
                for (int i = 0; i < bricksCount; i++)
                {
                    Console.SetCursorPosition(currentLeft, currentTop);
                    Console.Write(content[i]);
                    currentTop++;  
                }
            }
        }
    }
}
