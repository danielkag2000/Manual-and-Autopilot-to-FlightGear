using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ex2.Model.Client;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private ICommand _clearCommand;        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => OnClearClick()));
            }
        }

        private void OnClearClick()
        {
            TextCommand = "";
        }
    }
}
