using MyConnections;
using System;
using System.Data;
using System.Data.SqlClient;


namespace IAUEC_Apps.DAO.ConatctDAO
{
   public class MsgUnreadOstadDAO
    {
        #region Read
        public static DataTable SelectCountUnReadOstadDAO(string user)
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
                    cmd.Parameters.Add(new SqlParameter("@User", user));
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
        public static DataTable SelectConatctUnreadOstadDAO(string user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[Sp_GetCountMsgUnreadOstadByGroup]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@User", user));

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
        public static DataTable DeleteConatctUnreadOstadDAO(string user, int FlagGroup,int Sender=-1)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[contact].[Sp_DeleteConatctUnreadOstad]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@User", user));
                    cmd.Parameters.Add(new SqlParameter("@FlgGroup", FlagGroup));
                    cmd.Parameters.Add(new SqlParameter("@Sender", Sender));

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

