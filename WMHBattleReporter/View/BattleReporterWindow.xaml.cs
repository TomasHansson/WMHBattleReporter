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
using WMHBattleReporter.ViewModel;

namespace WMHBattleReporter.View
{
    /// <summary>
    /// Interaction logic for BattleReporterWindow.xaml
    /// </summary>
    public partial class BattleReporterWindow : Window
    {
        public BattleReportViewModel BattleReportViewModel { get; set; }

        public BattleReporterWindow()
        {
            InitializeComponent();
            BattleReportViewModel = Resources["BattleReportVM"] as BattleReportViewModel;
            BattleReportViewModel.SaveBattleReportCommand.SaveComplete += DisplayMessage;
        }

        private void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void AddFactionButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatabaseServices.FactionNameExists(NewFactionTextBox.Text))
            {
                MessageBox.Show("A faction with that name already exists in the database.");
                return;
            }
        }

        private void AddCasterButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatabaseServices.CasterNameExists(NewCasterTextBox.Text))
            {
                MessageBox.Show("A caster with that name already exists.");
                return;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Text = "";
            PasswordTextBox.Text = "";
            LoggedInGrid.Visibility = Visibility.Hidden;
            LoginOrRegisterGrid.Visibility = Visibility.Visible;
            GameStatisticsTabItem.IsSelected = true;
            AddBattleReportTabItem.Visibility = Visibility.Collapsed;
            AdminPageTabItem.Visibility = Visibility.Collapsed;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatabaseServices.UsernameExists(UsernameTextBox.Text))
            {
                MessageBox.Show("The username is already taken.");
                return;
            }

            LoginOrRegisterGrid.Visibility = Visibility.Hidden;
            LoggedInGrid.Visibility = Visibility.Visible;
            AddBattleReportTabItem.Visibility = Visibility.Visible;
            AdminPageTabItem.Visibility = Visibility.Visible;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DatabaseServices.UsernameExists(UsernameTextBox.Text) || !DatabaseServices.PasswordIsCorrect(UsernameTextBox.Text, PasswordTextBox.Text))
            {
                MessageBox.Show("Username and/or Password is incorrect.");
                return;
            }
                
            LoginOrRegisterGrid.Visibility = Visibility.Hidden;
            LoggedInGrid.Visibility = Visibility.Visible;
            AddBattleReportTabItem.Visibility = Visibility.Visible;
            AdminPageTabItem.Visibility = Visibility.Visible;
        }

        private void AddBattleReportTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            ResetNewBattleReportInformation();
        }

        private void ResetInputButton_Click(object sender, RoutedEventArgs e)
        {
            ResetNewBattleReportInformation();
        }

        private void ResetNewBattleReportInformation()
        {
            UserFactionComboBox.SelectedIndex = -1;
            UserCasterComboBox.SelectedIndex = -1;
            OpponentFactionComboBox.SelectedIndex = -1;
            OpponentCasterComboBox.SelectedIndex = -1;
            Points35RadioButton.IsChecked = false;
            Points50RadioButton.IsChecked = false;
            Points75RadioButton.IsChecked = false;
            Points100RadioButton.IsChecked = false;
            MastersRadioButton.IsChecked = false;
            ChampionsRadioButton.IsChecked = false;
            SteamRollerRadioButton.IsChecked = false;
            UserWonRadioButton.IsChecked = false;
            OpponentWonRadioButton.IsChecked = false;
        }

        private void ShowUsersResultsButton_Click(object sender, RoutedEventArgs e)
        {
            UserResultsStackPanel.Visibility = Visibility.Visible;
            FactionResultsComboBox.Visibility = Visibility.Collapsed;
            CasterResultsComboBox.Visibility = Visibility.Collapsed;
        }

        private void ShowFactionResultsButton_Click(object sender, RoutedEventArgs e)
        {
            UserResultsStackPanel.Visibility = Visibility.Collapsed;
            FactionResultsComboBox.Visibility = Visibility.Visible;
            CasterResultsComboBox.Visibility = Visibility.Collapsed;
        }

        private void ShowCasterResultsButton_Click(object sender, RoutedEventArgs e)
        {
            UserResultsStackPanel.Visibility = Visibility.Collapsed;
            FactionResultsComboBox.Visibility = Visibility.Collapsed;
            CasterResultsComboBox.Visibility = Visibility.Visible;
        }
    }
}
