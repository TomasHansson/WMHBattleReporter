using SQLite;

namespace WMHBattleReporter.Model
{
    public class Caster
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string Faction { get; set; }
        public int NumberOfGamesPlayed { get; set; }
        public int NumberOfGamesWon { get; set; }
        public int NumberOfGamesLost { get; set; }
        public float Winrate { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
