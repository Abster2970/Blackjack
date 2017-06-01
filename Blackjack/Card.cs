using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    enum CardsSuits
    {
        Spades,
        Clubs,
        Hearts,
        Diamonds
    }

    enum CardsValues
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6, 
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 10,
        Ace = 11
    }

    class Card
    {
        public CardsSuits Suit { get; set; }
        public CardsValues Value { get; set; }

        public Card (CardsSuits suit, CardsValues value)
        {
            Suit = suit;
            Value = value;
        }

        public static Card GetRandomCard()
        {
            Random rnd = new Random();

            int cardsSuitsCount = Enum.GetValues(typeof(CardsSuits)).Length;
            int cardsValuesCount = Enum.GetValues(typeof(CardsValues)).Length;

            int randomSuitId = rnd.Next(0, cardsSuitsCount - 1);
            int randomValueId = rnd.Next(0, cardsValuesCount - 1);

            string randomSuitName = Enum.GetNames(typeof(CardsSuits))[randomSuitId];
            string randomValueName = Enum.GetNames(typeof(CardsValues))[randomValueId];

            CardsSuits randomSuit = (CardsSuits)Enum.Parse(typeof(CardsSuits), randomSuitName);
            CardsValues randomValue = (CardsValues)Enum.Parse(typeof(CardsValues), randomValueName);

            Card randomCard = new Card(randomSuit, randomValue);

            return randomCard;
        }

        public override string ToString()
        {
            return String.Format($"{Value.ToString()} of {Suit.ToString()}");
        }
    }
}
