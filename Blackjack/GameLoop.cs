using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class GameLoop
    {
        Game game = new Game(5);

        public void Start()
        {
            for (;;)
            { 
                game.ClearAllHands();
                game.PrepareDeckToTheNewGame();
                game.GiveInitialCardsToEverybody();

                ConsoleHelper.PrintDealerCards(game.Dealer);

                foreach (Player player in game.Players)
                {
                    ConsoleHelper.AskPlayerForBet(player);
                    ConsoleHelper.PrintPlayerCards(player);

                    string action = "";
                    while (action != "s" && player.Hand.TotalValue <= 21)
                    {
                        action = ConsoleHelper.AskPlayerForTheNextAction(player);
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

                game.GiveCardsToDealerUntilFull();
                game.ShowHand(game.Dealer.Hand);
                ConsoleHelper.PrintDealerCards(game.Dealer);

                game.CountTheGameResults();
                ConsoleHelper.PrintTheGameResults(game);

                bool wantToContinueTheGame = ConsoleHelper.AskForContinueTheGame();
                if (wantToContinueTheGame)
                {
                    ConsoleHelper.ClearTheConsole();
                    continue;
                }
                break;
            }
        }
    }
}
