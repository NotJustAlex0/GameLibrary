using System;
using System.Threading;

namespace GameLibrary.Games
{
    public class RockPaperScissorsGame : IGame
    {
        private int playerWins = 0;
        private int botWins = 0;
        private int draws = 0;
        private Random random = new Random();

        public void Play()
        {
            do
            {
                playerWins = 0;
                botWins = 0;
                draws = 0;

                while (playerWins < 3 && botWins < 3)
                {
                    Console.Clear();
                    PrintScore();

                    Console.WriteLine("Choose:");
                    Console.WriteLine("1. Rock");
                    Console.WriteLine("2. Paper");
                    Console.WriteLine("3. Scissors");
                    Console.Write("Your choice: ");
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out int playerChoice) || playerChoice < 1 || playerChoice > 3)
                    {
                        Console.WriteLine("Invalid input. Press any key to try again.");
                        Console.ReadKey();
                        continue;
                    }

                    int botChoice = random.Next(1, 4);

                    Console.Clear();
                    PrintScore();

                    Console.WriteLine("You chose:");
                    PrintHand(playerChoice);
                    Thread.Sleep(500);

                    Console.WriteLine("Bot chose:");
                    PrintHand(botChoice);
                    Thread.Sleep(500);

                    if (playerChoice == botChoice)
                    {
                        Console.WriteLine("It's a draw!");
                        draws++;
                    }
                    else if ((playerChoice == 1 && botChoice == 3) ||
                             (playerChoice == 2 && botChoice == 1) ||
                             (playerChoice == 3 && botChoice == 2))
                    {
                        Console.WriteLine("You win this round!");
                        playerWins++;
                    }
                    else
                    {
                        Console.WriteLine("Bot wins this round!");
                        botWins++;
                    }

                    Thread.Sleep(3000);
                }

                Console.Clear();
                PrintScore();
                Console.WriteLine(playerWins == 3 ? "You won the game!" : "Bot won the game!");
                Console.Write("\nWould you like to play again? (y/n): ");

            } while (Console.ReadLine().Trim().ToLower() == "y");

            Console.WriteLine("Thanks for playing! Press any key to return to menu.");
            Console.ReadKey();
        }

        private void PrintScore()
        {
            Console.WriteLine($"Score - You: {playerWins} | Bot: {botWins} | Draws: {draws}\n");
        }

        private void PrintHand(int choice)
        {
            switch (choice)
            {
                case 1: 
                    Console.WriteLine(@"
    _______
---'   ____)
      (_____)
      (_____)
      (____)
---.__(___)
");
                    break;

                case 2:
                    Console.WriteLine(@"
    _______
---'   ____)____
          ______)
          _______)
         _______)
---.__________)
");
                    break;

                case 3:
                    Console.WriteLine(@"
    _______
---'   ____)____
          ______)
       __________)
      (____)
---.__(___)
");
                    break;
            }
        }
    }
}
