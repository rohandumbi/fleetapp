using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class TruckPayloadModel
    {
        public int id { get; set; }
        public int ScenarioID { get; set; }
        public String AssetModel { get; set; }
        public String MaterialType { get; set; }
        public int Payload { get; set; }
    }
}
