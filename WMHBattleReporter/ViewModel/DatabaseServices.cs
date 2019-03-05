﻿using SQLite;
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
        public static int LoggedInUsersId = 0;

        public static void InitializeTables()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                connection.CreateTable<BattleReport>();
                connection.CreateTable<Caster>();
                connection.CreateTable<Faction>();
                connection.CreateTable<User>();
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

        public static bool UsernameExists(string username)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                List<User> users = connection.Table<User>().Where(u => u.Username == username).ToList();
                if (users.Count > 0)
                    return true;
            }
            return false;
        }

        public static User GetUserById(string username)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                return connection.Table<User>().Where(u => u.Username == username).First();
            }
        }

        public static bool FactionNameExists(string factionName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                List<Faction> factions = connection.Table<Faction>().Where(f => f.Name == factionName).ToList();
                if (factions.Count > 0)
                    return true;
            }
            return false;
        }

        public static void DeleteFaction(string factionName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                Faction factionToDelete = connection.Table<Faction>().Where(f => f.Name == factionName).First();
                connection.Delete(factionToDelete);
            }
        }

        public static List<Faction> GetFactions()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                return connection.Table<Faction>().ToList();
            }
        }

        public static void DeleteCaster(string casterName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                Caster casterToDelete = connection.Table<Caster>().Where(c => c.Name == casterName).First();
                connection.Delete(casterToDelete);
            }
        }

        public static List<Caster> GetFactionCasters(string factionName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                int factionId = connection.Table<Faction>().Where(f => f.Name == factionName).First().Id;
                return connection.Table<Caster>().Where(c => c.FactionId == factionId).ToList();
            }
        }

        public static bool CasterNameExists(string casterName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
            {
                List<Caster> casters = connection.Table<Caster>().Where(c => c.Name == casterName).ToList();
                if (casters.Count > 0)
                    return true;
            }
            return false;
        }

        public static void SaveBattleReport(BattleReport battleReport)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                connection.Insert(battleReport);
        }

        public static User GetUserById(int userId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                return connection.Table<User>().Where(u => u.Id == userId).First();
        }

        public static void DeleteItem<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                connection.Delete(item);
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

        public static List<Caster> GetCasters()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databaseFile))
                return connection.Table<Caster>().ToList();
        }
    }
}
