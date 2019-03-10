using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WMHBattleReporter.Model;
using WMHBattleReporter.View;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public LoginViewModel ViewModel { get; set; }

        public LoginCommand(LoginViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!DatabaseServices.UsernameExists(ViewModel.Username) || !DatabaseServices.PasswordIsCorrect(ViewModel.Username, ViewModel.Password))
            {
                Message?.Invoke("The username and/or password is incorrect.");
                return;
            }

            ViewModel.LoggedInUser = DatabaseServices.GetUser(ViewModel.Username);
            DatabaseServices.LoggedInUser = ViewModel.LoggedInUser;
            DatabaseServices.UserLoggedIn = true;
            ViewModel.LoggedInUsersUsername = ViewModel.Username;
            ViewModel.UserLoggedIn = true;
            ViewModel.NoUserLoggedIn = false;
            ViewModel.LoggedInUserIsAdmin = ViewModel.LoggedInUser.IsAdmin;

            ViewModel.Username = string.Empty;
            ViewModel.Password = string.Empty;
        }

        public delegate void SendMessage(string message);
        public event SendMessage Message;
    }
}
