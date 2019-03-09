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

        public RegisterViewModel ViewModel { get; set; }

        public RegisterCommand(RegisterViewModel viewModel)
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
            {
                ErrorMessage?.Invoke("That username is already taken.");
                return;
            }

            if (ViewModel.Password != ViewModel.ConfirmedPassword)
            {
                ErrorMessage?.Invoke("Your password and confirmed password must match.");
                return;
            }
                
            User newUser = new User()
            {
                Username = ViewModel.Username,
                Password = ViewModel.Password,
                Region = ViewModel.Region,
                Email = ViewModel.Email
            };

            DatabaseServices.InsertItem(newUser);
        }

        public delegate void SendErrorMessage(string message);
        public event SendErrorMessage ErrorMessage;
    }
}
