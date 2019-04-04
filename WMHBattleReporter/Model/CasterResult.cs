using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMHBattleReporter.Model
{
    public class CasterResult
    {
        public int NumberOfGamesPlayed { get; set; }
        public int NumberOfGamesWon { get; set; }
        public int NumberOfGamesLost { get; set; }
        public float Winrate { get; set; }
        public string BestScenario { get; set; }
        public double BestScenarioRate { get; set; }
        public string WorstScenario { get; set; }
        public double WorstScenarioRate { get; set; }
        public double ScenarioRate { get; set; }
        public double AssassinationRate { get; set; }
        public double ClockRate { get; set; }
        public double LossScenarioRate { get; set; }
        public double LossAssassinationRate { get; set; }
        public double LossClockRate { get; set; }
    }
}

