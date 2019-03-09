using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.ViewModel.Commands;

namespace WMHBattleReporter.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public SetActivePageCommand SetActivePageCommand { get; set; }

        private bool dashboardPageActive = true;
        public bool DashboardPageActive
        {
            get { return dashboardPageActive; }
            set
            {
                dashboardPageActive = value;
                NotifyPropertyChanged();
            }
        }

        private bool gameEntryPageActive;
        public bool GameEntryPageActive
        {
            get { return gameEntryPageActive; }
            set
            {
                gameEntryPageActive = value;
                NotifyPropertyChanged();
            }
        }

        private bool statsViewerPageActive;
        public bool StatsViewerPageActive
        {
            get { return statsViewerPageActive; }
            set
            {
                statsViewerPageActive = value;
                NotifyPropertyChanged();
            }
        }

        private bool gameDataPageActive;
        public bool GameDataPageActive
        {
            get { return gameDataPageActive; }
            set
            {
                gameDataPageActive = value;
                NotifyPropertyChanged();
            }
        }

        private bool registerPageActive;
        public bool RegisterPageActive
        {
            get { return registerPageActive; }
            set
            {
                registerPageActive = value;
                NotifyPropertyChanged();
            }
        }

        private bool adminPageActive;
        public bool AdminPageActive
        {
            get { return adminPageActive; }
            set
            {
                adminPageActive = value;
                NotifyPropertyChanged();
            }
        }

        private string chosenPage = "Dashboard";
        public string ChosenPage
        {
            get { return chosenPage; }
            set
            {
                chosenPage = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MenuViewModel()
        {
            SetActivePageCommand = new SetActivePageCommand(this);
        }
    }
}
