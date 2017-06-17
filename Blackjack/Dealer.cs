using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Dealer : Participant
    {
        public Dealer()
        {
            Hand = new Hand(isDealer: true);
        }
    }
}
