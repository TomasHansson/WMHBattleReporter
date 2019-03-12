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
        public string NewTheme { get; set; }
        public string NewCaster { get; set; }

        public Faction FactionToDelete { get; set; }
        public Theme ThemeToDelete { get; set; }
        public Caster CasterToDelete { get; set; }

        private Faction newItemsFaction;
        public Faction NewItemsFaction
        {
            get { return newItemsFaction; }
            set
            {
                newItemsFaction = value;
                FillFactionCastersCollection();
                FillFactionThemesCollection();
            }
        }

        public AddThemeCommand AddThemeCommand { get; set; }
        public AddCasterCommand AddCasterCommand { get; set; }
        public AddFactionCommand AddFactionCommand { get; set; }
        public DeleteThemeCommand DeleteThemeCommand { get; set; }
        public DeleteCasterCommand DeleteCasterCommand { get; set; }
        public DeleteFactionCommand DeleteFactionCommand { get; set; }

        public ObservableCollection<Faction> Factions { get; set; } = new ObservableCollection<Faction>();
        public ObservableCollection<Faction> FactionOptions { get; set; } = new ObservableCollection<Faction>();
        public ObservableCollection<Caster> FactionCasters { get; set; } = new ObservableCollection<Caster>();
        public ObservableCollection<Theme> FactionThemes { get; set; } = new ObservableCollection<Theme>();

        public AdminViewModel()
        {
            AddThemeCommand = new AddThemeCommand(this);
            AddCasterCommand = new AddCasterCommand(this);
            AddFactionCommand = new AddFactionCommand(this);
            DeleteThemeCommand = new DeleteThemeCommand(this);
            DeleteCasterCommand = new DeleteCasterCommand(this);
            DeleteFactionCommand = new DeleteFactionCommand(this);

            FillFactionsCollections();
        }

        public void FillFactionsCollections()
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

        public void FillFactionCastersCollection()
        {
            FactionCasters.Clear();
            if (NewItemsFaction != null)
            {
                List<Caster> casters = DatabaseServices.GetFactionCasters(NewItemsFaction.Name);
                foreach (Caster caster in casters)
                    FactionCasters.Add(caster);
            }
        }

        public void FillFactionThemesCollection()
        {
            FactionThemes.Clear();
            if (NewItemsFaction != null)
            {
                List<Theme> themes = DatabaseServices.GetFactionThemes(NewItemsFaction.Name);
                foreach (Theme theme in themes)
                    FactionThemes.Add(theme);
            }
        }
    }
}
