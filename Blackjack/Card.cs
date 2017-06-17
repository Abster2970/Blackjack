using System;

namespace Blackjack
{
    class Card
    {
        public Suits Suit { get; set; }
        public Ranks Rank { get; set; }
        public bool IsFaceUp { get; set; }
        
        public Card (Suits suit, Ranks rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }
}
