using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetMonitor:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        public GetMonitor()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();

        }
        public string getInformation()
        {
            string returnsValue = "";
            this.ObjQuery.QueryString = "SELECT * FROM Win32_DesktopMonitor";
            this.ObjSearcher.Query = this.ObjQuery;
            foreach (ManagementObject obj in this.ObjSearcher.Get())
            {
                returnsValue += obj.GetPropertyValue("Description") + "*";
                returnsValue += obj.GetPropertyValue("MonitorType") + "*";
                returnsValue += obj.GetPropertyValue("MonitorManufacturer") + "*";
            }
            return returnsValue;
        }
    }
}
