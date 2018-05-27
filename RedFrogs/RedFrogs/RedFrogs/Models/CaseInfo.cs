using SQLite;

namespace RedFrogs.Models
{
    public class CaseInfo
    {
        [Newtonsoft.Json.JsonProperty("ID")]
        public string ID { get; set; }
        [Newtonsoft.Json.JsonProperty("EventName")]
        public string EventName { get; set; }
        [Newtonsoft.Json.JsonProperty("Name")]
        public string Name { get; set; }
        [Newtonsoft.Json.JsonProperty("Gender")]
        public string Gender { get; set; }
        [Newtonsoft.Json.JsonProperty("Age")]
        public int Age { get; set; }
        [Newtonsoft.Json.JsonProperty("Symptom")]
        public string Symptom { get; set; }
        [Newtonsoft.Json.JsonProperty("SymptomColour")]
        public string SymptomColour { get; set; }
        [Newtonsoft.Json.JsonProperty("IncidentType")]
        public string IncidentType { get; set; }
        [Newtonsoft.Json.JsonProperty("Specific")]
        public string Specific { get; set; }
        [Newtonsoft.Json.JsonProperty("SeenByMedic")]
        public bool SeenByMedic { get; set; }
        [Newtonsoft.Json.JsonProperty("IncidentReported")]
        public bool IncidentReported { get; set; }
        [Newtonsoft.Json.JsonProperty("ActionTaken")]
        public string ActionTaken { get; set; }
        [Newtonsoft.Json.JsonProperty("TimeInCare")]
        public string TimeInCare { get; set; }
        [Newtonsoft.Json.JsonProperty("VolunteerName")]
        public string VolunteerName { get; set; }

    }
}
