using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.ViewModel.Commands;

namespace WMHBattleReporter.ViewModel
{
    public class BattleReportViewModel
    {
        public SaveBattleReportCommand SaveBattleReportCommand { get; set; }

        public BattleReportViewModel()
        {
            SaveBattleReportCommand = new SaveBattleReportCommand(this);
        }
    }
}
