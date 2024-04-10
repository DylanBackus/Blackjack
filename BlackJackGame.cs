using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class BlackjackGame
    {
        private int playerAmount;
        private Deck deck;

        public void Start()
        {
            bool isValidInput;

            do
            {
                Console.WriteLine();
                Console.WriteLine("How many players do you want to play against? (1-4)");
                string playerAmountInput = Console.ReadLine();

                isValidInput = ValidatePlayerAmountInput(playerAmountInput);

            } while (!isValidInput);

            string[] playerNames = GeneratePlayerNames(playerAmount);
            Console.WriteLine();
            Console.WriteLine("You are playing against the following " + playerAmount + " Players: " + string.Join(", ", playerNames));
            Console.WriteLine();

            // print het geshufflede deck
            deck = new Deck(); // Nieuw deck
            deck.InitializeDeck(); // Initialiseren
            deck.ShuffleDeck(); // shuffelen
            Console.WriteLine(BoldText("Shuffled deck:"));
            deck.PrintDeck();
        }

        private bool ValidatePlayerAmountInput(string input)
        {
            if (int.TryParse(input, out playerAmount))
            {
                if (playerAmount >= 1 && playerAmount <= 4)
                {
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a number between 1 and 4.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid number.");
                Console.ResetColor();
            }

            return false;
        }

        private string[] GeneratePlayerNames(int playerAmount)
        {
            string[] availableNames = { "Robbert", "Melvin", "Bart", "Erik", "Marco", "Davor", "Niek" };
            Random rng = new Random();
            List<string> chosenNames = new List<string>(); // Niet dubbele namen
            string[] playerNames = new string[playerAmount];

            for (int i = 0; i < playerAmount; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = rng.Next(availableNames.Length);
                } while (chosenNames.Contains(availableNames[randomIndex])); // Kies nieuwe index als die al gekozen is

                playerNames[i] = availableNames[randomIndex];
            }

            return playerNames;
        }
        public string BoldText(int number)
        {
            return "\u001b[1m" + number + "\u001b[0m"; // \u001b[1m = Bold, \u001b[0m Verwijderd de stijl
        }
    }
}
