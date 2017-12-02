using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetIpAddress:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        public GetIpAddress()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
        }
        public string getInformation()
        {
            string returnsValue="";
            this.ObjQuery.QueryString = "SELECT * FROM Win32_NetworkAdapterConfiguration";
            this.ObjSearcher.Query = this.ObjQuery;
            foreach (ManagementObject obj in this.ObjSearcher.Get())
            {
                try
                {
                    foreach (string str in (string[])obj["IPAddress"])
                    {
                        returnsValue += str;
                    }
                    returnsValue += "*";
                }
                catch (NullReferenceException)
                {
                }
                catch (Exception)
                {
                }
            }
            return returnsValue;
        }
    }
}
