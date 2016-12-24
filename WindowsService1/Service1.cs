using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase

    {
        ServLisener l; 
        public Service1()
        {
            InitializeComponent();
            l = new ServLisener();
        }

        protected override void OnStart(string[] args)
        {
            Thread ls;
           
           ls =new Thread(new ThreadStart(l.start));
           ls.Start();
        }

        protected override void OnStop()
        {
            Thread.Sleep(1000);
            l.stop();

        }
    }
}
