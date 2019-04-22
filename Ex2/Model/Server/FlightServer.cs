using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Model.Server
{

    public class FlightServer : IFlightServer
    {
        private IVariablesServer variables;

        public FlightServer()
        {
            variables = new VariablesServer();

            // separators
            variables.LineSep = '\n';
            variables.VarSep = ',';

            // set the handler for property update event
            updateHandler = OnUpdate;
        }

        public uint Port { get => variables.Port; set => variables.Port = value; }

        public bool IsOpen => variables.IsOpen;

        // set the longitude and altitude according to their index
        public double Lon => variables[0];
        public double Lat => variables[1];

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
            variables.PropertyUpdate += updateHandler;
            variables.Open();
        }
    }
}
