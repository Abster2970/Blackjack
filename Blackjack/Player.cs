using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    enum PlayerState
    {
        Playing,
        Waiting
    }

    class Player
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        private Dealer CurrentDealer { get; }
        public PlayerState State { get; set; } = PlayerState.Playing;

        public int InitialAmountOfMoney
        {
            get { return InitialAmountOfMoney; }
            private set { InitialAmountOfMoney = value > 0 ? value : 0; }
        }

        public int BetValue
        {
            get { return BetValue; }
            private set { BetValue = value > 0 ? value : 0; }
        }

        public Player(Dealer currentDealer)
        {
            InitialAmountOfMoney = 1000;
            BetValue = 0;
            Cards = new List<Card>();
            CurrentDealer = currentDealer;
        }

        public void AddCard(Card newCard)
        {
            Cards.Add(newCard);
        }

        public void Hit()
        {
            CurrentDealer.GiveCard(this);
        }

        public void Stand()
        {
            State = PlayerState.Waiting;
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

        public void DoBet(int betValue)
        {
            BetValue = betValue;
            InitialAmountOfMoney -= betValue;
        }

        public void GetPrize()
        {
            InitialAmountOfMoney += BetValue * 2;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"#{Name} - {InitialAmountOfMoney}$");
        }
    }
}
