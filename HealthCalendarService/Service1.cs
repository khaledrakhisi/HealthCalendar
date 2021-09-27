using HealthCalendar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCalendarService
{
    public partial class Service1 : ServiceBase
    {
         
        private System.Timers.Timer _timer;      
        private bool isImageSavedForToday;       
        
        public Service1()
        {
            InitializeComponent();            
        }

        private void SelectRandomTheme()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, cls_settings.themes.Length);
            cls_settings.ApplyTheme(cls_settings.themes[randomNumber]);
        }
       
        protected override void OnStart(string[] args)
        {
            try
            {
                cls_utility.Log("Reseting Log file...", false);
            }
            catch { }

            cls_utility.InitSettings();
            cls_utility.Log("Default Settings................... OK");

            cls_utility.Log("Theme :" + cls_settings.themeID);
            //Theme Selection
            if (cls_settings.themeID == 0)//0 == Automatic Theme
            {
                SelectRandomTheme();

            }else if (cls_settings.themeID == -1){//-1 == Custom Theme

            }else{
                cls_settings.ApplyTheme(cls_settings.themes[cls_settings.themeID - 1]);
            }

            if (cls_utility.DoTheRoutine())
            {
                //cls_utility.SaveTheImage();
                isImageSavedForToday = false;
            }            

            try
            {
                _timer = new System.Timers.Timer();
                _timer.Interval = 1 * 60 * 1000; //every x Minute
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
                _timer.Start();

                cls_utility.Log("Scheduler Start................. OK");
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in Start Scheduler." + ex.Message);
            }
            
        }

        protected override void OnStop()
        {
            //cls_utility.Release();
        }        

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            cls_utility.InitSettings(); 

            string sCurrentTime = DateTime.Now.ToString("HH:mm");
            string sReReadyTime = (DateTime.Parse(cls_settings.scheduleTime).AddMinutes(3)).ToString("HH:mm");

            //try
            //{
            //    cls_utility.Log("Checking Scheduling at " + sCurrentTime + " and " + cls_settings.scheduleTime + " 3Mins Later is : " + sReReadyTime + " isImageSavedForToday: " + isImageSavedForToday);
            //}
            //catch
            //{
            //}

            if (sCurrentTime == cls_settings.scheduleTime && !isImageSavedForToday)
            {
                //Theme Selection
                if (cls_settings.themeID == 0)//0 == Automatic Theme
                {
                    SelectRandomTheme();
                }

                if (cls_utility.DoTheRoutine())
                {
                    
                }
                isImageSavedForToday = true;
            }
            else if (sCurrentTime == sReReadyTime)
            {
                isImageSavedForToday = false;
            }            
        }
    }
}
