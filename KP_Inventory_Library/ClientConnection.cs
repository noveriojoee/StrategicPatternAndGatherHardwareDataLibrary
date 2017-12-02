using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace KP_Inventory_Library
{
    public class ClientConnection
    {
        private TcpClient _clientConnection;
        private Stream _stm;
        private ASCIIEncoding _commandConverter;
        public ClientConnection()
        {
            this._clientConnection = new TcpClient();
            this._commandConverter = new ASCIIEncoding();


        }
        public void Invoke(string command,string destinationIpAddress,string PortDestination)
        {
            this._clientConnection = new TcpClient();
            this._clientConnection.Connect(destinationIpAddress, int.Parse(PortDestination));
            this._stm = this._clientConnection.GetStream();
            byte[] sendByte = this._commandConverter.GetBytes(command);//merubah pesan text menjadi byte agar siap di kirim
            this._stm.Write(sendByte, 0, command.Length);//mengirim kan data
        }
    }
}
