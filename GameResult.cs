using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public class GameResult
    {
        private Dealer dealer;
        private List<Player> players;

        public GameResult(Dealer dealer, List<Player> players)
        {
            this.dealer = dealer;
            this.players = players;
        }

        public void DetermineWinner()
        {
            int dealerHandValue = dealer.GetHandValue();

            foreach (var player in players)
            {
                int playerHandValue = player.GetHandValue();

                if (playerHandValue > 21)
                {
                    Console.WriteLine($"{player.Name} busted! Dealer wins.");
                }
                else if (dealerHandValue > 21 || playerHandValue > dealerHandValue)
                {
                    Console.WriteLine($"{player.Name} wins!");
                }
                else if (playerHandValue == dealerHandValue)
                {
                    Console.WriteLine($"It's a tie between {player.Name} and the dealer.");
                }
                else
                {
                    Console.WriteLine($"{player.Name} loses. Dealer wins.");
                }
            }
        }
    }
}
