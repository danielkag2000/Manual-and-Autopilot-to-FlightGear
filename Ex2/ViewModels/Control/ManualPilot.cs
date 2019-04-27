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

        /// <summary>
        /// the model
        /// </summary>
        private IMainModel Model;
        public ManualPilotVM()
            => Model = MainModel.Instance;

        /// <summary>
        /// the Aileron Value
        /// </summary>
        private double aileronValue = 0;
        public double AileronValue
        {
            get { return NiceRound(aileronValue); }
            set
            {
                aileronValue = value;
                NotifyPropertyChanged("AileronValue");
                SetProperty("/controls/flight/aileron", aileronValue);
            }
        }

        /// <summary>
        /// the Elevator Value
        /// </summary>
        private double elevatorValue = 0;
        public double ElevatorValue
        {
            get { return NiceRound(elevatorValue); }
            set
            {
                elevatorValue = value;
                NotifyPropertyChanged("ElevatorValue");
                SetProperty("/controls/flight/elevator", elevatorValue);
            }
        }

        /// <summary>
        /// the Rudder Value
        /// </summary>
        private double rudderValue = 0;
        public double RudderValue
        {
            get { return NiceRound(rudderValue); }
            set
            {
                rudderValue = value;
                NotifyPropertyChanged("RudderValue");
                SetProperty("/controls/flight/rudder", rudderValue);
            }
        }

        /// <summary>
        /// the Throttle Value
        /// </summary>
        private double throttleValue = 0;
        public double ThrottleValue
        {
            get { return NiceRound(throttleValue); }
            set
            {
                throttleValue = value;
                NotifyPropertyChanged("ThrottleValue");
                SetProperty("/controls/engines/current-engine/throttle", throttleValue);
            }
        }

        /// <summary>
        /// send the property with its value to the model
        /// </summary>
        /// <param name="path">the path in the flight simulator</param>
        /// <param name="value">the value to send</param>
        private void SetProperty(string path, double value)
        {
            if (Model.ClientModel.IsOpen)
            {
                Model.ClientModel.SendLine($"set {path} {value}");
            }
        }

        /// <summary>
        /// the Joystick Values Listener
        /// </summary>
        public JoystickValues ValuesListener
        {
            set
            {
                if (value == null)
                    return;

                AileronValue = value.Aileron;
                ElevatorValue = value.Elevator;
            }
        }

        /// <summary>
        /// do a nice round to the number
        /// </summary>
        /// <param name="d">the number to round</param>
        /// <returns>a nice round of the number</returns>
        private double NiceRound(double d) => Math.Round(d * 100) / 100;
    }
}
