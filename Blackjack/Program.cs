using System;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Dealer dealer = new Dealer();
            Player player = new Player(dealer);

            for(;;)
            {
                Console.WriteLine("Type a bet you wish: ");

                int betValue = int.Parse(Console.ReadLine());
                player.DoBet(betValue);
            }
            foreach (var card in dealer.Deck)
            {
                Console.WriteLine(card.ToString() + " " + card.GetValue());
            }
        }

        private void GetHelp()
        {

        }
    }
}