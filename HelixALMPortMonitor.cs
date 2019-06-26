using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HelixALMPortMonitoring
{
    public class HelixALMPortMonitor
    {
        public static Boolean HelixALMPort(string server, int port)
        {
            Socket socket = null;
            Boolean IsConnected = false;
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.SendTimeout = 5;
                socket.ReceiveTimeout = 5;
                
                socket.Connect(server, port);

                IsConnected = socket.Connected;

                if (IsConnected)
                {
                    Console.WriteLine(server + ":" + port + " is connected");
                }
                else
                {
                    Console.WriteLine(server + ":" + port + " unable to connect");
                }

                Console.WriteLine(server + ":" + port + " is active");
                return true;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    Console.WriteLine(server + ":" + port + " Connection refused");
                    return false;
                }
                else if (ex.SocketErrorCode == SocketError.TimedOut)
                {
                    Console.WriteLine(server + ":" + port + " Connection timedout");
                    return false;
                }
                else
                {
                    Console.WriteLine("Error: " + ex.SocketErrorCode);
                    return false;
                }
             
            }
            finally
            {
                if (socket?.Connected ?? false)
                {
                    socket?.Disconnect(false);
                }
                socket?.Close();
            }
        }

    }
}