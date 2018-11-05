using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class TruckGroupModel
    {
        public int id { get; set; }
        public int ScenarioID { get; set; }
        public String Name { get; set; }
        public String AssetModel { get; set; }
        public String FleetID { get; set; }
    }
}
