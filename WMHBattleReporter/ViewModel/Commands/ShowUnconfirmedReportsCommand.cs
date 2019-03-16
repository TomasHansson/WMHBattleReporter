using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class ShowUnconfirmedReportsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public UserProfileViewModel ViewModel { get; set; }

        public ShowUnconfirmedReportsCommand(UserProfileViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.UnconfirmedBattleReports.Clear();
            List<BattleReport> battleReports = DatabaseServices.GetBattleReports()
                .Where(br => br.OpponentsUsername == DatabaseServices.LoggedInUser.Username && br.ConfirmedByOpponent == false).ToList();
            foreach (BattleReport battleReport in battleReports)
                ViewModel.UnconfirmedBattleReports.Add(battleReport);
        }
    }
}
