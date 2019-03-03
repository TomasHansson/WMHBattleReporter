using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class DeleteCasterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public AdminViewModel AdminViewModel { get; set; }

        public DeleteCasterCommand(AdminViewModel adminViewModel)
        {
            AdminViewModel = adminViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (AdminViewModel.CasterToDelete == null)
                return;

            DatabaseServices.DeleteCaster(AdminViewModel.CasterToDelete.Name);
            AdminViewModel.RefillFactionCastersCollection();
        }
    }
}
