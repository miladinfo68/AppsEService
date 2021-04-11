using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.CommonDAO
{
    public class StuPresentDAO
    {
        //Test
        SqlConnection conn_Sida = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection conn_Adobe = new SqlConnection(new AdobeConnection().AdobeconnectionString);
        SqlConnection conn_AdobeOld = new SqlConnection(new AdobeConnection().VCconnectionString);
        SqlConnection conn_AdobeLive = new SqlConnection(new AdobeConnection().liveconnectionString);
        SqlConnection conn_class = new SqlConnection(new AdobeConnection().classconnectionString);
        SqlConnection conn_vc_New = new SqlConnection(new AdobeConnection().vc_new_connectionString);

        #region Read

        public DataTable GetStudentInfo(string StudentCode, string LastName, string FirstName, string tterm)
        {            
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;            
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_StudentInfo_ByStcodeLastnameFirstname]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Stcode", SqlDbType.VarChar);
            cmd.Parameters["@Stcode"].Value = StudentCode;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar);
            cmd.Parameters["@LastName"].Value = LastName;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar);
            cmd.Parameters["@FirstName"].Value = FirstName;
            cmd.Parameters.Add("@tterm", SqlDbType.VarChar);
            cmd.Parameters["@tterm"].Value = tterm;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();
            return dt;
        }

        public DataTable GetTotalTimeInAllClassesByStcode(string StudentCode,string term)
        {
            SqlConnection conn = new SqlConnection();
            Adobe.SettingDAO setting = new Adobe.SettingDAO();
            conn.ConnectionString = setting.getAdobeConnectionString(term);
            //if (con == "vc")
            //{
            //    conn.ConnectionString = new AdobeConnection().VCconnectionString;
            //}
            //if (con == "live")
            //{
            //    conn = conn_AdobeLive;
            //}
            //else if (con == "class")
            //{
            //    conn = conn_class;
            //}
            //else if (con == "vc_new")
            //{
            //    conn = conn_vc_New;
            //}
            //else if (con == "vc951")
            //{
            //    conn.ConnectionString = new AdobeConnection().vc_951;
            //}
            //else if (con == "vc952")
            //{
            //    conn.ConnectionString = new AdobeConnection().vc_952;
            //}
            //else if (con == "vc961")
            //{
            //    conn.ConnectionString = new AdobeConnection().vc_961;
            //}
            //else if (con == "vc962")
            //{
            //    conn.ConnectionString = new AdobeConnection().vc_962;
            //}
            DataTable dt = new DataTable();
            //if (con != "vc")
            //{
                    SqlDataReader rdr = null;
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception)
                    {
                        dt.TableName = "Exception";
                        return dt;
                    }
                    
                    SqlCommand cmd = new SqlCommand("R_GetTotalTimeInAllClassesByStcode", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@stcode", SqlDbType.VarChar);
                    cmd.Parameters["@stcode"].Value = StudentCode;
         

                    rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                    rdr.Dispose();
                    cmd.Connection.Close();
                    cmd.Dispose();
                    conn.Close();

            //}
          
            return dt;
        }
        public DataTable GetTotalTime(string StudentCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
           
            SqlConnection conn = conn_Adobe;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTotalTimeinMeetingsByStcode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Stcode", SqlDbType.NVarChar);
            cmd.Parameters["@Stcode"].Value = StudentCode;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }
        public DataTable GetTotalTime(string StudentCode, DateTime TermTimeStart, DateTime TermTimeEnd, string term)
        {

            SqlConnection conn = new SqlConnection();
            Adobe.SettingDAO setting = new Adobe.SettingDAO();
            conn.ConnectionString = setting.getAdobeConnectionString(term);

            //if (con == "Live")
            //{
            //    conn = conn_AdobeLive;
            //}
            //else if (con=="Class")
            //{
            //    conn = conn_class;
            //}
            //else if (con == "vc_new")
            //{
            //    conn = conn_vc_New;
            //}
            //else if (con == "vc951")
            //{
            //    conn.ConnectionString = new AdobeConnection().vc_951;
            //}
            //else if (con == "vc952")
            //{
            //    conn.ConnectionString = new AdobeConnection().vc_952;
            //}
            //else if (con == "vc961")
            //{
            //    conn.ConnectionString = new AdobeConnection().vc_961;
            //}
            //else if (con == "vc962")
            //{
            //    conn.ConnectionString = new AdobeConnection().vc_962;
            //}
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTotalTimeinMeetingsByStcode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Stcode", SqlDbType.NVarChar);
            cmd.Parameters["@Stcode"].Value = StudentCode;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }
        public DataTable GetTotalTime_Old(string StudentCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            SqlConnection conn = conn_AdobeOld;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTotalTimeinMeetingsByStcode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Stcode", SqlDbType.NVarChar);
            cmd.Parameters["@Stcode"].Value = StudentCode;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }

        public DataTable GetTotalTime93942(string StudentCode)
        {
            SqlConnection conn = conn_AdobeLive;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTotalTimeinMeetingsByStcode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Stcode", SqlDbType.NVarChar);
            cmd.Parameters["@Stcode"].Value = StudentCode;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }

        public DataTable GetTime(string StudentCode, string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            SqlConnection conn = conn_Adobe;

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTotalTimeinMeetingsByStcodeClassCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Stcode", SqlDbType.NVarChar);
            cmd.Parameters["@Stcode"].Value = StudentCode;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }
        public DataTable GetStudentTimeMeetingByClassCode(string StudentCode, string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd,string con)
        {
            SqlConnection conn = new SqlConnection();
            if (con == "Live")
            {
                conn = conn_AdobeLive;
            }
            else if (con == "Class")
            {
                conn = conn_class;
            }
            else if (con == "vc_new")
            {
                conn = conn_vc_New;
            }
            else if (con == "vc951")
            {
                conn.ConnectionString = new AdobeConnection().vc_951;
            }
            else if (con == "vc952")
            {
                conn.ConnectionString = new AdobeConnection().vc_952;
            }
            else if (con == "vc961")
            {
                conn.ConnectionString = new AdobeConnection().vc_961;
            }
            else if (con == "vc962")
            {
                conn.ConnectionString = new AdobeConnection().vc_962;
            }
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTotalTimeinMeetingsByStcodeClassCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Stcode", SqlDbType.NVarChar);
            cmd.Parameters["@Stcode"].Value = StudentCode;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }
        public DataTable GetTime_Old(string StudentCode, string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            SqlConnection conn = conn_AdobeOld;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTotalTimeinMeetingsByStcodeClassCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Stcode", SqlDbType.NVarChar);
            cmd.Parameters["@Stcode"].Value = StudentCode;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }

        public DataTable GetTime93942(string StudentCode, string ClassCode)
        {
            SqlConnection conn = conn_AdobeLive;

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTotalTimeinMeetingsByStcodeClassCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Stcode", SqlDbType.NVarChar);
            cmd.Parameters["@Stcode"].Value = StudentCode;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }

        public DataTable GetStudentClassInfoByTerm(string tterm, string stcode)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("SP_StudentClassesByTerm", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@term", SqlDbType.VarChar);
            cmd.Parameters["@term"].Value = tterm;
            cmd.Parameters.Add("@stcode", SqlDbType.VarChar);
            cmd.Parameters["@stcode"].Value = stcode;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();
            return dt;
        }
        public DataTable GetStudentClassInfo(string tterm,int iddanesh)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("Adobe.SP_GetStudentClassInfo_Sida", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tterm", SqlDbType.VarChar);
            cmd.Parameters["@tterm"].Value = tterm;
            cmd.Parameters.Add("@iddanesh", SqlDbType.VarChar);
            cmd.Parameters["@iddanesh"].Value = iddanesh;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();
            return dt;
        }
        public DataTable GetStudentClassInfo(string ClassCode, string tterm)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("Adobe.SP_GetStudentClassInfo_SidaWithClassCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClassCode", SqlDbType.VarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;
            cmd.Parameters.Add("@tterm", SqlDbType.VarChar);
            cmd.Parameters["@tterm"].Value = tterm;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();
            return dt;
        }
        public DataTable GetClassInfoByClassCode(string ClassCode, string tterm)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_GetClassInfoByClassCode]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@code", SqlDbType.VarChar);
            cmd.Parameters["@code"].Value = ClassCode;
            cmd.Parameters.Add("@tterm", SqlDbType.VarChar);
            cmd.Parameters["@tterm"].Value = tterm;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();
            return dt;
        }            
        public DataTable GetActiveTerm()
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn_Sida.Open();
            SqlConnection conn = conn_Sida;
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_GetAllTerm]", conn);
            cmd.CommandType = CommandType.StoredProcedure;        
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn_Sida.Close();

            DataTable DT11 = new DataTable();
            DT11.Columns.Add("tterm", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {                
                if (dt.Rows[i]["Term"].ToString().ElementAt(0) == '9'
                    && int.Parse(dt.Rows[i]["Term"].ToString().ElementAt(1).ToString()) > 2)
                {
                    DataRow row = DT11.NewRow();
                    row["tterm"] = dt.Rows[i]["Term"].ToString();
                    DT11.Rows.Add(row);
                }             
            }            
            return DT11;
        }


    

        public DataTable GetClassTime(string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            SqlConnection conn = conn_Adobe;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTimeinMeetingsByClassCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }

        public DataTable GetClassTime_Old(string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            SqlConnection conn = conn_AdobeOld;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTimeinMeetingsByClassCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;
            cmd.Parameters.Add("@TermTimeStart", SqlDbType.Date);
            cmd.Parameters["@TermTimeStart"].Value = TermTimeStart;
            cmd.Parameters.Add("@TermTimeEnd", SqlDbType.Date);
            cmd.Parameters["@TermTimeEnd"].Value = TermTimeEnd;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }

        public DataTable GetClassTime93942(string ClassCode)
        {
            SqlConnection conn = conn_AdobeLive;
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("R_StudentTimeinMeetingsByClassCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClassCode", SqlDbType.NVarChar);
            cmd.Parameters["@ClassCode"].Value = ClassCode;        

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }






        #endregion




    }
}
