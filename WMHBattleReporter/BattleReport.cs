using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMHBattleReporter
{
    public class BattleReport
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int UserId { get; set; }
        public int GameSize { get; set; }
        public string GameType { get; set; }
        public string UserFaction { get; set; }
        public string UserCaster { get; set; }
        public string EnemyFaction { get; set; }
        public string EnemyCaster { get; set; }
        public string WinningFaction { get; set; }
        public string WinningCaster { get; set; }
    }
}
