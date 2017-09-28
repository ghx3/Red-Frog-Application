using SQLite;

namespace RedFrogs.Models
{
    public class FeedBack
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Symptom { get; set; }
        public bool SeenByMedic { get; set; }
        public bool IncidentReport { get; set; }
        public string ActionTaken { get; set; }
        public string TimeInCare { get; set; }
        public string VolunteerName { get; set; }

    }
}
