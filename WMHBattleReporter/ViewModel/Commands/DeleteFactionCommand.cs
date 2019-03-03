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

        public AdminViewModel ViewModel { get; set; }

        public DeleteFactionCommand(AdminViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (ViewModel.FactionToDelete == null)
                return;

            DatabaseServices.DeleteFaction(ViewModel.FactionToDelete.Name);
            ViewModel.RefillFactionsCollections();
        }
    }
}
