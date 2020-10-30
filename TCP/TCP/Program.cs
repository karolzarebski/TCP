using ServerLibrary;
using System.Net;
using System.Text;
using WeatherLibrary;

namespace TCP
{
    class Program
    {
        static void Main(string[] args)
        {
            int bytes = Encoding.ASCII.GetBytes(Weather.GetWeather("warszawa")).Length;
            ServerEchoAPM serverEchoAPM = new ServerEchoAPM(IPAddress.Parse("127.0.0.1"), 2048);
            serverEchoAPM.Start();
        }
    }
}
