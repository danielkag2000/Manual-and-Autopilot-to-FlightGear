using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Model.Client
{
    class FlightClient : IFlightClient
    {
        private IPAddress ip;
        public string IP
        {
            get => ip.ToString();

            set
            {
                if (IsOpen)
                    throw new InvalidOperationException("Cannot change IP while connection is open!");

                // only set valid ip address
                if (!IPAddress.TryParse(value, out ip))
                    throw new ArgumentException($"Invalid IP inserted ({value}).");
            }
        }

        private int port;
        public uint Port
        {
            get => (uint) port;

            set
            {
                if (IsOpen)
                    throw new InvalidOperationException("Cannot change port while connection is open!");

                if (value < 65536)
                    this.port = (int) value;
                else
                    throw new ArgumentException($"Invalid port inserted({value}).");
            }
        }

        public bool IsOpen { get; private set; }

        private TcpClient client;

        public void Close()
        {
            if (!IsOpen)
                throw new InvalidOperationException("Cannot close a connection that has not been opened.");

            client.Close();
            client = null;

            IsOpen = false;
        }

        public void Open()
        {
            if (IsOpen)
                throw new InvalidOperationException("Cannot open connection before closing current one!");

            /* connect to flightgear server */
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            client = new TcpClient();
            client.Connect(endPoint);

            // change state to opened
            IsOpen = true;
        }

        public void SendLine(string line)
        {
            List<string> lines = new List<string>();
            lines.Add(line);
            SendLines(lines);
        }

        public void SendLines(IList<string> lines)
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                foreach (string line in lines)
                {
                    writer.Write(line);
                    writer.Write("\r\n");
                }
            }
        }
    }
}
