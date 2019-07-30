using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace P2PChatClient
{
    public class Messenger
    {
        public static void chatHandler(Socket senderSocket, string friendAlias, string userAlias)
        {
            Console.Write("Write message ");

            while (true)
            {

                // Send a message to Client using Send() method 
                
                Console.WriteLine("{0}:", userAlias);
                string replyMessage = Console.ReadLine();
                Console.WriteLine("------------------------------------------------------");

                if (replyMessage.ToLowerInvariant() == "quit")
                {
                    Console.WriteLine("Bye!");
                    CloseServer(senderSocket);
                    break;
                }

                byte[] message = Encoding.ASCII.GetBytes(replyMessage);
                senderSocket.Send(message);

                // Data buffer 
                byte[] bytes = new Byte[1024];
                string receivedMessage = null;

                int numByte = senderSocket.Receive(bytes);
                receivedMessage = Encoding.ASCII.GetString(bytes, 0, numByte);

                Console.WriteLine("{0}: {1} ", friendAlias, receivedMessage);
                Console.WriteLine("------------------------------------------------------");


                if (receivedMessage.ToLowerInvariant() == "quit")
                {
                    Console.WriteLine("Bye!");
                    CloseServer(senderSocket);
                    break;
                }
            }
        }

        private static void CloseServer(Socket senderSocket)
        {
            senderSocket.Shutdown(SocketShutdown.Both);
            senderSocket.Close();
        }
    }
}
