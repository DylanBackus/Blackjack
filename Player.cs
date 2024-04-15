using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Player
    {
        public string Name { get; private set; }
        private List<string> hand;
        private Random random;
        public int BetAmount { get; private set; } // Gokbedrag

        public Player(string name = null, NameGenerator nameGenerator = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                if (nameGenerator == null)
                {
                    throw new ArgumentNullException(nameof(nameGenerator));
                }
                Name = nameGenerator.GenerateRandomName();
            }
            else
            {
                Name = name;
            }

            hand = new List<string>();
            random = new Random();

            // Willekeurig gokbedrag tussen 30 en 10000 euro
            BetAmount = random.Next(30, 10001);
        }

        public void ReceiveCard(string card)
        {
            hand.Add(card);
        }

        public void PrintHand()
        {
            foreach (var card in hand)
            {
                Console.WriteLine(card);
            }
        }

        public bool WantsToHit()
        {
            // Simulate a random decision whether the player wants to hit or stand
            return random.Next(2) == 0; // 50% chance of hitting
        }

        public bool RandomDecision()
        {
            // Willekeurig beslissen tussen "Passen", "Dubbelen" of "Splitsen"
            int decision = random.Next(1, 4); // Genereer een willekeurig getal tussen 1 en 3

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
                    return false; // Als er iets misgaat, past de speler standaard
            }
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

            return value;
        }
    }
}
