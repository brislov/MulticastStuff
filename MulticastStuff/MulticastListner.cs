using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MulticastStuff
{
    internal class MulticastListner
    {
        private Socket? multicastSocket;

        private IPAddress multicastAddress;
        private int multicastPort;

        private IPAddress localAddress;
        private int localPort;

        internal MulticastListner(IPAddress multicastAddress, int multicastPort, IPAddress localAddress, int localPort)
        {
            this.multicastAddress = multicastAddress;
            this.multicastPort = multicastPort;

            this.localAddress = localAddress;
            this.localPort = localPort;

            JoinMulticastGroup();
        }

        private void JoinMulticastGroup()
        {
            multicastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint localEndPoint = new IPEndPoint(localAddress, localPort);
            multicastSocket.Bind(localEndPoint);

            MulticastOption multicastOption = new MulticastOption(multicastAddress, localAddress);
            multicastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);
        }

        internal void ReceiveBroadcastMessages(ref byte[] buffer)
        {
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            multicastSocket?.ReceiveFrom(buffer, ref remoteEndPoint);

            multicastSocket?.Close();
        }
    }
}
