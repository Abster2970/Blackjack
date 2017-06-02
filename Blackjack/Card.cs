using System;

namespace Blackjack
{
    enum CardsSuits
    {
        Spades,
        Clubs,
        Hearts,
        Diamonds
    }

    enum CardsNames
    {
        Two,
        Three,
        Four,
        Five,
        Six, 
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    class Card
    {
        public CardsSuits Suit { get; set; }
        public CardsNames Name { get; set; }
        static Random rnd = new Random();

        public Card (CardsSuits suit, CardsNames name)
        {
            Suit = suit;
            Name = name;
        }

        public static Card GetRandomCard()
        {
            int cardsSuitsCount = Enum.GetValues(typeof(CardsSuits)).Length;
            int cardsValuesCount = Enum.GetValues(typeof(CardsNames)).Length;

            int randomSuitId = rnd.Next(0, cardsSuitsCount - 1);
            int randomValueId = rnd.Next(0, cardsValuesCount - 1);

            string randomSuitName = Enum.GetNames(typeof(CardsSuits))[randomSuitId];
            string randomValueName = Enum.GetNames(typeof(CardsNames))[randomValueId];

            CardsSuits randomSuit = (CardsSuits)Enum.Parse(typeof(CardsSuits), randomSuitName);
            CardsNames randomValue = (CardsNames)Enum.Parse(typeof(CardsNames), randomValueName);

            Card randomCard = new Card(randomSuit, randomValue);

            return randomCard;
        }

        public int GetValue(int total)
        {
            int value = 0;
            switch (Name)
            {
                case CardsNames.Two: value = 2; break;
                case CardsNames.Three: value = 3; break;
                case CardsNames.Four: value = 4; break;
                case CardsNames.Five: value = 5; break;
                case CardsNames.Six: value = 6; break;
                case CardsNames.Seven: value = 7; break;
                case CardsNames.Eight: value = 8; break;
                case CardsNames.Nine: value = 9; break;

                case CardsNames.Ten:
                case CardsNames.Jack:
                case CardsNames.Queen:
                case CardsNames.King: value = 10; break;

                case CardsNames.Ace:
                    if (total + 11 > 21) value = 1;
                    else value = 11;
                    break;
            }
            return value;
        }

        public override string ToString()
        {
            return String.Format($"{Name.ToString()} of {Suit.ToString()}");
        }
    }
}
