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

        public ShowCasterResultsCommand ShowCasterResultsCommand { get; set; }
        public ShowFactionResultsCommand ShowFactionResultsCommand { get; set; }
        public ShowUserResultsCommand ShowUserResultsCommand { get; set; }

        private string currentUsersMostPlayedFaction;
        public string CurrentUsersMostPlayedFaction
        {
            get { return currentUsersMostPlayedFaction; }
            set
            {
                currentUsersMostPlayedFaction = value;
                NotifyPropertyChanged();
            }
        }

        public string CurrentUsersMostPlayedTheme { get; set; }

        private string currentUsersMostPlayedCaster;
        public string CurrentUsersMostPlayedCaster
        {
            get { return currentUsersMostPlayedCaster; }
            set
            {
                currentUsersMostPlayedCaster = value;
                NotifyPropertyChanged();
            }
        }

        private string currentUsersBestFaction;
        public string CurrentUsersBestFaction
        {
            get { return currentUsersBestFaction; }
            set
            {
                currentUsersBestFaction = value;
                NotifyPropertyChanged();
            }
        }

        public string CurrentUsersBestTheme { get; set; }

        private string currentUsersBestCaster;
        public string CurrentUsersBestCaster
        {
            get { return currentUsersBestCaster; }
            set
            {
                currentUsersBestCaster = value;
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


        private int usersNumberOfGamesPlayed;
        public int UsersNumberOfGamesPlayed
        {
            get { return usersNumberOfGamesPlayed; }
            set
            {
                usersNumberOfGamesPlayed = value;
                NotifyPropertyChanged();
            }
        }

        private int usersNumberOfGamesWon;
        public int UsersNumberOfGamesWon
        {
            get { return usersNumberOfGamesWon; }
            set
            {
                usersNumberOfGamesWon = value;
                NotifyPropertyChanged();
            }
        }

        private int usersNumberOfGamesLost;
        public int UsersNumberOfGamesLost
        {
            get { return usersNumberOfGamesLost; }
            set
            {
                usersNumberOfGamesLost = value;
                NotifyPropertyChanged();
            }
        }

        private float usersWinrate;
        public float UsersWinrate
        {
            get { return usersWinrate; }
            set
            {
                usersWinrate = value;
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
