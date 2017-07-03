using System.Collections.Generic;

namespace Blackjack
{
    class Hand
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public bool IsDealer { get; private set; }
        
        public Hand(bool isDealer)
        {
            IsDealer = isDealer;
        }
    }
}
