using IAUEC_Apps.Business.Conatct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace IAUEC_Apps.WinService
{
    partial class ServiceSmsChat : ServiceBase
    {
        System.Timers.Timer timer;
        DateTime scheduleTime;
        public ServiceSmsChat()
        {
            
            InitializeComponent();
            timer = new System.Timers.Timer();
           
            scheduleTime = DateTime.Today.AddDays(1).AddHours(8); // Schedule to run once a day at 8:00 a.m.
            WriteToFile("call ServiceSmsChat  Service  is started at " + scheduleTime);
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("call OnStart Service  is started at " + DateTime.Now);
            timer.Enabled = true;
            timer.Interval = scheduleTime.Subtract(DateTime.Now).TotalSeconds *1000;

            WriteToFile("call OnStart Service is  timer.Interval" + timer.Interval);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            var startTime = DateTime.Now.TimeOfDay;   
            WriteToFile("start Service is OnElapsedTime at " + DateTime.Now);
            WriteToFile("start Service is OnElapsedTime startTime at " + startTime);
           SendSmsContactBuisnes.SendSmsSchedular();
         
            var endTime = DateTime.Now.TimeOfDay;
            WriteToFile("end Service is OnElapsedTime at " + DateTime.Now);
            WriteToFile("end Service is  OnElapsedTime endTime at " + endTime);

            timer.Interval = 86400000  - (endTime - startTime).Milliseconds;
            WriteToFile(" OnElapsedTime timer.Interval  " + timer.Interval);

        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\LogsSendSmsChat";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\LogsSendSmsChat\\ServiceLogSms_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

    }
}
