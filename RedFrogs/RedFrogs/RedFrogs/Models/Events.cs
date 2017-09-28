using SQLite;

namespace RedFrogs.Models
{
    public class Events
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string EventName { get; set; }
    }
}
