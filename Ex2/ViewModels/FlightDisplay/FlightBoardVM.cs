using Ex2.Model.Server;
using FlightSimulator.ViewModels;
using System.ComponentModel;

namespace Ex2.ViewModels.FlightDisplay
{
    /// <summary>
    /// An object adapter from
    /// IFlightServer to IFlightBoardVM
    /// </summary>
    public class FlightBoardVM : IFlightBoardVM
    {
        public double Lon => Server.Lon;
        public double Lat => Server.Lat;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => Server.PropertyChanged += value;
            remove => Server.PropertyChanged -= value;
        }

        private IFlightServer Server { get; }
        public FlightBoardVM(IFlightServer server)
            => Server = server;
    }
}
