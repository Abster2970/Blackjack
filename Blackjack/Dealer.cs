using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Dealer
    {
        public List<Card> Cards { get; private set; }
        public List<Card> Deck { get; private set; }
        public List<Player> Players { get; set; }
        public int Total { get; set; } = 0;
        public string Name { get; set; } = "Dealer";
        public Dealer()
        {
            Players = new List<Player>();
            Cards = new List<Card>();
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
            Random rnd = new Random();
            int randomCardId = rnd.Next(0, Deck.Count);
            Card randomCard = Deck[randomCardId];

            Cards.Add(randomCard);
            Deck.RemoveAt(randomCardId);

            Total += randomCard.GetValue(Total);
            //int cardsValue = GetCardsValue();
            //if (cardsValue > 17) CountTheResult();
        }

        //public void CountTheResult()
        //{
        //    foreach(var player in Players)
        //    {
        //        int playerCardsValue = player.Total;
        //        int dealerCardsValue = Total;

        //        if ((playerCardsValue > dealerCardsValue && playerCardsValue <= 21) || dealerCardsValue > 21)
        //        {
        //            player.GetPrize();
        //        }
        //    }
        //}

        public void Reset()
        {
            this.Total = 0;
            this.Cards.Clear();
            this.FillDeck();

            foreach(var player in Players)
            {
                player.Total = 0;
                player.Cards.Clear();
            }
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
