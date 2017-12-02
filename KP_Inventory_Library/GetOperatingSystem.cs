using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetOperatingSystem:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        
        public GetOperatingSystem()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
        }
        //main method
        public string getInformation()
        {
            string returnsValue = "";
            try
            {
                this.ObjQuery.QueryString = "SELECT * FROM win32_OperatingSystem";
                this.ObjSearcher.Query = this.ObjQuery;
                foreach (ManagementObject obj in this.ObjSearcher.Get())
                {
                    returnsValue += obj.GetPropertyValue("Caption").ToString() + obj.GetPropertyValue("Version") + "*";
                    returnsValue += obj.GetPropertyValue("SerialNumber").ToString() + "*";  
                    if (!returnsValue.ToLower().Contains("xp"))
                        returnsValue += obj.GetPropertyValue("OSArchitecture").ToString() + "*";// not available in windows XP
                    else
                        returnsValue += " *";
                    
                    
                }
                return returnsValue;
            }
            catch (NullReferenceException)
            {
                return returnsValue + "null*";
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
