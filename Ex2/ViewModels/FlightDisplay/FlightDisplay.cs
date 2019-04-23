using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ex2.Model;
using Ex2.View;
using FlightSimulator.ViewModels;

namespace Ex2.ViewModels.FlightDisplay
{
    public class FlightDisplay : IFlightDisplayVM
    {
        private ICommand settingsCmd;
        public ICommand SettingsCommand =>
            settingsCmd ?? (settingsCmd = new SettingsButtonHandler());

        public ICommand ConnectCommand => throw new NotImplementedException();

        public IFlightBoardVM FlightBoardVM => throw new NotImplementedException();

        public FlightDisplay(IMainModel mainModel)
        {

        }
    }

    internal class SettingsButtonHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private bool closed = true;
        private bool WindowClosed
        {
            get => closed;
            set
            {
                if (closed == value)
                    return;

                closed = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        public bool CanExecute(object parameter) => WindowClosed;

        public void Execute(object parameter)
        {
            // create an instance of the window and add a close listener
            SettingsWindow window = new SettingsWindow();
            window.Closed += SettingsClosed;

            // open the window
            window.Show();

            // indicate it is not closed
            WindowClosed = false;
        }

        // indicate that the settings window has been closed
        private void SettingsClosed(object sender, EventArgs e) 
            => WindowClosed = true;
    }

}
