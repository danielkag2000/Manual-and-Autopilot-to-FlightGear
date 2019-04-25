using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ex2.Model;
using Ex2.View;
using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels;

namespace Ex2.ViewModels.FlightDisplay
{
    public class FlightDisplayVM : IFlightDisplayVM
    {
        // models
        private IMainModel MainModel { get; }
        private ISettingsModel SettingsModel { get; }

        // viewmodel for the flightboard view (Lon\Lat view)
        private IFlightBoardVM flightBoardVM;
        public IFlightBoardVM FlightBoardVM => flightBoardVM ??
            (flightBoardVM = new FlightBoardVM(MainModel.ServerModel));

        /* button commands */
        private ICommand settingsCmd;
        public ICommand SettingsCommand => settingsCmd ??
            (settingsCmd = new SettingsButtonHandler());

        private ICommand connectCmd;
        public ICommand ConnectCommand => connectCmd ??
            (connectCmd = new ConnectButtonHandler(MainModel, SettingsModel));

        // the disconnect command just uses the 'CloseOpenConnections' method of MainModel
        private ICommand disconnectCmd;
        public ICommand DisconnectCommand => disconnectCmd ??
            (disconnectCmd = new CommandHandler(() => MainModel.CloseOpenConnections()));

        public FlightDisplayVM(IMainModel main, ISettingsModel settings)
        {
            MainModel = main;
            SettingsModel = settings;

            // connect the server and client upon initialization
            ConnectCommand.Execute(null);
        }
    }
}
