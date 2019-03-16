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
        private LoginViewModel _loginVM;

        public BattleReporterWindow()
        {
            InitializeComponent();
            _loginVM = Resources["LoginVM"] as LoginViewModel;
            _loginVM.LoginCommand.Message += DisplayMessage;
        }

        private void DisplayMessage(string message) => MessageBox.Show(message);

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardButton.Command.Execute("Dashboard");
        }
    }
}
