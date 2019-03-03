using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.Model;
using WMHBattleReporter.ViewModel.Commands;

namespace WMHBattleReporter.ViewModel
{
    public class LoginOrRegisterViewModel : INotifyPropertyChanged
    {
        public LoginCommand LoginCommand { get; set; }
        public RegisterCommand RegisterCommand { get; set; }
        public LogoutCommand LogoutCommand { get; set; }
        private string loggedInUsersUsername;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string LoggedInUsersUsername
        {
            get { return loggedInUsersUsername; }
            set
            {
                loggedInUsersUsername = value;
                NotifyPropertyChanged();
            }
        }
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginOrRegisterViewModel()
        {
            LoginCommand = new LoginCommand(this);
            RegisterCommand = new RegisterCommand(this);
            LogoutCommand = new LogoutCommand(this);
        }
    }
}
