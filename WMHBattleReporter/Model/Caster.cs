using SQLite;

namespace WMHBattleReporter.Model
{
    public class Caster
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int FactionId { get; set; }
        public string Name { get; set; }
    }
}
