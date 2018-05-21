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

        [Newtonsoft.Json.JsonProperty("LitresWater")]
        public int LitresWater { get; set; }

        [Newtonsoft.Json.JsonProperty("NumRFLollies")]
        public int NumRFLollies { get; set; }

        [Newtonsoft.Json.JsonProperty("NumOtherGoods")]
        public int NumOtherGoods { get; set; }

        public DateTime EndDate { get; set; }

        [Newtonsoft.Json.JsonProperty("IsClosed")]
        public bool IsClosed { get; set; }

        [Newtonsoft.Json.JsonProperty("Location")]
        public string Location { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int IndvlInteractions { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int IndvlWaterCount { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int IndvlRFLolliesCount { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int IndvlOGCount { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay { get { return EndDate.ToLocalTime().ToString("dd/MM/yyyy"); } }
    }
}
