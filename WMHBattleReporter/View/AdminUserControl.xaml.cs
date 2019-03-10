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
    /// Interaction logic for AdminUserControl.xaml
    /// </summary>
    public partial class AdminUserControl : UserControl
    {
        private AdminViewModel _adminVM;

        public AdminUserControl()
        {
            InitializeComponent();
            _adminVM = Resources["AdminVM"] as AdminViewModel;
            _adminVM.AddFactionCommand.Message += DisplayMessage;
            _adminVM.AddCasterCommand.Message += DisplayMessage;
        }

        private void DisplayMessage(string message) => MessageBox.Show(message);
    }
}
