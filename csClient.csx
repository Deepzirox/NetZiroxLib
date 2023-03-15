#r "/home/zirox/Desktop/projects/NetZiroxLib/bin/Debug/net7.0/NetZiroxLib.dll"
using NetZiroxLib.Zsockets;

var newUser = new ZsocketClient(0, "Deepzirox", "127.0.0.1", 8090);
newUser.Connect();

var closed = false;

while(!closed) {
    Console.Write("C# ===> ");
    string text = Console.ReadLine();
    if (text == "exit") {
        closed = true;
        continue;
    } else if (text == "show") {
      Console.WriteLine(newUser.SocketBuffer);
      continue;
    }
    
    newUser.WriteData(text);
    newUser.ReadData();
    
}

newUser.CleanUp()
