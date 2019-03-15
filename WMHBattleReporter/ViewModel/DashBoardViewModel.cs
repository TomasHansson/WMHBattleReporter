using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel
{
    public class DashBoardViewModel
    {
        public ObservableCollection<Faction> TopFactions { get; set; } = new ObservableCollection<Faction>();
        public ObservableCollection<Caster> TopCasters { get; set; } = new ObservableCollection<Caster>();

        public DashBoardViewModel()
        {
            FillCollections();
        }

        private void FillCollections()
        {
            TopFactions.Clear();
            List<Faction> topFactions = DatabaseServices.GetFactions().OrderByDescending(f => f.Winrate).Take(10).ToList();
            foreach (Faction faction in topFactions)
                TopFactions.Add(faction);

            TopCasters.Clear();
            List<Caster> topCasters = DatabaseServices.GetCasters().OrderByDescending(c => c.Winrate).Take(10).ToList();
            foreach (Caster caster in topCasters)
                TopCasters.Add(caster);
        }
    }
}
