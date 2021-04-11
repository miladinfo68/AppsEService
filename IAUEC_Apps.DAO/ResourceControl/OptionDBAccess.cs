using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResourceControl.Entity;
using System.Data.SqlClient;
using System.Data;

namespace ResourceControl.DAL
{
    public class OptionDBAccess
    {
        public bool AddNewOption(Option option)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@name", option.Name)
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_OptionsInsert]", CommandType.StoredProcedure, parameters); ;
        }
        public bool UpdateOption(Option option)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID",option.ID),
                new SqlParameter("@name", option.Name),
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_OptionsUpdate]", CommandType.StoredProcedure, parameters);
        }


       //****************
        public bool DeleteOption(int optID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", optID)
            };

            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_OptionsDelete]", CommandType.StoredProcedure, parameters);
        }
        //***************

        public List<Option> GetOptionListByReqIDID(int reqID)
        {
            List<Option> optionlist = null;


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@reqID", reqID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_OptionsSelectByReqID]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    optionlist = new List<Option>();

                    foreach (DataRow row in table.Rows)
                    {
                        Option option = new Option();
                        option.ID = Convert.ToInt32(row["ID"]);
                        option.Name = row["name"] as string;
                        optionlist.Add(option);
                    }
                }
            }

            return optionlist;
        }

        //public Option GetOptionDetails(int optID)
        //{
        //    Option option = null;

        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@ID", optID)
        //    };

        //    using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("sp_OptionSelect", CommandType.StoredProcedure, parameters))
        //    {

        //        if (table.Rows.Count == 1)
        //        {
        //            DataRow row = table.Rows[0];


        //            option = new Option();

        //            option.ID = Convert.ToInt32(row["ID"]);
        //            option.Name = row["name"] as string;
        //        }
        //    }

        //    return option;
        //}
        public List<Option> GetOptionList()
        {
            List<Option> optionlist = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[sp_OptionsSelectAll]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    optionlist = new List<Option>();

                    foreach (DataRow row in table.Rows)
                    {
                        Option option = new Option();
                        option.ID = Convert.ToInt32(row["ID"]);
                        option.Name = row["name"] as string;
                  
                        optionlist.Add(option);
                    }
                }
            }

            return optionlist;
        }
        public List<Option> GetOptionListByResID(int resID)
        {
            List<Option> optionlist = null;
           

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@resID", resID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_OptionsSelectByResID]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    optionlist = new List<Option>();

                    foreach (DataRow row in table.Rows)
                    {
                        Option option = new Option();
                        option.ID = Convert.ToInt32(row["ID"]);
                        option.Name = row["name"] as string;
                        //option.IsActive = Convert.ToBoolean(row["IsActive"]);
                        optionlist.Add(option);
                    }                    
                }
            }

            return optionlist;
        }
        public List<Option> GetOptionListByCatID(int catID)
        {
            List<Option> optionlist = null;


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@catID", catID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_OptionsSelectByCatID]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    optionlist = new List<Option>();

                    foreach (DataRow row in table.Rows)
                    {
                        Option option = new Option();
                        option.ID = Convert.ToInt32(row["ID"]);
                        option.Name = row["name"] as string;
                        optionlist.Add(option);
                    }
                }
            }

            return optionlist;
        }
    }
}