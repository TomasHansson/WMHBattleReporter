using SQLite;

namespace WMHBattleReporter.Model
{
    public class Faction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfGamesPlayed { get; set; }
        public int NumberOfGamesWon { get; set; }
        public int NumberOfGamesLost { get; set; }
        public float WinPercentage { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
