using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class ShowFactionResultsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public GameStatisticsViewModel ViewModel { get; set; }

        public ShowFactionResultsCommand(GameStatisticsViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.CastersResultsPageActive = false;
            ViewModel.FactionResultsPageActive = true;
            ViewModel.UserResultsPageActive = false;

            ViewModel.FactionsResults.Clear();

            List<Faction> factions = DatabaseServices.GetFactions();
            factions.OrderByDescending(f => f.Winrate);
            foreach (Faction faction in factions)
                ViewModel.FactionsResults.Add($"{faction.Name} - G: {faction.NumberOfGamesPlayed} W: {faction.NumberOfGamesWon} L: {faction.NumberOfGamesLost} Winrate: {faction.Winrate * 100}.");
        }
    }
}
