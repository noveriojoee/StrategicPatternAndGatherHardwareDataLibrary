using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetHardisk:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        public GetHardisk()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
        }
        //suported method
        private string getEachPartitionData()
        {
            string returnsValue = "";
            #region buat Detect Partisi
            this.ObjQuery.QueryString = "SELECT * FROM Win32_LogicalDisk";
            this.ObjSearcher.Query = this.ObjQuery;
            foreach (ManagementObject obj in this.ObjSearcher.Get())
            {
                UInt64 t = UInt64.Parse(obj.GetPropertyValue("Size").ToString()) / 1073741824;
                returnsValue += t.ToString();//klo partisi nya ada 2 maka akan di ulang 2x tpi klo cuma 1 maka tidak akan di ulang
                break;
            }
            #endregion
            return returnsValue;
        }
        //main method
        public string getInformation()
        {
            string returnsValue = "";
            try
            {
                this.ObjQuery.QueryString = "SELECT * FROM Win32_DiskDrive";
                this.ObjSearcher.Query = this.ObjQuery;
                foreach (ManagementObject obj in this.ObjSearcher.Get())
                {
                    UInt64 t = UInt64.Parse(obj.GetPropertyValue("Size").ToString()) / 1073741824;
                    returnsValue += obj.GetPropertyValue("Manufacturer").ToString() +" "+obj.GetPropertyValue("Caption").ToString() + "*";
                    returnsValue += t.ToString() + "*";
                    break;
                }
                /*--------------------------------------------------------------*/
                return returnsValue;
            }
            catch (NullReferenceException)
            { return null; }
            catch (Exception)
            { return null; }
        }
    }
}
