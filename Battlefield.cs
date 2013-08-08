using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public static class Battlefield
    {
        public const int FieldWidth = 60;  // shirochinka
        public const int FieldHeight = 40; // visochinka
        public static char[,] content = new char[FieldHeight, FieldWidth];
        static List<Wall> walls = new List<Wall>();

        // set the battlefield
        public static void SetBattlefield()
        {
            SetDefaultChars();
            Settings();
            CreateWalls();
            // Create enemy tanks
        }

        // set the battlefield settings
        private static  void Settings()
        {
            Console.Title = "Tanks";
            Console.SetWindowSize(FieldWidth, FieldHeight);
            Console.SetBufferSize(FieldWidth, FieldHeight);
            Console.CursorVisible = false;
        }

        // add to the battlefield someting
        public static void Add(int row, int col, char value)
        {
            if (row < FieldHeight && row > 0 && col < FieldWidth && col > 0)
            {
                content[row, col] = value;
            }
            else
            {
                throw new ArgumentException("Invalid playground parameters, out of battlefield boundaries");
            }
        }

        // create a walls for the battlefield, the idea is that it could be composed
        static void CreateWalls()
        {
            // add some walls
            // be careful in adding the walls
            walls.Add(new Wall("hor", 25, 5, 5));
            walls.Add(new Wall("vert", 5, 10, 15));
            walls.Add(new Wall("hor", 10, 5, 40));
            walls.Add(new Wall("vert", 20, 10, 40));
            walls.Add(new Wall("hor", 20, 30, 40));

            // draw them
            foreach (Wall nextWall in walls)
            {
                nextWall.Draw();
            }
        }

        // set everyting to ' '
        private static void SetDefaultChars()
        {
            for (int i = 0; i < content.GetLength(0); i++)
            {
                for (int j = 0; j < content.GetLength(1); j++)
                {
                    content[i, j] = ' ';
                }   
            }
        }

    }
}
