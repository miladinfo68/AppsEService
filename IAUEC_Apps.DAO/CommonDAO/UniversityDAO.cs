using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.CommonDAO
{
   public class UniversityDAO
   {
       SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
       SqlConnection con2 = new SqlConnection(new AmozeshConnection().con_sida);
       #region Read

       public DataTable GetStudentPic(string stcode)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           cmd.CommandText="DBO.SP_GetStudentPic";
           cmd.CommandType=CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@stcode",stcode);
           DataTable dt = new DataTable();
           try 
           {
               conn.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               conn.Close();

           }
           catch(Exception)
           {throw;}
           return dt;
       }

       public DataTable GetTermJary()
       {
           SqlCommand cmdterm = new SqlCommand();
           cmdterm.Connection = conn;
           cmdterm.CommandText = "dbo.GetTermJary";
           cmdterm.CommandType = CommandType.StoredProcedure;
           DataTable dtterm = new DataTable();
           try 
           {
               conn.Open();
               SqlDataReader rdr;
               rdr = cmdterm.ExecuteReader();
               dtterm.Load(rdr);
               conn.Close();
           }
           catch(Exception)
           { throw; }
           return dtterm;
       }

       public DataTable GetNimsalJary()
       {
           SqlCommand cmdterm = new SqlCommand();
           cmdterm.Connection = conn;
           cmdterm.CommandText = "dbo.[GetNimsalJary]";
           cmdterm.CommandType = CommandType.StoredProcedure;
           DataTable dtterm = new DataTable();
           try
           {
               conn.Open();
               SqlDataReader rdr;
               rdr = cmdterm.ExecuteReader();
               dtterm.Load(rdr);
               conn.Close();
           }
           catch (Exception)
           { throw; }
           return dtterm;
       }

       public DataTable GetTop50Student()
       {
           SqlCommand cmdGetTop = new SqlCommand();
           cmdGetTop.Connection = con2;
           cmdGetTop.CommandText = "dbo.SP_GetTop50Student";
           cmdGetTop.CommandType = CommandType.StoredProcedure;

           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmdGetTop.ExecuteReader();
               dt.Load(rdr);
               con2.Close();

           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       // Term Jari
       public DataTable GetFieldByDepartman(int Departman)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           cmd.CommandText = "[dbo].[SP_GetFieldByDepartman]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Departman", Departman);
           DataTable dt = new DataTable();
           try
           {
               conn.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               conn.Close();

           }
           catch
           {
               throw;
           }
           return dt;
       }

       public string GetStudentMobileByStcode(string userId)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           cmd.CommandText = "[dbo].[SP_GetStudentMobileByStcode]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@userId", userId);
           string mobile = "";
           try
           {
               conn.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               while (rdr.Read())
               {
                   mobile = rdr.GetString(0);
               }
               conn.Close();

           }
           catch
           {
               throw;
           }
           return mobile;
       }

       public string GetProfMobileByCode(string userId)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           cmd.CommandText = "[dbo].[SP_GetProfMobileByStcode]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@userId", userId);
           string mobile = "";
           try
           {
               conn.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               while (rdr.Read())
               {
                   mobile = rdr.GetString(0);
               }
               conn.Close();

           }
           catch
           {
               throw;
           }
           return mobile;
       }
       #endregion

       
   }
}
