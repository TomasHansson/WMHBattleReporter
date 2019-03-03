using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class AddCasterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public AdminViewModel AdminViewModel { get; set; }

        public AddCasterCommand(AdminViewModel adminViewModel)
        {
            AdminViewModel = adminViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(AdminViewModel.NewCaster) || DatabaseServices.CasterNameExists(AdminViewModel.NewCaster)
                || AdminViewModel.NewCastersFaction == null)
                return;

            Caster newCaster = new Caster()
            {
                FactionId = AdminViewModel.NewCastersFaction.Id,
                Name = AdminViewModel.NewCaster
            };

            DatabaseServices.SaveCaster(newCaster);
            AdminViewModel.RefillFactionCastersCollection();
        }
    }
}
