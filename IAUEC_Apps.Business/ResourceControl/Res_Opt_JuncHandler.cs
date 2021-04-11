using IAUEC_Apps.DAO;
using IAUEC_Apps.DAO;
using ResourceControl.DAL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceControl.BLL
{
    public class Res_Opt_JuncHandler
    {
        Res_Opt_JuncDBAccess Res_Opt_JuncDB = null;

        public Res_Opt_JuncHandler()
        {
            Res_Opt_JuncDB = new Res_Opt_JuncDBAccess();
        }

        // public List<Res_Opt_Junc> GetRes_Opt_JuncList()
        //{            
        //    return Res_Opt_JuncDB.GetRes_Opt_JuncList();
        //}
         public List<Res_Opt_Junc> GetRes_Opt_JuncListByResID(int resID)
        {          
            return Res_Opt_JuncDB.GetRes_Opt_JuncListByResID(resID);
        }
         public bool UpdateRes_Opt_Junc(Res_Opt_Junc res_opt_junc)
        {          
            return Res_Opt_JuncDB.UpdateRes_Opt_Junc(res_opt_junc);
        }

        //public Res_Opt_Junc GetRes_Opt_JuncDetails(int res_opt_juncID)
        //{
        //    Res_Opt_JuncDBAccess Res_Opt_JuncDB = new Res_Opt_JuncDBAccess();
        //    return Res_Opt_JuncDB.GetRes_Opt_JuncDetails(res_opt_juncID);
        //}

        //public bool DeleteRes_Opt_Junc(int res_opt_juncID)
        //{
        //    return Res_Opt_JuncDB.DeleteRes_Opt_Junc(Res_opt_juncID);
        //}

         public bool AddNewRes_Opt_Junc(Res_Opt_Junc res_opt_junc)
        {
            return Res_Opt_JuncDB.AddNewRes_Opt_Junc(res_opt_junc);
        }
    }
}