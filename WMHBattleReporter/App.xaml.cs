using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WMHBattleReporter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string databaseFile = @"C:\Temp\BR-Database.db";
        public static User LoggedInUser { get; set; } = new User();
    }
}
