using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp
{
    public class Context
    {
        public static int ProjectId = -1;
        public static int ScenarioId = -1;

        public ScenarioModel Scenario;
        public List<FleetModel> Fleets;
        public List<HubModel> Hubs;
        public List<HubAllocationModel> HubAllocations;
        public List<TruckHubPriorityModel> TruckHubPriorities;
        public List<MachineParameterModel> MachineParameters;
        public List<TruckTypeMinePlanModel> TruckTypeMinePlans;
        public List<TruckHourModel> TruckHours;

        public List<String> AssetModels;

        public Context()
        {
            if (ProjectId != -1 && ScenarioId != -1)
            {
                LoadData();
            }

        }
        private void LoadData()
        {
            Scenario = new ScenarioDataAccess().GetScenario(Context.ScenarioId);
            Fleets = new FleetDataAccess().GetFleets();
            AssetModels = new FleetDataAccess().GetAssetModels();
            Hubs = new HubDataAccess().GetHubs();
            HubAllocations = new HubAllocationDataAccess().GetHubAllocations();
            TruckHubPriorities = new TruckHubPriorityDataAccess().GetTruckHubPriorities();
            MachineParameters = new MachineParameterDataAccess().GetMachineParameters();
            TruckTypeMinePlans = new TruckTypeMinePlanDataAccess().GetTruckTypeMinePlans();
            TruckHours = new TruckHourDataAccess().GetTruckHours();
        }

        public List<FleetModel> getFleetsByAssetModel(String AssetModel)
        {
            List<FleetModel> output = new List<FleetModel>();

            foreach (var Fleet in Fleets)
            {
                if (AssetModel.Equals(Fleet.AssetModel))
                {
                    output.Add(Fleet);
                }
            }

            return output;
        }

        public HubModel getHubById(int Id)
        {
            foreach (var Hub in Hubs)
            {
                if (Hub.Id == Id)
                {
                    return Hub;
                }
            }
            return null;
        }

        public int getHubPriority(String HubName, String AssetModel)
        {
            foreach (var TruckHubPriority in TruckHubPriorities)
            {
                if (TruckHubPriority.AssetModel.Equals(AssetModel) && TruckHubPriority.Hub.Equals(HubName))
                {
                    return TruckHubPriority.Priority;
                }
            }
            return 1;
        }

        public Decimal getEngineHours(String HubName, String AssetModel, String Mode, int period)
        {
            int StartYear = Scenario.StartYear;

            foreach (var MachineParameter in MachineParameters)
            {
                if (MachineParameter.AssetModel.Equals(AssetModel)
                    && MachineParameter.Hub.Equals(HubName)
                    && MachineParameter.Mode.Equals(Mode))
                {
                    List<MachineParameterYearMappingModel> mapping = MachineParameter.MachineParameterYearMapping;
                    foreach (var obj in mapping)
                    {
                        if (obj.Year == (StartYear + period - 1))
                        {
                            return obj.EngineHours;
                        }
                    }
                }
            }
            return 0;
        }

        public Decimal getRequiredHours(String HubName, int period)
        {
            int StartYear = Scenario.StartYear;

            foreach (var TruckTypeMinePlan in TruckTypeMinePlans)
            {
                if (TruckTypeMinePlan.Hub.Equals(HubName))
                {
                    List<TruckTypeMinePlanYearMappingModel> mapping = TruckTypeMinePlan.TruckTypeMinePlanYearMapping;
                    foreach (var obj in mapping)
                    {
                        if (obj.Year == (StartYear + period - 1))
                        {
                            return obj.Value;
                        }
                    }
                }
            }
            return 0;
        }
        public Decimal getRequiredHoursCoeff(String HubName, String AssetModel, String Mode, int period)
        {
            int StartYear = Scenario.StartYear;

            foreach (var MachineParameter in MachineParameters)
            {
                if (MachineParameter.AssetModel.Equals(AssetModel)
                    && MachineParameter.Hub.Equals(HubName)
                    && MachineParameter.Mode.Equals(Mode))
                {
                    List<MachineParameterYearMappingModel> mapping = MachineParameter.MachineParameterYearMapping;
                    foreach (var obj in mapping)
                    {
                        if (obj.Year == (StartYear + period - 1))
                        {
                            foreach (var TruckTypeMinePlan in TruckTypeMinePlans)
                            {
                                if (TruckTypeMinePlan.Hub.Equals(HubName))
                                {
                                    return (TruckTypeMinePlan.MinePlanPayload / obj.Payload) * (obj.UsableHours / obj.EngineHours);
                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}
