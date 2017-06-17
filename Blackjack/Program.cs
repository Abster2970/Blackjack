using System;
using System.Collections.Generic;

namespace Blackjack
{   
    class Program
    {
        static void Main(string[] args)
        {
            GameLoop gameLoop = new GameLoop();
            gameLoop.Start();
        }   
    }
}