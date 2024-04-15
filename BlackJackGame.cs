using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class BlackjackGame
    {
        private readonly Deck deck;
        private readonly List<Player> players;
        private readonly NameGenerator nameGenerator;
        private Dealer dealer;

        public BlackjackGame()
        {
            nameGenerator = new NameGenerator();
            deck = new Deck();
            players = new List<Player>();
        }

        public void Start()
        {
            int playerAmount = GetNumberOfPlayers();
            CreatePlayers(playerAmount);
            CreateDealer();
            PrintPlayerNames();
            deck.InitializeDeck();
            deck.ShuffleDeck();
            DealInitialCards();
            PrintPlayerCards();
            Console.WriteLine(BoldText("Dealer's Cards:"));
            dealer.PrintCards();
            DealerAction();
        }

        private int GetNumberOfPlayers()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("How many players do you want to play against? (1-4)");
                string playerAmountInput = Console.ReadLine();

                if (int.TryParse(playerAmountInput, out int playerAmount))
                {
                    if (playerAmount >= 1 && playerAmount <= 4)
                    {
                        return playerAmount;
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
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.ResetColor();
                }
            }
        }

        private void CreatePlayers(int playerAmount)
        {
            for (int i = 0; i < playerAmount; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Enter name for Player {i + 1} {BoldText("Leave blank for a random name.")}");
                string playerName = Console.ReadLine();
                while (IsPlayerNameDuplicate(playerName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Name already exists. Please enter a different name:");
                    Console.ResetColor();
                    playerName = Console.ReadLine();
                }

                players.Add(new Player(playerName != "" ? playerName : null, nameGenerator));
            }
        }

        private void CreateDealer()
        {
            dealer = new Dealer("Dealer");
        }

        private void PrintPlayerNames()
        {
            Console.WriteLine();
            Console.WriteLine("You are playing against the following players:");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name} - Bet: {player.BetAmount} euro");
            }
            Console.WriteLine();
        }

        private void DealInitialCards()
        {
            foreach (var player in players)
            {
                player.ReceiveCard(deck.DrawCard());
                player.ReceiveCard(deck.DrawCard());
            }

            dealer.ReceiveCard(deck.DrawCard(), true); // Face up card
            dealer.ReceiveCard(deck.DrawCard(), false); // Face down card
        }

        private void PrintPlayerCards()
        {
            foreach (var player in players)
            {
                Console.WriteLine("");
                Console.WriteLine(BoldText($"{player.Name}'s Cards:"));
                Console.WriteLine($"{player.Name} - Bet: {player.BetAmount} euro");
                player.PrintHand();
                Console.WriteLine();
            }
        }

        private void DealerAction()
        {
            while (dealer.ShouldHit())
            {
                dealer.ReceiveCard(deck.DrawCard(), true);
                Console.WriteLine("Dealer takes another card.");
            }

            Console.WriteLine("Dealer stands.");
            Console.WriteLine("Dealer's Cards:");
            dealer.PrintCards();
            Console.WriteLine($"Dealer's Hand Value: {dealer.GetHandValue()}");
            Console.WriteLine("");


            // Players take random decisions to hit or stand
            foreach (var player in players)
            {
                while (player.RandomDecision())
                {
                    player.ReceiveCard(deck.DrawCard());
                    Console.WriteLine($"{player.Name} takes another card.");
                    PrintPlayerCards();
                    if (player.GetHandValue() > 21)
                    {
                        Console.WriteLine($"{player.Name} busted!");
                        break;
                    }
                }
                Console.WriteLine($"{player.Name} stands.");
            }
            GameResult resultManager = new GameResult(dealer, players);
            resultManager.DetermineWinner();
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

        private string BoldText(string text)
        {
            return "\u001b[1m" + text + "\u001b[0m"; // \u001b[1m = Bold, \u001b[0m removes the style
        }
    }
}

// 17 aan het begin
// Cards aanmaken