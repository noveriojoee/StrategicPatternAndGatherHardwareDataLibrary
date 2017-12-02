using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetProcessor:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        public GetProcessor()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
        }
        //suported method
        private string getProcessorArchitecture()
        {
            string ProcArchit="";
            this.ObjQuery.QueryString = "SELECT * FROM win32_Processor";
            this.ObjSearcher.Query = this.ObjQuery;
            foreach (ManagementObject obj in this.ObjSearcher.Get())
            {
                UInt16 tmp = UInt16.Parse(obj.GetPropertyValue("Architecture").ToString());
                switch (tmp)
                {
                    case 0: ProcArchit = "x86";
                        break;
                    case 1: ProcArchit = "MIPS";
                        break;
                    case 2: ProcArchit = "Alpha";
                        break;
                    case 3: ProcArchit = "PowerPC";
                        break;
                    case 5: ProcArchit = "ARM";
                        break;
                    case 6: ProcArchit = "Itanium-based systems";
                        break;
                    case 9: ProcArchit = "x64";
                        break;
                    default:
                        break;
                }
            }
            return ProcArchit;
        }
        //main method
        public string getInformation()
        {
            string returnsValue = "";
            this.ObjQuery.QueryString = "SELECT * FROM win32_Processor";
            this.ObjSearcher.Query = this.ObjQuery;
            foreach (ManagementObject obj in this.ObjSearcher.Get())
            {
                returnsValue += obj.GetPropertyValue("Name") + "*";
                returnsValue += getProcessorArchitecture()+"*";
            }
            return returnsValue;
        }

    }
}
