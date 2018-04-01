using SQLite.Net.Attributes;

namespace RedFrogs.Models
{
    public class Events
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string EventName { get; set; }
        public int NumInteractions { get; set; }
        public string EndDate { get; set; }
    }
}
