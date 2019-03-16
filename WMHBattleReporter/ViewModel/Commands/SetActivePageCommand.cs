using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class SetActivePageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public MenuViewModel ViewModel { get; set; }

        public SetActivePageCommand(MenuViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SetAllPagesToInactive();
            string chosenPage = (string)parameter;
            switch (chosenPage)
            {
                case "Admin": ViewModel.AdminPageActive = true; ViewModel.ChosenPage = "Admin"; break;
                case "Dashboard": ViewModel.DashboardPageActive = true; ViewModel.ChosenPage = "Dashboard"; break;
                case "GameData": ViewModel.GameDataPageActive = true; ViewModel.ChosenPage = "Game Data"; break;
                case "GameEntry": ViewModel.GameEntryPageActive = true; ViewModel.ChosenPage = "Game Entry"; break;
                case "Register": ViewModel.RegisterPageActive = true; ViewModel.ChosenPage = "Register"; break;
                case "StatsViewer": ViewModel.StatsViewerPageActive = true; ViewModel.ChosenPage = "Stats Viewer"; break;
                case "UserProfile": ViewModel.UserProfilePageActive = true; ViewModel.ChosenPage = "User Profile"; break;
                default: ViewModel.DashboardPageActive = true; ViewModel.ChosenPage = "Dashboard"; break;
            }
        }

        private void SetAllPagesToInactive()
        {
            ViewModel.AdminPageActive = false;
            ViewModel.DashboardPageActive = false;
            ViewModel.GameDataPageActive = false;
            ViewModel.GameEntryPageActive = false;
            ViewModel.RegisterPageActive = false;
            ViewModel.StatsViewerPageActive = false;
            ViewModel.UserProfilePageActive = false;
        }
    }
}
