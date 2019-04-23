using System;
using System.Collections.Generic;
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
        private IMainModel MainModel { get; }
        private ISettingsModel SettingsModel { get; }

        private ICommand settingsCmd;
        public ICommand SettingsCommand => settingsCmd ??
            (settingsCmd = new SettingsButtonHandler());

        private ICommand connectCmd;
        public ICommand ConnectCommand => connectCmd ??
            (connectCmd = new ConnectButtonHandler(MainModel, SettingsModel));

        private IFlightBoardVM flightBoardVM;
        public IFlightBoardVM FlightBoardVM => flightBoardVM ??
            (flightBoardVM = new FlightBoardVM(MainModel.ServerModel));

        public FlightDisplayVM(IMainModel main, ISettingsModel settings)
        {
            MainModel = main;
            SettingsModel = settings;

            // connect the server and client upon initialization
            ConnectCommand.Execute(null);
        }
    }

}
