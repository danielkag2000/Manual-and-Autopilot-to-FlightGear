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
    public class FlightClient : IFlightClient
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


        private TcpClient client = null;
        public bool IsOpen { get => client != null && client.Connected; }

        private BinaryWriter clientWriter;

        public void Close()
        {
            if (IsOpen)
            {
                try { clientWriter.Dispose(); client.Close(); }
                catch (Exception) { }
            }

            client = null;
            clientWriter = null;
        }

        public void Open()
        {
            if (IsOpen)
                throw new InvalidOperationException("Cannot open connection before closing current one!");

            try {
                /* connect to flightgear server */
                IPEndPoint endPoint = new IPEndPoint(ip, port);
                client = new TcpClient();
                client.Connect(endPoint);
                Console.WriteLine("Connected to " + endPoint.ToString());
            }
            catch (Exception) { client = null; return; }


            clientWriter = new BinaryWriter(client.GetStream());
        }

        public void SendLine(string line)
        {
            
            List<string> lines = new List<string>();
            lines.Add(line);
            SendLines(lines);
        }

        public void SendLines(IList<string> lines)
        {
            if (!IsOpen)
                throw new InvalidOperationException("Cannot send lines to unopened socket.");

            foreach (string line in lines)
            {
                clientWriter.Write(line.ToCharArray());
                clientWriter.Write('\r');
                clientWriter.Write('\n');
                clientWriter.Flush();
            }
        }
    }
}
