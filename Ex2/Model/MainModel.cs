﻿using Ex2.Model.Client;
using Ex2.Model.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Model
{
    class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IFlightServer serverModel;
        private IFlightClient clientModel;

        public IFlightServer ServerModel => serverModel;
        public IFlightClient ClientModel => clientModel;

        public MainModel()
        {
            this.serverModel = new FlightServer();
            this.clientModel = new FlightClient();
        }
    }
}