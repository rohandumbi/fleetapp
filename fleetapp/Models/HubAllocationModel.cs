using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class HubAllocationModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int HubId { get; set; }
        public String AssetModel { get; set; }
        public Boolean IsManned { get; set; }
        public Boolean IsAHS { get; set; }
        public String HubName { get; set; }
    }
}
