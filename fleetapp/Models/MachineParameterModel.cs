using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class MachineParameterModel
    {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public String AssetModel { get; set; }
        public String Hub { get; set; }
        public String Mode { get; set; }
        public List<MachineParameterYearMappingModel> MachineParameterYearMapping { get; set; }
    }

    public class MachineParameterYearMappingModel
    {
        public int MachineParameterId { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        public Decimal SchEU { get; set; }
        public Decimal Npot { get; set; }
        public Decimal UtEu { get; set; }
        public int Payload { get; set; }
        public Decimal EngineHours { get; set; }
        public Decimal UsableHours { get; set; }

    }

    public class MPPresentationModel
    {
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public String AssetModel { get; set; }
        public String Hub { get; set; }
        public String Mode { get; set; }
        public String Type { get; set; }
        public List<MPPresenationYearMappingModel> MPPresenationYearMapping { get; set; }
    }
    public class MPPresenationYearMappingModel
    {      
        public int Year { get; set; }
        public Decimal Value { get; set; }
    }
}
