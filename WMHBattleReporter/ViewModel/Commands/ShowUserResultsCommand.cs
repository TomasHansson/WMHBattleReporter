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
            SetUsersMostPlayed();
            SetUsersBest();
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

        private void SetUsersMostPlayed()
        {
            List<BattleReport> allBattleReports = DatabaseServices.GetBattleReports();

            List<BattleReport> battleReportsPostedByUser = allBattleReports.Where(b => b.PostersUsername == currentUser.Username).ToList();
            List<BattleReport> battleReportsWithUserAsOpponent = allBattleReports.Where(b => b.OpponentsUsername == currentUser.Username).ToList();

            Dictionary<string, int> numberOfTimesFactionsWerePlayed = new Dictionary<string, int>();
            Dictionary<string, int> numberOfTimesThemesWerePlayed = new Dictionary<string, int>();
            Dictionary<string, int> numberOfTimesCastersWerePlayed = new Dictionary<string, int>();

            List<Faction> factions = DatabaseServices.GetFactions();
            foreach (Faction faction in factions)
                numberOfTimesFactionsWerePlayed.Add(faction.Name, 0);

            foreach (BattleReport battleReport in battleReportsPostedByUser)
                numberOfTimesFactionsWerePlayed[battleReport.PostersFaction]++;

            foreach (BattleReport battleReport in battleReportsWithUserAsOpponent)
                numberOfTimesFactionsWerePlayed[battleReport.OpponentsFaction]++;

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
        }

        private void SetUsersBest()
        {

        }
    }
}
