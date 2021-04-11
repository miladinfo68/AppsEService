using IAUEC_Apps.DAO.ResourceControl;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.ResourceControl
{
    public class DaneshkadeHandler
    {
        DaneshKadeDBAccess daneshDB = null;

        public DaneshkadeHandler()
        {
            daneshDB = new DaneshKadeDBAccess();
        }

        public List<Daneshkade> GetAllDaneshkade()
        {
            return daneshDB.GetAllDaneshkade();
        }
    }
}
