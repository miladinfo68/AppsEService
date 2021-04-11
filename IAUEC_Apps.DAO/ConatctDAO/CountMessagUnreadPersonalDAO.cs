using System;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.ConatctDAO
{
  public  class CountMessagUnreadPersonalDAO
    {
        #region Read
        public static DataTable SelectCountUnReadPersonalDA(string idUser)
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
                    cmd.Parameters.Add(new SqlParameter("@StUser", idUser));
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
