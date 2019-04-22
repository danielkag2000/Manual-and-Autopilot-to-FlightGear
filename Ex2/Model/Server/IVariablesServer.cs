using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Model.Server
{
    public delegate void UpdateHandler();

    public interface IVariablesServer
    {
        uint Port { get; set; }
        uint RefreshRate { get; set; }

        bool IsOpen { get; }

        IList<string> PropertyNames { set; get; }
        double this[string propertyName] { get; }

        event UpdateHandler PropertyUpdate;

        void Open();
        void Close();
    }
}
