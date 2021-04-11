using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;


namespace IAUEC_Apps.DAO.University.GraduateAffair
{
    public class GraduateReportDAO
    {
        SqlConnection con2 = new SqlConnection(new AmozeshConnection().con);
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection conFar = new SqlConnection(new AmozeshConnection().con);
        #region READ


        //create by bahrami
        public DataTable GetStudentGovahiByID(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Graduate.SP_GetStudentGovahiByID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        //create by bahrami
        public DataTable GetGraduateStudentsByParams(string WhereClause)
        {
            SqlCommand cmdgra = new SqlCommand();
            cmdgra.Connection = conn;
            cmdgra.CommandText = "Graduate.SP_GetGraduateStudentsByParams";
            cmdgra.CommandType = CommandType.StoredProcedure;
            cmdgra.Parameters.AddWithValue("@WhereClause", WhereClause);
            DataTable dtgra = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdgra.ExecuteReader();
                dtgra.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdgra.Dispose();

            }
            catch (Exception)
            { throw; }
            return dtgra;
        }




        //create by bahrami
        public DataTable GetGraduateStudent()
        {
            SqlCommand cmdfar = new SqlCommand();
            cmdfar.Connection = conn;
            cmdfar.CommandText = "Graduate.SP_GetGraduateStudent";
            cmdfar.CommandType = CommandType.StoredProcedure;
            DataTable dtfar = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdfar.ExecuteReader();
                dtfar.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdfar.Dispose();
            }
            catch (Exception)
            { throw; }
            return dtfar;
        }



        //create by bahrami
        public DataTable GetPishnevisElamFeraghat(string stcode)
        {
            SqlCommand cmdpish = new SqlCommand();
            cmdpish.Connection = conn;
            cmdpish.CommandText = "Graduate.SP_PishnevisElamFeraghat";
            cmdpish.CommandType = CommandType.StoredProcedure;
            cmdpish.Parameters.AddWithValue("@stcode", stcode);
            DataTable dtpish = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdpish.ExecuteReader();
                dtpish.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdpish.Dispose();
            }
            catch (Exception)
            { throw; }
            return dtpish;
        }


        //create by bahrami
        public DataTable GetStudentGovahiByStcode(string stcode)
        {
            SqlCommand cmdgetgov = new SqlCommand();
            cmdgetgov.Connection = conn;
            cmdgetgov.CommandText = "Graduate.SP_GetStudentGovahi";
            cmdgetgov.CommandType = CommandType.StoredProcedure;
            cmdgetgov.Parameters.AddWithValue("@stcode", stcode);


            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdgetgov.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdgetgov.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable getStudentInf(string stcode, string iddMelli, int vaiatTahsili)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Graduate.SP_getStudentInf";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@iddMeli", iddMelli);
            cmd.Parameters.AddWithValue("@vazkol", vaiatTahsili);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader sdr;
                conn.Open();
                sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                sdr.Dispose();
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }
        public DataTable GetStudentInfoByStcode(string stcode)
        {
            SqlCommand cmdgetinf = new SqlCommand();
            cmdgetinf.Connection = conn;
            cmdgetinf.CommandText = "request.SP_GetStudentInfoByStCode";
            cmdgetinf.CommandType = CommandType.StoredProcedure;
            cmdgetinf.Parameters.AddWithValue("@stcode", stcode);

            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdgetinf.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdgetinf.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable GetSeniorEncyclopedia(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Graduate].[SP_SeniorEncyclopedia]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", stCode);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetTHRMarkaz(int type, int act)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Far_SP_GetTHRMarkaz";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conFar;
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@act", act);
            DataTable dt = new DataTable();
            try
            {
                conFar.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conFar.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        #endregion
        public DataTable GetTehranMarkaz()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[Far_SP_GetTehranMarkaz]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conFar;
            DataTable dt = new DataTable();
            try
            {
                conFar.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conFar.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetTehranMarkazInDate(DateTime startDate, DateTime endDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[Far_SP_GetTehranMarkazInDate]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conFar;
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            DataTable dt = new DataTable();
            try
            {
                conFar.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conFar.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        #region Delete
        //create by Bahrami
        public void DeleteTaeidieTahsili(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Graduate.SP_DeleteTaeidieTahsili";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", ID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        public bool deleteStudentFromTehranMarkazList(string studentCode)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "[graduate].[SP_DeleteFareghStudentFromThrMrkz]";
                cmd.Parameters.AddWithValue("@studentCode", studentCode);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
            return true;
        }

        #endregion

        #region Create
        public int InsertTaeidieTahsili(string stcode, int type_govahi, string num_namehaz, string date_namehaz, string name_bekoja)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Graduate.SP_InsertTaeidieTahsili";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@type_govahi", type_govahi);
            cmd.Parameters.AddWithValue("@num_namehaz", num_namehaz);
            cmd.Parameters.AddWithValue("@date_namehaz", date_namehaz);
            cmd.Parameters.AddWithValue("@name_bekoja", name_bekoja);
            try
            {
                conn.Open();
                int rowId = 0;
                if (cmd.ExecuteScalar() != null)
                {
                    rowId = int.Parse(cmd.ExecuteScalar().ToString());
                }
                conn.Close();
                cmd.Dispose();
                return rowId;


            }
            catch (Exception)
            {
                throw;

            }



        }
        public void InsertFar_TehranMarkaz(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conFar;
            cmd.CommandText = "Far_TehranMarkaz";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            try
            {
                conFar.Open();
                cmd.ExecuteNonQuery();
                conFar.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }
        #endregion
        #region update
        public void UpdateConvertExcel_Far_Stcode(DataTable stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conFar;
            cmd.CommandText = "Far_SP_UpdateConvertExcel_Far_Stcode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StList", stcode);
            cmd.Parameters["@StList"].SqlDbType = SqlDbType.Structured;
            cmd.Parameters["@StList"].TypeName = "dbo.[list_Stcode]";
            try
            {
                conFar.Open();
                cmd.ExecuteNonQuery();
                conFar.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }
        
        public bool updateExcelStatusInTehranMarkazList(string studentCode,byte excelStatus)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "[graduate].[SP_UpdateExcelStatusFareghStudent]";
                cmd.Parameters.AddWithValue("@studentCode",studentCode);
                cmd.Parameters.AddWithValue("@newExcelStatus", excelStatus);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
            return true;
        }

        #endregion
    }
}
