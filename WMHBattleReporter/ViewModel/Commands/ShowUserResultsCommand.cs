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
            SetActiveResultsPage();
            FillTopUsersCollection();
        }

        private void SetActiveResultsPage()
        {
            ViewModel.CastersResultsPageActive = false;
            ViewModel.FactionResultsPageActive = false;
            ViewModel.UserResultsPageActive = true;
        }

        private void FillTopUsersCollection()
        {
            ViewModel.TopUsersResult.Clear();
            List<User> topTenUsers = ViewModel.SelectedRegion == "All Regions" ? 
                DatabaseServices.GetUsers().OrderByDescending(u => u.Winrate).Take(10).ToList() : 
                DatabaseServices.GetUsers().Where(u => u.Region == ViewModel.SelectedRegion).OrderByDescending(u => u.Winrate).Take(10).ToList();

            foreach (User user in topTenUsers)
            {
                Dictionary<string, int> gamesPlayedWithEachFaction = new Dictionary<string, int>();
                Dictionary<string, int> gamesPlayedWithEachTheme = new Dictionary<string, int>();
                Dictionary<string, int> gamesPlayedWithEachCaster = new Dictionary<string, int>();

                Dictionary<string, int> gamesWonWithEachFaction = new Dictionary<string, int>();
                Dictionary<string, int> gamesWonWithEachTheme = new Dictionary<string, int>();
                Dictionary<string, int> gamesWonWithEachCaster = new Dictionary<string, int>();

                CollectBattleReportsData(user, gamesPlayedWithEachFaction, gamesPlayedWithEachTheme, gamesPlayedWithEachCaster, 
                                         gamesWonWithEachFaction, gamesWonWithEachTheme, gamesWonWithEachCaster);

                string mostPlayedFaction = FindTopResult(gamesPlayedWithEachFaction, out int gamesWithMostPlayedFaction);
                string mostPlayedTheme = FindTopResult(gamesPlayedWithEachTheme, out int gamesWithMostPlayedTheme);
                string mostPlayedCaster = FindTopResult(gamesPlayedWithEachCaster, out int gamesWithMostPlayedCaster);

                Dictionary<string, float> winrateWithEachFaction = new Dictionary<string, float>();
                Dictionary<string, float> winrateWithEachTheme = new Dictionary<string, float>();
                Dictionary<string, float> winrateWithEachCaster = new Dictionary<string, float>();

                FindWinRate(gamesPlayedWithEachFaction, gamesWonWithEachFaction, winrateWithEachFaction);
                FindWinRate(gamesPlayedWithEachTheme, gamesWonWithEachTheme, winrateWithEachTheme);
                FindWinRate(gamesPlayedWithEachCaster, gamesWonWithEachCaster, winrateWithEachCaster);

                string bestPerformingFaction = FindTopResult(winrateWithEachFaction, out float winrateBestPerformingFaction);
                string bestPerformingTheme = FindTopResult(winrateWithEachTheme, out float winrateBestPerformingTheme);
                string bestPerformingCaster = FindTopResult(winrateWithEachCaster, out float winrateBestPerformingCaster);

                UserResult usersResult = new UserResult()
                {
                    Username = user.Username,
                    NumberOfGamesPlayed = user.NumberOfGamesPlayed,
                    NumberOfGamesWon = user.NumberOfGamesWon,
                    NumberOfGamesLost = user.NumberOfGamesLost,
                    Winrate = user.Winrate,
                    MostPlayedFaction = mostPlayedFaction,
                    GamesWithMostPlayedFaction = gamesWithMostPlayedFaction,
                    MostPlayedTheme = mostPlayedTheme,
                    GamesWithMostPlayedTheme = gamesWithMostPlayedTheme,
                    MostPlayedCaster = mostPlayedCaster,
                    GamesWithMostPlayedCaster = gamesWithMostPlayedCaster,
                    BestPerformingFaction = bestPerformingFaction,
                    WinrateBestPerformingFaction = winrateBestPerformingFaction,
                    BestPerformingTheme = bestPerformingTheme,
                    WinrateBestPerformingTheme = winrateBestPerformingTheme,
                    BestPerformingCaster = bestPerformingCaster,
                    WinrateBestPerformingCaster = winrateBestPerformingCaster
                };

                ViewModel.TopUsersResult.Add(usersResult);
            }
        }

        private static void CollectBattleReportsData(User user, Dictionary<string, int> gamesPlayedWithEachFaction, Dictionary<string, int> gamesPlayedWithEachTheme, Dictionary<string, int> gamesPlayedWithEachCaster, 
                                                     Dictionary<string, int> gamesWonWithEachFaction, Dictionary<string, int> gamesWonWithEachTheme, Dictionary<string, int> gamesWonWithEachCaster)
        {
            List<BattleReport> usersBattleReports = DatabaseServices.GetBattleReports().Where(br => br.PostersUsername == user.Username || br.OpponentsUsername == user.Username).ToList();
            foreach (BattleReport battleReport in usersBattleReports)
            {
                CollectFactionResults(user, gamesPlayedWithEachFaction, gamesWonWithEachFaction, battleReport);
                CollectThemeResults(user, gamesPlayedWithEachTheme, gamesWonWithEachTheme, battleReport);
                CollectCasterResults(user, gamesPlayedWithEachCaster, gamesWonWithEachCaster, battleReport);
            }
        }

        private static void CollectFactionResults(User user, Dictionary<string, int> gamesPlayedWithEachFaction, Dictionary<string, int> gamesWonWithEachFaction, BattleReport battleReport)
        {
            string faction = user.Username == battleReport.PostersUsername ? battleReport.PostersFaction : battleReport.OpponentsFaction;
            if (gamesPlayedWithEachFaction.ContainsKey(faction))
                gamesPlayedWithEachFaction[faction]++;
            else
                gamesPlayedWithEachFaction.Add(faction, 1);

            if (faction == battleReport.WinningFaction)
            {
                if (gamesWonWithEachFaction.ContainsKey(faction))
                    gamesWonWithEachFaction[faction]++;
                else
                    gamesWonWithEachFaction.Add(faction, 1);
            }
        }

        private static void CollectThemeResults(User user, Dictionary<string, int> gamesPlayedWithEachTheme, Dictionary<string, int> gamesWonWithEachTheme, BattleReport battleReport)
        {
            string theme = user.Username == battleReport.PostersUsername ? battleReport.PostersTheme : battleReport.OpponentsTheme;
            if (gamesPlayedWithEachTheme.ContainsKey(theme))
                gamesPlayedWithEachTheme[theme]++;
            else
                gamesPlayedWithEachTheme.Add(theme, 1);

            if (theme == battleReport.WinningTheme)
            {
                if (gamesWonWithEachTheme.ContainsKey(theme))
                    gamesWonWithEachTheme[theme]++;
                else
                    gamesWonWithEachTheme.Add(theme, 1);
            }
        }

        private static void CollectCasterResults(User user, Dictionary<string, int> gamesPlayedWithEachCaster, Dictionary<string, int> gamesWonWithEachCaster, BattleReport battleReport)
        {
            string caster = user.Username == battleReport.PostersUsername ? battleReport.PostersCaster : battleReport.OpponentsCaster;
            if (gamesPlayedWithEachCaster.ContainsKey(caster))
                gamesPlayedWithEachCaster[caster]++;
            else
                gamesPlayedWithEachCaster.Add(caster, 1);

            if (caster == battleReport.WinningCaster)
            {
                if (gamesWonWithEachCaster.ContainsKey(caster))
                    gamesWonWithEachCaster[caster]++;
                else
                    gamesWonWithEachCaster.Add(caster, 1);
            }
        }

        private static void FindWinRate(Dictionary<string, int> gamesPlayedWithEachItem, Dictionary<string, int> gamesWonWithEachItem, Dictionary<string, float> winrateWithEachItem)
        {
            foreach (var keyValuePair in gamesPlayedWithEachItem)
            {
                string item = keyValuePair.Key;
                float gamesPlayed = keyValuePair.Value;
                float gamesWon = gamesWonWithEachItem.ContainsKey(item) ? gamesWonWithEachItem[item] : 0;
                winrateWithEachItem.Add(item, gamesWon / gamesPlayed);
            }
        }

        private string FindTopResult<T>(Dictionary<string, T> results, out T value)
        {
            value = results.Values.Max();
            return results.First(kvp => kvp.Value.Equals(results.Values.Max())).Key;
        }
    }
}
