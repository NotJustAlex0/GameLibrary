using System;

namespace GameLibrary.Games
{
    public class SokobanGame : IGame
    {
        private char[][,] levels;
        private int currentLevel = 0;
        private char[,] map;
        private char[,] originalMap;
        private int playerX, playerY;
        private bool gameRunning = true;

        public void Play()
        {
            InitializeLevels();
            Console.Clear();
            Console.CursorVisible = false;

            while (currentLevel < levels.Length)
            {
                map = CloneLevel(levels[currentLevel]);
                originalMap = CloneLevel(map);
                SetPlayerPosition();
                gameRunning = true;

                while (gameRunning)
                {
                    Console.Clear();
                    DrawMap();
                    HandleInput();

                    if (CheckWin())
                    {
                        Console.Clear();
                        DrawMap();
                        Console.WriteLine("You completed level {0}! Press any key to continue.", currentLevel + 1);
                        Console.ReadKey(true);
                        currentLevel++;
                        break;
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("Congratulations! You completed all levels. Press any key to return to menu.");
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }

        private void InitializeLevels()
        {
            levels = new char[3][,];

            string[] level1 = new string[]
            {
                "#########",
                "#   .   #",
                "#   B   #",
                "#       #",
                "#   @   #",
                "#########"
            };
            levels[0] = ConvertToMap(level1);

            string[] level2 = new string[]
            {
                "###############",
                "#             #",
                "#   .B. B.    #",
                "#     B       #",
                "#         @   #",
                "###############"
            };
            levels[1] = ConvertToMap(level2);

            string[] level3 = new string[]
            {
                "#####################",
                "#         #         #",
                "#   B B   #   . .   #",
                "#   B .   #   . B   #",
                "#         #         #",
                "#   #######   ####  #",
                "#   @               #",
                "#                   #",
                "#####################"
            };
            levels[2] = ConvertToMap(level3);
        }
        private void SetPlayerPosition()
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == '@')
                    {
                        playerX = x;
                        playerY = y;
                        map[y, x] = ' ';
                        return;
                    }
                }
            }
        }
        private char[,] ConvertToMap(string[] lines)
        {
            char[,] newMap = new char[lines.Length, lines[0].Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    newMap[y, x] = lines[y][x];
                }
            }

            return newMap;
        }
        private void DrawMap()
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (x == playerX && 
                        y == playerY)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("@");
                        Console.ResetColor();
                    }
                    else
                    {
                        switch (map[y, x])
                        {
                            case '#': Console.ForegroundColor = ConsoleColor.DarkGray; break;
                            case '.': Console.ForegroundColor = ConsoleColor.Cyan; break;
                            case 'B': Console.ForegroundColor = ConsoleColor.Magenta; break;
                            case '*': Console.ForegroundColor = ConsoleColor.Green; break;
                        }
                        Console.Write(map[y, x]);
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Press R to restart the level. Press ESC to quit.");
        }
        private bool CheckWin()
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == '.') 
                        return false;
                }
            }
            return true;
        }
        private char[,] CloneLevel(char[,] level)
        {
            int rows = level.GetLength(0);
            int cols = level.GetLength(1);

            char[,] copy = new char[rows, cols];

            for (int y = 0; y < rows; y++)
                for (int x = 0; x < cols; x++)
                    copy[y, x] = level[y, x];

            return copy;
        }
        private void HandleInput()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            int dx = 0, dy = 0;

            switch (key.Key)
            {
                case ConsoleKey.W: dy = -1; 
                    break;
                case ConsoleKey.S: dy = 1; 
                    break;
                case ConsoleKey.A: dx = -1; 
                    break;
                case ConsoleKey.D: dx = 1; 
                    break;

                case ConsoleKey.Escape:
                    gameRunning = false;
                    return;

                case ConsoleKey.R:
                    map = CloneLevel(originalMap);
                    SetPlayerPosition();
                    return;
            }

            int newX = playerX + dx;
            int newY = playerY + dy;

            if (map[newY, newX] == '#') 
                return;

            if (map[newY, newX] == 'B' || 
                map[newY, newX] == '*')
            {
                int nextX = newX + dx;
                int nextY = newY + dy;
                if (map[nextY, nextX] == ' ' || 
                    map[nextY, nextX] == '.')
                {
                    map[nextY, nextX] = map[nextY, nextX] == '.' ? '*' : 'B';
                    map[newY, newX] = map[newY, newX] == '*' ? '.' : ' ';
                    playerX = newX;
                    playerY = newY;
                }
            }
            else
            {
                playerX = newX;
                playerY = newY;
            }
        }

    }
}
