using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    class Hand
    {
        private readonly List<Card> cards = new List<Card>();

        public bool IsDealer { get; private set; }

        public IReadOnlyCollection<Card> Cards
        {
            get { return cards.AsReadOnly(); }
        }

        public int SoftValue
        {
            get
            {
                return cards.Select(c => (int)c.Rank > 1 && (int)c.Rank < 11 ? (int)c.Rank : (int)c.Rank == 1 ? 11 : 10).Sum();
            }
        }

        public int TotalValue
        {
            get
            {
                var totalValue = SoftValue;
                var aces = cards.Count(c => c.Rank == Ranks.Ace);

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
                var totalValue = cards.Where(c => c.IsFaceUp)
                    .Select(c => (int)c.Rank > 1 && (int)c.Rank < 11 ? (int)c.Rank : (int)c.Rank == 1 ? 11 : 10).Sum();
                var aces = cards.Count(c => c.Rank == Ranks.Ace);

                while (aces-- > 0 && totalValue > 21)
                {
                    totalValue -= 10;
                }

                return totalValue;
            }
        }

        public List<Card> VisibleCards
        {
            get
            {
                return Cards.Where(c => c.IsFaceUp).ToList();
            }
        }

        public Hand(bool isDealer)
        {
            IsDealer = isDealer;
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public void Show()
        {
            foreach(var card in cards)
            {
                if (!card.IsFaceUp)
                {
                    card.Flip();
                }
            }
        }

        public void Clear()
        {
            cards.Clear();
        }
    }
}
