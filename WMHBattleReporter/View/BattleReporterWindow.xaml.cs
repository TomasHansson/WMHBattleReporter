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
        private AdminViewModel _adminVM;
        private BattleReportViewModel _battleReportVM;
        private GameStatisticsViewModel _gameStatisticsVM;
        private LoginViewModel _loginVM;
        private RegisterViewModel _registerVM;

        public BattleReporterWindow()
        {
            InitializeComponent();
            SetupViewModelProperties();
            SetupMessageEventSubscriptions();
        }

        private void SetupViewModelProperties()
        {
            _adminVM = Resources["AdminVM"] as AdminViewModel;
            _battleReportVM = Resources["BattleReportVM"] as BattleReportViewModel;
            _gameStatisticsVM = Resources["GameStatisticsVM"] as GameStatisticsViewModel;
            _loginVM = Resources["LoginVM"] as LoginViewModel;
            _registerVM = Resources["RegisterVM"] as RegisterViewModel;
        }

        private void SetupMessageEventSubscriptions()
        {
            _battleReportVM.SaveBattleReportCommand.SaveComplete += DisplayMessage;
            _loginVM.LoginCommand.ErrorMessage += DisplayMessage;
            _adminVM.AddFactionCommand.ErrorMessage += DisplayMessage;
        }

        private void DisplayMessage(string message) => MessageBox.Show(message);

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
