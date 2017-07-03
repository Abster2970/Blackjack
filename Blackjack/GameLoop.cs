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
                //Preparing to the new game
                game.ClearAllHands();
                game.PrepareDeckToNewGame();
                game.GiveInitialCardsToEverybody();

                ConsoleHelper.PrintDealerCards(game.Dealer);

                //Main part of the game - players receive cards until their hands are full or they stop
                game.AskAllPlayers(game);

                //Giving cards to the dealer and printing them
                game.GiveCardsToDealerUntilFull();
                game.ShowHand(game.Dealer.Hand);
                ConsoleHelper.PrintDealerCards(game.Dealer);
                
                //Counting and printing the game results
                game.CountGameResults();
                ConsoleHelper.PrintGameResults(game);

                //Asking user if he wants to continue the game or not
                bool wantToContinueGame = ConsoleHelper.AskForContinueGame();
                if (!wantToContinueGame)
                {
                    return;
                }
                ConsoleHelper.ClearConsole();
            }
        }
    }
}
