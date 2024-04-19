using System;

namespace Blackjack
{
    public class CardValueEvaluator
    {
        // Evalueer waarde van een kaart
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
                return 11; // As waarde later aanpassen indien nodig
            }
            else
            {
                throw new ArgumentException("Ongeldige kaartwaarde.");
            }
        }

        // Past waarde van as aan op basis van de huidige handwaarde.
        public int AdjustAceValue(int currentValue)
        {
            // Als de huidige handwaarde groter is dan 21, word de waarde van as aangepast van 11 naar 1.
            return (currentValue > 21) ? currentValue - 10 : currentValue;
        }
    }
}
