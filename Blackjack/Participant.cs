using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    abstract class Participant
    {
        public List<Card> Cards { get; set; }
        public int Total { get; set; } = 0;
        public string Name { get; set; }

        public Participant(string name)
        {
            Cards = new List<Card>();
            Name = name;
        }

        public void PrintCards()
        {
            Console.WriteLine($"\n#{Name} cards: ");
            foreach (var card in Cards)
            {
                Console.WriteLine(card.ToString());
            }
        }
    }
}
