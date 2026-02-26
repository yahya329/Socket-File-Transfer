using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        Console.WriteLine("Yahya Ahmed Yahya Fayez");
        Console.WriteLine("section 3");
        Console.WriteLine("Computer Systems");

        IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        sock.Connect(server);

        byte[] buffer = new byte[1024 * 1024];
        int received = sock.Receive(buffer);

        int fileNameLength = BitConverter.ToInt32(buffer, 0);

        string fileName = Encoding.ASCII.GetString(buffer, 4, fileNameLength);

        byte[] fileData = new byte[received - (4 + fileNameLength)];
        Array.Copy(buffer, 4 + fileNameLength, fileData, 0, fileData.Length);

        File.WriteAllBytes(fileName, fileData);

        Console.WriteLine("File received: " + fileName);

        sock.Close();
    }
}