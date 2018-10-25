using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.DataAccessClasses
{
    public class DataAccess
    {
        Random rnd = new Random();
        string[] streetAddresses = new string[] { "101 State Street", "425 Oak Avenue", "7 Wallace Way", "928 Edwards Court", "29 Main Avenue" };
        string[] cities = new string[] { "Springfield", "Wilshire", "Alexandria", "Franklin", "Clinton", "Fairview", "Madison" };
        string[] states = new string[] { "WI", "GA", "PA", "TX", "CA", "IL", "WA", "VA", "FL", "OK", "AZ" };
        string[] zipCodes = new string[] { "14121", "08904", "84732", "23410", "60095", "60618", "10456", "00926", "08701", "90280", "92335", "79936" };

        string[] firstNames = new string[] { "Bob", "Sue", "Carla", "Frank", "Monique", "Carlton", "Miguel", "Daniel", "Santiago", "John", "Robert" };
        string[] lastNames = new string[] { "Smith", "Jones", "Garcia", "Miller", "Thomas", "Lee", "Taylor", "Wilson", "Martinez", "Davis", "Hernandez" };
        bool[] aliveStatuses = new bool[] { true, false };
        DateTime lowEndDate = new DateTime(1943, 1, 1);
        int daysFromLowDate;

        public DataAccess()
        {
            daysFromLowDate = (DateTime.Today - lowEndDate).Days;
        }


        public List<HubModel> GetHubDefinitions()
        {
            List<HubModel> output = new List<HubModel>();
            //output.Add(new HubModel("1", "TP"));
            //output.Add(new HubModel("2", "S10"));
            //output.Add(new HubModel("3", "B1"));
            
            return output;
        }

        public List<HubAllocationModel> GetHubAllocations()
        {
            List<HubAllocationModel> output = new List<HubAllocationModel>();
            //output.Add(new HubAllocationModel("830E", "TP", false, false));
            //output.Add(new HubAllocationModel("830E", "S10", true, false));
            //output.Add(new HubAllocationModel("830E", "B1", true, true));
            //output.Add(new HubAllocationModel("830DC", "TP", false, false));
            //output.Add(new HubAllocationModel("830DC", "S10", true, false));
            //output.Add(new HubAllocationModel("830DC", "B1", false, false));
            //output.Add(new HubAllocationModel("XEMC", "TP", true, false));
            //output.Add(new HubAllocationModel("XEMC", "S10", false, false));
            //output.Add(new HubAllocationModel("XEMC", "B1", false, false));

            return output;
        }

        private T GetRandomItem<T>(T[] data)
        {
            return data[rnd.Next(0, data.Length)];
        }

        private DateTime GetRandomDate()
        {
            return lowEndDate.AddDays(rnd.Next(daysFromLowDate));
        }

        private int GetAgeInYears(DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (now < birthday.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}