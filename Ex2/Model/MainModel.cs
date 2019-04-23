using Ex2.Model.Client;
using Ex2.Model.Server;

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
        }
    }
}
