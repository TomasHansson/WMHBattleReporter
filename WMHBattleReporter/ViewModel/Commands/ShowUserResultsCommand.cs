using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class ShowUserResultsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public GameStatisticsViewModel ViewModel { get; set; }

        public ShowUserResultsCommand(GameStatisticsViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            User currentUser = DatabaseServices.GetUserById(DatabaseServices.LoggedInUsersId);
            ViewModel.UsersNumberOfGamesPlayed = currentUser.NumberOfGamesPlayed;
            ViewModel.UsersNumberOfGamesWon = currentUser.NumberOfGamesWon;
            ViewModel.UsersNumberOfGamesLost = currentUser.NumberOfGamesLost;
            ViewModel.UsersWinrate = currentUser.WinPercentage * 100;
        }
    }
}
