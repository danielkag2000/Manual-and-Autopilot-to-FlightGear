using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Model.Server
{

    class FlightServer : IFlightServer
    {
        private const string LON_PATH = "/position/longitude-deg";
        private const string LAT_PATH = "/position/latitude-deg";

        private IVariablesServer variables;

        public FlightServer()
        {
            variables = new VariablesServer();

            // register all property names from XML file
            variables.PropertyNames = new List<string>
            {
                "/position/longitude-deg",
                "/position/latitude-deg",
                "/instrumentation/altimeter/indicated-altitude-ft",
                "/instrumentation/altimeter/pressure-alt-ft",
                "/instrumentation/encoder/indicated-altitude-ft",
                "/instrumentation/encoder/pressure-alt-ft",
                "/instrumentation/gps/indicated-altitude-ft",
                "/instrumentation/gps/indicated-ground-speed-kt",
                "/instrumentation/gps/indicated-vertical-speed",
                "/controls/flight/aileron",
                "/controls/flight/elevator",
                "/controls/flight/rudder",
                "/controls/flight/flaps",
                "/controls/engines/current-engine/throttle",
                "/engines/engine/rpm"
            };

            // set the handler for property update event
            updateHandler = OnUpdate;
        }

        public uint Port { get => variables.Port; set => variables.Port = value; }

        public bool IsOpen => variables.IsOpen;

        public double Lon => variables[LON_PATH];
        public double Lat => variables[LAT_PATH];

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnUpdate()
        {
            if (PropertyChanged == null)
                return;

            // notify that both properties have been updated
            PropertyChanged(this, new PropertyChangedEventArgs("Lon"));
            PropertyChanged(this, new PropertyChangedEventArgs("Lat"));
        }

        private UpdateHandler updateHandler;

        public void Close()
        {
            variables.Close();
            variables.PropertyUpdate -= updateHandler;
        }

        public void Open()
        {
            variables.Open();
            variables.PropertyUpdate += updateHandler;
        }
    }
}
