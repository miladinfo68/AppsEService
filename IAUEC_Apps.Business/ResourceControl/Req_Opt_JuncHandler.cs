using ResourceControl.DAL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceControl.BLL
{
    public class Req_Opt_JuncHandler
    {
         Req_Opt_JuncDBAccess Req_Opt_JuncDB = null;

        public Req_Opt_JuncHandler()
        {
            Req_Opt_JuncDB = new Req_Opt_JuncDBAccess();
        }

        //public List<Req_Opt_Junc> GetReq_Opt_JuncList()
        //{
        //    return Req_Opt_JuncDB.GetReq_Opt_JuncList();
        //} 
         public List<Req_Opt_Junc> GetReq_Opt_JuncListByReqID(int reqID)
        {
            return Req_Opt_JuncDB.GetReq_Opt_JuncListByReqID(reqID);
        }
         public bool UpdateReq_Opt_Junc(Req_Opt_Junc req_opt_junc)
        {
            return Req_Opt_JuncDB.UpdateReq_Opt_Junc(req_opt_junc);
        }

        //static public Req_Opt_Junc GetReq_Opt_JuncDetails(int req_opt_juncID)
        //{
        //    Req_Opt_JuncDBAccess Req_Opt_JuncDB = new Req_Opt_JuncDBAccess();
        //    return Req_Opt_JuncDB.GetReq_Opt_JuncDetails(req_opt_juncID);
        //}

        public bool DeleteReq_Opt_Junc(int reqID)
        {
            return Req_Opt_JuncDB.DeleteReq_Opt_Junc(reqID);
        }

        public bool AddNewReq_Opt_Junc(Req_Opt_Junc req_opt_junc)
        {
            return Req_Opt_JuncDB.AddNewReq_Opt_Junc(req_opt_junc);
        }
    }
}