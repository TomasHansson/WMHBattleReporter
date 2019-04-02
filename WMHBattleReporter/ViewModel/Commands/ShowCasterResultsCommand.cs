using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class ShowCasterResultsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public GameStatisticsViewModel ViewModel { get; set; }

        public ShowCasterResultsCommand(GameStatisticsViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SetActiveResultsPage();

            ViewModel.CastersResults.Clear();

            /*

            List<Caster> casters = DatabaseServices.GetCasters();
            casters.OrderByDescending(c => c.Winrate);
            foreach (Caster caster in casters)
                ViewModel.CastersResults.Add($"{caster.Name} - G: {caster.NumberOfGamesPlayed} W: {caster.NumberOfGamesWon} L: {caster.NumberOfGamesLost} Winrate: {caster.Winrate * 100}.");
        
            */
        }

        private void SetActiveResultsPage()
        {
            ViewModel.CastersResultsPageActive = true;
            ViewModel.FactionResultsPageActive = false;
            ViewModel.UserResultsPageActive = false;
        }
    }
}
