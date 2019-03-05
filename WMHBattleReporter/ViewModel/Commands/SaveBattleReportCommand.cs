using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class SaveBattleReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public BattleReportViewModel ViewModel { get; set; }

        public SaveBattleReportCommand(BattleReportViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if ((!ViewModel.GameSizeIs35Points && !ViewModel.GameSizeIs50Points && !ViewModel.GameSizeIs75Points && !ViewModel.GameSizeIs100Points)
                || (!ViewModel.GameTypeIsMasters && !ViewModel.GameTypeIsChampions && !ViewModel.GameTypeIsSteamRoller)
                || (!ViewModel.UserWon && !ViewModel.OpponentWon)
                || ViewModel.UsersFaction == null || ViewModel.UsersCaster == null
                || ViewModel.OpponentsFaction == null || ViewModel.OpponentsCaster == null)
                return;

            BattleReport newBattleReport = new BattleReport()
            {
                UserFaction = ViewModel.UsersFaction.Name,
                UserCaster = ViewModel.UsersCaster.Name,
                OpponentFaction = ViewModel.OpponentsFaction.Name,
                OpponentCaster = ViewModel.OpponentsCaster.Name,
                UserId = DatabaseServices.LoggedInUsersId,
            };

            if (ViewModel.GameSizeIs35Points)
                newBattleReport.GameSize = 35;
            else if (ViewModel.GameSizeIs50Points)
                newBattleReport.GameSize = 50;
            else if (ViewModel.GameSizeIs75Points)
                newBattleReport.GameSize = 75;
            else if (ViewModel.GameSizeIs100Points)
                newBattleReport.GameSize = 100;

            if (ViewModel.GameTypeIsMasters)
                newBattleReport.GameType = "Masters";
            else if (ViewModel.GameTypeIsChampions)
                newBattleReport.GameType = "Champions";
            else if (ViewModel.GameTypeIsSteamRoller)
                newBattleReport.GameType = "Steam Roller";

            Faction winningFaction = null;
            Caster winningCaster = null;
            Faction losingFaction = null;
            Caster losingCaster = null;
            User currentUser = DatabaseServices.GetUserById(DatabaseServices.LoggedInUsersId);

            if (ViewModel.UserWon)
            {
                newBattleReport.WinningFaction = ViewModel.UsersFaction.Name;
                newBattleReport.WinningCaster = ViewModel.UsersCaster.Name;

                winningFaction = ViewModel.UsersFaction;
                winningCaster = ViewModel.UsersCaster;
                losingFaction = ViewModel.OpponentsFaction;
                losingCaster = ViewModel.OpponentsCaster;

                currentUser.NumberOfGamesWon++;
            }
            else if (ViewModel.OpponentWon)
            {
                newBattleReport.WinningFaction = ViewModel.OpponentsFaction.Name;
                newBattleReport.WinningCaster = ViewModel.OpponentsCaster.Name;

                winningFaction = ViewModel.OpponentsFaction;
                winningCaster = ViewModel.OpponentsCaster;
                losingFaction = ViewModel.UsersFaction;
                losingCaster = ViewModel.UsersCaster;

                currentUser.NumberOfGamesLost++;
            }

            currentUser.NumberOfGamesPlayed++;
            currentUser.WinPercentage = (float)currentUser.NumberOfGamesWon / (float)currentUser.NumberOfGamesPlayed;

            winningFaction.NumberOfGamesPlayed++;
            winningFaction.NumberOfGamesWon++;
            winningFaction.WinPercentage = (float)winningFaction.NumberOfGamesWon / (float)winningFaction.NumberOfGamesPlayed;
            losingFaction.NumberOfGamesPlayed++;
            losingFaction.NumberOfGamesLost++;
            losingFaction.WinPercentage = (float)losingFaction.NumberOfGamesWon / (float)losingFaction.NumberOfGamesPlayed;

            winningCaster.NumberOfGamesPlayed++;
            winningCaster.NumberOfGamesWon++;
            winningCaster.WinPercentage = (float)winningCaster.NumberOfGamesWon / (float)winningCaster.NumberOfGamesPlayed;
            losingCaster.NumberOfGamesPlayed++;
            losingCaster.NumberOfGamesLost++;
            losingCaster.WinPercentage = (float)losingCaster.NumberOfGamesWon / (float)losingCaster.NumberOfGamesPlayed;

            DatabaseServices.InsertItem(newBattleReport);
            DatabaseServices.UpdateItem(winningFaction);
            DatabaseServices.UpdateItem(losingFaction);
            DatabaseServices.UpdateItem(winningCaster);
            DatabaseServices.UpdateItem(losingCaster);
            DatabaseServices.UpdateItem(currentUser);
        }
    }
}
