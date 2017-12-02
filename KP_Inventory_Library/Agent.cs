using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace KP_Inventory_Library
{
    public class Agent
    {
        //fields
        private IGetInformationAlgorithm _agentsJob;
        
        
        
        //constructor
        /*
         * Dengan Adanya 2 Constructor ini memungkinkan pengguna membuat 1 agent untuk 1 task 
         * atau bisa juga membuat 1 agent untuk banyak task
         * 
         */
        public Agent()
        {
            
        }
        public Agent(string informationType)
        {
            this.SelectInformation(informationType);
        }
        
        //methods
        
        public void SelectInformation(string informationType)
        {
            switch (informationType.ToLower())
            {
                case "processor": this._agentsJob = new GetProcessor();
                    break;
                case "computersystem": this._agentsJob = new GetComputerSystem();
                    break;
                case "os": this._agentsJob = new GetOperatingSystem();
                    break;
                case "ram": this._agentsJob = new GetPhysicalMemory();
                    break;
                case "motherboard": this._agentsJob = new GetMotherboard();
                    break;
                case "vga": this._agentsJob = new GetVideoController();
                    break;
                case "hdd": this._agentsJob = new GetHardisk();
                    break;
                case "cdroom": this._agentsJob = new GetCdRoom();
                    break;
                case "sound": this._agentsJob = new GetSoundCard();
                    break;
                case "monitor": this._agentsJob = new GetMonitor();
                    break;
                case "network": this._agentsJob = new GetNetworkAdapter();
                    break;
                case "softwareregistry": this._agentsJob = new GetInstalledSoftwareFromRegistry();
                    break;
                case "softwarewmi": this._agentsJob = new GetInstalledSoftwareFromWMI();
                    break;
                case "ip": this._agentsJob = new GetIpAddress();
                    break;
                default:
                    break;
            }
        }
        public string GoGetInformation()
        {
            return this._agentsJob.getInformation();
        }
      
    }
}
