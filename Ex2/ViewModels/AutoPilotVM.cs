using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ex2.Model.Client;
using System.Threading.Tasks;

namespace Ex2.ViewModels
{
    class AutoPilotVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IFlightClient Model;

        public AutoPilotVM(IFlightClient model)
        {
            this.Model = model;
        }


        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
