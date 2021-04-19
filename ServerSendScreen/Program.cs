using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using ScreenSave;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using System.Drawing;
namespace ServerSendScreen
{
    class Program
    {



        static void Main(string[] args)
        {
            Thread thread = new Thread(ServerFunc);
            thread.IsBackground=true;
            thread.Start();
        }
     
         static private void ServerFunc()
        {
            TcpListener tcpClient = new TcpListener(IPAddress.Loopback, 11000);
            try
            {
                tcpClient.Start();
                Console.WriteLine("Start server");
                do
                {
                    if (tcpClient.Pending())
                    {
                       TcpClient client= tcpClient.AcceptTcpClient();
                        byte[] buff = new byte[1024];
                        NetworkStream ns = client.GetStream();

                     

                        ScreenCapture screenCapture = new ScreenCapture();

                      screenCapture.Capture();
                       // Bitmap bmp = new Bitmap(image, pictureBox1.Size);



                        //TcpClient clientSender = new TcpClient(IPAddress.Loopback.ToString(), 11000);
                        //byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                        //NetworkStream stream = client.GetStream();
                        //Console.WriteLine("Sent: {0}", message);



                        //try
                        //{
                        //    // Create a TcpClient.
                        //    // Note, for this client to work you need to have a TcpServer
                        //    // connected to the same address as specified by the server, port
                        //    // combination.
                        //    Int32 port = 13000;
                        //    TcpClient client = new TcpClient();

                        //    // Translate the passed message into ASCII and store it as a Byte array.
                        //    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                        //    // Get a client stream for reading and writing.
                        //    //  Stream stream = client.GetStream();

                        //    NetworkStream stream = client.GetStream();

                        //    // Send the message to the connected TcpServer.
                        //    stream.Write(data, 0, data.Length);

                        //    Console.WriteLine("Sent: {0}", message);

                        //    // Receive the TcpServer.response.

                        //    // Buffer to store the response bytes.
                        //    data = new Byte[256];

                        //    // String to store the response ASCII representation.
                        //    String responseData = String.Empty;

                        //    // Read the first batch of the TcpServer response bytes.
                        //    Int32 bytes = stream.Read(data, 0, data.Length);
                        //    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        //    Console.WriteLine("Received: {0}", responseData);

                        //    // Close everything.
                        //    stream.Close();
                        //    client.Close();
                        //}
                        //catch (ArgumentNullException e)
                        //{
                        //    Console.WriteLine("ArgumentNullException: {0}", e);
                        //}
                        //catch (SocketException e)
                        //{
                        //    Console.WriteLine("SocketException: {0}", e);
                        //}

                        Console.WriteLine("\n Press Enter to continue...");
                        Console.Read();

                    }



                } while (true);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                tcpClient.Stop();
            }

        }
    }
}
