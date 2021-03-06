﻿using SQLite;
using System;

namespace WMHBattleReporter.Model
{
    public class BattleReport
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime DatePlayed { get; set; }
        public string PostersUsername { get; set; }
        public string OpponentsUsername { get; set; }
        public bool ConfirmedByOpponent { get; set; }
        public int ConfirmationKey { get; set; }
        public int GameSize { get; set; }
        public string Scenario { get; set; }
        public string PostersFaction { get; set; }
        public string PostersCaster { get; set; }
        public string PostersTheme { get; set; }
        public int PostersControlPoints { get; set; }
        public int PostersArmyPoints { get; set; }
        public string PostersArmyList { get; set; }
        public string OpponentsFaction { get; set; }
        public string OpponentsCaster { get; set; }
        public string OpponentsTheme { get; set; }
        public int OpponentsControlPoints { get; set; }
        public int OpponentsArmyPoints { get; set; }
        public string OpponentsArmyList { get; set; }
        public string EndCondition { get; set; }
        public string WinnersUsername { get; set; }
        public string WinningFaction { get; set; }
        public string WinningCaster { get; set; }
        public string WinningTheme { get; set; }
        public string LosersUsername { get; set; }
        public string LosingFaction { get; set; }
        public string LosingCaster { get; set; }
        public string LosingTheme { get; set; }

        public override string ToString()
        {
            return DatePlayed.ToShortDateString();
        }
    }
}
