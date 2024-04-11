using System;

namespace Blackjack
{
    public class Player
    {
        public string Name { get; private set; }

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
        }
    }
}
