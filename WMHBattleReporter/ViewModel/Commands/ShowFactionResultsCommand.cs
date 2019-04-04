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
            SetActiveResultsPage();
            FillFactionsVersusResults();
            FillFactionsThemes();
            FillFactionsCasters();

        }

        private void SetActiveResultsPage()
        {
            ViewModel.CastersResultsPageActive = false;
            ViewModel.FactionResultsPageActive = true;
            ViewModel.UserResultsPageActive = false;
        }

        private void FillFactionsVersusResults()
        {
            ViewModel.FactionsVersusResults.Clear();

            List<BattleReport> factionsBattleReports = DatabaseServices.GetBattleReports().Where(br => (br.PostersFaction == ViewModel.SelectedFaction || br.OpponentsFaction == ViewModel.SelectedFaction) && br.PostersFaction != br.OpponentsFaction).ToList();
            List<Faction> opposingFactions = DatabaseServices.GetFactions().Where(f => f.Name != ViewModel.SelectedFaction).ToList();

            foreach (Faction faction in opposingFactions)
            {
                List<BattleReport> gamesAgainstFaction = factionsBattleReports.Where(br => br.PostersFaction == faction.Name || br.OpponentsFaction == faction.Name).ToList();
                int gamesPlayed = gamesAgainstFaction.Count;
                int gamesWon = gamesAgainstFaction.Where(br => br.WinningFaction == ViewModel.SelectedFaction).Count();
                int gamesLost = gamesPlayed - gamesWon;
                double winrate = (double)gamesWon / (double)gamesPlayed;
                VersusResult versusResult = new VersusResult()
                {
                    Opponent = faction.Name,
                    GamesPlayed = gamesPlayed,
                    GamesWon = gamesWon,
                    GamesLost = gamesLost,
                    Winrate = winrate
                };
                ViewModel.FactionsVersusResults.Add(versusResult);
            }
            ViewModel.FactionsVersusResults.OrderByDescending(vr => vr.Winrate);
        }

        private void FillFactionsThemes()
        {
            ViewModel.FactionThemes.Clear();
            List<Theme> factionThemes = DatabaseServices.GetFactionThemes(ViewModel.SelectedFaction);
            foreach (Theme theme in factionThemes)
                ViewModel.FactionThemes.Add(theme);
        }

        private void FillFactionsCasters()
        {
            ViewModel.FactionCasters.Clear();
            List<Caster> factionCasters = DatabaseServices.GetFactionCasters(ViewModel.SelectedFaction);
            foreach (Caster caster in factionCasters)
                ViewModel.FactionCasters.Add(caster);
        }
    }
}
