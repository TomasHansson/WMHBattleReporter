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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Text))
            {
                MessageBox.Show("You must specify a Username and a Password.");
                return;
            }
            using (SQLiteConnection connection = new SQLiteConnection(App.databaseFile))
            {
                connection.CreateTable<User>();
                var query = connection.Table<User>().Where(u => u.Username == UsernameTextBox.Text);
                if (query.Count() > 0)
                {
                    MessageBox.Show("That username is already used.");
                    return;
                }
                User newUser = new User()
                {
                    Username = UsernameTextBox.Text,
                    Password = PasswordTextBox.Text
                };
                connection.Insert(newUser);
            }
            Close();
        }
    }
}
