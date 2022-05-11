using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Web_Ataskaitos
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
       
        private static System.Timers.Timer bTimer;

        static void Main(string[] args)
        {

            channel chan = new channel();
            
            chan.channel_Load();

           // SetTimer();

           // Console.WriteLine("\nPress the Enter key to exit the application...\n");
            //Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            //Console.ReadLine();
            
          //  bTimer.Stop();
           // bTimer.Dispose();
            //radio rds = new radio();
            //rds.RDS_Load()
        }
    }
}
