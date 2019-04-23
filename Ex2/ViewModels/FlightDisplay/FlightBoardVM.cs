using Ex2.Model.Server;
using FlightSimulator.ViewModels;
using System.ComponentModel;

namespace Ex2.ViewModels.FlightDisplay
{
    /// <summary>
    /// An object adapter from
    /// IFlightServer to IFlightBoardVM
    /// </summary>
    public class FlightBoardVM : BaseNotify, IFlightBoardVM
    {
        public double Lon => Server.Lon;
        public double Lat => Server.Lat;

        private IFlightServer Server { get; }
        public FlightBoardVM(IFlightServer server)
        {
            Server = server;
            Server.PropertyChanged += OnVariableChange;
        }

        private void OnVariableChange(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Lon" || args.PropertyName == "Lat")
                NotifyPropertyChanged(args.PropertyName);
        }
    }
}
