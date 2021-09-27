using FarsiLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCalendar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fa-IR");
            //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

            Thread.CurrentThread.CurrentUICulture = new PersianCultureInfo();
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa-ir");
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
