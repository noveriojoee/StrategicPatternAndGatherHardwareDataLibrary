using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetInstalledSoftwareFromWMI:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        public GetInstalledSoftwareFromWMI()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
        }
        public string getInformation()
        {
            string returnsValue = "";
            try
            {
                this.ObjQuery.QueryString = "SELECT * FROM Win32_Product";
                this.ObjSearcher.Query = this.ObjQuery;
                foreach (ManagementObject obj in this.ObjSearcher.Get())
                {
                    returnsValue += obj.GetPropertyValue("Caption").ToString() + "*";//+ " -> " + obj.GetPropertyValue("InstallLocation").ToString() + "*"; //+ " -- " + obj.GetPropertyValue("InstallLocation") +"---"+obj.GetPropertyValue("Version")+"\n";
                }
            }
            catch (NullReferenceException)
            {
                returnsValue = "Data Cannot Be Retrieve";
            }
            catch (Exception)
            {
                returnsValue = "null";
            }            
            return returnsValue;
        }
    }
}
