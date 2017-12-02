using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class GetVideoController : IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        public GetVideoController()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
        }
        private string getVideoControllerType(int type)
        {
            //returning video card architectur which is vga or ega or cga or etc 
            //type = int.Parse(obj["VideoArchitecture"].ToString())
            string returnsValue = "";
            switch (type)
            {
                case 1: returnsValue = "Other";
                    break;
                case 2: returnsValue = "Unknown";
                    break;
                case 3: returnsValue = "CGA";
                    break;
                case 4: returnsValue = "EGA";
                    break;
                case 5: returnsValue = "VGA";
                    break;
                case 6: returnsValue = "SVGA";
                    break;
                case 7: returnsValue = "MDA";
                    break;
                case 8: returnsValue = "HGC";
                    break;
                case 9: returnsValue = "MCGA";
                    break;
                case 10: returnsValue = "8514A";
                    break;
                case 11: returnsValue = "XGA";
                    break;
                case 12: returnsValue = "Liniear Frame Buffer";
                    break;
                case 160: returnsValue = "PC-98";
                    break;
                default: returnsValue = "Unknown Type";
                    break;
            }
            return returnsValue;
        }
        public string getInformation()
        {
            string returnsValue = "";

            this.ObjQuery.QueryString = "SELECT * FROM Win32_VideoController";
            this.ObjSearcher.Query = this.ObjQuery;
            foreach (ManagementObject obj in this.ObjSearcher.Get())
            {
                try
                {
                    UInt64 s = (UInt64.Parse(obj["AdapterRAM"].ToString()) / 1048576); // ini satuan nya byte di bikin mb byte maka di bagi 1024*1024                 
                    if (string.IsNullOrEmpty(obj.GetPropertyValue("Caption").ToString()))
                        returnsValue += "Onboard*";
                    else
                        returnsValue += obj.GetPropertyValue("Caption").ToString() + "*"; //returning video card caption          
                    returnsValue += s.ToString() + "Mb*";//returning video card memory                                                   
                }
                catch (NullReferenceException)
                {
                    continue;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return returnsValue;

        }
    }
}
