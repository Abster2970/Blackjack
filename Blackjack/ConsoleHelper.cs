using System;

namespace Blackjack
{
    static class ConsoleHelper
    {
        public static void PrintCard(Card card)
        {
            string rank = card.Rank.ToString();
            string suit = card.Suit.ToString();
            Console.WriteLine($"{rank} of {suit}");
        }

        public static void PrintPlayerCards(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"#Player's N{player.ID} cards: <{Game.GetTotalHandValue(player.Hand)}>");
            foreach (var card in player.Hand.Cards)
            {
                PrintCard(card);
            }
        }

        public static void PrintDealerCards(Dealer dealer)
        {
            Console.WriteLine();
            Console.WriteLine($"Dealer's cards: <{Game.GetFaceHandValue(dealer.Hand)}>");
            foreach (var card in dealer.Hand.Cards)
            {
                if (card.IsFaceUp)
                {
                    PrintCard(card);
                }
                else
                {
                    Console.WriteLine("XXX");
                }
            }
        }

        public static void AskPlayerForBet(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"#Player's N{player.ID} cash: {player.Cash}");
            Console.WriteLine($"#Player N{player.ID}, type a bet you wish: ");

            int betValue = int.Parse(Console.ReadLine());
            Game.DoBet(player, betValue);
        }

        public static string AskPlayerForNextAction(Player player)
        {
            Console.WriteLine();
            Console.WriteLine("What do you want to do? (h/s)");

            string action = Console.ReadLine();
            return action;
        }

        public static void PrintGameResults(Game game)
        {
            Console.WriteLine();
            foreach (Player player in game.Players)
            {
                var totalPlayerValue = Game.GetTotalHandValue(player.Hand);
                var totalDealerValue = Game.GetTotalHandValue(game.Dealer.Hand);

                string result = $"#{player.ID} - LOST";
                if (totalPlayerValue == totalDealerValue && totalPlayerValue <= 21)
                {
                    result = $"#{player.ID} - DRAW";
                }
                if ((totalPlayerValue <= 21 && totalPlayerValue > totalDealerValue) || (totalDealerValue > 21 && totalPlayerValue <= 21))
                {
                    result = $"#{player.ID} - WON";
                }
                Console.WriteLine(result);
            }
        }

        public static bool AskForContinueGame()
        {
            Console.WriteLine("Press ENTER to continue or any other key to exit.");
            ConsoleKeyInfo key = Console.ReadKey();
            ConsoleKey afterGameAction = key.Key;

            if (afterGameAction == ConsoleKey.Enter)
            {
                return true;
            }
            return false;
        }

        public static int AskForNumberOfPlayers()
        {
            Console.Write("Type the number of players: ");
            int numberOfPlayers = int.Parse(Console.ReadLine());
            return numberOfPlayers;
        }

        public static void ClearConsole()
        {
            Console.Clear();
        }
    }
}
