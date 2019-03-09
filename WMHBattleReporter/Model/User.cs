using SQLite;

namespace WMHBattleReporter.Model
{
    public class User
    {
        [PrimaryKey]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Region { get; set; }
        public string Email { get; set; }
        public int NumberOfGamesPlayed { get; set; }
        public int NumberOfGamesWon { get; set; }
        public int NumberOfGamesLost { get; set; }
        public float Winrate { get; set; }
        public bool IsAdmin { get; set; }

        public override string ToString()
        {
            return Username;
        }
    }
}
