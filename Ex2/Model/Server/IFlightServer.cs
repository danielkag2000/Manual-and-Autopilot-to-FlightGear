using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Model.Server
{
    public interface IFlightServer : INotifyPropertyChanged
    {
        uint Port { get; set; }

        bool IsOpen { get; }

        double Lon { get; }
        double Lat { get; }

        void Open();
        void Close();
    }
}
