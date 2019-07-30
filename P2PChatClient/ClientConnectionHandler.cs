using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace P2PChatClient
{
    public class ClientConnectionHandler
    {

        private static IPAddress ipAddr;
        public static Socket GetSocket()
        {

            // Get host ip
            ipAddr = IPAddress.Parse(GetIPv4Address());

            // Creation TCP/IP Socket using Socket Class Costructor 
            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            return listener;
        }

        public static Socket ConnectToServer(Socket sender)
        {
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            sender.Connect(localEndPoint);
            Console.WriteLine("Socket connected to -> {0} ",
                                  sender.RemoteEndPoint.ToString());

            return sender;

        }

        public static string GetFriendAlias(Socket senderSocket)
        {

            // Receive friends alias
            byte[] encodedAlias = new Byte[1024];

            string friendAlias = Encoding.ASCII.GetString(encodedAlias, 0, senderSocket.Receive(encodedAlias));

            Console.WriteLine($"Connected to {0}!", senderSocket.RemoteEndPoint.ToString());

            Console.WriteLine("Friend Alias: {0}", friendAlias);
            Console.WriteLine("");

            return friendAlias;

        }

        public static string SendUserAlias(Socket senderSocket)
        {

            //Send my alias

            Console.WriteLine("Enter your alias:");
            string userAlias = Console.ReadLine();
            byte[] encodedAlias = new Byte[1024];

            encodedAlias = Encoding.ASCII.GetBytes(userAlias);

            senderSocket.Send(encodedAlias);

            return userAlias;

        }

        private static string GetIPv4Address()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                { 
                    return ip.ToString();
                }
            }
            return string.Empty;
        }
    }
}
