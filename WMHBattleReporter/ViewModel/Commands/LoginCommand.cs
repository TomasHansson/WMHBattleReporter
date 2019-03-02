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

        public LoginViewModel LoginViewModel { get; set; }

        public LoginCommand(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!DatabaseServices.UsernameExists(LoginViewModel.Username) || !DatabaseServices.PasswordIsCorrect(LoginViewModel.Username, LoginViewModel.Password))
                return;

            DatabaseServices.LoggedInUser = DatabaseServices.GetUser(LoginViewModel.Username);
        }
    }
}
