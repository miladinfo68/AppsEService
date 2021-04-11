using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceControl.Entity
{
    public class mainViewModel
    {
        public int WaitingForSendCount { get; set; }

        public int SentCount { get; set; }

        public int ApprovedCount { get; set; }

        public int DeniedCount { get; set; }

        public int InformedCount { get; set; }

        public int LostCount { get; set; }
    }
}
