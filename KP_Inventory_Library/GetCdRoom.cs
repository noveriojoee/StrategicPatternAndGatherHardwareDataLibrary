using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetCdRoom:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        public GetCdRoom()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
        }
        public string getInformation()
        {
            string returnsValue = "";
            try
            {
                this.ObjQuery.QueryString = "SELECT * FROM Win32_CDROMDrive";
                this.ObjSearcher.Query = this.ObjQuery;
                foreach (ManagementObject obj in this.ObjSearcher.Get())
                {
                    returnsValue += obj.GetPropertyValue("MediaType").ToString() + "*";
                    returnsValue += obj.GetPropertyValue("Caption").ToString() + "*";
                }
                return returnsValue;
            }
            catch (NullReferenceException)
            {
                return returnsValue+"null*";
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
