using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceControl.Entity
{
    public class RequestDateTime
    {
        public int DateTimeId { get; set; }

        public string Date { get; set; }

        
        private TimeSpan starttime;

        public long StartTime
        {
            get { return starttime.Ticks; }
            set { starttime =TimeSpan.FromTicks(value); }
        }


        private TimeSpan endtime;

        public long EndTime
        {
            get { return endtime.Ticks; }
            set { endtime = TimeSpan.FromTicks(value); }
        }

        public int RequestId { get; set; }

        public int ResourceId { get; set; }

        public string ClassName { get; set; }

        public bool MayConflict { get; set; }
    }
}
