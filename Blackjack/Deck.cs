using System.Collections.Generic;

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
