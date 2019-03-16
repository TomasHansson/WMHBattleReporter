using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel
{
    public class GameDataViewModel
    {
        public ObservableCollection<BattleReport> BattleReports { get; set; } = new ObservableCollection<BattleReport>();

        public GameDataViewModel()
        {
            FillCollection();
        }

        private void FillCollection()
        {
            List<BattleReport> battleReports = DatabaseServices.GetBattleReports().OrderByDescending(br => br.DatePlayed).ToList();
            foreach (BattleReport battleReport in battleReports)
                BattleReports.Add(battleReport);
        }
    }
}
