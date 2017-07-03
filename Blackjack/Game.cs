using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    class Game
    {
        public Deck Deck { get; private set; } = new Deck();
        public Dealer Dealer { get; private set; } = new Dealer();
        public List<Player> Players { get; private set; } = new List<Player>();

        public Game(int playersCount)
        {
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
        
        #region PlayersManipulations
        public void Hit(Player player)
        {
            GiveCard(player.Hand);
        }
        
        public void AskAllPlayers(Game game)
        {
            foreach (Player player in game.Players)
            {
                ConsoleHelper.AskPlayerForBet(player);
                ConsoleHelper.PrintPlayerCards(player);

                string action = "";
                while (action != "s" && Game.GetTotalHandValue(player.Hand) <= 21)
                {
                    action = ConsoleHelper.AskPlayerForNextAction(player);
                    if (action == "h")
                    {
                        game.Hit(player);
                    }
                    if (action == "s")
                    {
                        break;
                    }
                    ConsoleHelper.PrintPlayerCards(player);
                }
            }
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
            while (GetTotalHandValue(Dealer.Hand) <= 17)
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
        
        public static int GetTotalHandValue(Hand hand)
        {
            var totalValue = hand.Cards.Select(c => (int)c.Rank > 1 && (int)c.Rank < 11 ? (int)c.Rank : (int)c.Rank == 1 ? 11 : 10).Sum();
            var aces = hand.Cards.Count(c => c.Rank == Ranks.Ace);

            while (aces-- > 0 && totalValue > 21)
            {
                totalValue -= 10;
            }

            return totalValue;
        }
        
        public static int GetFaceHandValue(Hand hand)
        {
            var totalValue = hand.Cards.Where(c => c.IsFaceUp)
                   .Select(c => (int)c.Rank > 1 && (int)c.Rank < 11 ? (int)c.Rank : (int)c.Rank == 1 ? 11 : 10).Sum();
            var aces = hand.Cards.Count(c => c.Rank == Ranks.Ace);

            while (aces-- > 0 && totalValue > 21)
            {
                totalValue -= 10;
            }

            return totalValue;
        }
        #endregion

        #region CashManipulations
        
        public static void DoBet(Player player, int betValue)
        {
            if (betValue < 0)
            {
                betValue = 0;
            }
            if (player.Cash < betValue)
            {
                player.BetValue = player.Cash;
                player.Cash = 0;
            }
            if (player.Cash >= betValue)
            {
                player.BetValue = betValue;
                player.Cash -= betValue;
            }
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
        #endregion

        #region GameResults
        public void CountGameResults()
        {
            foreach (Player player in Players)
            {
                var totalPlayerValue = GetTotalHandValue(player.Hand);
                var totalDealerValue = GetTotalHandValue(Dealer.Hand);

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
