using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetSoundCard:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        public GetSoundCard()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
        }
        public string getInformation()
        {
            string returnsValue = "";
            try
            {
                this.ObjQuery.QueryString = "SELECT * FROM Win32_SoundDevice";
                this.ObjSearcher.Query = this.ObjQuery;
                foreach (ManagementObject obj in this.ObjSearcher.Get())
                {
                    try
                    {
                        returnsValue += obj.GetPropertyValue("ProductName").ToString() + "*";
                        //returnsValue += obj.GetPropertyValue("Manufacturer").ToString() + "*";
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                /*--------------------------------------------------------------*/
                return returnsValue;
            }
            catch (NullReferenceException)
            { return returnsValue+"null*"; }
            catch (Exception)
            { return null; }
        }
    }
}
