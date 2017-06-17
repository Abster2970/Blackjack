using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public int TotalValue
        {
            get
            {
                var totalValue = Cards.Select(c => (int)c.Rank > 1 && (int)c.Rank < 11 ? (int)c.Rank : (int)c.Rank == 1 ? 11 : 10).Sum();
                var aces = Cards.Count(c => c.Rank == Ranks.Ace);

                while (aces-- > 0 && totalValue > 21)
                {
                    totalValue -= 10;
                }

                return totalValue;
            }
        }

        public int FaceValue
        {
            get
            {
                var totalValue = Cards.Where(c => c.IsFaceUp)
                       .Select(c => (int)c.Rank > 1 && (int)c.Rank < 11 ? (int)c.Rank : (int)c.Rank == 1 ? 11 : 10).Sum();
                var aces = Cards.Count(c => c.Rank == Ranks.Ace);

                while (aces-- > 0 && totalValue > 21)
                {
                    totalValue -= 10;
                }

                return totalValue;
            }
        }
    }
}
