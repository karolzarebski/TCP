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

        /// <summary>
        /// Initializes server
        /// </summary>
        /// <param name="IP">Server ip address</param>
        /// <param name="port">Server port</param>
        public Server(IPAddress IP, int port)
        {
            _ipAddress = IP;
            _port = port;
        }

        /// <summary>
        ///  Accepts client connection
        /// </summary>
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

        /// <summary>
        /// Close connection with client
        /// </summary>
        /// <param name="ar">client</param>
        private void TransmissionCallback(IAsyncResult ar)
        {
            TcpClient client = (TcpClient)ar.AsyncState;
            client.Close();
        }

        /// <summary>
        /// Checks if given byte is proper char
        /// </summary>
        /// <param name="asciiChar">Given char in ASCII</param>
        /// <returns>True or false if given byte is ASCII</returns>
        private bool IsCharCorrect(byte asciiChar)
        {
            return asciiChar >= 33 && asciiChar <= 126;
        }

        /// <summary>
        /// Receives data from client, downloads weather for given location and sends it back to client
        /// </summary>
        /// <param name="stream">client stream</param>
        private void BeginDataTransmission(NetworkStream stream)
        {
            byte[] buffer = new byte[_bufferSize];

            string enterLocationMessage = "Enter location (Only english letters): ";
            string fethcingDataFromAPIMessage = "\r\nFetching data from API\r\n";

            stream.Write(Encoding.ASCII.GetBytes(enterLocationMessage), 0, enterLocationMessage.Length);

            while (true)
            {
                try
                {
                    stream.Read(buffer, 0, _bufferSize);

                    string location = Encoding.ASCII.GetString(buffer.Where(b => IsCharCorrect(b)).ToArray());

                    stream.Write(Encoding.ASCII.GetBytes(fethcingDataFromAPIMessage), 0, fethcingDataFromAPIMessage.Length);

                    byte[] weather = Encoding.ASCII.GetBytes(Weather.GetWeather(location));

                    stream.Write(weather, 0, weather.Length);

                    stream.Write(Encoding.ASCII.GetBytes(enterLocationMessage), 0, enterLocationMessage.Length);

                    Array.Clear(buffer, 0, buffer.Length);
                }
                catch
                {
                    break;
                }

            }
        }

        /// <summary>
        /// Starts tcp listener 
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Starting server");

            _tcpListener = new TcpListener(_ipAddress, _port);
            _tcpListener.Start();

            AcceptClient();
        }
    }
}