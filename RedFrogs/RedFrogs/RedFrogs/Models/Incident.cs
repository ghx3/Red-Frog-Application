using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFrogs.Models
{
   public class Incident
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int SymptomID { get; set; }
        public string IncidentName { get; set; }
    }
}
