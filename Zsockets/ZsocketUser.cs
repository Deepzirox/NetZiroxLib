using System.Net;
using System.Net.Sockets;


namespace NetZiroxLib.Zsockets {


    public class ZsocketUser {

        private int id;
        public int Id {
            get {
                return id;
            }
            set {
                id = value;
            }
        }
        private string? userName;
        public string UserName {
            get {
                if (userName == null)
                    throw new Exception("SocketUser.UserName can't be null in this context");
                return userName;
            }
            set {
                userName = value;
            }
        }
        private IPAddress? ipAddress;
        public string Ip {
            get {
                if (ipAddress == null)
                    throw new Exception("SocketUser.Ip can't be null in this context");
                return ipAddress.ToString();
            }
            set {
                ipAddress = IPAddress.Parse(value.AsSpan());
            }
        }

        

        public int Port;


        public ZsocketUser(int id, string username, string ip, int port) {
            this.Id = id;
            this.UserName = username;
            this.Ip = ip;
            this.Port = port;
        }



    }








}