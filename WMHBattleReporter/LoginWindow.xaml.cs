using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WMHBattleReporter
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            Visibility = Visibility.Hidden;
            registerWindow.ShowDialog();
            Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databaseFile))
            {
                connection.CreateTable<User>();
                var query = connection.Table<User>().Where(u => u.Username == UsernameTextBox.Text);
                if (query.Count() == 0)
                {
                    MessageBox.Show("No registered user with that username exists.");
                    return;
                }
                User loginUser = query.First();
                if (PasswordTextBox.Text != loginUser.Password)
                {
                    MessageBox.Show("Incorrect password.");
                    return;
                }
                App.LoggedInUser = loginUser;
            }
            Close();
        }
    }
}
