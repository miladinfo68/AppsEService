using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceControl.Entity
{
    public class Course
    {
        public int DID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int DaneshID { get;  set; }
        public string saatklass { get;  set; }
        public int? status { get; set; }
        public int? catID { get; set; }
    }
}