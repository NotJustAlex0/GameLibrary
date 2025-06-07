using System;
using System.Threading;

namespace GameLibrary.Games
{
    public class ManHangGame : IGame
    {
        public string Name = "ManHang";
        public string filePath = "C:\\Users\\user\\OneDrive\\Desktop\\Mziuri\\C#_Final_Project\\GameLibrary\\GameLibrary\\words.txt";
        public void Play()
        {
            Random rand = new Random();
            int wins = 0;
            int losses = 0;
            char playAgain;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter the name of a person you hate the most and want to kill:");
                string nemesis = Console.ReadLine();

                string word = LoadRandomWord(filePath, rand);
                string guessed = "";
                int mistakes = 0;
                bool win = false;

                do
                {
                    Console.Clear();
                    PrintMessage($"Nemesis: {nemesis}");
                    DrawHangman(mistakes);

                    Console.WriteLine($"+---------------------------------+");
                    Console.WriteLine($"| Lives left till gameover: {9 - mistakes} / 9 |");
                    Console.WriteLine($"+---------------------------------+");

                    PrintAvailableLetters(guessed);
                    Console.WriteLine($"|       Wins: {wins} | Losses: {losses}       |");
                    Console.WriteLine($"+---------------------------------+");

                    PrintMessage("Guess the word");
                    win = PrintWordAndCheckWin(word, guessed);

                    if (win || mistakes >= 9) break;

                    Console.Write("Guess a letter: > ");
                    string input = Console.ReadLine().ToUpper();
                    if (input.Length != 1 || !char.IsLetter(input[0]))
                    {
                        Console.WriteLine("Invalid input! Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }
                    char guess = input[0];
                    if (guessed.Contains(guess))
                    {
                        Console.WriteLine("You already guessed that letter. Press any key...");
                        Console.ReadKey();
                        continue;
                    }
                    guessed += guess;
                    if (!word.Contains(guess)) mistakes++;

                } while (mistakes < 10);

                Console.Clear();
                if (win)
                {
                    PrintMessage("YOU WON!");
                    wins++;
                }
                else
                {
                    PrintMessage("GAME OVER");
                    PrintMessage($"The word was: {word.ToUpper()}");
                    Console.WriteLine($"+---------------------------------+");
                    Console.WriteLine($"    You killed {nemesis} you moron!    ");
                    Console.WriteLine($"+---------------------------------+");
                    losses++;
                }

                Console.Write("Do you want to play again? (Y/N): ");
                playAgain = Console.ReadKey().KeyChar;
                Console.WriteLine();

            } while (char.ToUpper(playAgain) == 'Y');

            Console.WriteLine("Goodbye! Come again");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        private string LoadRandomWord(string path, Random rand)
        {
            if (!File.Exists(path)) return "DEFAULT";
            var lines = File.ReadAllLines(path);
            return lines[rand.Next(lines.Length)].ToUpper();
        }

        static private void PrintMessage(string message, bool top = true, bool bottom = true)
        {
            if (top)
            {
                Console.WriteLine("+---------------------------------+");
                Console.Write("|");
            }
            else Console.Write("|");

            message = message.PadLeft((33 + message.Length) / 2).PadRight(33);
            Console.WriteLine(message + "|");

            if (bottom)
                Console.WriteLine("+---------------------------------+");
        }

        private static void DrawHangman(int guessCount = 0)
        {
            PrintMessage("HANGMAN");

            switch (guessCount)
            {
                case 0:
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    break;

                case 1:
                    PrintMessage("|", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    break;

                case 2:
                    PrintMessage("|", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    break;

                case 3:
                    PrintMessage("|", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("O", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    break;

                case 4:
                    PrintMessage("|", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("O", false, false);
                    PrintMessage("/  ", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    break;

                case 5:
                    PrintMessage("|", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("O", false, false);
                    PrintMessage("/| ", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    break;

                case 6:
                    PrintMessage("|", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("O", false, false);
                    PrintMessage("/|\\", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    break;

                case 7:
                    PrintMessage("|", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("O", false, false);
                    PrintMessage("/|\\", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("", false, false);
                    PrintMessage("", false, false);
                    break;

                case 8:
                    PrintMessage("|", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("O", false, false);
                    PrintMessage("/|\\", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("/", false, false);
                    PrintMessage("", false, false);
                    break;

                case 9:
                    PrintMessage("|", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("O", false, false);
                    PrintMessage("/|\\", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("/ \\", false, false);
                    PrintMessage("", false, false);
                    break;

                default:
                    PrintMessage("|", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("O", false, false);
                    PrintMessage("/|\\", false, false);
                    PrintMessage("|", false, false);
                    PrintMessage("/ \\", false, false);
                    PrintMessage("", false, false);
                    break;
            }
        }


        private void PrintAvailableLetters(string taken)
        {
            PrintMessage("Available letters");
            string AM = "ABCDEFGHIJKLM";
            string NZ = "NOPQRSTUVWXYZ";

            foreach (char c in taken)
            {
                AM = AM.Replace(c.ToString(), " ");
                NZ = NZ.Replace(c.ToString(), " ");
            }

            // just adding spaces inbetween WITOUT TOP OR BOTTOM
            PrintMessage(string.Join(" ", AM.ToCharArray()), false, false);
            PrintMessage(string.Join(" ", NZ.ToCharArray()), false, false);
        }

        private bool PrintWordAndCheckWin(string word, string guessed)
        {
            bool won = true;
            string display = "";
            foreach (char c in word)
            {
                if (guessed.Contains(c)) display += c + " ";
                else { display += "_ "; won = false; }
            }
            PrintMessage(display.Trim(), false);
            return won;
        }
    }
}
