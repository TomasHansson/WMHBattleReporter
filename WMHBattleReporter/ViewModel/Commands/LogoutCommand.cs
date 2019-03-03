using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class LogoutCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public LoginOrRegisterViewModel ViewModel { get; set; }

        public LogoutCommand(LoginOrRegisterViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.LoggedInUsersUsername = "";
            DatabaseServices.LoggedInUsersId = 0;
        }
    }
}
