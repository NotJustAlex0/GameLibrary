using System;
using System.Collections.Generic;
using System.Threading;

namespace GameLibrary.Games
{
    public class SnakeGame : IGame
    {
        private const int Width = 34;
        private const int Height = 20;
        private List<(int x, int y)> snake = new List<(int x, int y)>();
        private (int x, int y) apple;

        private string direction = "RIGHT";
        
        private int speed = 200;
        private Random random = new Random();

        private int score = 0;
        private int highScore = 0;

        private bool gameOver = false;
        private bool gameStarted = false;

        public void Play()
        {
            Console.CursorVisible = false;

            do
            {
                Console.Clear();
                InitializeGame();

                DrawBoard();
                Console.SetCursorPosition(0, Height + 4);
                Console.WriteLine("Press any WASD key to start...");
                WaitForInitialInput();

                while (!gameOver)
                {
                    DrawBoard();
                    while (Console.KeyAvailable)
                    {
                        Input(Console.ReadKey(true).Key);
                    }
                    Logic();
                    Thread.Sleep(speed);
                }

                if (score > highScore)
                    highScore = score;

                Console.SetCursorPosition(0, Height + 4);
                Console.ResetColor();
                Console.WriteLine($"Game Over! Score: {score}");
                Console.Write("Would you like to play again? (y/n): ");
            }
            while (Console.ReadKey(true).Key == ConsoleKey.Y);

            Console.CursorVisible = true;
        }

        private void WaitForInitialInput()
        {
            while (!gameStarted)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.W || 
                        key == ConsoleKey.A || 
                        key == ConsoleKey.S || 
                        key == ConsoleKey.D)
                    {
                        Input(key);
                        gameStarted = true;
                    }
                }
            }
        }
        private void Input(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    if (direction != "DOWN") direction = "UP";
                    break;
                case ConsoleKey.S:
                    if (direction != "UP") direction = "DOWN";
                    break;
                case ConsoleKey.A:
                    if (direction != "RIGHT") direction = "LEFT";
                    break;
                case ConsoleKey.D:
                    if (direction != "LEFT") direction = "RIGHT";
                    break;
            }
        }
        private void InitializeGame()
        {
            snake.Clear();

            snake.Add((Width / 2, Height / 2));
            direction = "RIGHT";
            speed = 200;
            score = 0;

            gameOver = false;
            gameStarted = false;
            GenerateApple();
        }
        private void DrawBoard()
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x == 0 || x == Width - 1 || 
                        y == 0 || y == Height - 1)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("  ");
                    }
                    else if (x == apple.x && y == apple.y)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("  ");
                    }
                    else if (snake.Contains((x, y)))
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write("  ");
                    }
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.WriteLine($"Score: {score}");
            Console.WriteLine($"All-Time High Score: {highScore}");
        }        
        private void GenerateApple()
        {
            do {
                apple = (random.Next(1, Width - 1), 
                         random.Next(1, Height - 1));
            } 
            while (snake.Contains(apple));
        }
        private void Logic()
        {
            var head = snake[0];
            (int x, int y) newHead = head;

            switch (direction)
            {
                case "UP": newHead = (head.x, head.y - 1); 
                    break;
                case "DOWN": newHead = (head.x, head.y + 1); 
                    break;
                case "LEFT": newHead = (head.x - 1, head.y); 
                    break;
                case "RIGHT": newHead = (head.x + 1, head.y); 
                    break;
            }

            if (newHead.x <= 0 || newHead.x >= Width - 1 || 
                newHead.y <= 0 || newHead.y >= Height - 1 || 
                snake.Contains(newHead))
            {
                gameOver = true;
                return;
            } 

            snake.Insert(0, newHead);

            if (newHead == apple)
            {
                GenerateApple();
                if (speed > 50) 
                    speed -= 10;
                score++;
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }
    }
}
