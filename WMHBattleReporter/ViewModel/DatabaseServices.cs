using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel
{
    public static class DatabaseServices
    {
        private const string databaseFile = @"C:\Temp\BR-Database.db";
        public static User LoggedInUser { get; set; }
        public static bool UserLoggedIn { get; set; }
        public static bool LoggedInUserIsAsmin { get; set; }

        public static void InitializeTables()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                connection.CreateTable<BattleReport>();
                connection.CreateTable<Caster>();
                connection.CreateTable<Faction>();
                connection.CreateTable<User>();
                connection.CreateTable<Theme>();
            }
        }

        public static bool PasswordIsCorrect(string username, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                User user = connection.Table<User>().Where(u => u.Username == username).First();
                return password == user.Password;
            }
        }

        public static User GetUser(string username)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                return connection.Table<User>().Where(u => u.Username == username).First();
        }

        public static List<User> GetUsers()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                return connection.Table<User>().ToList();
        }

        public static List<Faction> GetFactions()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                return connection.Table<Faction>().ToList();
        }

        public static List<Caster> GetCasters()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                return connection.Table<Caster>().ToList();
        }

        public static List<Caster> GetFactionCasters(string factionName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                return connection.Table<Caster>().Where(c => c.Faction == factionName).ToList();
        }

        public static List<BattleReport> GetBattleReports()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                return connection.Table<BattleReport>().ToList();
        }

        public static List<Theme> GetThemes()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                return connection.Table<Theme>().ToList();
        }

        public static List<Theme> GetFactionThemes(string factionName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                return connection.Table<Theme>().Where(t => t.Faction == factionName).ToList();
        }

        public static bool UsernameExists(string username)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                List<User> users = connection.Table<User>().Where(u => u.Username == username).ToList();
                return users.Count > 0;
            }
        }

        public static bool FactionNameExists(string factionName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                List<Faction> factions = connection.Table<Faction>().Where(f => f.Name == factionName).ToList();
                return factions.Count > 0;
            }
        }

        public static bool ThemeNameExists(string themeName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                List<Theme> themes = connection.Table<Theme>().Where(t => t.Name == themeName).ToList();
                return themes.Count > 0;
            }
        }

        public static bool CasterNameExists(string casterName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                List<Caster> casters = connection.Table<Caster>().Where(c => c.Name == casterName).ToList();
                return casters.Count > 0;
            }
        }

        public static void InsertItem<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                connection.Insert(item);
        }

        public static void UpdateItem<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                connection.Update(item);
        }

        public static void DeleteItem<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                connection.Delete(item);
        }
    }
}
