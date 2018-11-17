using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class MinePlanModel
    {
        public int Id { get; set; }
        public String Hub { get; set; }
        public String Physical { get; set; }
        public List<MinePlanYearMappingModel> mapping { get; set; }
    }

    public class MinePlanYearMappingModel
    {
        public int MinePlanId { get; set; }
        public int Year { get; set; }
        public Decimal Value { get; set; }
    }
}
