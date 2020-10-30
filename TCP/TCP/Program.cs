using ServerLibrary;
using System.Net;

namespace TCP
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(IPAddress.Parse("127.0.0.1"), 2048);
            server.Start();
        }
    }
}
