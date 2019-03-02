using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.Model;
using WMHBattleReporter.ViewModel.Commands;

namespace WMHBattleReporter.ViewModel
{
    public class AdminViewModel
    {
        public string NewFaction { get; set; }
        public string NewCaster { get; set; }

        public Faction FactionToDelete { get; set; }
        public Caster CasterToDelete { get; set; }
        public Faction NewCastersFaction { get; set; }

        public AddCasterCommand AddCasterCommand { get; set; }
        public AddFactionCommand AddFactionCommand { get; set; }
        public DeleteCasterCommand DeleteCasterCommand { get; set; }
        public DeleteFactionCommand DeleteFactionCommand { get; set; }

        public ObservableCollection<Faction> Factions { get; set; } = new ObservableCollection<Faction>();
        public ObservableCollection<Caster> FactionCasters { get; set; } = new ObservableCollection<Caster>();

        public AdminViewModel()
        {
            AddCasterCommand = new AddCasterCommand(this);
            AddFactionCommand = new AddFactionCommand(this);
            DeleteCasterCommand = new DeleteCasterCommand(this);
            DeleteFactionCommand = new DeleteFactionCommand(this);

            List<Faction> factions = DatabaseServices.GetFactions();
            foreach (Faction faction in factions)
                Factions.Add(faction);
        }
    }
}
