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
    public class RegisterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public LoginOrRegisterViewModel ViewModel { get; set; }

        public RegisterCommand(LoginOrRegisterViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (DatabaseServices.UsernameExists(ViewModel.Username))
                return;

            User newUser = new User()
            {
                Username = ViewModel.Username,
                Password = ViewModel.Password
            };

            DatabaseServices.InsertItem(newUser);
            DatabaseServices.LoggedInUsersId = DatabaseServices.GetUserById(ViewModel.Username).Id;
            ViewModel.LoggedInUsersUsername = ViewModel.Username;
        }
    }
}
