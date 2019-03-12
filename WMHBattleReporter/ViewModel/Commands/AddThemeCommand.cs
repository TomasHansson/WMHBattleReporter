using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class AddThemeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public AdminViewModel ViewModel { get; set; }

        public AddThemeCommand(AdminViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(ViewModel.NewTheme) || ViewModel.NewItemsFaction == null)
                return;

            if (DatabaseServices.ThemeNameExists(ViewModel.NewTheme))
            {
                Message?.Invoke("That themename already exists.");
                return;
            }

            Theme newTheme = new Theme()
            {
                Faction = ViewModel.NewItemsFaction.Name,
                Name = ViewModel.NewTheme
            };

            DatabaseServices.InsertItem(newTheme);
            ViewModel.FillFactionThemesCollection();
        }

        public delegate void SendMessage(string message);
        public event SendMessage Message;
    }
}
