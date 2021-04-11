using System;
using System.Data;
using System.Data.SqlClient;

using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.ConatctDAO
{
   public class ContactUnreadGroupDAO
    {
        #region Read
        public static DataTable SelectConatctUnreadGroup(string idGrp)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[contact].[Sp_GetConatctMsgUnreadGroup]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@IdGroup", idGrp));

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
        public static DataTable DeleteConatctUnreadGroup(string IdGrp)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[contact].[Sp_DeleteConatctUnreadGroup]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@IdGrp", IdGrp));

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
