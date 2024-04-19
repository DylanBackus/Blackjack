using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class GameResult
    {
        private Dealer dealer; 
        private List<Player> players; 

        // add nieuw GameResult object met de dealer en spelers
        public GameResult(Dealer dealer, List<Player> players)
        {
            this.dealer = dealer;
            this.players = players;
        }

        // bepaalt de winnaar van het spel op basis van de handwaarden van de dealer en de spelers
        public void DetermineWinner()
        {
            int dealerHandValue = dealer.GetHandValue(); // handvalue van de dealer

            foreach (var player in players)
            {
                int playerHandValue = player.GetHandValue(); // handvalue van players

                if (playerHandValue > 21) // voor die glitch een max toe voegen
                {
                    Console.WriteLine($"{player.Name} heeft te veel! De dealer wint.");
                }
                else if (dealerHandValue > 21 || playerHandValue > dealerHandValue)
                {
                    Console.WriteLine($"{player.Name} wint!");
                }
                else if (playerHandValue == dealerHandValue)
                {
                    Console.WriteLine($"Het is een gelijkspel tussen {player.Name} en de dealer.");
                }
                else
                {
                    Console.WriteLine($"{player.Name} verliest. De dealer wint.");
                }
            }
        }

        // print kaarten van de dealer om de face down kaart te laten zien
        public void PrintDealerCards()
        {
            Console.WriteLine("\nKaarten van de dealer:");
            dealer.PrintCards();
            Console.WriteLine(" ");
        }
    }
}
