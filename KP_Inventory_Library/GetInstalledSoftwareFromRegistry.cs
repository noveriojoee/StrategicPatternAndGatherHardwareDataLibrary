using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;
using Microsoft.Win32;


namespace KP_Inventory_Library
{
    public class GetInstalledSoftwareFromRegistry:IGetInformationAlgorithm
    {
        public string getInformation()
        {
            string returnsValue = "";
            string softwareKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

            RegistryKey rk = Registry.LocalMachine.OpenSubKey(softwareKey);
            foreach (string skName in rk.GetSubKeyNames())
            {
                RegistryKey sk = rk.OpenSubKey(skName);
                if (sk.GetValue("DisplayName") != null)
                {
                    if (sk.GetValue("InstallLocation") == null)
                        returnsValue += sk.GetValue("DisplayName") + "*";//+ " -> unknownpath" + "*"; //" - Install path not known\n";
                    else
                        returnsValue += sk.GetValue("DisplayName") + "*";//" - " + sk.GetValue("InstallLocation") + "*";
                }
            }
            return returnsValue;
        }
    }
}
