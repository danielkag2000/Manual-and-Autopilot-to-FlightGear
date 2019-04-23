using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public interface IFlightBoardVM : INotifyPropertyChanged
    {
        double Lon { get; }
        double Lat { get; }
    }
}
