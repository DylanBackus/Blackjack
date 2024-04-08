using System;

namespace Blackjack
{
    public class BlackjackGame
    {
        private int playerAmount;
        private Deck deck;

        public void Start()
        {
            bool isValidInput;

            // Initialisatie van het decks
            deck = new Deck();
            deck.InitializeDeck();
            deck.ShuffleDeck();

            do
            {
                Console.WriteLine();
                Console.WriteLine("How many players do you want to play against? (1-7)");
                string? playerAmountInput = Console.ReadLine();

                isValidInput = ValidatePlayerAmountInput(playerAmountInput);

            } while (!isValidInput);

            Console.WriteLine();
            Console.WriteLine("You are playing against " + playerAmount + " players");
        }

        private bool ValidatePlayerAmountInput(string? input)
        {
            if (int.TryParse(input, out playerAmount))
            {
                if (playerAmount >= 1 && playerAmount <= 7)
                {
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a number between 1 and 7.");
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
    }
}