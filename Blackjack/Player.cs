using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Player : Participant
    {
        private int cash;
        private int betValue;
        
        public Dealer Dealer { get; private set; }

        public Player(string name, Dealer dealer)
        {
            Hand = new Hand(isDealer: false);
            Name = name;
            Dealer = dealer;
        }

        public int Cash
        {
            get
            {
                return cash;
            }
            set
            {
                cash = value > 0 ? value : 0;
            }
        }

        public int BetValue
        {
            get
            {
                return betValue;
            }
            set
            {
                betValue = value > 0 ? value : 0;
                Cash -= betValue;
            }
        }

        public void Hit()
        {
            Dealer.GiveCard(Hand);
        }
    }
}
