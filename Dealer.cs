using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Dealer
    {
        public string Name { get; private set; }
        private List<string> hand; // Hand van de dealer

        public Dealer(string name = "Dealer")
        {
            Name = name;
            hand = new List<string>(); //hand van de dealer
        }

        public void ReceiveCard(string card, bool faceUp)
        {
            hand.Add(card);
            if (faceUp)
            {
                Console.WriteLine($"{card}"); //faced down kaart
            }
            else
            {
                Console.WriteLine($"Face down kaart"); // face up kaart
            }
        }

        // print eerste kaart van de dealer af
        public void PrintFirstCard()
        {
            Console.WriteLine($"Eerste kaart van de dealer: {hand[0]}");
        }

        // Neemt kaarten voor de dealer van het deck tot waarde van de hand 17 of meer is.,
        public void TakeCards(Deck deck)
        {
            while (GetHandValue() < 17)
            {
                ReceiveCard(deck.DrawCard(), true);
            }

            Console.WriteLine("Dealer past.");
        }

        // print kaarten van de dealer af
        public void PrintCards()
        {
            foreach (var card in hand)
            {
                Console.WriteLine(card);
            }
        }

        // bereken waarde van de hand van de dealer
        public int GetHandValue()
        {
            int value = 0;
            int numberOfAces = 0;

            foreach (var card in hand)
            {
                string[] splitCard = card.Split(' ');
                string cardValue = splitCard[0];

                if (cardValue == "Jack" || cardValue == "Queen" || cardValue == "King")
                {
                    value += 10;
                }
                else if (cardValue == "Ace")
                {
                    value += 11;
                    numberOfAces++;
                }
                else
                {
                    value += int.Parse(cardValue);
                }
            }

            // verander waarde van as als de totale value 21> is
            while (value > 21 && numberOfAces > 0)
            {
                value -= 10;
                numberOfAces--;
            }

            return value;
        }
    }
}
