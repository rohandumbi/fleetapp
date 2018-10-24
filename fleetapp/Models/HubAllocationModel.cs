using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class HubAllocationModel
    {
        private String _assetModel;
        private String _hubName;
        private bool _isManned;
        private bool _isAHS;

        public HubAllocationModel(String assetModel, String hubName, bool isManned, bool isAHS)
        {
            this._assetModel = assetModel;
            this._hubName = hubName;
            this._isManned = isManned;
            this._isAHS = isAHS;
        }

        public String AssetModel
        {
            get { return _assetModel; }
            set { _assetModel = value; }
        }

        public String HubName
        {
            get { return _hubName; }
            set { _hubName = value; }
        }

        public bool IsAHS
        {
            get { return _isAHS; }
            set { _isAHS = value; }
        }

        public bool IsManned
        {
            get { return _isManned; }
            set { _isManned = value; }
        }
    }
}
