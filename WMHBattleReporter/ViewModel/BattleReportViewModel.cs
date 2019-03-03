using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.Model;
using WMHBattleReporter.ViewModel.Commands;

namespace WMHBattleReporter.ViewModel
{
    public class BattleReportViewModel
    {
        private Faction usersFaction;
        public Faction UsersFaction
        {
            get { return usersFaction; }
            set
            {
                usersFaction = value;
                RefillUserCasterCollection();
            }
        }

        public Caster UsersCaster { get; set; }

        private Faction opponentsFaction;
        public Faction OpponentsFaction
        {
            get { return opponentsFaction; }
            set
            {
                opponentsFaction = value;
                RefillOpponentCasterCollection();
            }
        }

        public Caster OpponentsCaster { get; set; }

        public ObservableCollection<Faction> UserFactionOptions { get; set; } = new ObservableCollection<Faction>();
        public ObservableCollection<Caster> UserCasterOptions { get; set; } = new ObservableCollection<Caster>();
        public ObservableCollection<Faction> OpponentFactionOptions { get; set; } = new ObservableCollection<Faction>();
        public ObservableCollection<Caster> OpponentCasterOptions { get; set; } = new ObservableCollection<Caster>();

        public bool GameSizeIs35Points { get; set; }
        public bool GameSizeIs50Points { get; set; }
        public bool GameSizeIs75Points { get; set; }
        public bool GameSizeIs100Points { get; set; }

        public bool GameTypeIsMasters { get; set; }
        public bool GameTypeIsChampions { get; set; }
        public bool GameTypeIsSteamRoller { get; set; }

        public bool UserWon { get; set; }
        public bool OpponentWon { get; set; }

        public SaveBattleReportCommand SaveBattleReportCommand { get; set; }

        private bool viewSelected;
        public bool ViewSelected
        {
            get { return viewSelected; }
            set
            {
                viewSelected = value;
                if (value)
                    RefillFactionCollections();
            }
        }

        public BattleReportViewModel()
        {
            SaveBattleReportCommand = new SaveBattleReportCommand(this);

            RefillFactionCollections();
        }

        public void RefillFactionCollections()
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

        public void RefillUserCasterCollection()
        {
            UserCasterOptions.Clear();
            if (UsersFaction != null)
            {
                List<Caster> casters = DatabaseServices.GetFactionCasters(UsersFaction.Name);
                foreach (Caster caster in casters)
                    UserCasterOptions.Add(caster);
            }
        }

        public void RefillOpponentCasterCollection()
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
