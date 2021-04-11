using System;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using System.Collections.Generic;

namespace IAUEC_Apps.DAO.ConatctDAO
{
    public class MessagePersonalDAO
    {
        #region Read
        public static DataTable SelectMessagePersonalDA(string idUser, string idContact,string idChat="-1")
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[Sp_GetMessagePersonal]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@StUser", idUser));
                    cmd.Parameters.Add(new SqlParameter("@StContact", idContact));
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
        public static DataTable SelectNewMessagePersonalDA(string idChat, string idSender, string idReciver)

        {
            try
            {

                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_GetNewMsgChat_Personal]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id_Chat", idChat));
                    cmd.Parameters.Add(new SqlParameter("@id_Sender", idSender));
                    cmd.Parameters.Add(new SqlParameter("@id_Reciver", idReciver));                    
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
        public static DataTable InsertMessagePersonalDAO(string idSender, string idReciver, string message, DateTime dateMessage,
                                                       string datePersianMessage, string timeMessage,
                                                       int flagReplayed = 0, int idReplayed = 0,int flagTypeFile = 1,string format="")


        {

            using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[Sp_InsertMessagePersonal]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@St_Sender", idSender));
                    cmd.Parameters.Add(new SqlParameter("@St_Reciver", idReciver));
                    cmd.Parameters.Add(new SqlParameter("@Message", message));
                    cmd.Parameters.Add(new SqlParameter("@DateChat", Convert.ToDateTime(dateMessage)));
                    cmd.Parameters.Add(new SqlParameter("@DatePersianChat", datePersianMessage));
                    cmd.Parameters.Add(new SqlParameter("@TimeChat", timeMessage));
                    cmd.Parameters.Add(new SqlParameter("@FlagReplayed", flagReplayed));
                    cmd.Parameters.Add(new SqlParameter("@ID_ChatReplayed", idReplayed));
                    cmd.Parameters.Add(new SqlParameter("@IDTypeFile", flagTypeFile));
                    cmd.Parameters.Add(new SqlParameter("@formatFile", format));
                    cmd.Parameters.Add("@ChatID_P", SqlDbType.Int);
                //    cmd.Parameters.Add("@image_p", SqlDbType.VarBinary);
                    cmd.Parameters["@ChatID_P"].Direction = ParameterDirection.Output;
                   // cmd.Parameters["@image_p"].Direction = ParameterDirection.Output;
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