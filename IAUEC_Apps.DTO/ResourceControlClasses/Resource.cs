using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceControl.Entity
{
    [Serializable]
    public class Resource
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public bool IsDeleted { get; set; }
        public int Capacity { get; set; }

        public string CategoryName { get; set; }
    }
}