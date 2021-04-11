using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceControl.Entity
{
    public class RC_User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RoleID { get; set; }
        public int DaneshID { get; set; }
    }
}