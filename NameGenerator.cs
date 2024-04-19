using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class NameGenerator
    {
        // Lijst met available names
        private List<string> availableNames;
        //gebruikte namen
        private Queue<string> usedNames;

        public NameGenerator()
        {
            availableNames = new List<string> { "Robert", "Melvin", "Bart", "Erik", "Marco", "Davor", "Niek" }; // namen om uit te kiezen
            usedNames = new Queue<string>();
        }

        // Genereert een willekeurige naam en zet deze in de gebruikte namen list
        public string GenerateRandomName()
        {
            if (availableNames.Count == 0)
            {
                availableNames.AddRange(usedNames); // Reset de lijst voor als game gereset wordt
                usedNames.Clear();
            }

            Random rng = new Random();
            int index = rng.Next(availableNames.Count);
            string name = availableNames[index]; // Haal random naam
            availableNames.RemoveAt(index); // verwijder gebruikte naam uit lijst met beschikbare namen
            usedNames.Enqueue(name); // Voeg gebruikte naam toe aan de queue met gebruikte namen
            return name;
        }
    }
}
