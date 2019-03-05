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

        public AdminViewModel ViewModel { get; set; }

        public AddFactionCommand(AdminViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(ViewModel.NewFaction) || DatabaseServices.FactionNameExists(ViewModel.NewFaction))
                return;

            Faction newFaction = new Faction()
            {
                Name = ViewModel.NewFaction
            };

            DatabaseServices.InsertItem(newFaction);
            ViewModel.RefillFactionsCollections();
        }
    }
}
