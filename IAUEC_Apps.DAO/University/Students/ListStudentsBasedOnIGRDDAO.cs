using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;
using System.Data;
using System.Data.SqlClient;

namespace IAUEC_Apps.DAO.University.Students
{
    public class ListStudentsBasedOnIGRDDAO
    {
        SqlConnection conn = new SqlConnection(new AmozeshConnection().con_sida);
        
        #region read
        public DataTable GetVazNom()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_GetVaznom";
            cmd.CommandType = CommandType.StoredProcedure;
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
            {
                throw;
            }
            return dt;
        }
        public DataTable GetNameVazNom( int StatusScore)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Students].[SP_GetNameVaznom]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatusScore", StatusScore);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetReportIGRDInfo(int Daneshkade , int Field , string Salvorod , string stcode , int NimsalVorod , int Dorpar , int Degree  , int Order , int Sex , int Vazkol , int Isargar, int TypeAccepted, int IS)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Students].[SP_ReportStudent]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field",Field);
            cmd.Parameters.Add("@SalVorod",Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod",NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar",Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree",Degree);
            cmd.Parameters.Add("@Vazkol",Vazkol);
            cmd.Parameters.Add("@order",Order);
            cmd.Parameters.Add("@Isargar",Isargar);
            cmd.Parameters.Add("@IS", IS);
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
            {
                throw;
            }
            return dt;
        }

        public DataTable GetReportGuestStudents(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Order, int Sex, DataTable Vazkol, int TypeAccepted,  string Term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_GuestStudents";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);
            cmd.Parameters.Add("@Vazkol", Vazkol);
            cmd.Parameters.Add("@order", Order);
           
            cmd.Parameters.Add("@Term", Term);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetReportTransferStudents(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, DataTable Vazkol, int TypeAccepted, string Term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Students].[SP_TransferStudents]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);
            cmd.Parameters.AddWithValue("@Vazkol", Vazkol);
            cmd.Parameters["@Vazkol"].SqlDbType = SqlDbType.Structured;
            cmd.Parameters["@Vazkol"].TypeName = "dbo.[integer_list_tbltype]";
            cmd.Parameters.Add("@Term", Term);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetReportAddressTelStudents(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Order, int Sex, int Vazkol, int Isargar, int TypeAccepted, int IS, string Term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Students].[SP_AddressTelStudents]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);
            cmd.Parameters.Add("@Vazkol", Vazkol);
            cmd.Parameters.Add("@order", Order);
            cmd.Parameters.Add("@Isargar", Isargar);
            cmd.Parameters.Add("@IS", IS);
            cmd.Parameters.Add("@Term", Term);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetReportLackOfStudents(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree,  int Sex,  int TypeAccepted, string Term , DataTable Vazkol , int type)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_LackOfStudents";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);
            cmd.Parameters.Add("@Vazkol", Vazkol);
            cmd.Parameters["@Vazkol"].SqlDbType = SqlDbType.Structured;
            cmd.Parameters["@Vazkol"].TypeName = "dbo.[integer_list_tbltype]";
            cmd.Parameters.Add("@Term", Term);
            cmd.Parameters.Add("@Type", type );
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetIncompleteStudents(DataTable Vazkol,int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int TypeAccepted, int naghsType, string naghsDate, int termstatus, string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Students].[Sp_IncompleteStudents]";
            cmd.CommandType = CommandType.StoredProcedure;         
            cmd.Parameters.AddWithValue("@Vazkol", Vazkol);
            cmd.Parameters["@Vazkol"].SqlDbType = SqlDbType.Structured;
            cmd.Parameters["@Vazkol"].TypeName = "dbo.[integer_list_tbltype]";
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);           
            cmd.Parameters.Add("@naghsType", naghsType);
            cmd.Parameters.Add("@naghsDate", naghsDate);
            cmd.Parameters.Add("@termstatus", termstatus);
            cmd.Parameters.Add("@term", term);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetstudentsGeneralInfo(DataTable Vazkol, int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int TypeAccepted)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Students].[Sp_GetstudentsGeneralInfo]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vazkol", Vazkol);
            cmd.Parameters["@Vazkol"].SqlDbType = SqlDbType.Structured;
            cmd.Parameters["@Vazkol"].TypeName = "dbo.[integer_list_tbltype]";
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);
            
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetStatusScoreStudent(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int Isargar, int TypeAccepted, int IS, int Vazkol , int VaziatNomre , string Term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_StatusScoreStudents";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Vazkol", Vazkol);
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);
            
            cmd.Parameters.Add("@Isargar", Isargar);
            cmd.Parameters.Add("@IS", IS);
            cmd.Parameters.Add("@Vaznom", VaziatNomre);
            cmd.Parameters.Add("@Term", Term);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetStatusTermStudent(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int TypeAccepted,  DataTable Vazkol ,string Term , int VaziatTerm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_StatusTermStudents";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);
            cmd.Parameters.AddWithValue("@Vazkol", Vazkol);
            cmd.Parameters["@Vazkol"].SqlDbType = SqlDbType.Structured;
            cmd.Parameters["@Vazkol"].TypeName = "dbo.[integer_list_tbltype]";
            cmd.Parameters.Add("@Term", Term);
            cmd.Parameters.Add("@VazTerm", VaziatTerm);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable Moadelsazi(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int TypeAccepted, DataTable Vazkol, string Term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_Moadelsazi";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);
            cmd.Parameters.AddWithValue("@Vazkol", Vazkol);
            cmd.Parameters["@Vazkol"].SqlDbType = SqlDbType.Structured;
            cmd.Parameters["@Vazkol"].TypeName = "dbo.[integer_list_tbltype]";
            cmd.Parameters.Add("@Term", Term);
          
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable Sabtenambatakhir(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int TypeAccepted, DataTable Vazkol, string Term,int vazeterm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_GetStudentSabtenambatakhir";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);
            cmd.Parameters.AddWithValue("@Vazkol", Vazkol);
            cmd.Parameters["@Vazkol"].SqlDbType = SqlDbType.Structured;
            cmd.Parameters["@Vazkol"].TypeName = "dbo.[integer_list_tbltype]";
            cmd.Parameters.Add("@Term", Term);
            cmd.Parameters.Add("@VazTerm", vazeterm);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetFormStudentsGuest(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int Isargar, int TypeAccepted, int IS, int Vazkol, string Term, string ShomareName , string TarikhName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandTimeout=3000;
            cmd.CommandText = "Students.SP_FormStudentsGuest";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Danesh", Daneshkade);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", Salvorod);
            cmd.Parameters.Add("@StCode", stcode);
            cmd.Parameters.Add("@NimsalVorod", NimsalVorod);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@dorpar", Dorpar);
            cmd.Parameters.Add("@Paziresh", TypeAccepted);
            cmd.Parameters.Add("@Degree", Degree);
            cmd.Parameters.Add("@Vazkol", Vazkol);
            cmd.Parameters.Add("@Isargar", Isargar);
            cmd.Parameters.Add("@IS", IS);
            cmd.Parameters.Add("@Term", Term);
            cmd.Parameters.Add("@ShomareName", ShomareName);
            cmd.Parameters.Add("@TarikhName", TarikhName);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetStudentIsargar(string stcode, int Degree, int Sex, int Education, int Field, string SalVorod, int StatusStu)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_Martyr";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@stcode", stcode);
            cmd.Parameters.Add("@Degree", Degree);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@Education", Education);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", SalVorod);
            cmd.Parameters.Add("@StatusStu", StatusStu);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetStudentBehzisti(string stcode, int Degree, int Sex, int Education, int Field, string SalVorod, int StatusStu)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Students.SP_UnderWelfare";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@stcode", stcode);
            cmd.Parameters.Add("@Degree", Degree);
            cmd.Parameters.Add("@Sex", Sex);
            cmd.Parameters.Add("@Education", Education);
            cmd.Parameters.Add("@Field", Field);
            cmd.Parameters.Add("@SalVorod", SalVorod);
            cmd.Parameters.Add("@StatusStu", StatusStu);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable ConvertNumberToWord(string Mark)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText="Students.SP_ConvertNumberToWord";
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.Connection=conn;
            cmd.Parameters.AddWithValue("@mark", Mark);
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
       
        #endregion
        #region insert
       
        #endregion
      
    }
}
