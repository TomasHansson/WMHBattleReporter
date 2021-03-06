﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
                || ViewModel.OpponentsFaction == null || ViewModel.OpponentsCaster == null || ViewModel.OpponentsTheme == null)
                return;

            if (!DatabaseServices.UsernameExists(ViewModel.OpponentsUsername))
            {
                Message?.Invoke("No user with the given name for the opponent exists in the database.");
                return;
            }

            if (DatabaseServices.LoggedInUser.Username == ViewModel.OpponentsUsername)
            {
                Message?.Invoke("You cannot specify your own username as the opponents username.");
                return;
            }

            BattleReport newBattleReport = CreateBattleReport();
            UpdateRelatedEntities(newBattleReport);
            DatabaseServices.LoggedInUser = DatabaseServices.GetUser(DatabaseServices.LoggedInUser.Username);
            DatabaseServices.InsertItem(newBattleReport);
            // NotifyOpponentByEmail(newBattleReport); // Attempts through my email-account is currently blocked.
            Message?.Invoke("The battle report has been saved.");
        }

        public delegate void SendMessage(string message);
        public event SendMessage Message;

        private BattleReport CreateBattleReport()
        {
            return new BattleReport()
            {
                DatePlayed = ViewModel.DatePlayed,
                PostersUsername = DatabaseServices.LoggedInUser.Username,
                OpponentsUsername = ViewModel.OpponentsUsername,
                ConfirmedByOpponent = false,
                ConfirmationKey = new Random().Next(1000,10000),
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
        }

        private static void UpdateRelatedEntities(BattleReport newBattleReport)
        {
            UpdateFactions(newBattleReport);
            UpdateThemes(newBattleReport);
            UpdateCasters(newBattleReport);
            UpdateUsers(newBattleReport);
        }

        private static void UpdateFactions(BattleReport newBattleReport)
        {
            Faction winningFaction = DatabaseServices.GetFactions().Where(f => f.Name == newBattleReport.WinningFaction).First();
            winningFaction.NumberOfGamesPlayed++;
            winningFaction.NumberOfGamesWon++;
            winningFaction.Winrate = (float)winningFaction.NumberOfGamesWon / (float)winningFaction.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(winningFaction);

            Faction losingFaction = DatabaseServices.GetFactions().Where(f => f.Name == newBattleReport.LosingFaction).First();
            losingFaction.NumberOfGamesPlayed++;
            losingFaction.NumberOfGamesLost++;
            losingFaction.Winrate = (float)losingFaction.NumberOfGamesWon / (float)losingFaction.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(losingFaction);
        }

        private static void UpdateThemes(BattleReport newBattleReport)
        {
            Theme winningTheme = DatabaseServices.GetThemes().Where(t => t.Name == newBattleReport.WinningTheme).First();
            winningTheme.NumberOfGamesPlayed++;
            winningTheme.NumberOfGamesWon++;
            winningTheme.Winrate = (float)winningTheme.NumberOfGamesWon / (float)winningTheme.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(winningTheme);

            Theme losingTheme = DatabaseServices.GetThemes().Where(t => t.Name == newBattleReport.LosingTheme).First();
            losingTheme.NumberOfGamesPlayed++;
            losingTheme.NumberOfGamesLost++;
            losingTheme.Winrate = (float)losingTheme.NumberOfGamesWon / (float)losingTheme.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(losingTheme);
        }

        private static void UpdateCasters(BattleReport newBattleReport)
        {
            Caster winningCaster = DatabaseServices.GetCasters().Where(c => c.Name == newBattleReport.WinningCaster).First();
            winningCaster.NumberOfGamesPlayed++;
            winningCaster.NumberOfGamesWon++;
            winningCaster.Winrate = (float)winningCaster.NumberOfGamesWon / (float)winningCaster.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(winningCaster);

            Caster losingCaster = DatabaseServices.GetCasters().Where(c => c.Name == newBattleReport.LosingCaster).First();
            losingCaster.NumberOfGamesPlayed++;
            losingCaster.NumberOfGamesLost++;
            losingCaster.Winrate = (float)losingCaster.NumberOfGamesWon / (float)losingCaster.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(losingCaster);
        }

        private static void UpdateUsers(BattleReport newBattleReport)
        {
            User winner = DatabaseServices.GetUser(newBattleReport.WinnersUsername);
            winner.NumberOfGamesPlayed++;
            winner.NumberOfGamesWon++;
            winner.Winrate = (float)winner.NumberOfGamesWon / (float)winner.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(winner);

            User loser = DatabaseServices.GetUser(newBattleReport.LosersUsername);
            loser.NumberOfGamesPlayed++;
            loser.NumberOfGamesLost++;
            loser.Winrate = (float)loser.NumberOfGamesWon / (float)loser.NumberOfGamesPlayed;
            DatabaseServices.UpdateItem(loser);
        }

        private void NotifyOpponentByEmail(BattleReport newBattleReport)
        {
            try
            {
                EmailProvider emailProvider = DatabaseServices.GetEmailProvider();

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(emailProvider.Address),
                    Subject = "Did you play in this Battle Report?",
                    Body = $"{newBattleReport.PostersUsername} posted a new battle report with you as the opponent." +
                    $"Please log in and verify the results, using the following key: {newBattleReport.ConfirmationKey}."
                };
                mail.To.Add(DatabaseServices.GetUser(newBattleReport.OpponentsUsername).Email);

                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential(emailProvider.Address, emailProvider.Password),
                    EnableSsl = true
                };

                smtpServer.Send(mail);
                Message?.Invoke("Mail-notification sent to opponent.");
            }
            catch (Exception ex)
            {
                Message?.Invoke(ex.Message);
            }
        }
    }
}
