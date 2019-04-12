using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex2.Model.Server
{
    class VariablesServer : IVariablesServer
    {
        private IDictionary<string, double> properties = new ConcurrentDictionary<string, double>();

        public double this[string propertyName] => properties[propertyName];


        private int port;
        public uint Port
        {
            get => (uint) port;
            set
            {
                if (IsOpen)
                    throw new InvalidOperationException("Cannot change port while server is open!");

                if (value < 65536)
                    port = (int)value;
                else
                    throw new ArgumentException($"Invalid port inserted. {value}");
            }
        }

        public bool IsOpen { get; private set; }

        private IList<string> propNames = new List<string>();
        public IList<string> PropertyNames
        {
            get => new List<string>(propNames);

            set
            {
                propNames = value;
                properties.Clear();
            }
        }
        
        public uint RefreshRate { get; set ; }

        private TcpListener listener;
        private TcpClient client;

        private bool running;

        public event UpdateHandler PropertyUpdate;

        public void Close()
        {
            if (!IsOpen)
                throw new InvalidOperationException("Cannot close, server is not opened");

            /* disable running of loop and wait for it to finish */
            running = false;
            Thread.Sleep(1000);

            // close the client connection and then the server connection
            client.Close();
            listener.Stop();
        }

        public void Open()
        {
            if (IsOpen)
                throw new InvalidOperationException("Cannot open, server is already opened");

            // start a listener
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
            listener = new TcpListener(ep);
            listener.Start();

            client = listener.AcceptTcpClient();

            running = true;
            IsOpen = true;

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                string[] propNames;
                Stopwatch watch = new Stopwatch();

                while (running)
                {
                    watch.Restart();

                    propNames = this.propNames.ToArray<string>();
                    int currentProperty = 0;
                    
                    while (reader.PeekChar() != '\n')
                    {
                        // skip commas
                        if (reader.PeekChar() == ',')
                            reader.ReadChar();

                        // now read the double value
                        double d = reader.ReadDouble();

                        // insert into the table
                        properties[propNames[currentProperty]] = d;

                        // and move to next property
                        currentProperty++;
                    }
                    // clear '\n'
                    reader.ReadChar();

                    // notify change
                    PropertyUpdate?.Invoke();

                    /* calculate how much time has passed since entering the loop */
                    watch.Stop();
                    long millisElapsed = watch.ElapsedMilliseconds;

                    // wait for at least 1 millisecond between reads
                    long millisWait = Math.Max(1000 / RefreshRate, 1);

                    /* sleep for the time left */
                    long millisLeft = Math.Max(0, millisWait - millisElapsed);
                    Thread.Sleep((int) millisLeft);
                }
            }
        }
    }
}
