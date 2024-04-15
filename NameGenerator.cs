using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class NameGenerator
    {
        private List<string> availableNames;
        private Queue<string> usedNames;

        public NameGenerator()
        {
            availableNames = new List<string> { "Robert", "Melvin", "Bart", "Erik", "Marco", "Davor", "Niek" };
            usedNames = new Queue<string>();
        }


        public string GenerateRandomName()
        {
            if (availableNames.Count == 0)
            {
                availableNames.AddRange(usedNames); // Lijst resetten voor als ik de game restart met die glitch

                usedNames.Clear();
            }

            Random rng = new Random();
            int index = rng.Next(availableNames.Count);
            string name = availableNames[index]; // 
            availableNames.RemoveAt(index); // Verwijder gebruikte naam uit de list
            usedNames.Enqueue(name); // Voeg de gebruikte naam toe aan de lijst met gebruikte namen
            return name;
        }
    }
}
