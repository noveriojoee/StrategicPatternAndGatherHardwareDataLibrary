using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetPhysicalMemory:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        public GetPhysicalMemory()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
        }
        private string getMemoryType(UInt16 tmp)
        {
            string returnsValue = "";
            switch (tmp)
            {
                case 0: returnsValue="Unknown";
                    break;
                case 1: returnsValue = "Other";
                    break;
                case 2: returnsValue = "DRAM";
                    break;
                case 3: returnsValue = "Synchronous DRAM";
                    break;
                case 4: returnsValue = "Cache DRAM";
                    break;
                case 5: returnsValue = "EDO";
                    break;
                case 6: returnsValue = "EDRAM";
                    break;
                case 7: returnsValue = "VRAM";
                    break;
                case 8: returnsValue = "SRAM";
                    break;
                case 9: returnsValue = "RAM";
                    break;
                case 10: returnsValue = "ROM";
                    break;
                case 11: returnsValue = "Flash";
                    break;
                case 12: returnsValue = "EEPROM";
                    break;
                case 13: returnsValue = "CDRAM";
                    break;
                case 14: returnsValue = "EPROM";
                    break;
                case 15: returnsValue = "CDRAM";
                    break;
                case 16: returnsValue = "3dRAM";
                    break;
                case 17: returnsValue = "SDRAM";
                    break;
                case 18: returnsValue = "SGRAM";
                    break;
                case 19: returnsValue = "RDRAM";
                    break;
                case 20: returnsValue = "DDR";
                    break;
                case 21: returnsValue = "DDR-2";
                    break;
                default: returnsValue = "-";
                    break;
            }
            return returnsValue;
        }
        public string getInformation()
        {
            string returnsValue = "";
            try
            {
                this.ObjQuery.QueryString = "SELECT * FROM Win32_PhysicalMemory";
                this.ObjSearcher.Query = this.ObjQuery;
                foreach (ManagementObject obj in this.ObjSearcher.Get())
                {
                    try
                    {
                        UInt64 t = (UInt64.Parse(obj.GetPropertyValue("Capacity").ToString())) / 1048576;
                        returnsValue += obj.GetPropertyValue("Manufacturer").ToString() + "*" + t.ToString() + " MB*";
                    }
                    catch (Exception)
                    { continue; }
                }
                return returnsValue;
            }
            catch (NullReferenceException)
            {
                return returnsValue + "*";
            }
            catch (Exception)
            {
                return returnsValue + "*"; ;
            }
        }
    }
}
