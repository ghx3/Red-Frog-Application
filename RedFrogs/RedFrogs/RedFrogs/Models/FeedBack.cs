using SQLite.Net.Attributes;

namespace RedFrogs.Models
{
    public class FeedBack
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string EventName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int Symptom { get; set; }
        public int SeenByMedic { get; set; }
        public int IncidentReported { get; set; }
        public string ActionTaken { get; set; }
        public string TimeInCare { get; set; }
        public string VolunteerName { get; set; }

    }
}
