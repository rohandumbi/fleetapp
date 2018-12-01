using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.LP
{
    public class EquationGenerator
    {
        private const double pvif = 0.1; 
        private const int MODE_MANNED = 0;
        private const int MODE_AHS = 1;

        Context ctx;


        public void generate()
        {
            ctx = new Context();
            FileStream fs = CreateFile();
            
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine("minimize");
                WriteObjectiveFunction(sw);
                sw.WriteLine("Subject to");
                WriteConstraints(sw);
                sw.WriteLine("end");
            }

            
        }

        private FileStream CreateFile()
        {
            Directory.CreateDirectory(@"C:\\FleetApp");
            String fileName = @"C:\\FleetApp\\fleetapp-dump.lp";
            
            return File.Create(fileName);
        }
        

        private void WriteObjectiveFunction(StreamWriter sw)
        {
            String line = "";
            int lineMaxLength = 200;
            foreach(var HubAllocation in ctx.HubAllocations)
            {
                List<FleetModel> Fleets = ctx.getFleetsByAssetModel(HubAllocation.AssetModel);
                HubModel Hub = ctx.getHubById(HubAllocation.HubId);
                int HubPriority = ctx.getHubPriority(Hub.Name, HubAllocation.AssetModel);
                int TimePeriod = ctx.Scenario.TimePeriod;
                
                for(var i=1; i<= TimePeriod; i++)
                {
                    double pvif = 0.1;
                    double coeff = pvif * (1 / Math.Pow(Convert.ToDouble(1 + i), Convert.ToDouble(i))) * HubPriority;
                    foreach (var Fleet in Fleets)
                    {
                        coeff = Math.Round(coeff * Fleet.Priority, 3);
                        if (coeff == 0) continue;
                        if(HubAllocation.IsManned)
                        {
                            line = line + " + " + coeff + "x" + Fleet.AssetNumber + "h" + Hub.HubNumber + "m1t" + i;
                        }
                        if (HubAllocation.IsAHS)
                        {
                            line = line + " + " + coeff + "x" + Fleet.AssetNumber + "h" + Hub.HubNumber + "m2t" + i;
                        }
                        if(line.Length > lineMaxLength)
                        {
                            sw.WriteLine(line);
                            line = "";
                        }
                    }
                   
                }
            }
        }

        private void WriteConstraints(StreamWriter sw)
        {
            WriteEngineHourConstraints(sw);
            WriteRequiredHourConstraints(sw);
            WriteMachineAgeConstraints(sw);
            WriteMachineAcrossHubConstraints(sw);
            WriteTruckHourConstraints(sw);
        }

        private void WriteEngineHourConstraints(StreamWriter sw)
        {
            sw.WriteLine("\\engine hours constraint");

            foreach (var HubAllocation in ctx.HubAllocations)
            {
                List<FleetModel> Fleets = ctx.getFleetsByAssetModel(HubAllocation.AssetModel);
                HubModel Hub = ctx.getHubById(HubAllocation.HubId);
                int TimePeriod = ctx.Scenario.TimePeriod;

                for (var i = 1; i <= TimePeriod; i++)
                {
                    foreach (var Fleet in Fleets)
                    {
                        if (HubAllocation.IsManned)
                        {
                            var engineHours = ctx.getEngineHours(Hub.Name, Fleet.AssetModel, "Manned", i);
                            engineHours = Math.Round(engineHours, 3);
                            String variable = @"x" + Fleet.AssetNumber + "h" + Hub.HubNumber + "m1t" + i;
                            sw.WriteLine(variable+ " >= 0");
                            sw.WriteLine(variable + " <= "+ engineHours);
                        }
                        if (HubAllocation.IsAHS)
                        {
                            var engineHours = ctx.getEngineHours(Hub.Name, Fleet.AssetModel, "AHS", i);
                            engineHours = Math.Round(engineHours, 3);
                            String variable = @"x" + Fleet.AssetNumber + "h" + Hub.HubNumber + "m2t" + i;
                            sw.WriteLine(variable + " >= 0");
                            sw.WriteLine(variable + " <= " + engineHours);
                        }
                    }

                }
            }
        }
        private void WriteRequiredHourConstraints(StreamWriter sw)
        {
            sw.WriteLine("\\required hours constraint for each hub");

            foreach (var Hub in ctx.Hubs)
            {
                int TimePeriod = ctx.Scenario.TimePeriod;

                for (var i = 1; i <= TimePeriod; i++)
                {
                    List<HubAllocationModel> HubAllocations = new List<HubAllocationModel>();
                    foreach(var HubAllocation in ctx.HubAllocations)
                    {
                        if(HubAllocation.HubId == Hub.Id)
                        {
                            HubAllocations.Add(HubAllocation);
                        }
                    }
                    foreach (var HubAllocation in HubAllocations)
                    {
                        String line = "";
                        List<FleetModel> Fleets = ctx.getFleetsByAssetModel(HubAllocation.AssetModel);
                        var requiredHours = ctx.getRequiredHours(Hub.Name, HubAllocation.AssetModel, i);
                        if (requiredHours == 0) continue;
                        foreach (var Fleet in Fleets)
                        {
                            
                            if (HubAllocation.IsManned)
                            {
                                var coeff = ctx.getRequiredHoursCoeff(Hub.Name, Fleet.AssetModel, "Manned", i);
                                coeff = Math.Round(coeff, 3);
                                String variable = " + " + coeff + "x" + Fleet.AssetNumber + "h" + Hub.HubNumber + "m1t" + i;
                                line = line + variable;

                            }
                            if (HubAllocation.IsAHS)
                            {
                                var coeff = ctx.getRequiredHoursCoeff(Hub.Name, Fleet.AssetModel, "AHS", i);
                                coeff = Math.Round(coeff, 3);
                                String variable = " + " + coeff + "x" + Fleet.AssetNumber + "h" + Hub.HubNumber + "m2t" + i;
                                line = line + variable;
                            }
                        }
                        sw.WriteLine(line +" <= " + requiredHours);
                    }

                }
            }
        }
        private void WriteMachineAgeConstraints(StreamWriter sw)
        {
            sw.WriteLine("\\machine age constraint");

            String line = "";
            int lineMaxLength = 200;
            foreach (var HubAllocation in ctx.HubAllocations)
            {
                List<FleetModel> Fleets = ctx.getFleetsByAssetModel(HubAllocation.AssetModel);
                HubModel Hub = ctx.getHubById(HubAllocation.HubId);
                int TimePeriod = ctx.Scenario.TimePeriod;
                foreach (var Fleet in Fleets)
                {
                    for (var i = 1; i <= TimePeriod; i++)
                    {
                        if (HubAllocation.IsManned)
                        {
                            line = line + " + x" + Fleet.AssetNumber + "h" + Hub.HubNumber + "m1t" + i;
                        }
                        if (HubAllocation.IsAHS)
                        {
                            line = line + " + x" + Fleet.AssetNumber + "h" + Hub.HubNumber + "m2t" + i;
                        }
                        if (line.Length > lineMaxLength)
                        {
                            sw.WriteLine(line);
                            line = "";
                        }
                    }
                    line = line + " <= " + (Fleet.FinalAge - Fleet.InitialAge);
                    sw.WriteLine(line);
                    line = "";
                }
            }

        }
        private void WriteMachineAcrossHubConstraints(StreamWriter sw)
        {
            sw.WriteLine("\\machine across all hub per year constraint");

            int TimePeriod = ctx.Scenario.TimePeriod;
            for (var i = 1; i <= TimePeriod; i++)
            {
                foreach (var AssetModel in ctx.AssetModels)
                {
                    List<FleetModel> Fleets = ctx.getFleetsByAssetModel(AssetModel);
                    List<HubAllocationModel> HubAllocations = new List<HubAllocationModel>();
                    foreach (var HubAllocation in ctx.HubAllocations)
                    {
                        if (HubAllocation.AssetModel.Equals(AssetModel))
                        {
                            HubAllocations.Add(HubAllocation);
                        }
                    }
                    if (HubAllocations.Count == 0) continue;

                    foreach (var Fleet in Fleets)
                    {
                        String line = "";
                        foreach (var HubAllocation in HubAllocations)
                        {
                            HubModel Hub = ctx.getHubById(HubAllocation.HubId);
                            if (HubAllocation.IsManned)
                            {
                                var engineHours = ctx.getEngineHours(Hub.Name, AssetModel, "Manned", i);
                                if(engineHours != 0)
                                {
                                    line = line + " + " + (1 / engineHours) + " x" + Fleet.AssetNumber + "h" + Hub.HubNumber + "m1t" + i;
                                } else
                                {
                                    Console.WriteLine("Manned - HubName :" + Hub.Name + " AssetModel :" + AssetModel + " i :" + i);
                                }
                                
                            }
                            if (HubAllocation.IsAHS)
                            {
                                var engineHours = ctx.getEngineHours(Hub.Name, AssetModel, "AHS", i);
                                if (engineHours != 0)
                                {
                                    line = line + " + " + (1 / engineHours) + " x" + Fleet.AssetNumber + "h" + Hub.HubNumber + "m2t" + i;
                                }
                                else
                                {
                                    Console.WriteLine("AHS - HubName :" + Hub.Name + " AssetModel :" + AssetModel + " i :" + i);
                                }
                            }
                        }
                        
                        line = line + " <= 1 ";
                        sw.WriteLine(line);
                    }
                }
            }
        }

        private void WriteTruckHourConstraints(StreamWriter sw)
        {
            sw.WriteLine("\\Truck hour constraint");

            foreach (var TruckHour in ctx.TruckHours)
            {
                List<FleetModel> fleets = ctx.getFleetsByAssetModel(TruckHour.AssetModel);
                var Hub = ctx.getHubById(TruckHour.HubId);
                int TimePeriod = ctx.Scenario.TimePeriod;
                int mode = (TruckHour.Mode == "Manned") ? 1 : 2;
                for (var i = 1; i <= TimePeriod; i++)
                {
                    Decimal value = -1;
                    foreach(var TruckHourMapping in TruckHour.TruckHourYearMapping)
                    {
                        if(TruckHourMapping.Year == (ctx.Scenario.StartYear + i))
                        {
                            value = TruckHourMapping.Value;
                            break;
                        }
                    }
                    if (value == -1) continue;
                    String line = "";
                    foreach(var fleet in fleets)
                    {
                        line = line + " + x" + fleet.AssetNumber + "h" + Hub.HubNumber + "m" + mode;
                    }
                    if(line.Length > 0)
                    {
                        line = line + " <= " + value;
                        sw.WriteLine(line);
                    }
                }
            }
        }
    }
}
