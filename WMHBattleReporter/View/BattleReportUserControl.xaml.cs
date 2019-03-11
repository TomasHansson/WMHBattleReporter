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
using WMHBattleReporter.ViewModel;

namespace WMHBattleReporter.View
{
    /// <summary>
    /// Interaction logic for BattleReportUserControl.xaml
    /// </summary>
    public partial class BattleReportUserControl : UserControl
    {
        private BattleReportViewModel _battleReportVM;

        public BattleReportUserControl()
        {
            InitializeComponent();
            _battleReportVM = Resources["BattleReportVM"] as BattleReportViewModel;
            _battleReportVM.SaveBattleReportCommand.Message += DisplayMessage;
        }

        private void DisplayMessage(string message) => MessageBox.Show(message);

        private void ResetInputButton_Click(object sender, RoutedEventArgs e)
        {
            UserFactionComboBox.SelectedIndex = -1;
            UserCasterComboBox.SelectedIndex = -1;
            OpponentFactionComboBox.SelectedIndex = -1;
            OpponentCasterComboBox.SelectedIndex = -1;
            UserWonRadioButton.IsChecked = false;
            OpponentWonRadioButton.IsChecked = false;
        }
    }
}
