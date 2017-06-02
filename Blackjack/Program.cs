using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Dealer dealer = new Dealer();
            List<Player> players = new List<Player>(){
                { new Player("1", dealer) },
                { new Player("2", dealer) },
                { new Player("3", dealer) }
            };

            for (;;)
            {
                Console.WriteLine("=================================================");
                dealer.GetCard();
                dealer.GetCard();

                Console.WriteLine($"Visible dealer's card: {dealer.Cards[0].ToString()}");
                foreach (var player in players)
                {
                    int initialCash = player.Cash;

                    Console.WriteLine();
                    Console.WriteLine($"#{player.Name} - Cash:{player.Cash}");
                    Console.WriteLine($"#{player.Name} Type a bet you wish: ");

                    int betValue = int.Parse(Console.ReadLine());
                    player.DoBet(betValue);
                    
                    player.Hit();
                    player.Hit();
                    player.PrintCards();

                    Console.WriteLine();
                    Console.WriteLine($"#{player.Name} - Cash:{player.Cash}");
                    char action;
                    do
                    {
                        Console.WriteLine($"#{player.Name} - Total:{player.Total}");
                        Console.WriteLine("What do you want to do? (h/s)");

                        action = Console.ReadLine()[0];

                        Console.WriteLine();
                        if (action == 'h')
                        {
                            player.Hit();
                            Console.WriteLine($"#{player.Name} gets {player.GetLastCard().ToString()}");
                        }
                    } while (action == 'h' && player.Total <= 21);

                    Console.WriteLine($"#{player.Name} - Total:{player.Total}");
                    Console.WriteLine("--------------------------");
                }
                

                while (dealer.Total <= 17)
                {
                    dealer.GetCard();
                }

                dealer.PrintCards();
                Console.WriteLine($"Dealer's total: {dealer.Total}");
                Console.WriteLine();

                foreach(var player in players)
                {
                    Console.WriteLine();
                    if ((player.Total <= 21 && player.Total > dealer.Total) || dealer.Total > 21)
                    {
                        player.GetPrize();
                        Console.WriteLine($"#{player.Name} - WON");
                    }
                    else
                    {
                        Console.WriteLine($"#{player.Name} - LOST");
                    }
                    //int finalCash = player.Cash;
                    //string diff = GetDiff(initialCash, finalCash);
                    Console.WriteLine($"#{player.Name} - Total:{player.Total}");
                    Console.WriteLine($"#{player.Name} - Cash:{player.Cash}");
                }
                dealer.Reset();
            }
        }


        //static private string GetDiff(int initialCash, int finalCash)
        //{
        //    string result = "";
        //    if (finalCash == initialCash)
        //    {
        //        result = "+0";
        //    }
        //    else if (finalCash > initialCash)
        //    {
        //        result = "+" + (finalCash - initialCash);
        //    }
        //    else
        //    {
        //        result = "" + (finalCash - initialCash);
        //    }
        //    return result;
        //}
    }
}