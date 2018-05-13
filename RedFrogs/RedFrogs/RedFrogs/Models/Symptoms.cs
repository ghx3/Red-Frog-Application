using SQLite;
namespace RedFrogs.Models
{
    public class Symptoms
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string SympName { get; set; }
        public string Colour { get; set; }

    }
}
