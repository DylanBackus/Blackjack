using System;

namespace Blackjack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StartGame startGame = new StartGame();
            startGame.Begin();
        }
    }
}
