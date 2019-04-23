using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Model.Server
{
    public class ParallelFlightServer : IFlightServer
    {
        private IFlightServer Server { get; }
        private Task serverTask = null;

        public ParallelFlightServer(IFlightServer server)
            => Server = server;
        

        public uint Port { get => Server.Port; set => Server.Port = value; }

        public bool IsOpen => Server.IsOpen;

        public double Lon => Server.Lon;
        public double Lat => Server.Lat;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => Server.PropertyChanged += value;
            remove => Server.PropertyChanged -= value;
        }

        public event ConnectionEvent OnConnection
        {
            add => Server.OnConnection += value;
            remove => Server.OnConnection -= value;
        }

        public void Close()
        {
            // close and wait for the server to finish up
            Server.Close();
            try { serverTask?.Wait(); }
            catch (Exception) { }
        }

        public void Open()
        {
            serverTask = new Task(() => Server.Open());
            serverTask.Start();
        }
    }
}
