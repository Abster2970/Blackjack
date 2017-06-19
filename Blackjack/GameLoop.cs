namespace Blackjack
{
    class GameLoop
    {
        Game game;

        public void Start()
        {
            int numberOfPlayers = ConsoleHelper.AskForNumberOfPlayers();
            game = new Game(numberOfPlayers);

            for (;;)
            {
                #region Preparing to the new game
                game.ClearAllHands();
                game.PrepareDeckToNewGame();
                game.GiveInitialCardsToEverybody();
                #endregion

                ConsoleHelper.PrintDealerCards(game.Dealer);

                #region General part of the game - players get cards until their hands are full or they considered to stop
                foreach (Player player in game.Players)
                {
                    ConsoleHelper.AskPlayerForBet(player);
                    ConsoleHelper.PrintPlayerCards(player);

                    string action = "";
                    while (action != "s" && player.Hand.TotalValue <= 21)
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
                #endregion

                #region Giving cards to the dealer and printing them
                game.GiveCardsToDealerUntilFull();
                game.ShowHand(game.Dealer.Hand);
                ConsoleHelper.PrintDealerCards(game.Dealer);
                #endregion

                #region Counting and printing the game results
                game.CountGameResults();
                ConsoleHelper.PrintGameResults(game);
                #endregion

                #region Asking user if he wants to continue the game or not
                bool wantToContinueGame = ConsoleHelper.AskForContinueGame();
                if (wantToContinueGame)
                {
                    ConsoleHelper.ClearConsole();
                    continue;
                }
                break;
                #endregion
            }
        }
    }
}
