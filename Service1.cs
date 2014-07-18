using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SwitchQueryService
{
    public partial class Service1 : ServiceBase
    {
        DateTime hora1=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,13,33,5);//hora 1 dia de hoy, hora a cambiar
        DateTime hora2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 33, 20);//hora 2 dia de hoy, hora a cambiar
        StreamWriter file;
        Timer timer;
        int interval1,interval2;
      
        //DateTime date=DateTime.Now;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
           
            file = new System.IO.StreamWriter("c:\\testService.txt");
            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true; // Enable it

        }

        protected override void OnStop()
        {
            string lines = "Hello.\r\nWorld.\r\nFrom Service Stop.";
            Console.WriteLine("Hello World On stop");
            // Write the string to a file.
            file.WriteLine(lines);
            file.Close();
        }
       protected void timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            TimeSpan rango;
            file.WriteLine("Fecha ahora= " + DateTime.Now);
            file.WriteLine("Fecha 1: "+hora1);
            file.WriteLine("Fecha 2: " + hora2);
            file.WriteLine("Diferencia de fecha actual a Fecha 1: "+(rango = hora1.Subtract(DateTime.Now)).ToString());
            file.WriteLine("Diferencia de fecha actual a Fecha 2: "+(rango = hora2.Subtract(DateTime.Now)).ToString());

            if (hora1.CompareTo(TimeSpan.Zero)<0)
            {
                timer.Interval=hora2.Millisecond;
                hora2.AddMinutes(1);
            }
            else
            {
                if (hora2.CompareTo(TimeSpan.Zero) < 0)
                {
                    timer.Interval = hora1.Millisecond;
                    hora1.AddMinutes(1);
                }
                else
                {
                    timer.Interval = Math.Min(hora1.Millisecond,hora2.Millisecond);
                    hora1.AddMinutes(1);
                    hora2.AddMinutes(1);
                }
            }
            

            //hora1.AddDays(1);
            //hora2.AddDays(2);

            //interval1 = (hora1.Minute - DateTime.Now.Minute) * 60000;
            //interval2 = (hora2.Minute - DateTime.Now.Minute) * 60000;
            //timer.Interval = Math.Min(interval1, interval2);
            //file.WriteLine("Interval despues= "+timer.Interval);
            file.WriteLine();

            //timer.Interval = 600000;
            

        }

    }
}
