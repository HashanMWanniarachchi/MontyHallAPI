using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallAPI
{
    public class SimulatedSession
    {
        public int NumberOfGames { get; set; }
        public bool ShouldSwitch { get; set; }

        public int Wins { get; set; }

        public SimulatedSession(int games, bool switching, int wins)
        {
            this.NumberOfGames = games;
            this.ShouldSwitch = switching;
            this.Wins = wins;
        }
    }
}
