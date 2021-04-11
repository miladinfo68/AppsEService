using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.Faculty;
//using IAUEC_Apps.DTO.University.CooperationRequest;


namespace IAUEC_Apps.DAO.University.Faculty
{
    public class CooperationRequestFaculty
    {
        InsertToSida ITS = new InsertToSida();
        SqlConnection con = new SqlConnection(new HrConnection().HR_con);
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        //InfoFaculty IF = new InfoFaculty();
        #region read

        public DataTable GetInfoProf(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_ShowInfoAllPeople]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetNameProvince()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_CodeProvince]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable SelectInfoPeopleBystatus(int status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_SelectInfoPeopleBystatus]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@status", status);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public void UpdateInfoPeopleStatus(string codemeli, int status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_UpdateInfoPeopleStatus]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@userName", codemeli);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                throw;
            }

        }
        //public DataTable GetInfoProf()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandText = "[HR].[SP_ShowInfoAllPeople]";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        con.Open();
        //        SqlDataReader rdr;
        //        rdr = cmd.ExecuteReader();
        //        dt.Load(rdr);
        //        con.Close();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return dt;
        //}

        public DataTable GetCodeProf()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_GetCodeProf]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetInfoPeoByFilter(int CodeOstad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_ShowInfoPeoByFilter]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@code_ostad", CodeOstad);
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetInfoPeoByCodeMeli(int CodeOstad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_ShowInfoPeoByFilter]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@codemeli", CodeOstad.ToString());
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetInfoPeoByFilterPDF(int CodeOstad, int doc_type)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_ShowInfoPeoByFilterPDF]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@code_ostad", CodeOstad);
            cmd.Parameters.AddWithValue("@doc_type", doc_type);
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable DepartmanProf(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_CodeDepartmanProf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetOstadInfoFromHR(int codeostad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[dbo].[SP_GetOstadInfoByCodeOstad]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@code_ostad", codeostad);

            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                conn.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch(Exception ex)
            {
                throw;
            }
            return dt;
        }
        public int HasNotationId(int codeostad)
        {


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Request].[SP_HasNationalCode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@code_ostad", codeostad);

            var returnParameter = cmd.Parameters.Add("@resualt", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            return  Convert.ToInt32(returnParameter.Value);
          


        }

        #endregion

        #region update

        public void UpdateInfoPeople(int CodeOstad, int Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "HR.SP_UpdateStatusRequest";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@Status", Status);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                throw;
            }
        }
        public void UpdateMartabeInfoPeople(int codeOstad, int martabe)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "HR.SP_UpdateMartabe";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@CodeOstad", codeOstad);
            cmd.Parameters.AddWithValue("@Martabe", martabe);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetFilePDF(int Code_Ostad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HR.SP_FilePDF";
            cmd.Parameters.AddWithValue("@CodeOstad", Code_Ostad);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        #endregion

        #region write


        public DataTable InsertToPortalPazhuhesh(int Code,string password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_InsertFacultyHumanResource]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@code", Code);
            cmd.Parameters.AddWithValue("@suppPassword", password);
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable CheckTeacherIsInWebUserOst(long Codeostad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[dbo].[sp_checkExistTeacherInWebUserOst]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@codeOstad", Codeostad);
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        //public int GetInsertInfoPeople(InfoFaculty IP)
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandText = "[HR].[SP_InsertInfoPeo]";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@name", IP.Name);
        //    cmd.Parameters.AddWithValue("@family", IP.Family);
        //    cmd.Parameters.AddWithValue("@name_ep", IP.Fathers_Name);
        //    cmd.Parameters.AddWithValue("@idd", IP.Idd);
        //    cmd.Parameters.AddWithValue("@idd_meli", IP.Idd_Meli);
        //    cmd.Parameters.AddWithValue("@sex", IP.Sex);
        //    cmd.Parameters.AddWithValue("@sal_tav", IP.Sal_Tav);
        //    cmd.Parameters.AddWithValue("@mahal_tav", IP.Mahal_Tav);
        //    cmd.Parameters.AddWithValue("@mahal_sodor", IP.Mahal_Sodor);
        //    cmd.Parameters.AddWithValue("@id_madrak", IP.Madrak);
        //    cmd.Parameters.AddWithValue("@idresh", IP.Field);
        //    cmd.Parameters.AddWithValue("@sal_madrak", IP.Sal_Madrak);
        //    cmd.Parameters.AddWithValue("@university", IP.Uni);
        //    cmd.Parameters.AddWithValue("@martabe", IP.Martabe);
        //    cmd.Parameters.AddWithValue("@payeh", IP.Payeh);
        //    cmd.Parameters.AddWithValue("@sanavat_tadris", IP.Sanavat_tadris);
        //    cmd.Parameters.AddWithValue("@type_estekhdam", IP.type_estekhdam);
        //    cmd.Parameters.AddWithValue("@nahveh_hamk", IP.Cooperation);
        //    cmd.Parameters.AddWithValue("@uni_khedmat", IP.Uni_Khedmat);
        //    cmd.Parameters.AddWithValue("@date_hokm", IP.Date_Hokm);
        //    cmd.Parameters.AddWithValue("@date_runhokm", IP.Date_RunHokm);
        //    cmd.Parameters.AddWithValue("@number_hokm", IP.Number_Hokm);
        //    cmd.Parameters.AddWithValue("@marital_status", IP.Marital_Status);
        //    cmd.Parameters.AddWithValue("@status_nezam", IP.Status_Nezam);
        //    cmd.Parameters.AddWithValue("@num_bime", IP.Number_Bime);
        //    cmd.Parameters.AddWithValue("@siba", IP.Siba);
        //    cmd.Parameters.AddWithValue("@tel_home", IP.Tel_Home);
        //    cmd.Parameters.AddWithValue("@tel_kar", IP.Tel_Kar);
        //    cmd.Parameters.AddWithValue("@mobile", IP.Mobile);
        //    cmd.Parameters.AddWithValue("@add_home", IP.Add_Home);
        //    cmd.Parameters.AddWithValue("@add_kar", IP.Add_Kar);
        //    cmd.Parameters.AddWithValue("@code_posti", IP.Code_Posti);
        //    cmd.Parameters.AddWithValue("@code_ostan_home", IP.Code_Ostan_Home);
        //    cmd.Parameters.AddWithValue("@code_city_home", IP.Code_City_Home);
        //    cmd.Parameters.AddWithValue("@code_city_work", IP.Code_City_Work);
        //    cmd.Parameters.AddWithValue("@code_ostan_work", IP.Code_Ostan_Work);
        //    cmd.Parameters.AddWithValue("@add_eamil", IP.Email);
        //    cmd.Parameters.AddWithValue("@country", IP.Country);
        //    cmd.Parameters.AddWithValue("@userID", IP.userID);
        //    cmd.Parameters.AddWithValue("@IsRetired", IP.ISRetired);
        //    cmd.Parameters.AddWithValue("@Cooperation", IP.Study);

        //    try
        //    {
        //        con.Open();
        //        int infopeopleid = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        //        con.Close();
        //        return infopeopleid;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public DataTable InsertToFostadSida(int Code_Ostad,string password)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_InsertToSida]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@code", Code_Ostad);
            cmd.Parameters.AddWithValue("@suppPassword", password);
            try
            {

                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch(Exception ee)
            {
                throw;
            }
            return dt;
        }
        public void InsertImageToOstImage(int Code_Ostad, byte[] Image)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_InsertImageToOstImage]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@CodeOstad", Code_Ostad);
            cmd.Parameters.AddWithValue("@Pic", Image);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}
