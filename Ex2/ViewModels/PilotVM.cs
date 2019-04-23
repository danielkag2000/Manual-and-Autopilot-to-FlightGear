﻿using Ex2.Model;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.ViewModels
{
    public partial class PilotVM : BaseNotify
    {
        private MainModel Model;

        public PilotVM()
        {
            this.Model = MainModel.GetInstance();
        }
    }
}
