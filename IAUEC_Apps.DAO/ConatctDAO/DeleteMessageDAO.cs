using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.ConatctDAO
{
    public class DeleteMessageDAO
    {
        public static DataTable SelectCheckforDeleteMsgGrp(string idGrp, string idChat, string idSender)

        {
            try
            {

                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("[Contact].[Sp_CheckDeleteMessageGroup]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@groupId", idGrp));
                    cmd.Parameters.Add(new SqlParameter("@chatId", idChat));
                    cmd.Parameters.Add(new SqlParameter("@sender", idSender));
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
        public static DataTable SelectCheckforDeleteMsgPersonal(string idReciver, string idChat, string idSender)

        {
            try
            {

                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("[Contact].[Sp_CheckDeleteMessagePersonal]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@reciverId", idReciver));
                    cmd.Parameters.Add(new SqlParameter("@chatId", idChat));
                    cmd.Parameters.Add(new SqlParameter("@sender", idSender));
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
        public static DataTable DeleteMsg(string idChat)

        {
            try
            {

                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[Sp_DeleteMessage]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@chatId", idChat));
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
    }
}
