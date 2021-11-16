using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MulticastStuff
{
    internal class MulticastBroadcaster
    {
        private Socket? _multicastSocket;

        private IPAddress _multicastAddress;
        private int _multicastPort;

        private IPAddress _localAddress;

        internal MulticastBroadcaster(IPAddress multicastAddress, int multicastPort, IPAddress localAddress)
        {
            _multicastAddress = multicastAddress;
            _multicastPort = multicastPort;

            _localAddress = localAddress;

            JoinMulticastGroup();
        }

        private void JoinMulticastGroup()
        {
            _multicastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint localEndPoint = new IPEndPoint(_localAddress, 0);
            _multicastSocket.Bind(localEndPoint);

            MulticastOption multicastOption = new MulticastOption(_multicastAddress, _localAddress);
            _multicastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);
        }

        internal void BroadcastMessage(string message)
        {
            IPEndPoint multicastEndPoint = new IPEndPoint(_multicastAddress, _multicastPort);
            _multicastSocket?.SendTo(Encoding.ASCII.GetBytes(message), multicastEndPoint);
        }

        internal void Close()
        {
            _multicastSocket?.Close();
        }
    }
}
