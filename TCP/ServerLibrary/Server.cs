using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WeatherLibrary;

namespace ServerLibrary
{
    public class Server
    {
        public delegate void TransmissionDataDelegate(NetworkStream stream);

        private IPAddress _ipAddress;
        private int _port;
        private int _bufferSize = 85;

        private TcpListener _tcpListener;
        private NetworkStream _networkStream;

        public Server (IPAddress IP, int port)
        {
            _ipAddress = IP;
            _port = port;
        }

        private void AcceptClient()
        {
            while (true)
            {
                TcpClient tcpClient = _tcpListener.AcceptTcpClient();

                _networkStream = tcpClient.GetStream();

                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);

                transmissionDelegate.BeginInvoke(_networkStream, TransmissionCallback, tcpClient);
            }
        }

        private void TransmissionCallback(IAsyncResult ar)
        {
            TcpClient client = (TcpClient)ar.AsyncState;
            client.Close();
        }

        private void BeginDataTransmission(NetworkStream stream)
        {
            byte[] buffer = new byte[_bufferSize];

            while (true)
            {
                stream.Read(buffer, 0, _bufferSize);

                string location = Encoding.ASCII.GetString(buffer.Where(b => b != 0).ToArray());

                if (!location.Any(c => !char.IsLetter(c)))
                {
                    byte[] weather = Encoding.ASCII.GetBytes(Weather.GetWeather(location));

                    stream.Write(weather, 0, weather.Length);
                }

                Array.Clear(buffer, 0, buffer.Length);
            }
        }

        public void Start()
        {
            _tcpListener = new TcpListener(_ipAddress, _port);
            _tcpListener.Start();

            AcceptClient();
        }
    }
}