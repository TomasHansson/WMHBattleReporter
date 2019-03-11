using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class ShowUserResultsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public GameStatisticsViewModel ViewModel { get; set; }

        private User currentUser;

        public ShowUserResultsCommand(GameStatisticsViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!DatabaseServices.UserLoggedIn)
            {
                Message?.Invoke("No user is currently logged in.");
                return;
            }

            SetActiveResultsPage();
            currentUser = DatabaseServices.LoggedInUser;
            SetBasicStatistics();
            SetAdvancedStatistics();
        }

        public delegate void SendMessage(string message);
        public event SendMessage Message;

        private void SetActiveResultsPage()
        {
            ViewModel.CastersResultsPageActive = false;
            ViewModel.FactionResultsPageActive = false;
            ViewModel.UserResultsPageActive = true;
        }

        private void SetBasicStatistics()
        {
            ViewModel.UsersNumberOfGamesPlayed = currentUser.NumberOfGamesPlayed;
            ViewModel.UsersNumberOfGamesWon = currentUser.NumberOfGamesWon;
            ViewModel.UsersNumberOfGamesLost = currentUser.NumberOfGamesLost;
            ViewModel.UsersWinrate = currentUser.Winrate * 100;
        }

        private void SetAdvancedStatistics()
        {
            List<BattleReport> allBattleReports = DatabaseServices.GetBattleReports();
            List<BattleReport> reportsByUser = allBattleReports.Where(b => b.PostersUsername == currentUser.Username).ToList();
            List<BattleReport> reportsAgainstUser = allBattleReports.Where(b => b.OpponentsUsername == currentUser.Username).ToList();

            SetUsersFactionResults(reportsByUser, reportsAgainstUser);
            SetUsersThemeResults(reportsByUser, reportsAgainstUser);
            SetUsersCasterResults(reportsByUser, reportsAgainstUser);
        }

        private void SetUsersFactionResults(List<BattleReport> reportsByUser, List<BattleReport> reportsAgainstUser)
        {
            Dictionary<string, int> numberOfTimesFactionsWerePlayed = new Dictionary<string, int>();
            Dictionary<string, int> numberOfTimesFactionsWon = new Dictionary<string, int>();
            Dictionary<string, float> factionsWinrate = new Dictionary<string, float>();

            List<Faction> factions = DatabaseServices.GetFactions();
            foreach (Faction faction in factions)
            {
                numberOfTimesFactionsWerePlayed.Add(faction.Name, 0);
                numberOfTimesFactionsWon.Add(faction.Name, 0);
                factionsWinrate.Add(faction.Name, 0);
            }
                
            foreach (BattleReport battleReport in reportsByUser)
            {
                numberOfTimesFactionsWerePlayed[battleReport.PostersFaction]++;
                if (battleReport.WinningFaction == battleReport.PostersFaction)
                    numberOfTimesFactionsWon[battleReport.PostersFaction]++;
            }

            foreach (BattleReport battleReport in reportsAgainstUser)
            {
                numberOfTimesFactionsWerePlayed[battleReport.OpponentsFaction]++;
                if (battleReport.WinningFaction == battleReport.OpponentsFaction)
                    numberOfTimesFactionsWon[battleReport.OpponentsFaction]++;
            }

            foreach (Faction faction in factions)
            {
                int gamesPlayed = numberOfTimesFactionsWerePlayed[faction.Name];
                int gamesWon = numberOfTimesFactionsWon[faction.Name];
                factionsWinrate[faction.Name] = (float)gamesWon / (float)gamesPlayed;
            }

            if (numberOfTimesFactionsWerePlayed.Count == 0)
            {
                ViewModel.CurrentUsersMostPlayedFaction = "No games recorded for user.";
            }
            else
            {
                string mostPlayedFaction = numberOfTimesFactionsWerePlayed.First(f => f.Value == numberOfTimesFactionsWerePlayed.Values.Max()).Key;
                int numberOfGames = numberOfTimesFactionsWerePlayed.Values.Max();
                ViewModel.CurrentUsersMostPlayedFaction = $"{mostPlayedFaction} ({numberOfGames})";
            }

            if (numberOfTimesFactionsWerePlayed.Count == 0)
            {
                ViewModel.CurrentUsersBestFaction = "No games recorded for user.";
            }
            else
            {
                string bestFaction = factionsWinrate.First(f => f.Value == factionsWinrate.Values.Max()).Key;
                float winrate = factionsWinrate.Values.Max();
                ViewModel.CurrentUsersBestFaction = $"{bestFaction} ({winrate})";
            }
        }

        private void SetUsersThemeResults(List<BattleReport> reportsByUser, List<BattleReport> reportsAgainstUser)
        {
            Dictionary<string, int> numberOfTimesThemesWerePlayed = new Dictionary<string, int>();
        }

        private void SetUsersCasterResults(List<BattleReport> reportsByUser, List<BattleReport> reportsAgainstUser)
        {
            Dictionary<string, int> numberOfTimesCastersWerePlayed = new Dictionary<string, int>();
            Dictionary<string, int> numberOfTimesCastersWon = new Dictionary<string, int>();
            Dictionary<string, float> castersWinrate = new Dictionary<string, float>();

            List<Caster> casters = DatabaseServices.GetCasters();
            foreach (Caster caster in casters)
            {
                numberOfTimesCastersWerePlayed.Add(caster.Name, 0);
                numberOfTimesCastersWon.Add(caster.Name, 0);
                castersWinrate.Add(caster.Name, 0);
            }

            foreach (BattleReport battleReport in reportsByUser)
            {
                numberOfTimesCastersWerePlayed[battleReport.PostersCaster]++;
                if (battleReport.WinningCaster == battleReport.PostersCaster)
                    numberOfTimesCastersWon[battleReport.PostersCaster]++;
            }

            foreach (BattleReport battleReport in reportsAgainstUser)
            {
                numberOfTimesCastersWerePlayed[battleReport.OpponentsCaster]++;
                if (battleReport.WinningCaster == battleReport.OpponentsCaster)
                    numberOfTimesCastersWon[battleReport.OpponentsCaster]++;
            }

            foreach (Caster caster in casters)
            {
                int gamesPlayed = numberOfTimesCastersWerePlayed[caster.Name];
                int gamesWon = numberOfTimesCastersWon[caster.Name];
                castersWinrate[caster.Name] = (float)gamesWon / (float)gamesPlayed;
            }

            if (numberOfTimesCastersWerePlayed.Count == 0)
            {
                ViewModel.CurrentUsersMostPlayedCaster = "No games recorded for user.";
            }
            else
            {
                string mostPlayedCaster = numberOfTimesCastersWerePlayed.First(f => f.Value == numberOfTimesCastersWerePlayed.Values.Max()).Key;
                int numberOfGames = numberOfTimesCastersWerePlayed.Values.Max();
                ViewModel.CurrentUsersMostPlayedCaster = $"{mostPlayedCaster} ({numberOfGames})";
            }

            if (numberOfTimesCastersWerePlayed.Count == 0)
            {
                ViewModel.CurrentUsersBestCaster = "No games recorded for user.";
            }
            else
            {
                string bestCaster = castersWinrate.First(f => f.Value == castersWinrate.Values.Max()).Key;
                float winrate = castersWinrate.Values.Max();
                ViewModel.CurrentUsersBestCaster = $"{bestCaster} ({winrate})";
            }
        }
    }
}
