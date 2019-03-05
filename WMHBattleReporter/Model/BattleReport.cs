using SQLite;

namespace WMHBattleReporter.Model
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
        public string OpponentFaction { get; set; }
        public string OpponentCaster { get; set; }
        public string WinningFaction { get; set; }
        public string WinningCaster { get; set; }
    }
}
