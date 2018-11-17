using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class TruckHourModel
    {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public String AssetModel { get; set; }
        public String GroupName { get; set; }
        public String Hub { get; set; }
        public String Mode { get; set; }
        public List<TruckHourYearMappingModel> TruckHourYearMapping { get; set; }
    }

    public class TruckHourYearMappingModel
    {
        public int TruckHourId { get; set; }
        public int Year { get; set; }
        public Decimal Value { get; set; }
    }
}
