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
       static ServLisener l;
        Thread ls;
        public Service1()
        {
            InitializeComponent();
            l = new ServLisener();
            this.CanPauseAndContinue = true;

        }

        protected override void OnStart(string[] args)
        {

           
           ls =new Thread(new ThreadStart(l.start));
           ls.Start();
          
        }

        protected override void OnContinue()
        {
            ls.Resume();
        }
        protected override void OnPause()
        {
            ls.Suspend();
            
        }
        protected override void OnStop()
        {
            l.stop();
            

        }
    }
}
