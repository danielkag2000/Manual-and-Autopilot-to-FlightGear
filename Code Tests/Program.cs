using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ex2;
using Ex2.Model.Client;
using Ex2.Model.Server;

namespace Code_Tests
{
    class Program
    {
        static void Main(string[] args)
        {

            Program p = new Program();

            p.OpenServer();
            Console.WriteLine("Opened Server");
            Console.ReadKey();

            p.client.Open();
            string ln;

            while (!(ln = Console.ReadLine()).Equals("s") && p.client.IsOpen)
            {
                p.client.SendLine(ln);
                //Console.WriteLine($"Lon: {p.server.Lon}, Lat: {p.server.Lat}");
            }

            Console.WriteLine("Client Finished");

            p.client.Close();
            p.CloseServer();

            Console.WriteLine("Closed");
            Console.ReadKey();
        }

        IFlightClient client;
        IFlightServer server;
        private Task serverTask;

        Program()
        {
            client = new FlightClient();
            client.IP = "127.0.0.1";
            client.Port = 5402;

            server = new FlightServer();
            server.Port = 5400;
        }

        void OpenServer()
        {
            serverTask = new Task(() =>
            {
                server.Open();
            });

            serverTask.Start();
        }

        void CloseServer()
        {
            server.Close();
            serverTask.Wait();
        }
    }
}
