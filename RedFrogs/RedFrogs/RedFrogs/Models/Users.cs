using SQLite;

namespace RedFrogs.Models
{
    public class Users
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }
        
        [Newtonsoft.Json.JsonProperty("Password")]
        public string Password { get; set; }

        [Newtonsoft.Json.JsonProperty("IsTeamLeader")]
        public bool IsTeamLeader { get; set; }

    }
}
