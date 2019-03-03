using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private Faction newCastersFaction;
        public Faction NewCastersFaction
        {
            get { return newCastersFaction; }
            set
            {
                newCastersFaction = value;
                RefillFactionCastersCollection();
            }
        }

        public AddCasterCommand AddCasterCommand { get; set; }
        public AddFactionCommand AddFactionCommand { get; set; }
        public DeleteCasterCommand DeleteCasterCommand { get; set; }
        public DeleteFactionCommand DeleteFactionCommand { get; set; }

        public ObservableCollection<Faction> Factions { get; set; } = new ObservableCollection<Faction>();
        public ObservableCollection<Faction> FactionOptions { get; set; } = new ObservableCollection<Faction>();
        public ObservableCollection<Caster> FactionCasters { get; set; } = new ObservableCollection<Caster>();

        public AdminViewModel()
        {
            AddCasterCommand = new AddCasterCommand(this);
            AddFactionCommand = new AddFactionCommand(this);
            DeleteCasterCommand = new DeleteCasterCommand(this);
            DeleteFactionCommand = new DeleteFactionCommand(this);

            RefillFactionsCollections();
        }

        public void RefillFactionsCollections()
        {
            Factions.Clear();
            FactionOptions.Clear();
            List<Faction> factions = DatabaseServices.GetFactions();
            foreach (Faction faction in factions)
            {
                Factions.Add(faction);
                FactionOptions.Add(faction);
            }
        }

        public void RefillFactionCastersCollection()
        {
            FactionCasters.Clear();
            if (NewCastersFaction != null)
            {
                List<Caster> casters = DatabaseServices.GetFactionCasters(NewCastersFaction.Name);
                foreach (Caster caster in casters)
                    FactionCasters.Add(caster);
            }
        }
    }
}
