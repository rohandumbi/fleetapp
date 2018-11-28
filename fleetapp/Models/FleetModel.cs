using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class FleetModel : INotifyPropertyChanged
    {
        private int _priority;
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int AssetNumber { get; set; }
        public String AssetType { get; set; }
        public String AssetModel { get; set; }
        public String FleetId { get; set; }
        public int InitialAge { get; set; }
        public int FinalAge { get; set; }
        //public int Priority { get; set; }
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
