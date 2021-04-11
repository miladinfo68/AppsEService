using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ResourceControl.DAL
{
    public class Req_Opt_JuncDBAccess
    {
        public bool AddNewReq_Opt_Junc(Req_Opt_Junc req_opt_junc)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@Req_id", req_opt_junc.Req_id),
                new SqlParameter("@Opt_id", req_opt_junc.Opt_id)
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_req_opt_juncInsert]", CommandType.StoredProcedure, parameters); ;
        }
        public bool UpdateReq_Opt_Junc(Req_Opt_Junc req_opt_junc)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                //new SqlParameter("@ID",req_opt_junc.ID),
                new SqlParameter("@Req_id", req_opt_junc.Req_id),
                new SqlParameter("@Opt_id", req_opt_junc.Opt_id),
                new SqlParameter("@IsActive", req_opt_junc.IsActive)
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_req_opt_juncUpdate]", CommandType.StoredProcedure, parameters);
        }
        public bool DeleteReq_Opt_Junc(int reqID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@reqID", reqID)
            };

            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_req_opt_juncDelete]", CommandType.StoredProcedure, parameters);
        }
        //public Req_Opt_Junc GetReq_Opt_JuncDetails(int req_opt_ID)
        //{
        //    Req_Opt_Junc req_opt_junc = null;

        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@ID", req_opt_ID)
        //    };

        //    using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_Req_Opt_JuncSelect]", CommandType.StoredProcedure, parameters))
        //    {

        //        if (table.Rows.Count == 1)
        //        {
        //            DataRow row = table.Rows[0];


        //            req_opt_junc = new Req_Opt_Junc();

        //            req_opt_junc.ID = Convert.ToInt32(row["ID"]);
        //            req_opt_junc.Req_id = Convert.ToInt32(row["Req_id"]);
        //            req_opt_junc.Opt_id = Convert.ToInt32(row["Opt_id"]);
        //        }
        //    }

        //    return req_opt_junc;
        //}

        //public List<Req_Opt_Junc> GetReq_Opt_JuncList()
        //{
        //    List<Req_Opt_Junc> req_opt_junclist = null;

        //    using (DataTable table = SqlDBHelper.ExecuteSelectCommand("sp_Req_Opt_JuncsSelectAll", CommandType.StoredProcedure))
        //    {
        //        if (table.Rows.Count > 0)
        //        {
        //            req_opt_junclist = new List<Req_Opt_Junc>();

        //            foreach (DataRow row in table.Rows)
        //            {
        //                Req_Opt_Junc req_opt_junc = new Req_Opt_Junc();
        //                req_opt_junc.ID = Convert.ToInt32(row["ID"]);
        //                req_opt_junc.Req_id = Convert.ToInt32(row["Req_id"]);
        //                req_opt_junc.Opt_id = Convert.ToInt32(row["Opt_id"]);
                  
        //                req_opt_junclist.Add(req_opt_junc);
        //            }
        //        }
        //    }

        //    return req_opt_junclist;
        //}


        public List<Req_Opt_Junc> GetReq_Opt_JuncListByReqID(int reqID)
        {
            List<Req_Opt_Junc> req_opt_junclist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ReqID", reqID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_req_opt_juncSelectByReqID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    req_opt_junclist = new List<Req_Opt_Junc>();

                    foreach (DataRow row in table.Rows)
                    {
                        Req_Opt_Junc req_opt_junc = new Req_Opt_Junc();
                        req_opt_junc.ID = Convert.ToInt32(row["ID"]);
                        req_opt_junc.Req_id = Convert.ToInt32(row["Req_id"]);
                        req_opt_junc.Opt_id = Convert.ToInt32(row["Opt_id"]);
                        req_opt_junc.IsActive = Convert.ToBoolean(row["IsActive"]);
                        req_opt_junclist.Add(req_opt_junc);
                    }
                }
            }

            return req_opt_junclist;
        }
    }
    
}