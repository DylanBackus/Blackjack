using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Deck
    {
        // Array met kaarten in het deck
        private string[] DeckArray { get; set; }
        private CardValueEvaluator cardValueEvaluator; // add CardValueEvaluator voor kaartwaarde evalautie

        // Niew kaarten deck
        public Deck()
        {
            cardValueEvaluator = new CardValueEvaluator();
        }

        // Add het kaartendeck met standaard kaarten
        public void InitializeDeck()
        {
            List<string> deckList = new List<string>();

            string[] suits = { "Harten", "Ruiten", "Klaveren", "Schoppen" };
            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Boer", "Vrouw", "Koning", "Aas" };

            foreach (string suit in suits)
            {
                foreach (string value in values)
                {
                    deckList.Add(value + " van " + suit);
                }
            }

            DeckArray = deckList.ToArray();
        }

        // Shufflen van deck
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
            Console.WriteLine(BoldText("Het deck is geschud."));
            Console.WriteLine("");
        }

        // Random kaart wordt uit het deck gepakt
        public string DrawCard()
        {
            if (DeckArray.Length == 0)
            {
                throw new InvalidOperationException("Het deck is leeg.");
            }

            string card = DeckArray[0];
            List<string> tempList = new List<string>(DeckArray);
            tempList.RemoveAt(0);
            DeckArray = tempList.ToArray();

            return card;
        }

        // waarde van de kaart evalueren
        public int EvaluateCardValue(string card)
        {
            string[] splitCard = card.Split(' ');
            string cardValue = splitCard[0];
            return cardValueEvaluator.EvaluateCardValue(cardValue);
        }

        //Verander waarde van as op basis van huidige handwaarde
        public int AdjustAceValue(int currentValue)
        {
            return cardValueEvaluator.AdjustAceValue(currentValue);
        }

        private string BoldText(string text)
        {
            return "\u001b[1m" + text + "\u001b[0m"; // bold text
        }
    }
}
