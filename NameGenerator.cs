using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class NameGenerator
    {
        private List<string> availableNames;

        public NameGenerator()
        {
            availableNames = new List<string> { "Robert", "Melvin", "Bart", "Erik", "Marco", "Davor", "Niek" };
        }

        public string GenerateRandomName()
        {

            Random rng = new Random();
            int index = rng.Next(availableNames.Count); // hoeveel namen moet ie kiezen
            string name = availableNames[index]; // Willekeurige naam uit de list gehaald  
            availableNames.RemoveAt(index); // Naam die gekozen is uit de availableNames en die naam word uit de list gehaald
            return name;
        }
    }
}