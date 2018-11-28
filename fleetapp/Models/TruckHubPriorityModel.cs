using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class TruckHubPriorityModel : INotifyPropertyChanged
    {
        private int _priority;
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public String AssetModel { get; set; }
        public String Hub { get; set; }
        public int Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged("Priority");
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
