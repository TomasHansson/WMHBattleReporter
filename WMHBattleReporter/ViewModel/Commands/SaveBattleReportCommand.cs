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
                EnemyFaction = ViewModel.OpponentsFaction.Name,
                EnemyCaster = ViewModel.OpponentsCaster.Name,
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

            if (ViewModel.UserWon)
            {
                newBattleReport.WinningFaction = ViewModel.UsersFaction.Name;
                newBattleReport.WinningCaster = ViewModel.UsersCaster.Name;
            }
            else if (ViewModel.OpponentWon)
            {
                newBattleReport.WinningFaction = ViewModel.OpponentsFaction.Name;
                newBattleReport.WinningCaster = ViewModel.OpponentsCaster.Name;
            }

            DatabaseServices.SaveBattleReport(newBattleReport);
        }
    }
}
