using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.Model;
using WMHBattleReporter.ViewModel.Commands;

namespace WMHBattleReporter.ViewModel
{
    public class BattleReportViewModel
    {
        public string OpponentsUsername { get; set; }

        private Faction usersFaction;
        public Faction UsersFaction
        {
            get { return usersFaction; }
            set
            {
                usersFaction = value;
                RefillUserThemeCollection();
                RefillUserCasterCollection();
            }
        }
        public Theme UsersTheme { get; set; }
        public Caster UsersCaster { get; set; }

        private Faction opponentsFaction;
        public Faction OpponentsFaction
        {
            get { return opponentsFaction; }
            set
            {
                opponentsFaction = value;
                RefillOpponentThemeCollection();
                RefillOpponentCasterCollection();
            }
        }
        public Theme OpponentsTheme { get; set; }
        public Caster OpponentsCaster { get; set; }

        public ObservableCollection<Faction> UserFactionOptions { get; set; } = new ObservableCollection<Faction>();
        public ObservableCollection<Theme> UserThemeOptions { get; set; } = new ObservableCollection<Theme>();
        public ObservableCollection<Caster> UserCasterOptions { get; set; } = new ObservableCollection<Caster>();
        public ObservableCollection<Faction> OpponentFactionOptions { get; set; } = new ObservableCollection<Faction>();
        public ObservableCollection<Theme> OpponentThemeOptions { get; set; } = new ObservableCollection<Theme>();
        public ObservableCollection<Caster> OpponentCasterOptions { get; set; } = new ObservableCollection<Caster>();

        public List<int> GameSizeOptions { get; } = new List<int>() { 35, 50, 75, 100 };
        public int GameSize { get; set; }

        public List<string> ScenarioOptions { get; } = new List<string>() { "Scenario 1", "Scenario 2" };
        public string Scenario { get; set; }

        public List<string> EndConditionOptions { get; } = new List<string>() { "Scenario", "Assassination", "Clock" };
        public string EndCondition { get; set; }

        public int UsersControlPoints { get; set; }
        public int UsersArmyPoints { get; set; }
        public string UsersArmyList { get; set; }

        public int OpponentsControlPoints { get; set; }
        public int OpponentsArmyPoints { get; set; }
        public string OpponentsArmyList { get; set; }

        public bool UserWon { get; set; }
        public bool OpponentWon { get; set; }

        public DateTime DatePlayed { get; set; } = DateTime.Today;

        public SaveBattleReportCommand SaveBattleReportCommand { get; set; }

        public BattleReportViewModel()
        {
            SaveBattleReportCommand = new SaveBattleReportCommand(this);

            FillFactionCollections();
        }

        private void FillFactionCollections()
        {
            UserFactionOptions.Clear();
            OpponentFactionOptions.Clear();
            List<Faction> factions = DatabaseServices.GetFactions();
            foreach (Faction faction in factions)
            {
                UserFactionOptions.Add(faction);
                OpponentFactionOptions.Add(faction);
            }
        }

        private void RefillUserThemeCollection()
        {
            UserThemeOptions.Clear();
            if (UsersFaction != null)
            {
                List<Theme> themes = DatabaseServices.GetFactionThemes(UsersFaction.Name);
                foreach (Theme theme in themes)
                    UserThemeOptions.Add(theme);
            }
        }

        private void RefillUserCasterCollection()
        {
            UserCasterOptions.Clear();
            if (UsersFaction != null)
            {
                List<Caster> casters = DatabaseServices.GetFactionCasters(UsersFaction.Name);
                foreach (Caster caster in casters)
                    UserCasterOptions.Add(caster);
            }
        }

        private void RefillOpponentThemeCollection()
        {
            OpponentThemeOptions.Clear();
            if (OpponentsFaction != null)
            {
                List<Theme> themes = DatabaseServices.GetFactionThemes(OpponentsFaction.Name);
                foreach (Theme theme in themes)
                    OpponentThemeOptions.Add(theme);
            }
        }

        private void RefillOpponentCasterCollection()
        {
            OpponentCasterOptions.Clear();
            if (OpponentsFaction != null)
            {
                List<Caster> casters = DatabaseServices.GetFactionCasters(OpponentsFaction.Name);
                foreach (Caster caster in casters)
                    OpponentCasterOptions.Add(caster);
            }
        }
    }
}
