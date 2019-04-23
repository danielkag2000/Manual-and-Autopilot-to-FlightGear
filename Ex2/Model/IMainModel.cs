using Ex2.Model.Client;
using Ex2.Model.Server;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Model
{
    public interface IMainModel
    {
        IFlightServer ServerModel { get; }
        IFlightClient ClientModel { get; }

        void CloseOpenConnections();
        void InitConnections(ISettingsModel settingsModel);
        void OpenConnections();
    }
}
