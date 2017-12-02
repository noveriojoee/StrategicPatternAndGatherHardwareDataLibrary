using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetComputerSystem:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        
        public GetComputerSystem()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
            
        }
        private string getMachineSerialNumber()
        {
            string returnsValue="";
            this.ObjQuery.QueryString = "SELECT * FROM Win32_Bios";
            this.ObjSearcher.Query = this.ObjQuery;
            foreach (ManagementObject obj in this.ObjSearcher.Get())
            {
                returnsValue += obj.GetPropertyValue("SerialNumber") + "*";
            }
            return returnsValue;
        }
        public string getInformation()
        {
            string returnsValue = "";
            this.ObjQuery.QueryString = "SELECT * FROM Win32_ComputerSystem";
            this.ObjSearcher.Query = this.ObjQuery;
            foreach (ManagementObject obj in this.ObjSearcher.Get())
            { 
                returnsValue += obj.GetPropertyValue("Name") + "*";
                returnsValue += obj.GetPropertyValue("Manufacturer") + "*";
                returnsValue+= obj.GetPropertyValue("Model") + "*"; 
            }
            return returnsValue + getMachineSerialNumber();
        }

    }
}
