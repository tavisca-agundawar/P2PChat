using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace P2PChatServer
{
    class Server
    {
        static void Main(string[] args)
        {
            ExecuteServer();
        }

        public static void ExecuteServer()
        {
            string userAlias;
            string friendAlias;

            Console.WriteLine("Started Server Execution");

            try
            {
                //Get socket info
                Socket listener = ConnectionHandler.GetSocket();

                //Get client connection
                Socket clientSocket = ConnectionHandler.Connect(listener);

                //Get friend alias (username)
                friendAlias = ConnectionHandler.GetFriendAlias(clientSocket);
                
                //Send user alias
                userAlias = ConnectionHandler.SendUserAlias(clientSocket);

                //Start chat
                Console.WriteLine("----------- Begin Chat -----------");
                Console.WriteLine("Waiting for client message.");
                Messenger.chatHandler(clientSocket, friendAlias, userAlias);

            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

    }
}
