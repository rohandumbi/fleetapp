using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class FleetModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public String AssetType { get; set; }
        public String AssetModel { get; set; }
        public String FleetId { get; set; }
    }
}
