using Ex2.Model;
using FlightSimulator.Model.EventArgs;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.ViewModels
{

    public class ManualPilotVM : INotifyPropertyChanged
    {

        private IMainModel Model;
        private Joystick joystick;
        public ManualPilotVM()
        {
            Model = MainModel.Instance;
        }

        public void updateAileronAndElevator(Joystick sender, VirtualJoystickEventArgs args)
        {
            AileronValue = args.Aileron;
            ElevatorValue = args.Elevator;
        }

        private double aileronValue = 0;
        public double AileronValue
        {
            get { return aileronValue; }
            set
            {
                aileronValue = value;
                NotifyPropertyChanged("AileronValue");
                SetProperty("/controls/flight/aileron", aileronValue);
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
                SetProperty("/controls/flight/elevator", elevatorValue);
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
                SetProperty("/controls/flight/rudder", rudderValue);
            }
        }

        private double throttleValue = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public double ThrottleValue
        {
            get { return throttleValue; }
            set
            {
                throttleValue = value;
                NotifyPropertyChanged("ThrottleValue");
                SetProperty("/controls/engines/current-engine/throttle", throttleValue);
            }
        }

        private void SetProperty(string path, double value)
        {
            //Model.ClientModel.SendLine($"set {path} {value}");
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
