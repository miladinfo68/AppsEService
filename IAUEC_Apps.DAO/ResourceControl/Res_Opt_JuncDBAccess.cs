using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ResourceControl.DAL
{
    public class Res_Opt_JuncDBAccess
    {
        public bool AddNewRes_Opt_Junc(Res_Opt_Junc res_opt_junc)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@Res_id", res_opt_junc.Res_id),
                new SqlParameter("@Opt_id", res_opt_junc.Opt_id),
                
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_res_opt_juncInsert]", CommandType.StoredProcedure, parameters); ;
        }
        public bool UpdateRes_Opt_Junc(Res_Opt_Junc res_opt_junc)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
               // new SqlParameter("@ID",res_opt_junc.ID),
                new SqlParameter("@Res_Id", res_opt_junc.Res_id),
                new SqlParameter("@Opt_Id", res_opt_junc.Opt_id),
                new SqlParameter("@IsActive",res_opt_junc.IsActive)
                
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_res_opt_juncUpdate]", CommandType.StoredProcedure, parameters);
        }
        public bool DeleteRes_Opt_Junc(int res_opt_ID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", res_opt_ID)
            };

            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_res_opt_juncDelete]", CommandType.StoredProcedure, parameters);
        }
        public List<Res_Opt_Junc> GetRes_Opt_JuncListByResID(int resID)
        {
            List<Res_Opt_Junc> res_opt_junclist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ResID", resID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_res_opt_juncSelectByResID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    res_opt_junclist = new List<Res_Opt_Junc>();

                    foreach (DataRow row in table.Rows)
                    {
                        Res_Opt_Junc res_opt_junc = new Res_Opt_Junc();
                        res_opt_junc.ID = Convert.ToInt32(row["ID"]);
                        res_opt_junc.Res_id = Convert.ToInt32(row["Res_id"]);
                        res_opt_junc.Opt_id = Convert.ToInt32(row["Opt_id"]);
                        res_opt_junc.IsActive = Convert.ToBoolean(row["IsActive"]);
                        res_opt_junclist.Add(res_opt_junc);
                    }
                }
            }

            return res_opt_junclist;
        }
        //No need for selecting all records in junction table
        //public List<Res_Opt_Junc> GetRes_Opt_JuncList()
        //{
        //    List<Res_Opt_Junc> res_opt_junclist = null;

        //    using (DataTable table = SqlDBHelper.ExecuteSelectCommand("sp_res_opt_juncSelect", CommandType.StoredProcedure))
        //    {
        //        if (table.Rows.Count > 0)
        //        {
        //            res_opt_junclist = new List<Res_Opt_Junc>();

        //            foreach (DataRow row in table.Rows)
        //            {
        //                Res_Opt_Junc res_opt_junc = new Res_Opt_Junc();
        //                res_opt_junc.ID = Convert.ToInt32(row["ID"]);
        //                res_opt_junc.Res_id = Convert.ToInt32(row["Res_id"]);
        //                res_opt_junc.Opt_id = Convert.ToInt32(row["Opt_id"]);
        //                res_opt_junc.IsActive = Convert.ToBoolean(row["IsACtive"]);

        //                res_opt_junclist.Add(res_opt_junc);
        //            }
        //        }
        //    }

        //    return res_opt_junclist;
        //}
    }
}