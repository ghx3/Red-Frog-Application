using SQLite;
using System;

namespace RedFrogs.Models
{
    public class Events
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("EventName")]
        public string EventName { get; set; }

        [Newtonsoft.Json.JsonProperty("NumInteractions")]
        public int NumInteractions { get; set; }

        public DateTime EndDate { get; set; }

        [Newtonsoft.Json.JsonProperty("IsClosed")]
        public int IsClosed { get; set; }

        [Newtonsoft.Json.JsonProperty("Location")]
        public string Location { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay { get { return EndDate.ToLocalTime().ToString("dd/MM/yyyy"); } }
    }
}
