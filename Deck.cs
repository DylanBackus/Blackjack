using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Deck
    {
        private string[] DeckArray { get; set; }

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
            Console.WriteLine("Deck has been shuffled.");
        }

        public void PrintDeck()
        {
            foreach (string card in DeckArray)
            {
                Console.WriteLine(card);
            }
        }
    }
}
