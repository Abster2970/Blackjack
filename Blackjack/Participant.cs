using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    abstract class Participant
    {
        public string Name { get; set; }
        public Hand Hand { get; protected set; }

        public Participant()
        {
        }

        //public void PrintCards()
        //{
        //    Console.WriteLine($"\n#{Name} cards: ");
        //    foreach (var card in Cards)
        //    {
        //        Console.WriteLine(card.ToString());
        //    }
        //}
    }
}
