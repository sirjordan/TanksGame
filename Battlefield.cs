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

        public static void UpdateSystemField() 
        {
            Console.SetCursorPosition(FieldWidth + 6, 18);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("SCORE: " + GameEngine.playerTank.TanksKilled);
            Console.SetCursorPosition(FieldWidth + 6, 19);
            Console.Write("LIVES: " + GameEngine.playerTank.life);
        }


        // set the battlefield settings
        private static  void Settings()
        {
            Console.Title = "Tanks";
            Console.SetWindowSize(FieldWidth+20, FieldHeight);
            
            for (int i = 0; i < FieldHeight; i++)
            {
                Console.SetCursorPosition(FieldWidth, i);
                Console.Write("||");
            }

            Console.SetCursorPosition(FieldWidth + 5, 3);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("T A N K S");
            Console.SetCursorPosition(FieldWidth + 5, 4);
            Console.Write("---------");

            Console.SetCursorPosition(FieldWidth + 6, 18);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("SCORE: " + GameEngine.playerTank.TanksKilled);

            Console.SetCursorPosition(FieldWidth + 6, 19);
            Console.Write("LIVES: " + GameEngine.playerTank.life);
             
            Console.SetBufferSize(FieldWidth + 20, FieldHeight);
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
            walls.Add(new Wall("vert", 5, 35, 25));
            walls.Add(new Wall("vert", 5, 35, 35));
            walls.Add(new Wall("hor", 10, 35, 25));
            walls.Add(new Wall("hor", 20, 25, 20));
            walls.Add(new Wall("hor", 30, 10, 15));
            

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

        // fix the * bug
        public static void ClearBug()
        {
            for (int i = 0; i < content.GetLength(0); i++)
            {
                for (int j = 0; j < content.GetLength(1); j++)
                {
                    if (content[i, j] == ' ')
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(' ');
                    }
                }
            }
        }
    }
}
