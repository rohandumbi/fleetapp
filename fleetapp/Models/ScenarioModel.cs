using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class ScenarioModel
    {
        public int id { get; set; }
        public int ProjectID { get; set; }
        public String Name { get; set; }
        public int StartYear { get; set; }
        public int TimePeriod { get; set; }
    }
}
