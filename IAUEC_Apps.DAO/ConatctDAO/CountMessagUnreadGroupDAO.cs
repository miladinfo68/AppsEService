using System;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
namespace IAUEC_Apps.DAO.ConatctDAO
{
  public  class CountMessagUnreadGroupDAO
    {
        #region Read
        public static DataTable SelectCountUnReadGroupDAO(string idGroup)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[Sp_GetCountChatUnReadGroup]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@IdGrp", idGroup));
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
