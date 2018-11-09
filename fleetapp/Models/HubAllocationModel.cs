using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class HubAllocationModel
    {
        public int id { get; set; }
        public int ProjectID { get; set; }
        public int HubID { get; set; }
        public String AssestModel { get; set; }
        public Boolean IsManned { get; set; }
        public Boolean IsAHS { get; set; }
    }
}
