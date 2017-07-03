namespace Blackjack
{
    class Player : Participant
    {
        public int Cash { get; set; }
        public int BetValue { get; set; }
        public Dealer Dealer { get; private set; }

        public Player(int id, Dealer dealer, int cash)
        {
            Hand = new Hand(isDealer: false);
            ID = id;
            Dealer = dealer;
            Cash = cash;
        }
    }
}
