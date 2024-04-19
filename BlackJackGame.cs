using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class BlackjackGame
    {
        private readonly Deck deck; //waarde kan niet worden veranderd
        private readonly List<Player> players; 
        private readonly NameGenerator nameGenerator; 
        private Dealer dealer;

        public BlackjackGame()
        {
            nameGenerator = new NameGenerator();
            deck = new Deck(); // maakt deck
            players = new List<Player>(); // list van spelers
        }

        // Start het blackjackspel.
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
            dealer.PrintFirstCard(); 
            DealerAction(); 
            GameResult gameResult = new GameResult(dealer, players); 
            gameResult.PrintDealerCards();
            gameResult.DetermineWinner(); 
        }

        private int GetNumberOfPlayers()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Hoeveel spelers wil je tegen spelen? (1-4)");
                string playerAmountInput = Console.ReadLine();

                if (int.TryParse(playerAmountInput, out int playerAmount)) // controleren of input goed is
                {
                    if (playerAmount >= 1 && playerAmount <= 4)
                    {
                        return playerAmount;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Voer a.u.b. een nummer tussen 1 en 4 in.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ongeldige invoer. Voer a.u.b. een geldig nummer in.");
                    Console.ResetColor();
                }
            }
        }

        // Creëert de spelers gebaseert op input
        private void CreatePlayers(int playerAmount)
        {
            for (int i = 0; i < playerAmount; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Voer de naam in voor Speler {i + 1} {BoldText("Laat leeg voor een willekeurige naam.")}");
                string playerName = Console.ReadLine();
                while (IsPlayerNameDuplicate(playerName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Naam bestaat al. Voer a.u.b. een andere naam in:");
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

        // Print namen van players en bet enz
        private void PrintPlayerNames()
        {
            Console.WriteLine();
            Console.WriteLine("Je speelt tegen de volgende spelers:");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name} - Inzet: {player.BetAmount} euro");
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

            dealer.ReceiveCard(deck.DrawCard(), true); // eerste kaart (face up)
            dealer.ReceiveCard(deck.DrawCard(), false); // tweede kaart (face down)
        }

        // Drukt de kaarten van de spelers af
        private void PrintPlayerCards()
        {
            foreach (var player in players)
            {
                Console.WriteLine("");
                Console.WriteLine(BoldText($"{player.Name}'s Kaarten:"));
                Console.WriteLine($"{player.Name} - Inzet: {player.BetAmount} euro");
                player.PrintHand();
                Console.WriteLine();
            }
        }

        private void DealerAction()
        {
            // Spelers neemt random beslissingen om kaarten te nemen of te passen
            foreach (var player in players)
            {
                while (player.RandomDecision())
                {
                    player.ReceiveCard(deck.DrawCard());
                    Console.WriteLine($"{player.Name} neemt nog een kaart.");
                    PrintPlayerCards();
                    if (player.GetHandValue() > 21)
                    {
                        Console.WriteLine($"{player.Name} heeft verloren!");
                        break;
                    }
                }
                Console.WriteLine($"{player.Name} past.");
            }

            // Handelingen van de dealer
            Console.WriteLine("\nBeurt van de dealer:");
            while (dealer.GetHandValue() < 17)
            {
                Console.WriteLine("Dealer neemt een kaart.");
                dealer.ReceiveCard(deck.DrawCard(), true);
            }
            Console.WriteLine("Dealer past.\n");
        }

        // Controleert of de geinpute playername al bestaat
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
            return "\u001b[1m" + text + "\u001b[0m"; // Bold
        }
    }
}
