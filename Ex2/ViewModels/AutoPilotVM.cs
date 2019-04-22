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

        //private IFlightClient Model;
        private string text;
        public string TextCommand
        {
            get { return text; }
            set {
                text = value;
                NotifyPropertyChanged("TextCommand");
            }
        }

        public AutoPilotVM()
        {
            //this.Model = model;
        }


        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
