using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace NetZiroxLib.Zsockets {
    public class ZsocketServer
    {
        private CancellationTokenSource? Token;
        private ConcurrentBag<TcpClient>? _clients { get; set; }
        private TcpListener? _listener;

        private int ServerPort { get; set; }
        private string ServerIp { get; set; }
        public string ServerBuffer {get; set;}

        public ZsocketServer(string connection_string)
        {
            List<string> conn_args = connection_string.Split(':').ToList();
            this.ServerIp = conn_args[0];
            this.ServerPort = int.Parse(conn_args[1]);
            this._clients = new();
            this.ServerBuffer = "";

        }

        public void CreateTcpListener()
        {
            try
            {
                Console.WriteLine(this.ServerIp);
                // parsing my own ipAdress using type safe memory checking
                ReadOnlySpan<char> ipBytes = this.ServerIp.AsSpan();
                IPAddress ipAddress = IPAddress.Parse(ipBytes);
                // creating listener
                this._listener = new TcpListener(ipAddress, this.ServerPort);
                _listener.Start();
                
                Console.WriteLine("Server is listening");
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e.ToString());
            }
        }

        public void BroadCast(string message) {
            if (this._clients != null)
                foreach (var client in this._clients)
                {
                    byte[] msg = Encoding.ASCII.GetBytes(message);
                    client.GetStream().Write(msg, 0, msg.Length);
                }
        }

         public void CheckNewData()
        {
            string res = "";

            if (this._clients != null)
                foreach (var client in this._clients)
                {
                    if (client.Available > 0)
                    {
                        byte[] msg = new byte[1024];

                        client.GetStream().Read(msg, 0, msg.Length);
                        res = Encoding.ASCII.GetString(msg);

                        if (res != "Unhandled exception")
                            this.ServerBuffer += res;
                    }
                }
        }


        public void CloseAllConnections() {
            if (this._clients != null)
                foreach (var client in this._clients)
                    client.Close();
        }

        public void CloseListener()
        {
            if (this._listener != null)
            {
                this._listener.Stop();
            }
        }

        public void CancelToken() {
            if (this.Token != null)
                this.Token.Cancel();
        }

        public void CleanUp()
        {
            this.CancelToken();
            this.CloseListener();
            this.CloseAllConnections();
        }

        public async void WaitClients()
        {

            if (this._listener == null)
                throw new Exception("No listener server started");

            if (this._clients == null)
                throw new Exception("this._clients cannot be NULL");

            int num = 0;
            this.Token = new CancellationTokenSource();
            await Task.Run(() => {
                while(!this.Token.IsCancellationRequested)
                {    
                    TcpClient client = this._listener.AcceptTcpClient();
                    this._clients.Add(client);
                    
                    var ipAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    var port = ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();
                    Console.WriteLine($"Nuevo cliente conectado -> {ipAddress}:{port}");
                    num++;
                }
            }, this.Token.Token);
            
        }
    }
}