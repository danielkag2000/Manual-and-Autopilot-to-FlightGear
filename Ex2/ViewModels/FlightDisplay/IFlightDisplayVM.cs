using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ex2.ViewModels.FlightDisplay
{
    public interface IFlightDisplayVM
    {
        ICommand SettingsCommand { get; }
        ICommand ConnectCommand { get; }

        IFlightBoardVM FlightBoardVM { get; }
    }
}
