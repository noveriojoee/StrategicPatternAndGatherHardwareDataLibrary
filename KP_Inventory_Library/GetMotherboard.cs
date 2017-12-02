using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetMotherboard : IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;

        public GetMotherboard ()
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
                this.ObjQuery.QueryString = "SELECT * FROM Win32_BaseBoard";
                this.ObjSearcher.Query = this.ObjQuery;
                foreach (ManagementObject obj in this.ObjSearcher.Get())
                {
                    returnsValue += obj.GetPropertyValue("Manufacturer") + " "+obj.GetPropertyValue("serialNumber")+"*";
                }
                //// Executing the query...
                //// Because the machine has a single Motherborad,
                //// then a single object (row) returned.
                //foreach (ManagementObject obj in objs)
                //{
                //    // Retrieving the properties (columns)
                //    // Writing column name then its value
                //    foreach (PropertyData data in obj.Properties)
                //    {
                //        returnsValue += data.Name + " "+data.Value+"*";                        
                //    }
                //}
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
