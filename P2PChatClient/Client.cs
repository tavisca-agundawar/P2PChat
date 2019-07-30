using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace P2PChatClient
{
    public class Client
    {
        static void Main(string[] args)
        {
            ExecuteClient();
        }

        static void ExecuteClient()
        {
            string userAlias, friendAlias;

            try
            {
                Socket sender = ClientConnectionHandler.GetSocket();

                sender = ClientConnectionHandler.ConnectToServer(sender);

                userAlias = ClientConnectionHandler.SendUserAlias(sender);

                friendAlias = ClientConnectionHandler.GetFriendAlias(sender);

                Messenger.chatHandler(sender, friendAlias, userAlias);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }
    }
} 
