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
        private double aileronValue = 0;
        public double AileronValue
        {
            get { return aileronValue; }
            set
            {
                aileronValue = value;
                NotifyPropertyChanged("AileronValue");
            }
        }

        private double elevatorValue = 0;
        public double ElevatorValue
        {
            get { return elevatorValue; }
            set
            {
                elevatorValue = value;
                NotifyPropertyChanged("ElevatorValue");
            }
        }

        private double rudderValue = 0;
        public double RudderValue
        {
            get { return rudderValue; }
            set
            {
                rudderValue = value;
                NotifyPropertyChanged("RudderValue");
            }
        }

        private double throttleValue = 0;
        public double ThrottleValue
        {
            get { return throttleValue; }
            set
            {
                throttleValue = value;
                NotifyPropertyChanged("ThrottleValue");
            }
        }
    }
}
