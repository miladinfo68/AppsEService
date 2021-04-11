using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceControl.Entity
{
    [Serializable]
    public class Option
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int IsDeleted { get; set; }

    }
}