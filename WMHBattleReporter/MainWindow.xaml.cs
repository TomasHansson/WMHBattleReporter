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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WMHBattleReporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void AddBattleReport_Click(object sender, RoutedEventArgs e)
        {
            BattleReportWindow battleReportWindow = new BattleReportWindow();
            Visibility = Visibility.Hidden;
            battleReportWindow.ShowDialog();
            Visibility = Visibility.Visible;
        }
    }
}
