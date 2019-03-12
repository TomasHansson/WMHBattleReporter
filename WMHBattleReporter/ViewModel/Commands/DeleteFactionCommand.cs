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

            List<Caster> casters = DatabaseServices.GetFactionCasters(ViewModel.FactionToDelete.Name);
            foreach (Caster caster in casters)
                DatabaseServices.DeleteItem(caster);

            List<Theme> themes = DatabaseServices.GetFactionThemes(ViewModel.FactionToDelete.Name);
            foreach (Theme theme in themes)
                DatabaseServices.DeleteItem(theme);

            DatabaseServices.DeleteItem(ViewModel.FactionToDelete);
            ViewModel.FillFactionsCollections();
        }
    }
}
