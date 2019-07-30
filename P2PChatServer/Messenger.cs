using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace P2PChatServer
{
    public class Messenger
    {
        public static void chatHandler(Socket clientSocket, string friendAlias, string userAlias)
        {
            while (true)
            {

                // Data buffer 
                byte[] bytes = new Byte[1024];
                string data = null;

                int numByte = clientSocket.Receive(bytes);
                data = Encoding.ASCII.GetString(bytes, 0, numByte);

                Console.WriteLine("{0}: {1} ", friendAlias, data);
                Console.WriteLine("------------------------------------------------------");

                //Console.Write("Write reply ");
                Console.WriteLine("{0}:", userAlias);
                string replyMessage = Console.ReadLine();
                Console.WriteLine("------------------------------------------------------");

                // Send a message to Client using Send() method 

                byte[] message = Encoding.ASCII.GetBytes(replyMessage);
                clientSocket.Send(message);

                if (replyMessage.ToLowerInvariant() == "quit")
                {
                    Console.WriteLine("Bye!");
                    CloseServer(clientSocket);
                    break;
                }
            }
        }

        private static void CloseServer(Socket clientSocket)
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}
