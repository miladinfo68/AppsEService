using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ResourceControl.DAL
{
    public class RC_UserDBAccess
    {
        //public bool AddNewRC_User(RC_User user)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {                
        //        new SqlParameter("@name", user.Name),
        //        new SqlParameter("@RoleID", user.RoleID),
        //        new SqlParameter("@DaneshID",user.DaneshID)
        //    };
        //    return SqlDBHelper.ExecuteNonQuery("sp_RC_UsersInsert", CommandType.StoredProcedure, parameters); ;
        //}
        //public bool UpdateRC_User(RC_User user)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@ID",user.ID),
        //        new SqlParameter("@name", user.Name),
        //        new SqlParameter("@RoleID", user.RoleID),
        //        new SqlParameter("@DaneshID",user.DaneshID)
        //    };
        //    return SqlDBHelper.ExecuteNonQuery("sp_RC_UsersUpdate", CommandType.StoredProcedure, parameters);
        //}

        public List<RC_User> GetOstadListByDaneshID(int daneshID)
        {
            List<RC_User> userlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@daneshID",daneshID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetOstadListByDaneshID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {                

                    userlist = new List<RC_User>();

                    foreach (DataRow row in table.Rows)
                    {
                        RC_User user = new RC_User();
                        user.ID = Convert.ToInt32(row["code_ostad"]);
                        user.Name = (row["name"] as string) + " " + (row["family"] as string);
                        //user.RoleID = Convert.ToInt32(row["RoleID"]);
                        //user.DaneshID = Convert.ToInt32(row["DaneshID"]);
                        userlist.Add(user);
                    }
                }
            }

            return userlist;
        }

        public RC_User Get_Ostad_Details(int ostadID)
        {
            RC_User user = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ostadID", ostadID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetOstadName]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];

                    user = new RC_User();
                    user.ID = Convert.ToInt32(row["code_ostad"]);
                    user.Name = (row["name"] as string)+" "+(row["family"] as string);                    
                    //user.DaneshID = Convert.ToInt32(row["DaneshID"]);
                }
            }

            return user;
        }

        //public bool DeleteRC_User(int catID)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@ID", catID)
        //    };

        //    return SqlDBHelper.ExecuteNonQuery("sp_RC_UsersDelete", CommandType.StoredProcedure, parameters);
        //}


        public RC_User GetUserDetails(int usrID)
        {
            RC_User user = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userID", usrID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("spgetuserdetail", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];

                    user = new RC_User();

                    user.ID = Convert.ToInt32(row["code_ostad"]);
                    user.Name = row["name"] as string+" "+row["family"] as string;
                    //user.RoleID =Convert.ToInt32(row["RoleID"]);
                    //user.DaneshID = Convert.ToInt32(row["DaneshID"]);
                }
            }

            return user;
        }


        //public List<RC_User> GetRC_UserList()
        //{
        //    List<RC_User> userlist = null;

        //    using (DataTable table = SqlDBHelper.ExecuteSelectCommand("sp_RC_UsersSelectAll", CommandType.StoredProcedure))
        //    {
        //        if (table.Rows.Count > 0)
        //        {
        //            userlist = new List<RC_User>();

        //            foreach (DataRow row in table.Rows)
        //            {
        //                RC_User user = new RC_User();
        //                user.ID = Convert.ToInt32(row["ID"]);
        //                user.Name = row["name"] as string;
        //                user.RoleID =Convert.ToInt32(row["RoleID"]);
        //                user.DaneshID = Convert.ToInt32(row["DaneshID"]);
        //                userlist.Add(user);
        //            }
        //        }
        //    }

        //    return userlist;
        //}

        public RC_User Get_Ostad_DetailsByCourseId(int CourseId)
        {
            RC_User user = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CourseId", CourseId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[Get_Ostad_DetailsByCourseId]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];

                    user = new RC_User();
                    user.ID = Convert.ToInt32(row["code_ostad"]);
                    user.Name = (row["name"] as string) + " " + (row["family"] as string);
                    //user.DaneshID = Convert.ToInt32(row["DaneshID"]);
                }
            }

            return user;
        }
    }
}