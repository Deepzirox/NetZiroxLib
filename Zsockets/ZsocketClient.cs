using System.Text;
using System.Net;
using System.Net.Sockets;



namespace NetZiroxLib.Zsockets
{

    public class ZsocketClient : ZsocketUser {

        private TcpClient? ownedSocket;
        public string SocketBuffer {get; set;} 
        public TcpClient Socket {
            get {
                if (ownedSocket == null)
                    throw new Exception("SocketUser.Socket can't be null in this context, please Connect()");
                return ownedSocket;
            }
            set {
                if (value.Connected)
                    ownedSocket = value;
            }
        }

        public ZsocketClient(int id, string username, string ip, int port) : base(id, username, ip, port) {
            this.Id = id;
            this.UserName = username;
            this.Ip = ip;
            this.Port = port;
            this.SocketBuffer = "";
            // socket is null till connected
            this.ownedSocket = null;
        }

        public bool Connect() {
            
            bool connected = false;

            try {
                TcpClient newClient = new(this.Ip, this.Port);
                if (newClient.Connected) {
                    this.Socket = newClient;
                    connected = true;
                }
                    
            } catch(Exception e) {
                Console.WriteLine($"Cannot be connected to {this.Ip}: {e.ToString()}");
                return !!connected;
            }

            return connected;
            
        }

        public async void WriteData(string data) {
            var socketStream = this.Socket.GetStream();
            var dataString = Encoding.UTF8.GetBytes(data);
            await socketStream.WriteAsync(dataString, 0, data.Length);
        }

        public void ReadData(){
            var socketClient = this.Socket.Client;
            byte[] buffer = new byte[1024];

            try {

                var bytes = socketClient.Receive(buffer);
                if (bytes > 0)
                  this.SocketBuffer = Encoding.UTF8.GetString(buffer);
                
              
            } catch(Exception e) {
                Console.Error.WriteLine($"Error reading data; maybe connection was closed: {e.ToString()}");
            }

        }


        public void CleanUp() {
            this.Socket.Close();
        }
        
    }

    
}
