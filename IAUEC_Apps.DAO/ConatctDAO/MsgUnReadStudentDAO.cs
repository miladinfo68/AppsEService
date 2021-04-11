using MyConnections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.ConatctDAO
{
  public class MsgUnReadStudentDAO
    {

        #region Insert 

        #endregion

        #region Read
        public static DataTable SelectCountUnReadStudentDAO(string stUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[Sp_GetCountChatUnRead]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@User", stUser));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static DataTable SelectConatctUnreadStudentDAO(string stUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[contact].[Sp_GetCountMsgUnreadOstadByGroup]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@User", stUser));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region Delete
        public static DataTable DeleteConatctUnreadStudentDAO(string stUser ,int flgGroup,int sender= -1)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[contact].[Sp_DeleteConatctUnreadStudent]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stUser", stUser));
                    cmd.Parameters.Add(new SqlParameter("@FlgGroup", flgGroup));
                    cmd.Parameters.Add(new SqlParameter("@Sender", sender));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
