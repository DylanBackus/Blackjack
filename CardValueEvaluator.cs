using System;

namespace Blackjack
{
    public class CardValueEvaluator
    {
        public int EvaluateCardValue(string cardValue)
        {
            if (int.TryParse(cardValue, out int numericValue))
            {
                return numericValue;
            }
            else if (cardValue == "Jack" || cardValue == "Queen" || cardValue == "King")
            {
                return 10;
            }
            else if (cardValue == "Ace")
            {
                return 11; // We zullen de Aas-waarde later aanpassen indien nodig
            }
            else
            {
                throw new ArgumentException("Invalid card value.");
            }
        }

        public int AdjustAceValue(int currentValue)
        {
            // Als het huidige handwaarde groter is dan 21, moeten we de waarde van Aas aanpassen van 11 naar 1.
            return (currentValue > 21) ? currentValue - 10 : currentValue;
        }
    }
}
