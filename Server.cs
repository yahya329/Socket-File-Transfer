using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Yahya Ahmed Yahya Fayez");
        Console.WriteLine("Section 3");
        Console.WriteLine("Computer Systems");
        Console.WriteLine("=================");

        // Validation 

        if (args.Length == 0)
        {
            Console.WriteLine("pls enter path as arguments");
            return;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("don't found the file !");
        }

        string fileName = Path.GetFileName(filePath);

        byte[] fileNameBytes = Encoding.ASCII.GetBytes(fileName);

        byte[] numOfbytes = File.ReadAllBytes(filePath);

        byte[] lengthBytes = BitConverter.GetBytes(fileNameBytes.Length);

        byte[] data =new byte[4+ fileNameBytes.Length+numOfbytes.Length];

        lengthBytes.CopyTo(data, 0);
        fileNameBytes.CopyTo(data, 4);
        numOfbytes.CopyTo(data, 4+fileNameBytes.Length);

        IPEndPoint theIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);

        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        sock.Bind(theIP);
        sock.Listen(1);

        Console.WriteLine("Waiting for client...");
        Socket client = sock.Accept();

        client.Send(data);
        Console.WriteLine("File sent.");

        client.Close();
        sock.Close();

    }
}