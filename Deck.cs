using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Deck
    {
        private string[] DeckArray { get; set; }
        private CardValueEvaluator cardValueEvaluator;

        public Deck()
        {
            cardValueEvaluator = new CardValueEvaluator();
        }

        public void InitializeDeck()
        {
            List<string> deckList = new List<string>();

            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            foreach (string suit in suits)
            {
                foreach (string value in values)
                {
                    deckList.Add(value + " of " + suit);
                }
            }

            DeckArray = deckList.ToArray();
        }

        public void ShuffleDeck()
        {
            Random rng = new Random();
            int length = DeckArray.Length;
            while (length > 1)
            {
                length--;
                int randIndex = rng.Next(length + 1);
                string value = DeckArray[randIndex];
                DeckArray[randIndex] = DeckArray[length];
                DeckArray[length] = value;
            }
            Console.WriteLine(BoldText("Deck has been shuffled."));
            Console.WriteLine("");
        }

        public string DrawCard()
        {
            if (DeckArray.Length == 0)
            {
                throw new InvalidOperationException("The deck is empty.");
            }

            string card = DeckArray[0];
            List<string> tempList = new List<string>(DeckArray);
            tempList.RemoveAt(0);
            DeckArray = tempList.ToArray();

            return card;
        }

        public int EvaluateCardValue(string card)
        {
            string[] splitCard = card.Split(' ');
            string cardValue = splitCard[0];
            return cardValueEvaluator.EvaluateCardValue(cardValue);
        }

        public int AdjustAceValue(int currentValue)
        {
            return cardValueEvaluator.AdjustAceValue(currentValue);
        }

        private string BoldText(string text)
        {
            return "\u001b[1m" + text + "\u001b[0m"; // Bold style
        }
    }
}
