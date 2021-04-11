using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.Adobe;
using System.Data;
using IAUEC_Apps.DTO.AdobeClasses;

namespace IAUEC_Apps.Business.Adobe
{
    public class SettingBusiness
    {
        SettingDAO s = new SettingDAO();
        #region Read
        public DataTable GetSettingByTerm(string tterm)
        {
            return s.GetSettingByTerm(tterm);
        }
        public SettingDTO GetSettingByTermC(string tterm)
        {
            DataTable dt = s.GetSettingByTerm(tterm);
            SettingDTO set = new SettingDTO();
            set.ConName = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                set.Term = tterm;
                set.DBConName = dt.Rows[i]["DB_Con"].ToString();
                set.DomainName = dt.Rows[i]["DomainName"].ToString();
                set.ConName = dt.Rows[i]["ConName"].ToString();
                set.hpass = dt.Rows[i]["hpass"].ToString();
                set.vpass = dt.Rows[i]["vpass"].ToString();
                set.vName = dt.Rows[i]["vName"].ToString();
                set.hName = dt.Rows[i]["hName"].ToString();
                set.aPass = dt.Rows[i]["aPass"].ToString();
                //if (dt.Rows[i]["version"] != null)
                //    set.version = dt.Rows[i]["version"].ToString();
            }
            return set;
        }
        #endregion
    }
}
