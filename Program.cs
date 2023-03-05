using NetZiroxLib.Zsockets;

ZsocketServer server = new("127.0.0.1:8090");
server.CreateTcpListener();
server.WaitClients();

int seconds_to_shutdown = 30;
int seconds_passed = 0;

while(seconds_passed < seconds_to_shutdown) {
    
    Task.Delay(1000).Wait();
    server.CheckNewData();
    seconds_passed++;

}

// hilo principal


foreach (var item in server.ServerBuffer.Split("-"))
{
    Console.WriteLine(item);
}

if (seconds_passed == seconds_to_shutdown) {
    server.BroadCast("Thanks for visiting the server!");
    server.CleanUp();
    Console.WriteLine("Cleaning up resources");
}
    
