using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Participant dealer = new Dealer("Dealer");
            List<Participant> players = new List<Participant>(){
                { new Player("1", dealer as Dealer) },
                { new Player("2", dealer as Dealer) },
                { new Player("3", dealer as Dealer) }
            };

            for (;;)
            {
                Console.WriteLine("=================================================");
                (dealer as Dealer).GetCard();
                (dealer as Dealer).GetCard();

                Console.WriteLine($"Visible dealer's card: {dealer.Cards[0].ToString()}");
                foreach (var player in players)
                {
                    int initialCash = (player as Player).Cash;

                    Console.WriteLine();
                    Console.WriteLine($"#{player.Name} - Cash:{(player as Player).Cash}");
                    Console.WriteLine($"#{player.Name} Type a bet you wish: ");

                    int betValue = int.Parse(Console.ReadLine());
                    (player as Player).DoBet(betValue);

                    (player as Player).Hit();
                    (player as Player).Hit();
                    player.PrintCards();

                    Console.WriteLine();
                    Console.WriteLine($"#{player.Name} - Cash:{(player as Player).Cash}");
                    char action;
                    do
                    {
                        Console.WriteLine($"#{player.Name} - Total:{player.Total}");
                        Console.WriteLine("What do you want to do? (h/s)");

                        action = Console.ReadLine()[0];

                        Console.WriteLine();
                        if (action == 'h')
                        {
                            (player as Player).Hit();
                            Console.WriteLine($"#{player.Name} gets {(player as Player).GetLastCard().ToString()}");
                        }
                    } while (action == 'h' && player.Total <= 21);

                    Console.WriteLine($"#{player.Name} - Total:{player.Total}");
                    Console.WriteLine("--------------------------");
                }
                

                while (dealer.Total <= 17)
                {
                    (dealer as Dealer).GetCard();
                }

                dealer.PrintCards();
                Console.WriteLine($"Dealer's total: {dealer.Total}");
                Console.WriteLine();

                foreach(var player in players)
                {
                    Console.WriteLine();
                    if ((player.Total <= 21 && player.Total > dealer.Total) || dealer.Total > 21)
                    {
                        (player as Player).GetPrize();
                        Console.WriteLine($"#{player.Name} - WON");
                    }
                    else
                    {
                        Console.WriteLine($"#{player.Name} - LOST");
                    }

                    Console.WriteLine($"#{player.Name} - Total:{player.Total}");
                    Console.WriteLine($"#{player.Name} - Cash:{(player as Player).Cash}");
                }
                (dealer as Dealer).Reset();
            }
        }
    }
}