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

            List<User> topTenUsers = DatabaseServices.GetUsers().OrderByDescending(u => u.Winrate).Take(10).ToList();

            foreach (User user in topTenUsers)
            {
                Dictionary<string, int> gamesPlayedWithEachFaction = new Dictionary<string, int>();
                Dictionary<string, int> gamesPlayedWithEachTheme = new Dictionary<string, int>();
                Dictionary<string, int> gamesPlayedWithEachCaster = new Dictionary<string, int>();

                Dictionary<string, int> gamesWonWithEachFaction = new Dictionary<string, int>();
                Dictionary<string, int> gamesWonWithEachTheme = new Dictionary<string, int>();
                Dictionary<string, int> gamesWonWithEachCaster = new Dictionary<string, int>();

                Dictionary<string, float> winrateWithEachFaction = new Dictionary<string, float>();
                Dictionary<string, float> winrateWithEachTheme = new Dictionary<string, float>();
                Dictionary<string, float> winrateWithEachCaster = new Dictionary<string, float>();

                List<BattleReport> usersBattleReports = DatabaseServices.GetBattleReports().Where(br => br.PostersUsername == user.Username || br.OpponentsUsername == user.Username).ToList();

                foreach (BattleReport battleReport in usersBattleReports)
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

                foreach (var keyValuePair in gamesPlayedWithEachFaction)
                {
                    string faction = keyValuePair.Key;
                    float gamesPlayed = keyValuePair.Value;
                    float gamesWon = gamesWonWithEachFaction.ContainsKey(faction) ? gamesWonWithEachFaction[faction] : 0;
                    winrateWithEachFaction.Add(faction, gamesWon / gamesPlayed);
                }

                foreach (var keyValuePair in gamesPlayedWithEachTheme)
                {
                    string theme = keyValuePair.Key;
                    float gamesPlayed = keyValuePair.Value;
                    float gamesWon = gamesWonWithEachTheme.ContainsKey(theme) ? gamesWonWithEachTheme[theme] : 0;
                    winrateWithEachTheme.Add(theme, gamesWon / gamesPlayed);
                }

                foreach (var keyValuePair in gamesPlayedWithEachCaster)
                {
                    string caster = keyValuePair.Key;
                    float gamesPlayed = keyValuePair.Value;
                    float gamesWon = gamesWonWithEachCaster.ContainsKey(caster) ? gamesWonWithEachCaster[caster] : 0;
                    winrateWithEachCaster.Add(caster, gamesWon / gamesPlayed);
                }

                string mostPlayedFaction = gamesPlayedWithEachFaction.First(f => f.Value == gamesPlayedWithEachFaction.Values.Max()).Key;
                int gamesWithMostPlayedFaction = gamesPlayedWithEachFaction.Values.Max();

                string mostPlayedTheme = gamesPlayedWithEachTheme.First(t => t.Value == gamesPlayedWithEachTheme.Values.Max()).Key;
                int gamesWithMostPlayedTheme = gamesPlayedWithEachTheme.Values.Max();

                string mostPlayedCaster = gamesPlayedWithEachCaster.First(c => c.Value == gamesPlayedWithEachCaster.Values.Max()).Key;
                int gamesWithMostPlayedCaster = gamesPlayedWithEachCaster.Values.Max();

                string bestPerformingFaction = winrateWithEachFaction.First(f => f.Value == winrateWithEachFaction.Values.Max()).Key;
                float winrateBestPerformingFaction = winrateWithEachFaction.Values.Max();

                string bestPerformingTheme = winrateWithEachTheme.First(t => t.Value == winrateWithEachTheme.Values.Max()).Key;
                float winrateBestPerformingTheme = winrateWithEachTheme.Values.Max();

                string bestPerformingCaster = winrateWithEachCaster.First(c => c.Value == winrateWithEachCaster.Values.Max()).Key;
                float winrateBestPerformingCaster = winrateWithEachCaster.Values.Max();

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
    }
}
