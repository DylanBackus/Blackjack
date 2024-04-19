using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Player
    {
        // Naam van players
        public string Name { get; private set; }

        // bet van players
        public int BetAmount { get; private set; }

        private List<string> hand; // Hand van de players
        private Random random; // Wordt gebruikt voor het genereren van random waarden

        public Player(string name = null, NameGenerator nameGenerator = null)
        {
            // Als er geen naam is opgegeven, wordt er een willekeurige naam gegenereerd met namegenerator
            if (string.IsNullOrEmpty(name))
            {
                if (nameGenerator == null)
                {
                    throw new ArgumentNullException(nameof(nameGenerator), "Naamgenerator kan niet null zijn wanneer er geen naam is opgegeven.");
                }
                Name = nameGenerator.GenerateRandomName(); // Genereert een random naam
            }
            else
            {
                Name = name;
            }

            hand = new List<string>(); // add de hand van de speler
            random = new Random(); // add de wilekeurige getallengenerator

            // Willekeurig bet tussen 10 en 10000 euro
            BetAmount = random.Next(10, 10001);
        }

        // Add kaart aan hand
        public void ReceiveCard(string card)
        {
            hand.Add(card);
        }

        // Print hand
        public void PrintHand()
        {
            foreach (var card in hand)
            {
                Console.WriteLine(card);
            }
        }

        // Geeft true terug als de speler een extra kaart wil, anders false
        public bool WantsToHit()
        {
            // random of speler hit of past (50% kans)
            return random.Next(2) == 0;
        }

        // Geeft true terug als de speler kiest om te dubbelen of splitsen, anders false.
        public bool RandomDecision()
        {
            // random beslissing tussen "Passen", "Dubbelen" of "Splitsen"
            int decision = random.Next(1, 4); // random nummer tussen 1 en 3 

            switch (decision)
            {
                case 1:
                    Console.WriteLine($"{Name} past.");
                    return false; // Speler past
                case 2:
                    Console.WriteLine($"{Name} dubbelt.");
                    return true; // Speler dubbelt
                case 3:
                    Console.WriteLine($"{Name} splitst.");
                    return true; // Speler splitst
                default:
                    Console.WriteLine($"{Name} past.");
                    return false; // Standaard pas als er iets mis gaat
            }
        }

        // Berekende waarde van de hand van speler
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

            // Past de waarde van A's aan indien nodig
            while (value > 21 && numberOfAces > 0)
            {
                value -= 10;
                numberOfAces--;
            }

            return value;
        }
    }
}
