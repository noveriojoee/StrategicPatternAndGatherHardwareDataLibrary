using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KP_Inventory_Library
{
    public class NetworkDataAdapter
    {
        private string _interFaceName;
        private string _interFaceBrand;
        private string _IpAddress;
        private string _macAddress;
        public NetworkDataAdapter(string interfaceName,string interfaceBrand,string ipAddress,string macAddress)
        {
            this._interFaceName = interfaceName;
            this._interFaceBrand = interfaceBrand;
            this._IpAddress = ipAddress;
            this._macAddress = macAddress;
        }
    }
}
