using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class TruckPayloadModel
    {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public String AssetModel { get; set; }
        public String MaterialType { get; set; }
        public Decimal Payload { get; set; }
    }
}
