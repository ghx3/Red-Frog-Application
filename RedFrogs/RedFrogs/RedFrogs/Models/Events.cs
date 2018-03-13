using SQLite.Net.Attributes;

namespace RedFrogs.Models
{
    public class Events
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string EventName { get; set; }
        public int numInteractions { get; set; }
    }
}
