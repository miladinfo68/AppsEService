using System;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using System.Collections.Generic;

namespace IAUEC_Apps.DAO.ConatctDAO
{
  public  class MessageGroupDAO
    {
        #region Read
        public static DataTable SelectMessageGroupDA(string idUser, string idGrp,string idChat="-1")

        {
            try
            {

                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[Sp_GetMessageGroup]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id_User", idUser));
                    cmd.Parameters.Add(new SqlParameter("@ID_Grp", idGrp));
                    cmd.Parameters.Add(new SqlParameter("@id_Chat", idChat));
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
        public static DataTable SelectNewMessageGroupDA(string idChat,string idGrp)

        {
            try
            {

                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_GetNewMsgChat_Group]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id_Chat", idChat));
                    cmd.Parameters.Add(new SqlParameter("@id_Grp", idGrp));
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
        #region Insert
        public static DataTable InsertMessageGroupDAO(string idSender, string message, DateTime dateMessage,
                                                       string datePersianMessage, string timeMessage,string idGrp,
                                                       int flagReplayed = 0, int idReplayed = 0, int flagTypeFile = 1,string format="")


        {

            using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[Sp_InsertMessageGroup]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@St_Sender", idSender));
                    cmd.Parameters.Add(new SqlParameter("@Message", message));
                    cmd.Parameters.Add(new SqlParameter("@DateChat", Convert.ToDateTime(dateMessage)));
                    cmd.Parameters.Add(new SqlParameter("@DatePersianChat", datePersianMessage));
                    cmd.Parameters.Add(new SqlParameter("@TimeChat", timeMessage));
                    cmd.Parameters.Add(new SqlParameter("@ID_Grp", idGrp));
                    cmd.Parameters.Add(new SqlParameter("@FlagReplayed", flagReplayed));
                    cmd.Parameters.Add(new SqlParameter("@ID_ChatReplayed", idReplayed));
                    cmd.Parameters.Add(new SqlParameter("@IDTypeFile", flagTypeFile));
                    cmd.Parameters.Add(new SqlParameter("@formatFile", format));
                    cmd.Parameters.Add("@ChatID_P", SqlDbType.Int);
                    
                    cmd.Parameters["@ChatID_P"].Direction = ParameterDirection.Output;
                  //  cmd.Parameters["@image_p"].Direction = ParameterDirection.Output;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        #endregion
    }
}
