using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    abstract class Participant
    {
        public string Name { get; set; }
        public Hand Hand { get; protected set; }

        public Participant()
        {
        }
    }
}
