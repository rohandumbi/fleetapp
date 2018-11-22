using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class TruckHubPriorityModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public String AssetModel { get; set; }
        public String Hub { get; set; }
        public int Priority { get; set; }
    }
}
