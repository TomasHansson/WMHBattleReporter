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
    public class GameStatisticsViewModel : INotifyPropertyChanged
    {
        public List<string> RegionOptions { get; } = new List<string> { "All Regions", "North America", "South America", "Europa", "Africa", "Asia", "Oceania" };
        public string SelectedRegion { get; set; }
        public ObservableCollection<UserResult> TopUsersResult { get; set; } = new ObservableCollection<UserResult>();

        public ObservableCollection<string> FactionsList { get; set; } = new ObservableCollection<string>();
        public string SelectedFaction { get; set; }
        public ObservableCollection<VersusResult> FactionsVersusResults { get; set; } = new ObservableCollection<VersusResult>();
        public ObservableCollection<Theme> FactionThemes { get; set; } = new ObservableCollection<Theme>();
        public ObservableCollection<Caster> FactionCasters { get; set; } = new ObservableCollection<Caster>();

        public ObservableCollection<Caster> CastersList { get; set; } = new ObservableCollection<Caster>();
        public Caster SelectedCaster { get; set; }
        public ObservableCollection<CasterResult> CasterResults { get; set; } = new ObservableCollection<CasterResult>();

        public ShowCasterResultsCommand ShowCasterResultsCommand { get; set; }
        public ShowFactionResultsCommand ShowFactionResultsCommand { get; set; }
        public ShowUserResultsCommand ShowUserResultsCommand { get; set; }

        private bool userResultsPageActive;
        public bool UserResultsPageActive
        {
            get { return userResultsPageActive; }
            set
            {
                userResultsPageActive = value;
                NotifyPropertyChanged();
            }
        }

        private bool castersResultsPageActive;
        public bool CastersResultsPageActive
        {
            get { return castersResultsPageActive; }
            set
            {
                castersResultsPageActive = value;
                NotifyPropertyChanged();
            }
        }

        private bool factionResultsPageActive;
        public bool FactionResultsPageActive
        {
            get { return factionResultsPageActive; }
            set
            {
                factionResultsPageActive = value;
                NotifyPropertyChanged();
            }
        }

        public GameStatisticsViewModel()
        {
            ShowCasterResultsCommand = new ShowCasterResultsCommand(this);
            ShowFactionResultsCommand = new ShowFactionResultsCommand(this);
            ShowUserResultsCommand = new ShowUserResultsCommand(this);

            FillFactionsList();
            FillCastersList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FillFactionsList()
        {
            List<Faction> factions = DatabaseServices.GetFactions();
            foreach (Faction faction in factions)
                FactionsList.Add(faction.Name);
        }

        private void FillCastersList()
        {
            List<Caster> casters = DatabaseServices.GetCasters();
            foreach (Caster caster in casters)
                CastersList.Add(caster);
        }
    }
}
