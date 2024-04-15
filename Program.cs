using System;

namespace Blackjack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StartGame();
        }

        public static void StartGame()
        {
            BlackjackGame game = new BlackjackGame();
            game.Start();


            while (true) // Wachten op invoer van de speler om opnieuw te starten
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "restart")
                {
                    Console.Clear(); // wis het consolevenster
                    game.Start(); // Start een nieuwe game
                }
                else if (input.ToLower() == "exit")
                {
                    break; // Stop de loop en eindig het programma
                }
                else
                {
                }
            }

        }
    }
}
