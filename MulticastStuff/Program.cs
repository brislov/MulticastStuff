
using MulticastStuff;
using System.Net;
using System.Text;

IPAddress multicastAddress = IPAddress.Parse("224.168.100.2");
int multicastPort = 11000;

IPAddress localAddress = IPAddress.Parse("172.28.112.1");
int localPort = multicastPort;

MulticastBroadcaster multicastBroadcaster = new MulticastBroadcaster(multicastAddress, multicastPort, localAddress);
MulticastListner multicastListner = new MulticastListner(multicastAddress, multicastPort, localAddress, localPort);

byte[] buffer = new byte[100];
Thread listnerThread = new Thread(() => multicastListner.ReceiveBroadcastMessages(ref buffer));

listnerThread.Start();

multicastBroadcaster.BroadcastMessage("Hello over there!");

Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, buffer.Length));
