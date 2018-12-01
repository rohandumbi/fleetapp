using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class TruckHourModel : INotifyPropertyChanged
    {
        private List<TruckHourYearMappingModel> _truckHourYearMapping;
        public int Id { get; set; }
        public int ScenarioId { get; set; }
        public String AssetModel { get; set; }
        public String GroupName { get; set; }
        public int HubId { get; set; }
        public String HubName { get; set; }
        public String Mode { get; set; }

        public List<TruckHourYearMappingModel> TruckHourYearMapping
        {
            get { return _truckHourYearMapping; }
            set
            {
                _truckHourYearMapping = value;
                OnPropertyChanged("TruckHourYearMapping");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class TruckHourYearMappingModel : INotifyPropertyChanged
    {
        private Decimal _value;
        public int TruckHourId { get; set; }
        public int Year { get; set; }
        public Decimal Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
