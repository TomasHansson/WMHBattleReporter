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

        public LoginViewModel ViewModel { get; set; }

        public LogoutCommand(LoginViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.LoggedInUser = null;
            DatabaseServices.LoggedInUser = null;
            DatabaseServices.UserLoggedIn = false;
            ViewModel.LoggedInUsersUsername = string.Empty;
            ViewModel.UserLoggedIn = false;
            ViewModel.NoUserLoggedIn = true;
            ViewModel.LoggedInUserIsAdmin = false;
        }
    }
}
