using System;
using GameLibrary;
using GameLibrary.Games; // Adjust if your namespace is different

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("|   Welcome to GameLibrary!   |");
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("1. Play ManHang");
            Console.WriteLine("2. Play Tic Tac Toe");
            Console.WriteLine("3. Play Rock Paper Scissors");
            Console.WriteLine("4. SnakeGame");
            Console.WriteLine("5. Sokoban");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            string input = Console.ReadLine();
            IGame game ;

            switch (input)
            {
                case "1":
                    game = new ManHangGame();
                    break;
                case "2":
                    game = new TicTacToeGame();
                    break;
                case "3":
                    game = new RockPaperScissorsGame();
                    break;
                case "4":
                    game = new SnakeGame();
                    break;
                case "5":
                    game = new SokobanGame();
                    break;

                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again.");
                    Console.ReadKey();
                    continue;
            }

            Console.Clear();
            game.Play();
        }
    }
}
