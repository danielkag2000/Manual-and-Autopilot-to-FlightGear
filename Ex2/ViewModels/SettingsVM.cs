using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ex2.ViewModels
{
    public class SettingsVM : BaseNotify
    {

        public ICommand OKCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private Window window;

        /// <summary>
        /// Initialize a settings ViewModel
        /// </summary>
        /// <param name="settingsWindow">The settings window</param>
        public SettingsVM(Window settingsWindow)
        {
            InitDefaults();
            OKCommand = new OKCommandHandler(this);
            CancelCommand = new CancelCommandHandler(this);
            window = settingsWindow;  
        }

        /// <summary>
        /// Closes the settings window
        /// </summary>
        internal void CloseWindow()
        {
            Application.Current.MainWindow?.Show();
            window.Close();
        }

        /// <summary>
        /// Initializes the properties to the values inside the config,
        /// only used upon initialization of the settings window
        /// </summary>
        private void InitDefaults()
        {
            ISettingsModel settings = ApplicationSettingsModel.Instance;
            settings.ReloadSettings();

            try
            {
                FlightServerIP = settings.FlightServerIP;
                FlightCommandPort = settings.FlightCommandPort.ToString();
                FlightInfoPort = settings.FlightInfoPort.ToString();
            }
            catch (ArgumentException) { }
        }


        /// <summary>
        /// Occurrs when Invalid changes state from valid to invalid or the other way
        /// </summary>
        internal event EventHandler ChangedValidility;

        private int invalid;
        /// <summary>
        /// Invalid holds whether there is a property which is invalid
        /// if there is none, its value will be 0
        /// otherwise it will be different than 0
        /// </summary>
        internal int Invalid
        {
            get => invalid;
            set
            {
                bool changed = false;
                if (value != 0 && invalid == 0 || invalid != 0 && value == 0)
                    changed = true;

                invalid = value;

                if (changed)
                    ChangedValidility?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// A TryParse generic function
        /// </summary>
        /// <typeparam name="T">The type to parse into</typeparam>
        /// <param name="value">The string value to parse from</param>
        /// <param name="type">A variable from the type T</param>
        /// <returns>True if succeeded, false otherwise</returns>
        delegate bool TryParse<T>(string value, out T type);

        /// <summary>
        /// Tests whether a given value matches the given parser,
        /// if matches returns successfully
        /// otherwise, throws an ArgumentException and sets the
        /// Invalid property to point the property with index 'propertyIndex' is invalid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyIndex"></param>
        /// <param name="value"></param>
        /// <param name="parser"></param>
        private void TestValue<T>(int propertyIndex, string value, TryParse<T> parser)
        {
            T var;
            if (!parser(value, out var))
            {
                Invalid |= 1 << propertyIndex;
                
                throw new ArgumentException($"Invalid value: {value}");
            }
            Invalid &= ~(1 << propertyIndex);
        }

        /***********************************************
         ***********************************************
         ***********************************************
         ************** BOUND PROPERTIES *************** 
         ***********************************************
         ***********************************************
         ***********************************************/
        
        private string remoteIP;
        public string FlightServerIP
        {
            get => remoteIP;
            set
            {
                TestValue<IPAddress>(0, value, IPAddress.TryParse);
                remoteIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        private bool TryParsePort(string str, out uint i)
        {
            return uint.TryParse(str, out i) && i < 65536;
        }

        private string localPort;
        public string FlightInfoPort
        {
            get => localPort;

            set
            {
                TestValue<uint>(1, value, TryParsePort);
                localPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }

        private string remotePort;
        public string FlightCommandPort
        {
            get => remotePort;
            set
            {
                TestValue<uint>(2, value, TryParsePort);
                remotePort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        /***********************************************
         ***********************************************
         ***********************************************
         ********** END OF BOUND PROPERTIES ************
         ***********************************************
         ***********************************************
         ***********************************************/

    }

    /// <summary>
    /// A command handler for the OK button
    /// </summary>
    internal class OKCommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private SettingsVM vm;

        public OKCommandHandler(SettingsVM vm)
        {
            this.vm = vm;
            vm.ChangedValidility += delegate (object sender, EventArgs args)
            {
                CanExecuteChanged?.Invoke(this, new EventArgs());
            };
        }

        public bool CanExecute(object parameter)
        {
            return vm.Invalid == 0;
        }

        public void Execute(object parameter)
        {
            ApplicationSettingsModel.Instance.FlightCommandPort = int.Parse(vm.FlightCommandPort);
            ApplicationSettingsModel.Instance.FlightInfoPort = int.Parse(vm.FlightInfoPort);
            ApplicationSettingsModel.Instance.FlightServerIP = vm.FlightServerIP;
            ApplicationSettingsModel.Instance.SaveSettings();

            vm.CloseWindow();
        }
    }

    /// <summary>
    /// A command handler for the cancel button
    /// </summary>
    internal class CancelCommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private SettingsVM vm;

        public CancelCommandHandler(SettingsVM vm) { this.vm = vm; }

        public bool CanExecute(object parameter) { return true;  }

        public void Execute(object parameter) { vm.CloseWindow(); }
    }
}
