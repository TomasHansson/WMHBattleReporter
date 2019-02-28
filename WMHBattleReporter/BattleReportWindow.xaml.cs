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
    /// Interaction logic for BattleReportWindow.xaml
    /// </summary>
    public partial class BattleReportWindow : Window
    {
        public BattleReportWindow()
        {
            InitializeComponent();
        }

        private void SaveBattleReport_Click(object sender, RoutedEventArgs e)
        {
            BattleReport battleReport = new BattleReport()
            {
                UserId = App.LoggedInUser.Id,
                GameSize = Convert.ToInt32(GameSizeTextBox.Text),
                GameType = GameTypeTextBox.Text,
                UserFaction = UserFactionTextBox.Text,
                UserCaster = UserCasterTextBox.Text,
                EnemyFaction = EnemyFactionTextBox.Text,
                EnemyCaster = EnemyCasterTextBox.Text
            };
            if (UserWonRadioButton.IsChecked == true)
            {
                battleReport.WinningFaction = UserFactionTextBox.Text;
                battleReport.WinningCaster = UserCasterTextBox.Text;
            }
            else if (OpponentWonRadioButton.IsChecked == true)
            {
                battleReport.WinningFaction = EnemyFactionTextBox.Text;
                battleReport.WinningCaster = EnemyCasterTextBox.Text;
            }
            using (SQLiteConnection connection = new SQLiteConnection(App.databaseFile))
            {
                connection.CreateTable<BattleReport>();
                connection.Insert(battleReport);
            }
            Close();
        }
    }
}
