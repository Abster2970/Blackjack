using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Dealer
    {
        public List<Card> Cards { get; private set; }
        public List<Card> Deck { get; private set; }
        private List<Player> players = new List<Player>();

        public Dealer()
        {
            FillDeck();
        }

        private void FillDeck()
        {
            Deck = new List<Card>();

            string[] suitsNames = Enum.GetNames(typeof(CardsSuits));
            string[] valuesNames = Enum.GetNames(typeof(CardsNames));

            foreach (string suitName in suitsNames)
            {
                CardsSuits currSuit = (CardsSuits)Enum.Parse(typeof(CardsSuits), suitName);
                foreach (string valueName in valuesNames)
                {
                    CardsNames currValue = (CardsNames)Enum.Parse(typeof(CardsNames), valueName);
                    Deck.Add(new Card(currSuit, currValue));
                }
            }
        }
        
        public void GiveCard(Player player)
        {
            Random rnd = new Random();
            int randomCardId = rnd.Next(0, Deck.Count);
            Card randomCard = Deck[randomCardId];

            player.AddCard(randomCard);
            Deck.RemoveAt(randomCardId);
        }

        public void GetCard()
        {
            int cardsValue = GetCardsValue();
            if (cardsValue > 17) CountTheResult();

            Random rnd = new Random();
            int randomCardId = rnd.Next(0, Deck.Count);
            Card randomCard = Deck[randomCardId];

            Cards.Add(randomCard);
            Deck.RemoveAt(randomCardId);
        }

        public void CountTheResult()
        {
            bool isAnyPlayerHasWon = false;

            players.ForEach((player) =>
            {
                int playerCardsValue = player.GetCardsValue();
                int dealerCardsValue = GetCardsValue();

                if (playerCardsValue > dealerCardsValue)
                {
                    player.GetPrize();
                    isAnyPlayerHasWon = true;
                }
                player.PrintInfo();
            });

            //...
        }

        public int GetCardsValue()
        {
            int value = 0;
            foreach (var card in Cards)
            {
                value += card.GetValue();
            }
            return value;
        }
    }
}
