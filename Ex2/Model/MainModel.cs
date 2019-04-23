using Ex2.Model.Client;
using Ex2.Model.Server;
using FlightSimulator.Model.Interface;
using System;

namespace Ex2.Model
{
    /// <summary>
    /// The main model used in the project (for the client and server)
    /// Its purpose is to serve as a singleton so that there will
    /// be only one client and one server models
    /// </summary>
    class MainModel : IMainModel
    {
        #region Singleton
        private static MainModel instance;

        public static MainModel Instance =>
            instance ??
            (instance = new MainModel(new ParallelFlightServer(new FlightServer()),
                new FlightClient()));
        #endregion

        public IFlightServer ServerModel { get; private set; }
        public IFlightClient ClientModel { get; private set; }
        
        private MainModel(IFlightServer serverModel, IFlightClient clientModel)
        {
            ServerModel = serverModel;
            ClientModel = clientModel;

            // adds a listener so that when the servers opens, the client will open
            ServerModel.OnConnection += OpenClient;
        }

        public void CloseOpenConnections()
        {
            // close old connections
            if (ClientModel.IsOpen)
                ClientModel.Close();
            if (ServerModel.IsOpen)
                ServerModel.Close();
        }

        public void InitConnections(ISettingsModel settings)
        {
            /* set connection parameters */
            ClientModel.IP = settings.FlightServerIP;
            ClientModel.Port = (uint)settings.FlightCommandPort;

            ServerModel.Port = (uint)settings.FlightInfoPort;
        }

        private void OpenClient()
        {
            try { ClientModel.Open(); }
            catch (Exception) { }
        }

        // open the server (the client will be opened thanks to the OnConnection event 
        public void OpenConnections() =>
            ServerModel.Open();
    }
}
