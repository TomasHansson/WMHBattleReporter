using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class DeleteThemeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public AdminViewModel ViewModel { get; set; }

        public DeleteThemeCommand(AdminViewModel viewModel)
        {
            ViewModel = ViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (ViewModel.ThemeToDelete == null)
                return;

            DatabaseServices.DeleteItem(ViewModel.ThemeToDelete);
            ViewModel.FillFactionThemesCollection();
        }
    }
}
