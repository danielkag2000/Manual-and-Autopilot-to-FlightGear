using Ex2.Model;
using FlightSimulator.Model.EventArgs;
using FlightSimulator.ViewModels;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.ViewModels
{

    public class ManualPilotVM : BaseNotify
    {

        private MainModel Model;
        public ManualPilotVM()
        {
            this.Model = MainModel.GetInstance();
        }

        public void UpdateAileronAndElevator(Joystick sender, VirtualJoystickEventArgs args)
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
    }
}
