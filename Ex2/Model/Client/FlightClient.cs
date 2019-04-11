using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Model.Client
{
    class FlightClient : IFlightClient
    {
        public string IP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Port { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsOpen => throw new NotImplementedException();

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void SendLine(string line)
        {
            throw new NotImplementedException();
        }

        public void SendLines(IList<string> lines)
        {
            throw new NotImplementedException();
        }
    }
}
