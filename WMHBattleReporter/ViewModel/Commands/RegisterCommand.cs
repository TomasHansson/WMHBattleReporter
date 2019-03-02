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

        public RegisterViewModel RegisterViewModel { get; set; }

        public RegisterCommand(RegisterViewModel registerViewModel)
        {
            RegisterViewModel = registerViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (DatabaseServices.UsernameExists(RegisterViewModel.Username))
                return;

            User newUser = new User()
            {
                Username = RegisterViewModel.Username,
                Password = RegisterViewModel.Password
            };

            DatabaseServices.SaveUser(newUser);
            DatabaseServices.LoggedInUser = DatabaseServices.GetUser(RegisterViewModel.Username);
        }
    }
}
