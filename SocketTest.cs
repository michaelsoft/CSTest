using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace CSTest
{
    class SocketTest
    {
        public static void Test()
        {
            var servers = new string[] { "10.2.8.111", "10.2.8.113"};
            var ports = new int[] { 80, 1433, 4200, 8080 };

            foreach (var server in servers)
            {
                foreach (var port in ports)
                {
                    SocketSendReceive(server, port);
                } 
            } 
            
        }

        private static void SocketSendReceive(string server, int port)
        {
            Byte[] bytesSent = Encoding.ASCII.GetBytes("hi");
            Byte[] bytesReceived = new Byte[256];

            try
            {
                Socket s = ConnectSocket(server, port);
                s.Send(bytesSent, bytesSent.Length, 0);
                var bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                if (bytes > 0)
                   Console.WriteLine($"[+] {server} - {port} opened");
            }
            catch (System.Exception)
            {
                
                Console.WriteLine($"[-] {server} - {port} closed");
            }
        }

        private static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = Dns.GetHostEntry(server);

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            foreach(IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket =
                    new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect(ipe);

                if(tempSocket.Connected)
                {
                    s = tempSocket;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return s;
        }
    }
}
