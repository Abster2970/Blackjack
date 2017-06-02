using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Player
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        private Dealer CurrentDealer { get; }
        public int Total { get; set; } = 0;

        private int cash;
        private int betValue;

        public int Cash
        {
            get { return cash; }
            private set { cash = value > 0 ? value : 0; }
        }

        public int BetValue
        {
            get { return betValue; }
            private set { betValue = value > 0 ? value : 0; }
        }

        public Player(string name, Dealer currentDealer)
        {
            Cash = 1000;
            BetValue = 0;
            Cards = new List<Card>();
            Name = name;
            CurrentDealer = currentDealer;
            currentDealer.Players.Add(this);
        }

        public void AddCard(Card newCard)
        {
            Cards.Add(newCard);
            Total += newCard.GetValue(Total);

            //Console.WriteLine($"#{Name} gets {newCard.ToString()}");
        }
        
        public void Hit()
        {
            CurrentDealer.GiveCard(this);
        }

        public void DoBet(int betValue)
        {
            BetValue = betValue;
            Cash -= betValue;
        }

        public void GetPrize()
        {
            Cash += BetValue * 2;
        }

        public void PrintCards()
        {
            Console.WriteLine($"\n#{Name} cards: ");
            foreach(var card in Cards)
            {
                Console.WriteLine(card.ToString());
            }
        }

        public Card GetLastCard()
        {
            return Cards[Cards.Count - 1];
        }
    }
}
