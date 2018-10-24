using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class HubDefinitionModel
    {
        private String _hubNumber;
        private String _hubName;

        public HubDefinitionModel(String hubNumber, String hubName)
        {
            this._hubNumber = hubNumber;
            this._hubName = hubName;
        }

        public String HubNumber
        {
            get { return _hubNumber; }
            set { _hubNumber = value; }
        }

        public String HubName
        {
            get { return _hubName; }
            set { _hubName = value; }
        }
    }
}
