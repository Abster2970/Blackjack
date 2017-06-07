using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    class Deck
    {
        private readonly List<Card> cards = new List<Card>(52);

        public IReadOnlyCollection<Card> Cards
        {
            get { return cards.AsReadOnly(); }
        }

        public Deck()
        {
            this.Fill();
        }
        
        public void Fill()
        {
            cards.Clear();
            cards.AddRange(
                Enumerable.Range(1, 4)
                .SelectMany(s => Enumerable.Range(1, 13).Select(r => new Card((Suits)s, (Ranks)r))));
        }

        public void Shuffle()
        {
            var cardsCount = cards.Count;
            var cardsToShuffle = cardsCount;
            var rnd = new Random();

            while(cardsToShuffle > 1)
            {
                var firstRndCardId = rnd.Next(0, cardsCount);
                var secondRndCardId = rnd.Next(0, cardsCount);

                var tmp = cards[firstRndCardId];
                cards[firstRndCardId] = cards[secondRndCardId];
                cards[secondRndCardId] = tmp;

                cardsToShuffle--;
            }
        }
        
        public Card GetCard()
        {
            if (Cards.Count == 0) return null;
            
            var card = cards.First();
            cards.Remove(card);

            return card;
        }
    }
}
