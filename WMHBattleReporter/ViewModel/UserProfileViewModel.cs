using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.Model;
using WMHBattleReporter.ViewModel.Commands;

namespace WMHBattleReporter.ViewModel
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BattleReport> UnconfirmedBattleReports { get; set; } = new ObservableCollection<BattleReport>();
        public ShowUnconfirmedReportsCommand ShowUnconfirmedReportsCommand { get; set; }
        public ConfirmBattleReportCommand ConfirmBattleReportCommand { get; set; }
        public BattleReport SelectedBattleReport { get; set; }

        private int confirmationKey;        
        public int ConfirmationKey
        {
            get { return confirmationKey; }
            set { confirmationKey = value; }
        }

        private string confirmationKeyAsString;
        public string ConfirmationKeyAsString
        {
            get { return confirmationKeyAsString; }
            set
            {
                confirmationKeyAsString = value;
                if (!int.TryParse(confirmationKeyAsString, out confirmationKey))
                {
                    Message?.Invoke("You must supply an integer.");
                    confirmationKeyAsString = string.Empty;
                }
                NotifyPropertyChanged();
            }
        }

        public delegate void SendMessage(string message);
        public event SendMessage Message;

        public UserProfileViewModel()
        {
            ConfirmBattleReportCommand = new ConfirmBattleReportCommand(this);
            ShowUnconfirmedReportsCommand = new ShowUnconfirmedReportsCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
