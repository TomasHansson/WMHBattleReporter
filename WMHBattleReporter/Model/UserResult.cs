﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMHBattleReporter.Model
{
    public class UserResult
    {
        public string Username { get; set; }
        public int NumberOfGamesPlayed { get; set; }
        public int NumberOfGamesWon { get; set; }
        public int NumberOfGamesLost { get; set; }
        public float Winrate { get; set; }
        public string MostPlayedFaction { get; set; }
        public int GamesWithMostPlayedFaction { get; set; }
        public string MostPlayedTheme { get; set; }
        public int GamesWithMostPlayedTheme { get; set; }
        public string MostPlayedCaster { get; set; }
        public int GamesWithMostPlayedCaster { get; set; }
        public string BestPerformingFaction { get; set; }
        public float WinrateBestPerformingFaction { get; set; }
        public string BestPerformingTheme { get; set; }
        public float WinrateBestPerformingTheme { get; set; }
        public string BestPerformingCaster { get; set; }
        public float WinrateBestPerformingCaster { get; set; }
    }
}
