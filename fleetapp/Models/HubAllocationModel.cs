using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class HubAllocationModel : INotifyPropertyChanged
    {
        private Boolean _isManned;
        private Boolean _isAHS;
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int HubId { get; set; }
        public String AssetModel { get; set; }
        public String HubName { get; set; }

        public Boolean IsManned
        {
            get { return _isManned; }
            set
            {
                _isManned = value;
                OnPropertyChanged("IsManned");
            }
        }

        public Boolean IsAHS
        {
            get { return _isAHS; }
            set
            {
                _isAHS = value;
                OnPropertyChanged("IsAHS");
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
