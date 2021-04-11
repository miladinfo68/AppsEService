using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.CommonDAO;

namespace IAUEC_Apps.Business.Common
{
    public class AcceptRuleBusiness
    {
        CommonDAO cd = new CommonDAO();

        public DataTable Rule( int App_ID)
        {
            return cd.AcceptEmailRule(App_ID);
        }
    }
}
