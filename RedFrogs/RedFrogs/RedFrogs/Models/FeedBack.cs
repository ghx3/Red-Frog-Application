using System;
using SQLite;

namespace RedFrogs.Models
{
    public class FeedBack
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("EventName")]
        public string EventName { get; set; }

        [Newtonsoft.Json.JsonProperty("City")]
        public string City { get; set; }

        [Newtonsoft.Json.JsonProperty("SupportTo")]
        public string SupportTo { get; set; }

        [Newtonsoft.Json.JsonProperty("EventDate")]
        public DateTime EventDate { get; set; }

        [Newtonsoft.Json.JsonProperty("SupportGiven")]
        public string SupportGiven { get; set; }

        [Newtonsoft.Json.JsonProperty("ActivationLocation")]
        public string ActivationLocation { get; set; }

        [Newtonsoft.Json.JsonProperty("LeaderName")]
        public string LeaderName { get; set; }

        [Newtonsoft.Json.JsonProperty("VolunteerNumber")]
        public int VolunteerNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("HoursSpent")]
        public int HoursSpent { get; set; }

        [Newtonsoft.Json.JsonProperty("PatronNumber")]
        public int PatronNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("PatronInteractionNum")]
        public int PatronInteractionNum { get; set; }
        
        [Newtonsoft.Json.JsonProperty("PancakesProvided")]
        public bool PancakesProvided { get; set; }

        [Newtonsoft.Json.JsonProperty("NumberPancakes")]
        public int NumberPancakes { get; set; }

        [Newtonsoft.Json.JsonProperty("WaterProvided")]
        public bool WaterProvided { get; set; }

        [Newtonsoft.Json.JsonProperty("AmountWater")]
        public int AmountWater { get; set; }
        
        [Newtonsoft.Json.JsonProperty("AnyGiveaways")]
        public bool AnyGiveaways { get; set; }

        [Newtonsoft.Json.JsonProperty("GivenAway")]
        public string GivenAway { get; set; }

        [Newtonsoft.Json.JsonProperty("AnyPraiseReports")]
        public bool AnyPraiseReports { get; set; }

        [Newtonsoft.Json.JsonProperty("PraiseReport")]
        public string PraiseReport { get; set; }

        [Newtonsoft.Json.JsonProperty("AnyIncidents")]
        public bool AnyIncidents { get; set; }

        [Newtonsoft.Json.JsonProperty("IncidentDescription")]
        public string IncidentDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("FollowUpNeeded")]
        public bool FollowUpNeeded { get; set; }

        [Newtonsoft.Json.JsonProperty("FollowUpName")]
        public string FollowUpName { get; set; }

        [Newtonsoft.Json.JsonProperty("EventID")]
        public string EventID { get; set; }


    }
}
