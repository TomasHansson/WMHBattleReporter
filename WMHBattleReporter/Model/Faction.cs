using SQLite;

namespace WMHBattleReporter.Model
{
    public class Faction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
