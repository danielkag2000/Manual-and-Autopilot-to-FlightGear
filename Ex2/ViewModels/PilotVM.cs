using Ex2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.ViewModels
{
    public partial class PilotVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MainModel Model;

        public PilotVM()
        {
            this.Model = MainModel.GetInstance();
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
