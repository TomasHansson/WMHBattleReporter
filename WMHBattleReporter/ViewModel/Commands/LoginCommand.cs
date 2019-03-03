﻿using System;
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

        public LoginOrRegisterViewModel ViewModel { get; set; }

        public LoginCommand(LoginOrRegisterViewModel viewModel)
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
                return;

            ViewModel.LoggedInUsersUsername = DatabaseServices.GetUser(ViewModel.Username).Username;
        }
    }
}
