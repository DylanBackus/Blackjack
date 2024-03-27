using System;

namespace Blackjack
{
    internal class StartGame
    {
        public void Begin()
        {
            BlackjackGame game = new BlackjackGame();
            game.Start();
        }
    }
}