using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFrogs.Models
{
   public class Specific
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IncidentID { get; set; }
        public string Name { get; set; }
    }
}
