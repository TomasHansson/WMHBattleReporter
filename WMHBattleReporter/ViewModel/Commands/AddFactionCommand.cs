using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class AddFactionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public AdminViewModel AdminViewModel { get; set; }

        public AddFactionCommand(AdminViewModel adminViewModel)
        {
            AdminViewModel = adminViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(AdminViewModel.NewFaction) || DatabaseServices.FactionNameExists(AdminViewModel.NewFaction))
                return;

            Faction newFaction = new Faction()
            {
                Name = AdminViewModel.NewFaction
            };

            DatabaseServices.SaveFaction(newFaction);

            List<Faction> factions = DatabaseServices.GetFactions();
            AdminViewModel.Factions.Clear();
            foreach (Faction faction in factions)
                AdminViewModel.Factions.Add(faction);
        }
    }
}
