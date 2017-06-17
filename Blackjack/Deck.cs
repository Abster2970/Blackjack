using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    class Deck
    {
        public List<Card> Cards { get; set; } = new List<Card>(52);

        public Deck()
        {
        }
    }
}
