using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WeatherLibrary;

namespace ServerLibrary
{
    public class ServerEchoAPM : ServerEcho
    {
        public delegate void TransmissionDataDelegate(NetworkStream stream);

        public ServerEchoAPM(IPAddress IP, int port) : base(IP, port)
        {

        }

        protected override void AcceptClient()
        {
            while (true)
            {
                TcpClient tcpClient = TcpListener.AcceptTcpClient();

                Stream = tcpClient.GetStream();

                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);

                transmissionDelegate.BeginInvoke(Stream, TransmissionCallback, tcpClient);
            }
        }

        private void TransmissionCallback(IAsyncResult ar)
        {
            TcpClient client = (TcpClient)ar.AsyncState;
            client.Close();
        }

        protected override void BeginDataTransmission(NetworkStream stream)
        {
            byte[] buffer = new byte[Buffer_size];

            while (true)
            {
                stream.Read(buffer, 0, Buffer_size);

                string location = Encoding.ASCII.GetString(buffer.Where(b => b != 0).ToArray());

                if (!location.Any(c => !char.IsLetter(c)))
                {
                    byte[] weather = Encoding.ASCII.GetBytes(Weather.GetWeather(location));

                    stream.Write(weather, 0, weather.Length);
                }

                Array.Clear(buffer, 0, buffer.Length);
            }
        }

        public override void Start()
        {
            StartListening();
            AcceptClient();
        }
    }
}