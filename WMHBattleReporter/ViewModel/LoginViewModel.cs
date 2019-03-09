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
    public class LoginViewModel : INotifyPropertyChanged
    {
        public LoginCommand LoginCommand { get; set; }
        public LogoutCommand LogoutCommand { get; set; }
        public User LoggedInUser { get; set; }

        private bool userLoggedIn;
        public bool UserLoggedIn
        {
            get { return userLoggedIn; }
            set
            {
                userLoggedIn = value;
                NotifyPropertyChanged();
            }
        }

        private bool noUserLoggedIn = true;
        public bool NoUserLoggedIn
        {
            get { return noUserLoggedIn; }
            set
            {
                noUserLoggedIn = value;
                NotifyPropertyChanged();
            }
        }

        private string loggedInUsersUsername;
        public string LoggedInUsersUsername
        {
            get { return "Current User: " + loggedInUsersUsername; }
            set
            {
                loggedInUsersUsername = value;
                NotifyPropertyChanged();
            }
        }

        private bool loggedInUserIsAdmin;
        public bool LoggedInUserIsAdmin
        {
            get { return loggedInUserIsAdmin; }
            set
            {
                loggedInUserIsAdmin = value;
                NotifyPropertyChanged();
            }
        }


        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                NotifyPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LoginViewModel()
        {
            LoginCommand = new LoginCommand(this);
            LogoutCommand = new LogoutCommand(this);
        }
    }
}
