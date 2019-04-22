﻿using System;
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
    public class VariablesServer : IVariablesServer
    {
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

        public char LineSep { get; set; }
        public char VarSep { get; set; }

        private IDictionary<int, double> properties = new ConcurrentDictionary<int, double>();
        public double this[int propertyIndex]
        {
            get
            {
                double value = .0;
                properties.TryGetValue(propertyIndex, out value);
                return value;
            }
        }

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
            Console.WriteLine("Client Connected!");

            running = true;
            IsOpen = true;
            
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                int currentProperty;

                while (running)
                {
                    currentProperty = 0;
                    int ch;
                    string num = "";
                    
                    // read until end of line
                    while ((ch = reader.ReadChar()) != LineSep)
                    {
                        // read until end of current variable
                        do num += (char) ch;
                        while ((ch = reader.Read()) != VarSep && ch != LineSep && ch != -1);
                            

                        // if actually read a number
                        if (num.Length > 0)
                        {
                            // set the current double
                            double d;
                            if (double.TryParse(num, out d))
                                properties[currentProperty++] = d;
                            num = "";
                        }

                        if (ch == LineSep)
                            break;
                    }

                    // notify change
                    PropertyUpdate?.Invoke();
                }
            }
        }
    }
}
