using Ex2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.ViewModels
{
    /// <summary>
    /// the ViewModel for main view
    /// </summary>
    public partial class PilotVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IMainModel Model;

        public PilotVM()
        {
            Model = MainModel.Instance;
        }

        /// <summary>
        /// Notify Property Changed
        /// </summary>
        /// <param name="propName">the property change value</param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
