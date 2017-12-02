using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Management;
using System.Management.Instrumentation;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace KP_Inventory_Library
{
    public class GetNetworkAdapter:IGetInformationAlgorithm
    {
        private WqlObjectQuery ObjQuery;
        private ManagementObjectSearcher ObjSearcher;
        public GetNetworkAdapter()
        {
            this.ObjQuery = new WqlObjectQuery();
            this.ObjSearcher = new ManagementObjectSearcher();
        }
        private string getIpaddress(string macAddress)
        {
            //code di ambil dari website stack overflow
            string returnsValue = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.GetPhysicalAddress().ToString() == macAddress.ToString().Replace(":", ""))
                {
                    //returnsValue += item.GetPhysicalAddress().ToString() + "*";        
                    // Read the IP configuration for each network 
                    IPInterfaceProperties properties = item.GetIPProperties();
                    // Each network interface may have multiple IP addresses
                    foreach (IPAddressInformation address in properties.UnicastAddresses)
                    {
                        // We're only interested in IPv4 addresses for now 
                        if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                            continue;
                        // Ignore loopback addresses (e.g., 127.0.0.1) 
                        if (IPAddress.IsLoopback(address.Address))
                            continue;
                        returnsValue += address.Address.ToString() + "*";
                    }
                }
            }
            if (string.IsNullOrEmpty(returnsValue))
            {returnsValue = "";}
            return returnsValue;
        }

        public string getInformation()
        {
            string returnsValue = "";
            try
            {
                this.ObjQuery.QueryString = "SELECT * FROM Win32_NetworkAdapter";
                this.ObjSearcher.Query = this.ObjQuery;
                foreach (ManagementObject obj in this.ObjSearcher.Get())
                {
                    try
                    {
                        //
                        UInt16 tmp = UInt16.Parse(obj.GetPropertyValue("AdapterTypeID").ToString());
                        if (tmp.Equals(0))//ethernet 802.3 (ini adalah local area network) || wireless (card)
                        {
                            if (obj.GetPropertyValue("ProductName").ToString().ToLower().Contains("virtualbox") || obj.GetPropertyValue("PNPDeviceID").ToString().ToLower().Contains("root\\") || obj.GetPropertyValue("Manufacturer").ToString().ToLower().Contains("microsoft"))
                            { continue; }
                            else
                            {
                                string productName = obj.GetPropertyValue("ProductName").ToString();
                                string manufacturer = obj.GetPropertyValue("Manufacturer").ToString();
                                string macAddress= obj.GetPropertyValue("MacAddress").ToString();
                                string IpAddress= this.getIpaddress(macAddress).ToString();
                                if (string.IsNullOrEmpty(productName))
                                    productName = "Nama Product Tidak di temukan";
                                if (string.IsNullOrEmpty(manufacturer))
                                    manufacturer = "Brand tidak di temukan";
                                if (string.IsNullOrEmpty(macAddress))
                                    macAddress = "Mac Address Tidak Di temukan";
                                if (string.IsNullOrEmpty(IpAddress))
                                    IpAddress = "Ip Address Tidak di temukan";
                                returnsValue += ("*"+productName + "*" + manufacturer + "*" + macAddress + "*" + IpAddress+"*");
                                returnsValue += "@";
                            }
                        }
                    }
                    catch (Exception) {
                        returnsValue += "@";
                        continue; 
                    }
                }
                return returnsValue;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
