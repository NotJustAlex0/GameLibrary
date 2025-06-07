using System;
using System.Threading;

namespace GameLibrary.Games
{
    public class TicTacToeGame : IGame
    {
        private char[] board;
        private char playerSymbol = 'X';
        private char botSymbol = 'O';
        private Random random = new Random();

        private int playerScore = 0;
        private int botScore = 0;
        private int draws = 0;

        public void Play()
        {
            do
            {
                board = new char[9];
                for (int i = 0; i < board.Length; i++)
                    board[i] = ' ';

                char winner;
                while (true)
                {
                    Console.Clear();
                    PrintScore();
                    PrintBoard();

                    Console.Write("Enter your move (1-9): ");
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out int position) || position < 1 || position > 9 || board[position - 1] != ' ')
                    {
                        Console.WriteLine("Invalid move. Press any key to try again.");
                        Console.ReadKey();
                        continue;
                    }

                    board[position - 1] = playerSymbol;

                    if (CheckWinner(out winner))
                    {
                        Console.Clear();
                        PrintScore();
                        PrintBoard();
                        Console.WriteLine($"{winner} wins!");
                        if (winner == playerSymbol) playerScore++;
                        else botScore++;
                        break;
                    }

                    if (IsBoardFull())
                    {
                        Console.Clear();
                        PrintScore();
                        PrintBoard();
                        Console.WriteLine("It's a draw!");
                        draws++;
                        break;
                    }

                    Thread.Sleep(700); // Delay for bot move

                    int botMove = GetBotMove();
                    board[botMove] = botSymbol;

                    if (CheckWinner(out winner))
                    {
                        Console.Clear();
                        PrintScore();
                        PrintBoard();
                        Console.WriteLine($"{winner} wins!");
                        if (winner == playerSymbol) playerScore++;
                        else botScore++;
                        break;
                    }

                    if (IsBoardFull())
                    {
                        Console.Clear();
                        PrintScore();
                        PrintBoard();
                        Console.WriteLine("It's a draw!");
                        draws++;
                        break;
                    }
                }

                Console.Write("Play again? (y/n): ");
            } while (Console.ReadLine().Trim().ToLower() == "y");

            Console.WriteLine("Press any key to return to menu.");
            Console.ReadKey();
        }

        private void PrintScore()
        {
            Console.WriteLine($"Score - You: {playerScore} | Bot: {botScore} | Draws: {draws}\n");
        }

        private void PrintBoard()
        {
            string horizontalBorder = "+-----------+-----------+-----------+";

            for (int row = 0; row < 3; row++)
            {
                Console.WriteLine(horizontalBorder);

                for (int line = 0; line < 5; line++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        int index = row * 3 + col;
                        if (line == 0 || line == 4)
                        {
                            Console.Write("|           ");
                        }
                        else
                        {
                            PrintBigSymbolLine(board[index], line);
                        }
                    }
                    Console.WriteLine("|");
                }
            }
            Console.WriteLine(horizontalBorder);
        }

        private void PrintBigSymbolLine(char symbol, int line)
        {
            if (symbol == ' ')
            {
                Console.Write("|           ");
                return;
            }

            Console.Write("| ");

            switch (symbol)
            {
                case 'X':
                    PrintBigXLine(line);
                    break;
                case 'O':
                    PrintBigOLine(line);
                    break;
                default:
                    Console.Write("   ?       ");
                    break;
            }
            Console.Write(" ");
        }

        private void PrintBigXLine(int line)
        {
            switch (line)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" \\   /   ");
                    break;
                case 2:
                    Console.Write("   X     ");
                    break;
                case 3:
                    Console.Write(" /   \\   ");
                    Console.ResetColor();
                    break;
            }
        }

        private void PrintBigOLine(int line)
        {
            switch (line)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("  ___    ");
                    break;
                case 2:
                    Console.Write(" /   \\   ");
                    break;
                case 3:
                    Console.Write(" \\___/   ");
                    Console.ResetColor();
                    break;
            }
        }

        private int GetBotMove()
        {
            List<int> availableMoves = new List<int>();
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == ' ')
                    availableMoves.Add(i);
            }
            return availableMoves[random.Next(availableMoves.Count)];
        }

        private bool IsBoardFull()
        {
            foreach (char c in board)
            {
                if (c == ' ') return false;
            }
            return true;
        }

        private bool CheckWinner(out char winner)
        {
            int[,] winConditions = new int[,] {
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
                { 0, 4, 8 }, { 2, 4, 6 }
            };

            for (int i = 0; i < winConditions.GetLength(0); i++)
            {
                int a = winConditions[i, 0];
                int b = winConditions[i, 1];
                int c = winConditions[i, 2];

                if (board[a] != ' ' && board[a] == board[b] && board[b] == board[c])
                {
                    winner = board[a];
                    return true;
                }
            }

            winner = ' ';
            return false;
        }
    }
}
