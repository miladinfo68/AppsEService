
using System;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.ConatctDAO
{
    public class ConatctUnreadPersonalDAO
    {
        #region Read
        public static DataTable SelectConatctUnreadPersonal(string stcode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[contact].[Sp_GetConatctMsgUnreadPersonal]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@StUser", stcode));

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
        public static DataTable DeleteConatctUnreadPersoanl(string stcode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[contact].[Sp_DeleteConatctUnreadPersonal]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@StUser", stcode));

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
