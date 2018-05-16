using SQLite;

namespace RedFrogs.Models
{
    public class Users
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("Username")]
        public string Username { get; set; }

        [Newtonsoft.Json.JsonProperty("Password")]
        public string Password { get; set; }

        [Newtonsoft.Json.JsonProperty("VolName")]
        public string VolName { get; set; }

        [Newtonsoft.Json.JsonProperty("IsLeader")]
        public bool IsLeader { get; set; }
    }
}
