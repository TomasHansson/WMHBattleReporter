using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class ConfirmBattleReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public UserProfileViewModel ViewModel { get; set; }

        public ConfirmBattleReportCommand(UserProfileViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (ViewModel.SelectedBattleReport == null)
                return;

            if (ViewModel.ConfirmationKey != ViewModel.SelectedBattleReport.ConfirmationKey)
            {
                Message?.Invoke("You've supplied an incorrect key.");
                return;
            }
            else
            {
                ViewModel.SelectedBattleReport.ConfirmedByOpponent = true;
                DatabaseServices.UpdateItem(ViewModel.SelectedBattleReport);
                ViewModel.UnconfirmedBattleReports.Remove(ViewModel.SelectedBattleReport);
                Message?.Invoke("You've confirmed the selected battle-report.");
            }
        }

        public delegate void SendMessage(string message);
        public event SendMessage Message;
    }
}
