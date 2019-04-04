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
            FillVersusResults();
            FillCasterStatistics();
        }

        private void SetActiveResultsPage()
        {
            ViewModel.CastersResultsPageActive = true;
            ViewModel.FactionResultsPageActive = false;
            ViewModel.UserResultsPageActive = false;
        }

        private void FillVersusResults()
        {

        }

        private void FillCasterStatistics()
        {
            ViewModel.CasterResults.Clear();

            List<BattleReport> castersWinningBattles = DatabaseServices.GetBattleReports().Where(br => br.WinningCaster == ViewModel.SelectedCaster.Name).ToList();
            double winsOnScenario = castersWinningBattles.Where(br => br.EndCondition == "Scenario").Count();
            double winsOnAssassination = castersWinningBattles.Where(br => br.EndCondition == "Assassination").Count();
            double winsOnClock = castersWinningBattles.Where(br => br.EndCondition == "Clock").Count();

            List<BattleReport> castersLosingBattles = DatabaseServices.GetBattleReports().Where(br => br.LosingCaster == ViewModel.SelectedCaster.Name).ToList();
            double lossesOnScenario = castersLosingBattles.Where(br => br.EndCondition == "Scenario").Count();
            double lossesOnAssassination = castersLosingBattles.Where(br => br.EndCondition == "Assassination").Count();
            double lossesOnClock = castersLosingBattles.Where(br => br.EndCondition == "Clock").Count();

            CasterResult casterResult = new CasterResult()
            {
                NumberOfGamesPlayed = ViewModel.SelectedCaster.NumberOfGamesPlayed,
                NumberOfGamesLost = ViewModel.SelectedCaster.NumberOfGamesLost,
                NumberOfGamesWon = ViewModel.SelectedCaster.NumberOfGamesWon,
                Winrate = ViewModel.SelectedCaster.Winrate,
                ScenarioRate = winsOnScenario / ViewModel.SelectedCaster.NumberOfGamesWon,
                AssassinationRate = winsOnAssassination / ViewModel.SelectedCaster.NumberOfGamesWon,
                ClockRate = winsOnClock / ViewModel.SelectedCaster.NumberOfGamesWon,
                LossScenarioRate = lossesOnScenario / ViewModel.SelectedCaster.NumberOfGamesLost,
                LossAssassinationRate = lossesOnAssassination / ViewModel.SelectedCaster.NumberOfGamesLost,
                LossClockRate = lossesOnClock / ViewModel.SelectedCaster.NumberOfGamesLost
            };

            // This implemenatation is tied to the fact that currently, the only options available for scenario is "Scenario 1" and "Scenario 2". 
            // This should be improved and then this implementation will need to change as well.
            double winsOnScenario1 = castersWinningBattles.Where(br => br.Scenario == "Scenario 1").Count();
            double lossesOnScenario1 = castersLosingBattles.Where(br => br.Scenario == "Scenario 1").Count();
            double winrateScenario1 = winsOnScenario1 / (lossesOnScenario1 + winsOnScenario1);

            double winsOnScenario2 = castersWinningBattles.Where(br => br.Scenario == "Scenario 2").Count();
            double lossesOnScenario2 = castersLosingBattles.Where(br => br.Scenario == "Scenario 2").Count();
            double winrateScenario2 = winsOnScenario2 / (lossesOnScenario2 + winsOnScenario2);

            if (winrateScenario1 >= winrateScenario2)
            {
                casterResult.BestScenario = "Scenario 1";
                casterResult.BestScenarioRate = winrateScenario1;

                casterResult.WorstScenario = "Scenario 2";
                casterResult.WorstScenarioRate = winrateScenario2;
            }
            else
            {
                casterResult.BestScenario = "Scenario 2";
                casterResult.BestScenarioRate = winrateScenario2;

                casterResult.WorstScenario = "Scenario 1";
                casterResult.WorstScenarioRate = winrateScenario1;
            }

            ViewModel.CasterResults.Add(casterResult);
        }
    }
}
