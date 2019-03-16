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
    /// Interaction logic for UserProfileControl.xaml
    /// </summary>
    public partial class UserProfileControl : UserControl
    {
        private UserProfileViewModel _userProfileVM;

        public UserProfileControl()
        {
            InitializeComponent();
            _userProfileVM = Resources["UserProfileVM"] as UserProfileViewModel;
            _userProfileVM.Message += ShowMessage;
            _userProfileVM.ConfirmBattleReportCommand.Message += ShowMessage;
        }

        private void ShowMessage(string message) => MessageBox.Show(message);
    }
}
