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
        public ObservableCollection<string> FactionsResults { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> CastersResults { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<UserResult> TopUsersResult { get; set; } = new ObservableCollection<UserResult>();

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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
