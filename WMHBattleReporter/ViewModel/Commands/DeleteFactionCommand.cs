using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class DeleteFactionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public AdminViewModel AdminViewModel { get; set; }

        public DeleteFactionCommand(AdminViewModel adminViewModel)
        {
            AdminViewModel = adminViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (AdminViewModel.FactionToDelete == null)
                return;

            DatabaseServices.DeleteFaction(AdminViewModel.FactionToDelete.Name);

            List<Faction> factions = DatabaseServices.GetFactions();
            AdminViewModel.Factions.Clear();
            foreach (Faction faction in factions)
                AdminViewModel.Factions.Add(faction);
        }
    }
}
