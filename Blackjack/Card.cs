using System;

namespace Blackjack
{
    enum Suits
    {
        Spades = 1,
        Clubs = 2,
        Hearts = 3,
        Diamonds = 4
    }

    enum Ranks
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6, 
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }

    class Card
    {
        public Suits Suit { get; set; }
        public Ranks Rank { get; set; }
        public bool IsFaceUp { get; private set; }
        
        public Card (Suits suit, Ranks rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public void Flip()
        {
            IsFaceUp = !IsFaceUp;
        }

        public override string ToString()
        {
            return String.Format($"{Rank.ToString()} of {Suit.ToString()}");
        }
    }
}
