using SwitchTracking;
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
        DateTime hora1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 22, 0);//hora 1 dia de hoy, hora a cambiar
        DateTime hora2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 29, 0);//hora 2 dia de hoy, hora a cambiar
        StreamWriter file;
        Timer timer;
        int corridas=0;
        int interval1, interval2;

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
            try
            {


                if (corridas>=1)
                {
                    SwitchQuery.spSwitchProcess();
                   
                }
                corridas++;
                file.WriteLine("Fecha actual= " + DateTime.Now);
                TimeSpan rango1, rango2;
                
                //file.WriteLine("Fecha 1: " + hora1);
                //file.WriteLine("Fecha 2: " + hora2);
                //file.WriteLine("Diferencia de fecha actual a Fecha 1: " + (rango1 = hora1.Subtract(DateTime.Now)).ToString());
                //file.WriteLine("Diferencia de fecha actual a Fecha 2: " + (rango2 = hora2.Subtract(DateTime.Now)).ToString());
                rango1 = hora1.Subtract(DateTime.Now);
                rango2 = hora2.Subtract(DateTime.Now);
                if (TimeSpan.Compare(rango1, TimeSpan.Zero) == -1)//la hora 1 ya paso
                {
                    file.WriteLine("Hora 1 ya paso");
                    timer.Interval = hora2.Millisecond;
                    hora1 = hora1.AddHours(1);

                }
                else
                {
                    if (TimeSpan.Compare(rango2, TimeSpan.Zero) == -1)//la hora 2 ya paso
                    {
                        file.WriteLine("Hora 2 ya paso");
                        timer.Interval = hora1.Millisecond;
                        hora2 = hora2.AddHours(1);
                    }
                    else
                    {
                        timer.Interval = Math.Min(rango1.TotalMilliseconds, rango2.TotalMilliseconds);
                        //file.WriteLine("Nuevo Intervalo "+timer.Interval/1000+" segundos");
                        if (TimeSpan.Compare(rango1, rango2) == -1)
                        {
                            hora1 = hora1.AddHours(1);
                        }
                        else
                        {
                            hora2 = hora2.AddHours(1);
                        }
                    }
                }

                file.WriteLine();


            }
            catch (Exception ex)
            {
                file.WriteLine(ex.GetBaseException());
                
            }


        }
    }
}
