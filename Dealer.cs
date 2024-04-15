using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public class Dealer
    {
        public string Name { get; private set; }
        private List<string> hand;

        public Dealer(string name)
        {
            Name = name;
            hand = new List<string>();
        }

        public void ReceiveCard(string card, bool faceUp)
        {
            if (faceUp)
            {
                hand.Add(card);
            }
            else
            {
                hand.Insert(0, card); // Voeg de facedown kaart toe aan het begin van de hand
            }
        }

        public void PrintCards()
        {
            foreach (var card in hand)
            {
                Console.WriteLine(card);
            }
        }

        public bool ShouldHit()
        {
            return GetHandValue() < 17;
        }

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

            // Adjust the value if there are aces and the total value is over 21
            while (value > 21 && numberOfAces > 0)
            {
                value -= 10;
                numberOfAces--;
            }

            // Ensure the hand value does not exceed 21
            return Math.Min(value, 21);
        }
        public bool HasBlackjack()
        {
            //check of de dealer blackjack heeft (ace + 10- value kaart)
            if (hand.Count != 2)
            {
                return false;
            }

            string[] card1Split = hand[0].Split(' ');
            string[] card2Split = hand[1].Split(' ');

            if (card1Split.Length != 3 || card2Split.Length != 3)
            {
                return false;
            }

            string card1Value = card1Split[0];
            string card2Value = card2Split[0];

            if ((card1Value == "Ace" && (card2Value == "10" || card2Value == "Jack" || card2Value == "Queen" || card2Value == "King")) ||
                (card2Value == "Ace" && (card1Value == "10" || card1Value == "Jack" || card1Value == "Queen" || card1Value == "King")))
            {
                return true;
            }

            return false;
        }
    }
}