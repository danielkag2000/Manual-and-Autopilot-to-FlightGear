using Ex2.Model.Client;
using Ex2.Model.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Model
{
    class MainModel : IMainModel
    {
        #region Singleton
        private static MainModel instance;

        public static MainModel Instance =>
            instance ??
            (instance = new MainModel(new FlightServer(), new FlightClient()));
        #endregion

        //public event PropertyChangedEventHandler PropertyChanged;

        public IFlightServer ServerModel { get; private set; }
        public IFlightClient ClientModel { get; private set; }

        

        private MainModel(IFlightServer serverModel, IFlightClient clientModel)
        {
            ServerModel = serverModel;
            ClientModel = clientModel;
        }
    }
}
