# StrategicPatternAndGatherHardwareDataLibrary
here i want to share with you one of my work, i creating a library to gather computer hardware data using Windows Management Instrument (WMI) and implementing Strategic Pattern Code Design to make library look like an agent. Compiled it with .NET 3.5 i will result as a library


#-- Prerequisites --#
- Visual studio 2010 or later
- dotnet 3.5 or latest


#-- Hot Tow Use --# 
        //main methods
        private void getHardwareInformation()
        {
            Agent specialAgent = new Agent();
            string[] tmpInformation;
            int rowBoundaries;
            int mountData;

            #region getMotherBoardInformation
            specialAgent.SelectInformation("motherboard");
            tmpInformation = specialAgent.GoGetInformation().Split('*');
            this.MotherBoardTypeTB.Text = tmpInformation[0];
            #endregion

            #region getProcessor Information
            specialAgent.SelectInformation("Processor");
            tmpInformation = specialAgent.GoGetInformation().Split('*');
            this.ProcessorInformationTB.Text = tmpInformation[0] + " " + tmpInformation[1];
            #endregion

            #region getOperatingSystem Information
            this.specialAgent.SelectInformation("os");
            tmpInformation = this.specialAgent.GoGetInformation().Split('*');
            this.OperatingSystemInformationTB.Text = tmpInformation[0] + " " + tmpInformation[2];
            this.OperatingSystemSerialNumberTB.Text = tmpInformation[1];
            #endregion

            #region getHardiskInformation
            this.specialAgent.SelectInformation("hdd");
            tmpInformation = this.specialAgent.GoGetInformation().Split('*');
            this.HardiskInformationTB.Text = tmpInformation[0];
            this.HardiskSizeInformationTB.Text = tmpInformation[1] + " GB\n";
            #endregion

            #region getNetworkInterfaces
            this.specialAgent.SelectInformation("network");
            tmpInformation = this.specialAgent.GoGetInformation().Split('@');
            string[] info;
            for (int row = 0; row < tmpInformation.Count(); row++)
            {
                info = tmpInformation[row].ToString().Split('*');
                if (info.Count() > 2)
                {
                    this.NetworkGridView.Rows.Add(info[1], info[2], info[3], info[4]);
                }
            }
            #endregion

            #region getPhysicalMemoryInformation
            this.specialAgent.SelectInformation("ram");
            tmpInformation = this.specialAgent.GoGetInformation().Split('*');
            //input to pyscialMemoryGridView
            mountData = 2;
            rowBoundaries = (tmpInformation.Length / mountData);
            for (int row = 0; row < rowBoundaries; row++)
            {
                if (mountData * row % mountData == 0)
                {
                    this.physicalMemoryGrdView.Rows.Add((tmpInformation[(mountData * row)]), (tmpInformation[(mountData * row) + 1]));
                }
            }
            //end
            #endregion

            #region getSoundCardInformation
            this.specialAgent.SelectInformation("sound");
            tmpInformation = this.specialAgent.GoGetInformation().Split('*');
            foreach (string item in tmpInformation)
            {
                this.SoundcardInformationTB.Text += item + " ";
            }
            #endregion

            #region getVgaCardInformation
            this.specialAgent.SelectInformation("vga");
            tmpInformation = this.specialAgent.GoGetInformation().Split('*');
            foreach (string item in tmpInformation)
            {
                this.VGA_InformationTB.Text += item + " ";
            }
            #endregion

            #region getDVDRoomInformation
            this.specialAgent.SelectInformation("cdroom");
            tmpInformation = this.specialAgent.GoGetInformation().Split('*');
            foreach (string item in tmpInformation)
            {
                this.DVDRomTB.Text += item + " ";
            }
            #endregion

            #region getComputerSystem Information
            try
            {
                specialAgent.SelectInformation("ComputerSystem");
                tmpInformation = specialAgent.GoGetInformation().Split('*');
                ComputerName_TB.Text = tmpInformation[0];
                ComputerSerialTB.Text = tmpInformation[3];
                ComputerBrandTB.Text = tmpInformation[1];
                ComputerModelTB.Text += tmpInformation[2];

            }
            catch (Exception)
            {
              throws new Exception();
            }
            #endregion

        }
    
}

