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


namespace KP_Inventory_Library
{
    public class Connection
    {
        //Connection Event
        public delegate void Operation(Connection c,string message);
        public delegate void listeningOperation(Connection connection);
        public event Operation onConnectionError;
        public event listeningOperation onIncommingCommand;
        //connection class fields
        private TcpListener _listener;
        private IPAddress _SelfAddress;
        private int _UsedPort;
        private Socket _socket;
        private byte[] _recivedBytes;
        private int _mountRecivedData;
        private bool _IsConnectedState;
        private Thread _thread;
        private string _inCommingCommand;
        private string _ipEndPoint;

        //property
        public string InCommingCommand
        {get { return _inCommingCommand; }}
        public IPAddress SelfAddress
        {
            get { return _SelfAddress; }
            set { _SelfAddress = value; }
        }
        public int UsedPort
        {
            get { return _UsedPort; }
            set { _UsedPort = value; }
        }
        public string ServerIP
        {get { return this._ipEndPoint; }}
        
        
        //consturctor
        public Connection()
        {
            this._recivedBytes = new byte[100];
            this._SelfAddress = IPAddress.Parse("127.0.0.1");
            this._UsedPort = 8000;
            this._ipEndPoint = "";
        }
        public Connection(string selfIpAddress,int usedPort)
        {
            this._recivedBytes = new byte[100];
            this._SelfAddress =IPAddress.Parse(selfIpAddress);
            this._UsedPort = usedPort;
        }
        public void startListening()
        {
            this._listener = new TcpListener(IPAddress.Any, this._UsedPort);//IPAddress.Any,
            this._thread = new Thread(this.Listening);
            this._IsConnectedState = true;
            this._listener.Start();
            this._thread.Start();
        }
        public void startListening(string selfAddres, int usedPort)
        {
            this._SelfAddress =IPAddress.Parse(selfAddres);
            this._UsedPort = usedPort;
            this.startListening();
        }

        #region testing Methods StartListeningNewVersion
        public void startListening(int usedPort)
        {
            this._UsedPort = usedPort;
            this.startListening();
        }
        #endregion

        public void Listening()
        {
            try
            {
                while (this._IsConnectedState == true)
                {
                    this._inCommingCommand = "";
                    this._socket = this._listener.AcceptSocket();
                    this._mountRecivedData = this._socket.Receive(this._recivedBytes);
                    foreach (byte bite in this._recivedBytes)
                        this._inCommingCommand += Convert.ToChar(bite).ToString();
                    //biarkan command yang di terima class connection di olah di main program dengan menggunakan even OnIncommingCommand
                    //inComming command merupakan variable tipe data string yang akan di ambil di main program karena memiliki property bersifat public
                    this._ipEndPoint = ((IPEndPoint)this._socket.LocalEndPoint).Address.ToString();
                    this.onIncommingCommand(this);
                }
            }
            catch (Exception) 
            {  }
        }
        public void StopListening()
        {
            this._IsConnectedState = false;
            try
            {
                this._socket.Close();
            }
            catch (NullReferenceException) {/*Ignore Null Socket Object*/ }
            catch (Exception E)
            {onConnectionError(this, E.Message);}
            finally
            {
                this._listener.Stop();
                this._thread.Abort();
            }
        }
     
    }
}
