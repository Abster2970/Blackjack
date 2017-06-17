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

        public Player(int id, Dealer dealer, int cash)
        {
            Hand = new Hand(isDealer: false);
            ID = id;
            Dealer = dealer;
            Cash = cash;
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
                betValue = value > Cash ? Cash : value > 0 ? value : 0;
                Cash -= betValue;
            }
        }
    }
}
