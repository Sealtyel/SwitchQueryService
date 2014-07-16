using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SwitchQueryService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            string lines = "Hello.\r\nWorld.\r\nFrom Service.";

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\testService.txt");
            file.WriteLine(lines);
            file.Close();
        }

        protected override void OnStart(string[] args)
        {
            //Console.WriteLine("Hello World from a service");
            string lines = "Hello.\r\nWorld.\r\nFrom Service Start.";

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\testServiceStart.txt");
            file.WriteLine(lines);
            file.Close();

        }

        protected override void OnStop()
        {
            string lines = "Hello.\r\nWorld.\r\nFrom Service Stop.";

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\testServiceStop.txt");
            file.WriteLine(lines);
            file.Close();
        }
    }
}
