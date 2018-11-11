using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class TruckHoursModel
    {
        public int id { get; set; }
        public int ScenarioID { get; set; }
        public String AssetModel { get; set; }
        public String GroupName { get; set; }
        public String Hub { get; set; }
        public String Mode { get; set; }
        public List<TruckHoursYearMappingModel> mapping { get; set; }
    }

    public class TruckHoursYearMappingModel
    {
        public int TruckHoursID { get; set; }
        public int Year { get; set; }
        public Decimal Value { get; set; }
    }
}
