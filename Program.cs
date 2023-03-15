using NetZiroxLib.Zsockets;

ZsocketServer server = new("127.0.0.1:8090");
server.CreateTcpListener();
server.WaitClients();

int seconds_to_shutdown = 100;
int seconds_passed = 0;

while(seconds_passed < seconds_to_shutdown) {
    
    Task.Delay(1000).Wait();
    server.BroadcastNewData();

    seconds_passed++;

}


if (seconds_passed == seconds_to_shutdown) {
    Console.WriteLine(server.ServerBuffer);
    server.CleanUp();
    Console.WriteLine("Cleaning up resources");
}
    
