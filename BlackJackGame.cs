using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class BlackjackGame
    {
        private int playerAmount;
        private Deck deck;
        private List<Player> players;
        private NameGenerator nameGenerator;

        public BlackjackGame()
        {
            nameGenerator = new NameGenerator();
        }

        public void Start()
        {
            bool isValidInput;

            do
            {
                Console.WriteLine();
                Console.WriteLine("How many players do you want to play against? (1-4)");
                string playerAmountInput = Console.ReadLine(); // hoeveel spelers

                isValidInput = ValidatePlayerAmountInput(playerAmountInput); // heeft dealer 1-4 ingegeven

            } while (!isValidInput);

            players = new List<Player>();
            for (int i = 0; i < playerAmount; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Enter name for Player {i + 1} {BoldText("Leave blank for a random name.")}");
                string playerName = Console.ReadLine(); // zelf naam geven
                while (IsPlayerNameDuplicate(playerName))
                {
                    Console.WriteLine("Name already exists. Please enter a different name:");
                    playerName = Console.ReadLine();
                }

                players.Add(new Player(playerName != "" ? playerName : null, nameGenerator)); // player object aanmaken
            }

            Console.WriteLine();
            Console.WriteLine("You are playing against the following players:");
            foreach (var player in players)
            {
                Console.WriteLine(player.Name);
            }
            Console.WriteLine();

            deck = new Deck(); // Nieuw deck
            deck.InitializeDeck(); // Initialiseren
            deck.ShuffleDeck(); // shuffelen
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

        private bool IsPlayerNameDuplicate(string name)
        {
            foreach (var player in players)
            {
                if (player.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public string BoldText(string number)
        {
            return "\u001b[1m" + number + "\u001b[0m"; // \u001b[1m = Bold, \u001b[0m Verwijderd de stijl
        }

    }
}
