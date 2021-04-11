using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceControl.Entity
{
    public class Req_Opt_Junc
    {
        public int ID { get; set; }
        public int Req_id { get; set; }
        public int Opt_id { get; set; }
        public bool IsActive { get; set; }

    }
}