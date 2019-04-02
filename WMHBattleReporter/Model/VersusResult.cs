using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMHBattleReporter.Model
{
    public class VersusResult
    {
        public string Opponent { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesLost { get; set; }
        public int GamesWon { get; set; }
        public double Winrate { get; set; }
    }
}
