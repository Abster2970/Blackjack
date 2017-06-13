using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Dealer : Participant
    {
        public Deck Deck { get; private set; }

        public Dealer()
        {
            Deck = new Deck();
            Hand = new Hand(isDealer: true);
        }
        
        public void GiveCard(Hand hand)
        {
            var card = Deck.GetCard();
            hand.AddCard(card);
        }

        public void Deal(Hand hand)
        {
            var firstCard = Deck.GetCard();
            hand.AddCard(firstCard);

            var secondcard = Deck.GetCard();

            if (hand.IsDealer)
            {
                secondcard.Flip();
            }

            hand.AddCard(secondcard);
        }

        public void PrepareDeckToTheNewGame()
        {
            Deck.Fill();
            Deck.Shuffle();
        }

        public void GivePrize(Player player)
        {
            player.Cash += player.BetValue * 2;
        }

        public void ReturnMoney(Player player)
        {
            player.Cash += player.BetValue;
        }

        public void GiveExtraCash(Player player, int cash)
        {
            player.Cash += cash;
        }
    }
}
