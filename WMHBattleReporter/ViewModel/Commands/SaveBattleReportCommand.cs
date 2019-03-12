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
            if ((!ViewModel.UserWon && !ViewModel.OpponentWon)
                || ViewModel.UsersFaction == null || ViewModel.UsersCaster == null || ViewModel.UsersTheme == null
                || ViewModel.OpponentsFaction == null || ViewModel.OpponentsCaster == null || ViewModel.OpponentsTheme == null
                || !DatabaseServices.UsernameExists(ViewModel.OpponentsUsername)
                || DatabaseServices.LoggedInUser.Username == ViewModel.OpponentsUsername)
                return;

            BattleReport newBattleReport = new BattleReport()
            {
                DatePlayed = ViewModel.DatePlayed,
                PostersUsername = DatabaseServices.LoggedInUser.Username,
                OpponentsUsername = ViewModel.OpponentsUsername,
                ConfirmedByOpponent = false,
                GameSize = ViewModel.GameSize,
                Scenario = ViewModel.Scenario,
                PostersFaction = ViewModel.UsersFaction.Name,
                PostersCaster = ViewModel.UsersCaster.Name,
                PostersTheme = ViewModel.UsersTheme.Name,
                PostersControlPoints = ViewModel.UsersControlPoints,
                PostersArmyPoints = ViewModel.UsersArmyPoints,
                PostersArmyList = ViewModel.UsersArmyList,
                OpponentsFaction = ViewModel.OpponentsFaction.Name,
                OpponentsCaster = ViewModel.OpponentsCaster.Name,
                OpponentsTheme = ViewModel.OpponentsTheme.Name,
                OpponentsControlPoints = ViewModel.OpponentsControlPoints,
                OpponentsArmyPoints = ViewModel.OpponentsArmyPoints,
                OpponentsArmyList = ViewModel.OpponentsArmyList,
                EndCondition = ViewModel.EndCondition,
                WinnersUsername = ViewModel.UserWon ? DatabaseServices.LoggedInUser.Username : ViewModel.OpponentsUsername,
                WinningFaction = ViewModel.UserWon ? ViewModel.UsersFaction.Name : ViewModel.OpponentsFaction.Name,
                WinningCaster = ViewModel.UserWon ? ViewModel.UsersCaster.Name : ViewModel.OpponentsCaster.Name,
                WinningTheme = ViewModel.UserWon ? ViewModel.UsersTheme.Name : ViewModel.OpponentsTheme.Name,
                LosersUsername = ViewModel.UserWon ? ViewModel.OpponentsUsername : DatabaseServices.LoggedInUser.Username,
                LosingFaction = ViewModel.UserWon ? ViewModel.OpponentsFaction.Name : ViewModel.UsersFaction.Name,
                LosingCaster = ViewModel.UserWon ? ViewModel.OpponentsCaster.Name : ViewModel.UsersCaster.Name,
                LosingTheme = ViewModel.UserWon ? ViewModel.OpponentsTheme.Name : ViewModel.UsersTheme.Name
            };

            Faction winningFaction = DatabaseServices.GetFactions().Where(f => f.Name == newBattleReport.WinningFaction).First();
            winningFaction.NumberOfGamesPlayed++;
            winningFaction.NumberOfGamesWon++;
            winningFaction.Winrate = (float)winningFaction.NumberOfGamesWon / (float)winningFaction.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(winningFaction);

            Theme winningTheme = DatabaseServices.GetThemes().Where(t => t.Name == newBattleReport.WinningTheme).First();
            winningTheme.NumberOfGamesPlayed++;
            winningTheme.NumberOfGamesWon++;
            winningTheme.Winrate = (float)winningTheme.NumberOfGamesWon / (float)winningTheme.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(winningTheme);

            Caster winningCaster = DatabaseServices.GetCasters().Where(c => c.Name == newBattleReport.WinningCaster).First();
            winningCaster.NumberOfGamesPlayed++;
            winningCaster.NumberOfGamesWon++;
            winningCaster.Winrate = (float)winningCaster.NumberOfGamesWon / (float)winningCaster.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(winningCaster);

            User winner = DatabaseServices.GetUser(newBattleReport.WinnersUsername);
            winner.NumberOfGamesPlayed++;
            winner.NumberOfGamesWon++;
            winner.Winrate = (float)winner.NumberOfGamesWon / (float)winner.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(winner);

            Faction losingFaction = DatabaseServices.GetFactions().Where(f => f.Name == newBattleReport.LosingFaction).First();
            losingFaction.NumberOfGamesPlayed++;
            losingFaction.NumberOfGamesLost++;
            losingFaction.Winrate = (float)losingFaction.NumberOfGamesWon / (float)losingFaction.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(losingFaction);

            Theme losingTheme = DatabaseServices.GetThemes().Where(t => t.Name == newBattleReport.LosingTheme).First();
            losingTheme.NumberOfGamesPlayed++;
            losingTheme.NumberOfGamesLost++;
            losingTheme.Winrate = (float)losingTheme.NumberOfGamesWon / (float)losingTheme.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(losingTheme);

            Caster losingCaster = DatabaseServices.GetCasters().Where(c => c.Name == newBattleReport.LosingCaster).First();
            losingCaster.NumberOfGamesPlayed++;
            losingCaster.NumberOfGamesLost++;
            losingCaster.Winrate = (float)losingCaster.NumberOfGamesWon / (float)losingCaster.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(losingCaster);

            User loser = DatabaseServices.GetUser(newBattleReport.LosersUsername);
            loser.NumberOfGamesPlayed++;
            loser.NumberOfGamesLost++;
            loser.Winrate = (float)loser.NumberOfGamesWon / (float)loser.NumberOfGamesPlayed;

            DatabaseServices.LoggedInUser = DatabaseServices.GetUser(DatabaseServices.LoggedInUser.Username);

            DatabaseServices.InsertItem(newBattleReport);

            Message?.Invoke("The battle report has been saved.");
        }

        public delegate void SendMessage(string message);
        public event SendMessage Message;
    }
}
