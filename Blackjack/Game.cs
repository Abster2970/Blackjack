using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    class Game
    {
        public Deck Deck { get; private set; } = new Deck();
        public Dealer Dealer { get; private set; } = new Dealer();
        public List<Player> Players { get; private set; }

        public Game(int playersCount)
        {
            Players = new List<Player>();
            InitPlayers(playersCount);
        }

        #region Initializations
        private void InitPlayers(int playersCount)
        {
            for (int i = 0; i < playersCount; i++)
            {
                int playerID = (i + 1);
                Players.Add(new Player(playerID, Dealer, 1000));
            }
        }
        #endregion
        
        #region PlayersActions
        public void Hit(Player player)
        {
            GiveCard(player.Hand);
        }
        #endregion  

        #region CardsManipulations
        public void GiveInitialCardsToEverybody()
        {
            Deal(Dealer.Hand);
            foreach(Player player in Players)
            {
                Deal(player.Hand);
            }
        }

        private void Deal(Hand hand)
        {
            var firstCard = GetCardFromDeck();
            AddCardToHand(hand, firstCard);

            var secondcard = GetCardFromDeck();
            if (hand.IsDealer)
            {
                FlipCard(secondcard);
            }
            AddCardToHand(hand, secondcard);
        }

        public void GiveCard(Hand hand)
        {
            var card = GetCardFromDeck();
            AddCardToHand(hand, card);
        }

        public void AddCardToHand(Hand hand, Card card)
        {
            hand.Cards.Add(card);
        }

        public void FlipCard(Card card)
        {
            card.IsFaceUp = !card.IsFaceUp;
        }

        public void GiveCardsToDealerUntilFull()
        {
            while (Dealer.Hand.TotalValue <= 17)
            {
                GiveCard(Dealer.Hand);
            }
        }
        #endregion

        #region DeckManipulations
        public void RefillDeck()
        {
            Deck.Cards.Clear();
            Deck.Cards.AddRange(
                Enumerable.Range(1, 4)
                .SelectMany(s => Enumerable.Range(1, 13).Select(r => new Card((Suits)s, (Ranks)r))));
        }

        public void ShuffleDeck()
        {
            var cardsCount = Deck.Cards.Count;
            var cardsToShuffle = cardsCount;
            var rnd = new Random();

            while (cardsToShuffle > 1)
            {
                var firstRndCardId = rnd.Next(0, cardsCount);
                var secondRndCardId = rnd.Next(0, cardsCount);

                var tmp = Deck.Cards[firstRndCardId];
                Deck.Cards[firstRndCardId] = Deck.Cards[secondRndCardId];
                Deck.Cards[secondRndCardId] = tmp;

                cardsToShuffle--;
            }
        }

        public void PrepareDeckToNewGame()
        {
            RefillDeck();
            ShuffleDeck();
        }

        public Card GetCardFromDeck()
        {
            if (Deck.Cards.Count == 0) return null;

            var card = Deck.Cards.First();
            Deck.Cards.Remove(card);

            return card;
        }
        #endregion

        #region HandManipulations
        public void ShowHand(Hand hand)
        {
            foreach (var card in hand.Cards)
            {
                if (!card.IsFaceUp)
                {
                    FlipCard(card);
                }
            }
        }

        public void ClearHand(Hand hand)
        {
            hand.Cards.Clear();
        }

        public void ClearAllHands()
        {
            ClearHand(Dealer.Hand);
            foreach(Player player in Players)
            {
                ClearHand(player.Hand);
            }
        }
        #endregion

        #region CashManipulations
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
        #endregion

        #region GameResults
        public void CountGameResults()
        {
            foreach (Player player in Players)
            {
                var totalPlayerValue = player.Hand.TotalValue;
                var totalDealerValue = Dealer.Hand.TotalValue;

                if (totalPlayerValue == totalDealerValue && totalPlayerValue <= 21)
                {
                    ReturnMoney(player);
                }
                if ((totalPlayerValue <= 21 && totalPlayerValue > totalDealerValue) || (totalDealerValue > 21 && totalPlayerValue <= 21))
                {
                    GivePrize(player);
                }
                if (player.Cash == 0)
                {
                   GiveExtraCash(player, 1);
                }
            }
        }
        #endregion
    }
}
