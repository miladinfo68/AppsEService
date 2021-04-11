using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.University.Research
{
   public class ResearchDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection connR = new SqlConnection(new ResearchConnection().con);
        #region Read
      
       //create by bahrami
        public DataTable StInSomFields(int idresh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Research.SP_StInSomeFields";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idresh",idresh);
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
            catch(Exception)
            { throw; }
            return dt;
        }

        public DataTable GetThesisProfInfo()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connR;
            cmd.CommandText = "SP_GetThesisProfInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connR.Open();
               SqlDataReader rdr;
                rdr=cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                connR.Close();
                cmd.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }
       //create by bahrami
       
        public DataTable GetAllOstadInfo()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Research.SP_GetAllOstadInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
               SqlDataReader rdr;
                rdr=cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }

       //create by bahrami

        public DataTable GetProfInfoByID(int id_os)
        {
            SqlCommand cmdos = new SqlCommand();
            cmdos.Connection = connR;
            cmdos.CommandText = "SP_GetProfInfoByID";
            cmdos.CommandType = CommandType.StoredProcedure;
            cmdos.Parameters.AddWithValue("@id_os",id_os);
            DataTable dtos=new DataTable();
            try
            {
                connR.Open();
                SqlDataReader rdr;
                rdr=cmdos.ExecuteReader();
                dtos.Load(rdr);
                rdr.Dispose();
                connR.Close();
                cmdos.Dispose();
                
            }
            catch(Exception)
            { throw; }
            return dtos;
        }


       //create by bahrami
        public DataTable GetProfInfo()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Research.SP_GetProfInfo";
            cmd.CommandType = CommandType.StoredProcedure;
           
            DataTable dt=new DataTable();
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
            catch(Exception)
            { throw; }
            return dt;
        }


       //create by bahrami
        public DataTable GetTeacherUserPass(int ocode)//,string password
        {
            SqlCommand cmdt = new SqlCommand();
            cmdt.Connection = conn;
            /// Login via Amozesh
            /// cmdt.CommandText = "Research.SP_GetTeacherUserPass";
            /// 

            /// Login via Supplementary
            cmdt.CommandText = "dbo.SP_GetTeacherLogin";
            /// 
            cmdt.CommandType = CommandType.StoredProcedure;
            cmdt.Parameters.AddWithValue("@ocode",ocode);
            //cmdt.Parameters.AddWithValue("@password", password);
            DataTable dtt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdt.ExecuteReader();
                dtt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdt.Dispose();
            }
            catch(Exception)
            { throw; }
            return dtt;
        }

        public DataTable GetTeacherName(int Code)
        {
            SqlCommand cmdt = new SqlCommand();
            cmdt.Connection = conn;
            cmdt.CommandText = "Research.SP_GetTeacherName";
            cmdt.CommandType = CommandType.StoredProcedure;
            cmdt.Parameters.AddWithValue("@ocode", Code);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdt.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdt.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
       //create by bahrami
        public DataTable GetOstadInfoByCodeOstad(int CodeOstad)
        {
            SqlCommand cmdost = new SqlCommand();
            cmdost.Connection = conn;
            cmdost.CommandText = "Research.SP_GetOstadInfoByCodeOstad";
            cmdost.CommandType = CommandType.StoredProcedure;
            cmdost.Parameters.AddWithValue("@code_ostad", CodeOstad);
            DataTable dtost=new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr=cmdost.ExecuteReader();
                dtost.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdost.Dispose();
                
            }
            catch(Exception)
            { throw; }
            return dtost;

 
        }

        public DataTable GetOstadInfoByCodeOstad_Portal(int CodeOstad)
        {
            SqlCommand cmdost = new SqlCommand();
            cmdost.Connection = conn;
            cmdost.CommandText = "request.SP_GetOstadInfoByCodeOstad_portal";
            cmdost.CommandType = CommandType.StoredProcedure;
            cmdost.Parameters.AddWithValue("@code_ostad", CodeOstad);
            DataTable dtost = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdost.ExecuteReader();
                dtost.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdost.Dispose();

            }
            catch (Exception)
            { throw; }
            return dtost;


        }

        //create by bahrami
        public DataTable GetStudentFromStudentsByStcode(int stcode)
      {
          SqlCommand cmdch=new SqlCommand();
          cmdch.Connection=connR;
          cmdch.CommandText="SP_GetStudentFromStudentsByStcode";
          cmdch.CommandType=CommandType.StoredProcedure;
          cmdch.Parameters.AddWithValue("@stcode",stcode);
          DataTable dtch=new DataTable();
          try
          {
              connR.Open();
              SqlDataReader rdr;
              rdr=cmdch.ExecuteReader();
              dtch.Load(rdr);
              rdr.Dispose();
              connR.Close();
              cmdch.Dispose();
          }
          catch(Exception)
          {throw;}
          return dtch;
      }

       //create by bahrami
        public DataTable GetGrohInfobyId(int id_groh)
        {
            SqlCommand cmdg = new SqlCommand();
            cmdg.Connection = connR;
            cmdg.CommandText = "SP_GetGrohInfoById";
            cmdg.CommandType = CommandType.StoredProcedure;
            cmdg.Parameters.AddWithValue("@id_groh", id_groh);
            DataTable dt = new DataTable();
            try
            {
                connR.Open();
                SqlDataReader rdr;
                rdr = cmdg.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                connR.Close();
                cmdg.Dispose();
            }
            catch(Exception)
            { throw; }
            return dt;
        }

        //create by bahrami
        public DataTable CheckStInSecondTerm(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Research.SP_CheckStInSecondTerm";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("stcode",stcode);
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
            catch(Exception)
            { throw; }
            return dt;
        }


        #endregion

        #region Creat


       //create by Bahrami

        public void insert_Ostad(string name_user, int oscode, string name_, string famili, string resh, int groh, string mail, string cli_ip, string date_sabt, string time_sabt, int tay_groh, int rotbe, string user_pass, int magta, int fall, int z_rah, int z_mosh, int tham, int jens, int payeh, int tayed_os, int lock_mad, string date_b, string mahal_b, string mahal_s, int sh_sh, int ostan, string bimeh, int bank, string hesab, int groh_k, int ozv_shora, string estkh, string code_sazmani, string mob_os, string address_os, string code_meli, int result_code )
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = connR;
            cmdins.CommandText = "SP_insert_Ostad";
            cmdins.CommandType = CommandType.StoredProcedure;
            cmdins.Parameters.AddWithValue("@nam_user", name_user);
            cmdins.Parameters.AddWithValue("@oscode",oscode);
            cmdins.Parameters.AddWithValue("@name_",name_);
            cmdins.Parameters.AddWithValue("@famili",famili);
            cmdins.Parameters.AddWithValue("@resh",resh);
            cmdins.Parameters.AddWithValue("@groh",groh);
            cmdins.Parameters.AddWithValue("@mail",mail);
            cmdins.Parameters.AddWithValue("@cli_ip",cli_ip);
            cmdins.Parameters.AddWithValue("@date_sabt",date_sabt);
            cmdins.Parameters.AddWithValue("@time_sabt",time_sabt);
            cmdins.Parameters.AddWithValue("@tay_groh",tay_groh);
            cmdins.Parameters.AddWithValue("@rotbe",rotbe);
            cmdins.Parameters.AddWithValue("@user_pass",user_pass);
            cmdins.Parameters.AddWithValue("@magta",magta);
            cmdins.Parameters.AddWithValue("@fall",fall);
            cmdins.Parameters.AddWithValue("@z_rah",z_rah);
            cmdins.Parameters.AddWithValue("@z_mosh",z_mosh);
            cmdins.Parameters.AddWithValue("@tham",tham);
            cmdins.Parameters.AddWithValue("@jens",jens);
            cmdins.Parameters.AddWithValue("@payeh",payeh);
            cmdins.Parameters.AddWithValue("@tayed_os",tayed_os);
            cmdins.Parameters.AddWithValue("@lock_mad",lock_mad);
            cmdins.Parameters.AddWithValue("@date_b",date_b);
            cmdins.Parameters.AddWithValue("@mahal_b",mahal_b);
            cmdins.Parameters.AddWithValue("@mahal_s",mahal_s);
            cmdins.Parameters.AddWithValue("@sh_sh",sh_sh);
            cmdins.Parameters.AddWithValue("@ostan",ostan);
            cmdins.Parameters.AddWithValue("@bimeh",bimeh);
            cmdins.Parameters.AddWithValue("@bank",bank);
            cmdins.Parameters.AddWithValue("@hesab",hesab);
            cmdins.Parameters.AddWithValue("@groh_k",groh_k);
            cmdins.Parameters.AddWithValue("@ozv_shora",ozv_shora);
            cmdins.Parameters.AddWithValue("@estkh",estkh);
            cmdins.Parameters.AddWithValue("@code_sazmani",code_sazmani);
            cmdins.Parameters.AddWithValue("@mob_os",mob_os);
            cmdins.Parameters.AddWithValue("@address_os",address_os);
            cmdins.Parameters.AddWithValue("@code_meli", code_meli);
            cmdins.Parameters.AddWithValue("@result_code", result_code);

            try 
            {
                connR.Open();
                cmdins.ExecuteNonQuery();
                connR.Close();
                cmdins.Dispose();
            }
            catch(Exception)
            { throw; }
        }

       //create by bahrami
        public void InsertIntoOstan(int id_ostan, string name_ostan)
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = connR;
            cmdins.CommandText = "SP_InsertIntoOstan";
            cmdins.CommandType = CommandType.StoredProcedure;
            cmdins.Parameters.AddWithValue("@id_ostan",id_ostan);
            cmdins.Parameters.AddWithValue("@name_ostan",name_ostan);
            try 
            {
                connR.Open();
                cmdins.ExecuteNonQuery();
                connR.Close();
                cmdins.Dispose();
            }
            catch(Exception)
            { throw; }
        }




       // create by bahrami
       public void InsertStudentInfoToStudents(int stcode,string name,string famili,int vrodi,string code_meli,int resh,int groh,string date_sabt,string time_sabt,int maghta,string mob_stu,string address_stu,int term,string mail,int jens,string pic_stu)

       {
           SqlCommand cmd=new SqlCommand();
           cmd.Connection=connR;
           cmd.CommandText="SP_InsertStudentInfoToStudents";
           cmd.CommandType=CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@stcode",stcode);
           cmd.Parameters.AddWithValue("@name",name);
           cmd.Parameters.AddWithValue("@famili",famili);
           cmd.Parameters.AddWithValue("@vrodi",vrodi);
           cmd.Parameters.AddWithValue("@code_meli",code_meli);
           cmd.Parameters.AddWithValue("@resh", resh);
           cmd.Parameters.AddWithValue("@groh",groh);
           cmd.Parameters.AddWithValue("@date_sabt",date_sabt);
           cmd.Parameters.AddWithValue("@time_sabt",time_sabt);
           cmd.Parameters.AddWithValue("@maghta",maghta);
           cmd.Parameters.AddWithValue("@mob_stu",mob_stu);
           cmd.Parameters.AddWithValue("@address_stu",address_stu);
           cmd.Parameters.AddWithValue("@term",term);
           cmd.Parameters.AddWithValue("@jens",jens);
           cmd.Parameters.AddWithValue("@pic_stu", pic_stu);
           cmd.Parameters.AddWithValue("@mail", mail);
           try
           {
               connR.Open();
               cmd.ExecuteNonQuery();
               connR.Close();
               cmd.Dispose();
           }
           catch(Exception)
           {throw;}
       }

       //create by bahrami
        public void InsertIntoGrouh(int id_groh,string name_groh,int id_college)
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = connR;
            cmdins.CommandText="SP_InsertIntoGrouh";
            cmdins.CommandType=CommandType.StoredProcedure;
            cmdins.Parameters.AddWithValue("@id_groh",id_groh);
            cmdins.Parameters.AddWithValue("@name_groh",name_groh);
            cmdins.Parameters.AddWithValue("@id_college",id_college);
            try 
            {
                connR.Open();
                cmdins.ExecuteNonQuery();
                connR.Close();
                cmdins.Dispose();

            }
            catch(Exception)
            { throw; }
        }
        #endregion
        #region Update

        public void UpdateOstademail(int codeostad, string email)
        {
            SqlCommand cmd = new SqlCommand();
                cmd.Connection=connR;
                cmd.CommandText = "[dbo].[SP_UpdateOstademail]";
                cmd.CommandType=CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@codeostad",codeostad);
            cmd.Parameters.AddWithValue("@email",email);
           
            try 
            {  connR.Open();
                cmd.ExecuteNonQuery();
                connR.Close();
                cmd.Dispose();
               
            }
            catch(Exception)
            { throw; }
        }

        public bool changePortalEntryPermit(string stcode,bool permit)
        {
            SqlCommand cmd = new SqlCommand("sp_changePortalEntryPermit", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@allow", permit);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int r = cmd.ExecuteNonQuery();
                conn.Close();
                return r > 0;

            }
            catch(Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }

        
        public DataTable getPortalEntryPermition()
        {
            SqlCommand cmd = new SqlCommand("sp_getPortalEntryPermit", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dataTable.Load(dr);
                conn.Close();

            }
            catch(Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dataTable;
        }


       public void SP_UpdateOstadMobile(int codeostad, string mobile,string code_meli)
        {
            SqlCommand cmd = new SqlCommand();
                cmd.Connection=connR;
                cmd.CommandText="SP_UpdateOstadMobile";
                cmd.CommandType=CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@codeostad",codeostad);
            cmd.Parameters.AddWithValue("@mobile",mobile);
            cmd.Parameters.AddWithValue("@code_meli", code_meli);
           
            try 
            {  connR.Open();
                cmd.ExecuteNonQuery();
                connR.Close();
                cmd.Dispose();
               
            }
            catch(Exception)
            { throw; }
        }

              //create by bahrami

        public void UpdateostadDarajePayeh(int payeh, int rotbe, int codeostad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connR;
            cmd.CommandText = "SP_UpdateOstadDarajePayeh";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@payeh",payeh);
            cmd.Parameters.AddWithValue("@rotbe",rotbe);
            cmd.Parameters.AddWithValue("@codeostad", codeostad);
            try 
            {
                connR.Open();
                cmd.ExecuteNonQuery();
                connR.Close();
                cmd.Dispose();
            }
            catch(Exception)
            { throw; }
        }
        #endregion
    }
}
