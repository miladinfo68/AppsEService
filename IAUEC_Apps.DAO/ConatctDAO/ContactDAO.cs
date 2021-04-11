using System;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.ConatctDAO
{
   public class ContactDAO
    {
        
    # region Read
        public static DataTable SelectConatctOstad(string stcode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                { 
       
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_GetOstadDefence]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }


        public static DataTable SelectConatctStudentForType(string stUser ,string typOstad)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_GetContactStudent_" + typOstad + "]", conn)
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
        public static DataTable SelectContactAStudent(string stUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_GetAContactStudentByStcode]", conn)
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
        public static DataTable SelectConatctStudentAll(string stUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_GetContactStudent_OS_All]", conn)
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
        public static DataTable SelectStages(string stcode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Contact].[SP_Getstages]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));

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
