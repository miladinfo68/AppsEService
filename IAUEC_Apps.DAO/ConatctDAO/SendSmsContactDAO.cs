using IAUEC_Apps.DAC.Connections;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IAUEC_Apps.DAO.ConatctDAO
{
    public class SendSmsContactDAO
    {

        #region Read
        public static DataTable SelectLogSms(string idGrp, int waiting = -1)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_GetLogSendSms]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ID_Group", idGrp));
                  //  cmd.Parameters.Add(new SqlParameter("@startdate", startdate));
                  //  cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@waiting", waiting));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmd.Dispose();
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable CheckSendedSms(string idGrp)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_CheckSendedSmsToday]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ID_Group", idGrp));
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
        #region insert
        public static DataTable InsertLogSms(string fullName, string idGrp,
                                                    string datePersian, string time,string TextMsg,
                                                    bool Waiting)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_InsertLogSendSms]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ID_Group ", idGrp));
                    cmd.Parameters.Add(new SqlParameter("@fullName", fullName));
                    cmd.Parameters.Add(new SqlParameter("@DatePersian", datePersian));
                    cmd.Parameters.Add(new SqlParameter("@Time", time));
                    cmd.Parameters.Add(new SqlParameter("@TextMsg", TextMsg));
                    cmd.Parameters.Add(new SqlParameter("@Waiting", Waiting));
                    cmd.Parameters.Add("@ID", SqlDbType.Int);

                    cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
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
        public static DataTable UpdateWaitingSendSms(string idGrp, int waiting=-1)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_ChangeWaitingLogSendSmsByID_Group]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ID_Group", idGrp));
                    cmd.Parameters.Add(new SqlParameter("@waiting", waiting));
                    cmd.Parameters.Add("@ID", SqlDbType.Int);

                    cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
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
