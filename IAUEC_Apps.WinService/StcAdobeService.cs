﻿using IAUEC_Apps.Business.Adobe;
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
    partial class StcAdobeService : ServiceBase
    {
        System.Timers.Timer timer;
        DateTime scheduleTime;
        private static readonly object Instancelock = new object();
        public StcAdobeService()
        {
            InitializeComponent();
            timer = new System.Timers.Timer();
            // scheduleTime = DateTime.Today.AddDays(1).AddHours(2); // Schedule to run once a day at 2:00 a.m.
            WriteToFile("call StcAdobeService  Service  is started at " + scheduleTime);

        }
        protected override void OnStart(string[] args)
        {
            WriteToFile("call OnStart Service  is started at " + DateTime.Now);
            timer.Enabled = true;
            // timer.Interval = scheduleTime.Subtract(DateTime.Now).TotalSeconds*1000;
            timer.Interval = 60000 * 60;
            WriteToFile("call OnStart Service is  timer.Interval" + timer.Interval);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            var startTime = DateTime.Now.TimeOfDay;
            WriteToFile("start Service is OnElapsedTime at " + DateTime.Now);
            WriteToFile("start Service is OnElapsedTime startTime at " + startTime);
            //AdobeDefenceBusiness.CreateAllMeetingNotExist();

            var endTime = DateTime.Now.TimeOfDay;
            WriteToFile("end Service is OnElapsedTime at " + DateTime.Now);
            WriteToFile("end Service is  OnElapsedTime endTime at " + endTime);

            //timer.Interval = 86400000 - (endTime - startTime).Milliseconds;
           // WriteToFile(" OnElapsedTime timer.Interval  " + timer.Interval);
        }
        public void WriteToFile(string Message)
        {
            lock (Instancelock)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\LogsCheckExistMeetingAdobe";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\LogsCheckExistMeetingAdobe\\ServiceLogAdobe_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
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
}