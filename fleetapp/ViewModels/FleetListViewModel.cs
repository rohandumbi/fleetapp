﻿using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fleetapp.ViewModels
{
    public class FleetListViewModel : Screen
    {
        private FleetDataAccess _fleetDataAccess;
        private String _fleetFileName;
        public BindableCollection<FleetModel> Fleets { get; set; }

        public FleetListViewModel()
        {
            _fleetDataAccess = new FleetDataAccess();
            LoadFleetList();
        }

        private void LoadFleetList()
        {
            Fleets = new BindableCollection<FleetModel>(_fleetDataAccess.GetFleets());

        }

        public String FleetFile
        {
            set { _fleetFileName = value; }
        }

        public void ImportFile()
        {
            if(String.IsNullOrEmpty(_fleetFileName))
            {
                Console.WriteLine("Please select a file");
                MessageBox.Show("Please select a file!");
                return;
            }
            IEnumerable<FleetModel> Fleets = ReadCSV(_fleetFileName);
            _fleetDataAccess.DeleteAll();
            _fleetDataAccess.InsertFleets(Fleets);
            LoadFleetList();
            NotifyOfPropertyChange("Fleets");
        }

        private IEnumerable<FleetModel> ReadCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));
            lines = lines.Skip(1).ToArray();
            var _assetNumber = 0;
            return lines.Select(line =>
            {
                
                string[] data = line.Split(',');
                _assetNumber += 1;
                return new FleetModel
                {
                    ProjectId = Context.ProjectId,
                    AssetNumber = _assetNumber,
                    AssetType = data[0],
                    AssetModel = data[1],
                    FleetId = data[2],
                    InitialAge = Int32.Parse(data[3]),
                    FinalAge = Int32.Parse(data[4]),
                    Priority = 1
                };
            });
        }
    }
}
