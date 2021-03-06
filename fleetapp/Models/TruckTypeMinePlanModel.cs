﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class TruckTypeMinePlanModel
    {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public String Hub { get; set; }
        public String TruckType { get; set; }
        public Decimal MinePlanPayload { get; set; }
        public List<TruckTypeMinePlanYearMappingModel> TruckTypeMinePlanYearMapping { get; set; }

    }
    public class TruckTypeMinePlanYearMappingModel
    {
        public int TruckTypeMinePlanId { get; set; }
        public int Year { get; set; }
        public Decimal Value { get; set; }
    }
}
