using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class FleetModel
    {
        public int id { get; set; }
        public int ProjectID { get; set; }
        public String AssetType { get; set; }
        public String AssetModel { get; set; }
        public String FleetID { get; set; }
    }
}
