using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.Adobe
{
    public class ManagementPanelDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection connAdobeTesti = new SqlConnection( "server=192.168.30.190; database=karimiadobe; user=karimi ; password=123456 ; connection timeout=30");
  
            
        #region GET

        public DataTable Get_CourseType()
        {
            SqlCommand cmdchk = new SqlCommand();
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            cmdchk.CommandType = CommandType.StoredProcedure;
            cmdchk.CommandText = "Adobe.SP_GetCourseType";
            cmdchk.Connection = conn;
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            rdr = cmdchk.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmdchk.Connection.Close();
            cmdchk.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }

        public DataTable GetCourseTimeClass()
        {
            SqlCommand cmdchk = new SqlCommand();
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            cmdchk.CommandType = CommandType.StoredProcedure;
            cmdchk.CommandText = "Adobe.SP_GetCourseTimeClass";
            cmdchk.Connection = conn;
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            rdr = cmdchk.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmdchk.Connection.Close();
            cmdchk.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }

        public DataTable GetGetAdobe_Ability()
        {
            SqlCommand cmdchk = new SqlCommand();
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            cmdchk.CommandType = CommandType.StoredProcedure;
            cmdchk.CommandText = "Adobe.SP_GetAdobe_Ability";
            cmdchk.Connection = conn;
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            rdr = cmdchk.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmdchk.Connection.Close();
            cmdchk.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close(); 
            return dt;
        }


        /// <summary>
        /// check Name in Adobe.Customer 
        /// if Not found -> Return TRUE 
        /// else -> Return False
        /// </summary>
        /// <param name="CustomerName"></param>
        /// <returns></returns>
        public bool Check_CustomerName(string CustomerName)
        {            
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Check_CustomerName]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar);
            cmd.Parameters["@CustomerName"].Value = CustomerName;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close(); 

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;              
        }

        public DataTable GetDayTime()
        {
            SqlCommand cmdchk = new SqlCommand();
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            cmdchk.CommandType = CommandType.StoredProcedure;
            cmdchk.CommandText = "Adobe.SP_Get_Customers_DayTime";
            cmdchk.Connection = conn;
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            rdr = cmdchk.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmdchk.Connection.Close();
            cmdchk.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close(); 
            return dt;
        } 


        /// <summary>
        /// CustomerId  با پارامتر  Customers_Users  دریافت کل اطلاعات موجود در جدول 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataTable Get_Customers_Users_ByCustomerId(int CustomerId)
        {            
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_Users_ByCustomerId]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerId", SqlDbType.Int);
            cmd.Parameters["@CustomerId"].Value = CustomerId; 
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();         
            return dt;          
        }
                
        public DataTable Get_Customers_Users_ByCustomerIdWithNationalCode(int CustomerId, string NationalCode)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_Users_ByCustomerIdWithNationalCode]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerId", SqlDbType.Int);
            cmd.Parameters["@CustomerId"].Value = CustomerId;
            cmd.Parameters.Add("@NationalCode", SqlDbType.NVarChar);
            cmd.Parameters["@NationalCode"].Value = NationalCode;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }

        public DataTable Get_Customers()
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers]", conn);
            cmd.CommandType = CommandType.StoredProcedure; 
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }
         
        /// <summary>
        ///  برای جستجو درخواست ها
        ///  را پرکنید  Name or CustomerId میتوانید به دلخواه یکی از موارد           
        ///  CustomerId=-1 عضو بی اثر می باشد
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ScoId"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataTable Get_Customers_ClassNameByName(string Name, int ScoId, int CustomerId)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_ClassName_ByName_ScoId]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters["@Name"].Value = Name;
            cmd.Parameters.Add("@ScoId", SqlDbType.Int);
            cmd.Parameters["@ScoId"].Value = ScoId;
            cmd.Parameters.Add("@CustomerId", SqlDbType.Int);
            cmd.Parameters["@CustomerId"].Value = CustomerId; 
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }
                 
        public DataTable Get_Customers_ClassName_ById(int Id)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_ClassName_ById]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = Id;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }


        public DataTable Get_Professor_ByName_Family_NationalCode(string Name, string Family, string NationalCode)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[SP_Get_Professor_ByName_Family_NationalCode]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters["@Name"].Value = Name;
            cmd.Parameters.Add("@Family", SqlDbType.NVarChar);
            cmd.Parameters["@Family"].Value = Family;
            cmd.Parameters.Add("@NationalCode", SqlDbType.VarChar);
            cmd.Parameters["@NationalCode"].Value = NationalCode;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }
        
        public DataTable Get_Student_ByName_Family_NationalCode(string Name, string Family, string NationalCode)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[SP_Get_Student_ByName_Family_NationalCode]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Family", Family);
            cmd.Parameters.AddWithValue("@NationalCode", NationalCode);
         
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }
        
        public DataTable Get_Customers_UserInfo_ThesisDefense(string UserId, int Type)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_UserInfo_ThesisDefense]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@Type", Type);
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }


        public DataTable Get_Customers_Users_ByNameAndFamily(string Name, string Family)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_Users_ByNameAndFamily]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Family", Family);
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }

        public DataTable Get_Customers_Meeting_ByClassId(int ClassId)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_Meeting_ByClassId]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);       
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }


        
        public DataTable Get_Customers_Users_InCustomerClass_ByClassId(int ClassId)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_Users_InCustomerClass_ByClassId]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }

        public DataTable Get_Customers_Users_InCustomerClass_ByClassId2(int ClassId)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_Users_InCustomerClass_ByClassId2]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }




        public DataTable Get_Customers_ClassDayTime_ByClassId(int ClassId)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_ClassDayTime_ByClassId]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            return dt;
        }

        /// <summary>
        /// میتوانید به یکی از پارامترهای اعلام شده را درست وارد کرده 
        /// و مابقی را یا صفر یا خالی درنظر بگیرید تا بدرستی خروجی دریافت کنید
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="UserName"></param>
        /// <param name="UserPass"></param>
        /// <param name="ScoName"></param>
        /// <param name="Tel"></param>
        /// <param name="Fax"></param>
        /// <param name="UserMobile"></param>
        /// <returns></returns>
        public DataTable Get_Customers_FullParam(long Id,string Name,string UserName,string UserPass
            ,string ScoName,string Tel,string Fax,string UserMobile)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_FullParam]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@UserPass", UserPass);
            cmd.Parameters.AddWithValue("@ScoName", ScoName);
            cmd.Parameters.AddWithValue("@Tel", Tel);
            cmd.Parameters.AddWithValue("@Fax", Fax);
            cmd.Parameters.AddWithValue("@UserMobile", UserMobile);
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            return dt;
        }





        public DataTable Get_Customers_ClassName_RejectReason(int ClassNameId)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Get_Customers_ClassName_RejectReason_ByClassNameId]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassNameId", ClassNameId);
    
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            return dt;
        }





        #endregion


         

        #region GetAdobe

        public DataTable Get_SP_Get_ScosByName(string Name)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();

            if (connAdobeTesti.State != System.Data.ConnectionState.Open)
                connAdobeTesti.Open();

            SqlCommand cmd = new SqlCommand("[dbo].[SP_GetMeetingInfo_ByMeetingName]", connAdobeTesti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters["@Name"].Value = Name;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;


        }

        public DataTable Get_PRINCIPALS_ByLOGIN(string Login)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();

            if (connAdobeTesti.State != System.Data.ConnectionState.Open)
                connAdobeTesti.Open();
            
            SqlCommand cmd = new SqlCommand("[dbo].[SP_Get_PRINCIPALS_ByLOGIN]", connAdobeTesti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LOGIN", Login);

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }








        #endregion
















        #region Insert

        public bool Insert_CourseClass(ManagementPanelDTO MpClass)
        {
            SqlCommand cmdCourseClassSave = new SqlCommand();
            cmdCourseClassSave.Connection = conn;
            cmdCourseClassSave.CommandText = "Adobe.SP_Insert_Course_Class";
            cmdCourseClassSave.CommandType = CommandType.StoredProcedure;
            cmdCourseClassSave.Parameters.AddWithValue("@Class", MpClass.Class);
            cmdCourseClassSave.Parameters.AddWithValue("@Course", MpClass.Course);
            cmdCourseClassSave.Parameters.AddWithValue("@MeetingCount", MpClass.MeetingCount);
            cmdCourseClassSave.Parameters.AddWithValue("@UserCount", MpClass.UserCount);
            cmdCourseClassSave.Parameters.AddWithValue("@tterm", MpClass.Tterm);
            cmdCourseClassSave.Parameters.AddWithValue("@IdUniversity", MpClass.IdUniversity);
            cmdCourseClassSave.Parameters.AddWithValue("@id_Course_TimeClass", MpClass.IdCourseTimeClass);
            cmdCourseClassSave.Parameters.AddWithValue("@id_Course_Type", MpClass.IdCourseType);
            SqlParameter outputIdParam = new SqlParameter("@Id", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                cmdCourseClassSave.ExecuteNonQuery();
                long Id = long.Parse(outputIdParam.Value.ToString());

                if (Id == 0)
                {
                    conn.Close();
                    return false;
                }
                else
                {
                    conn.Close();
                    for (int i = 0; i < MpClass.List_Id_AdobeAbility.Count; i++)
                    {
                        Insert_Course_AdobeAbility(Id, MpClass.List_Id_AdobeAbility.ElementAt(i));
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        
        public bool Insert_Course_AdobeAbility(long Id_CourseClass, long Id_AdobeAbility)
        {
            SqlCommand cmdCourse_AdobeAbilitySave = new SqlCommand();
            cmdCourse_AdobeAbilitySave.Connection = conn;
            cmdCourse_AdobeAbilitySave.CommandText = "Adobe.Course_AdobeAbility";
            cmdCourse_AdobeAbilitySave.CommandType = CommandType.StoredProcedure;
            cmdCourse_AdobeAbilitySave.Parameters.AddWithValue("@Id_CourseClass", Id_CourseClass);
            cmdCourse_AdobeAbilitySave.Parameters.AddWithValue("@Id_AdobeAbility", Id_AdobeAbility);
            
            DataTable dt = new DataTable();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                cmdCourse_AdobeAbilitySave.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception)
            {                
                return false;
            }
        }
        
        public bool Insert_CourseUser(ManagementPanelDTO MpDTO)
        {
            SqlCommand cmdexamclassSave = new SqlCommand();
            cmdexamclassSave.Connection = conn;
            cmdexamclassSave.CommandText = "Adobe.SP_Insert_Course_User";
            cmdexamclassSave.CommandType = CommandType.StoredProcedure;
            cmdexamclassSave.Parameters.AddWithValue("@Name", MpDTO.Name);
            cmdexamclassSave.Parameters.AddWithValue("@Family", MpDTO.Family);
            cmdexamclassSave.Parameters.AddWithValue("@LatinName", MpDTO.LatinName);
            cmdexamclassSave.Parameters.AddWithValue("@LatinFamily", MpDTO.LatinFamily);
            cmdexamclassSave.Parameters.AddWithValue("@NationalID", MpDTO.NationalID);
            cmdexamclassSave.Parameters.AddWithValue("@Mobile", MpDTO.Mobile);
            cmdexamclassSave.Parameters.AddWithValue("@Email_Address", MpDTO.EmailAddress);
            cmdexamclassSave.Parameters.AddWithValue("@UserName", MpDTO.UserName);
            cmdexamclassSave.Parameters.AddWithValue("@Password", MpDTO.Password);
            cmdexamclassSave.Parameters.AddWithValue("@TypeAccount", MpDTO.TypeAccount);

            DataTable dt = new DataTable();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                cmdexamclassSave.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        /// <summary>
        /// Insert Data into Adobe.Customers
        /// </summary>
        /// <param name="CustomerName"></param>
        /// <param name="CustomerTel"></param>
        /// <param name="CustomerFax"></param>
        /// <param name="CustomerEmail"></param>
        /// <param name="CustomerAddress"></param>
        /// <param name="CustomerUser"></param>
        /// <param name="CustomerUserMobile"></param>
        /// <returns></returns>
        public int Create_Customer(string CustomerName, string CustomerTel, string CustomerFax, string CustomerEmail
                , string CustomerAddress, string CustomerUser, string CustomerUserMobile)
        {          
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Adobe.SP_Insert_Customer";
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@Name", CustomerName);
            cmd.Parameters.AddWithValue("@Address", CustomerAddress);
            cmd.Parameters.AddWithValue("@Tel", CustomerTel);
            cmd.Parameters.AddWithValue("@Fax", CustomerFax);
            cmd.Parameters.AddWithValue("@Email", CustomerEmail);
            cmd.Parameters.AddWithValue("@UserName", CustomerUser);
            cmd.Parameters.AddWithValue("@UserMobile", CustomerUserMobile);

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            int modified = (int)cmd.ExecuteScalar();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            
            cmd.Connection.Close();
            cmd.Dispose();

            return modified;   
        }


        public int Create_Customers_ClassName(MPanelDTO MClass)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Adobe].[SP_Insert_Customers_ClassName]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustomerId", MClass.Customers_ClassName_CustomerId);
            cmd.Parameters.AddWithValue("@Name", MClass.Customers_ClassName_Name);
            cmd.Parameters.AddWithValue("@NameLatin", MClass.Customers_ClassName_NameLatin);
            cmd.Parameters.AddWithValue("@UserCount", MClass.Customers_ClassName_UserCount);
            cmd.Parameters.AddWithValue("@SessionCount", MClass.Customers_ClassName_SessionCount);
            cmd.Parameters.AddWithValue("@DateStart", MClass.Customers_ClassName_DateStart);
            cmd.Parameters.AddWithValue("@DateEnd", MClass.Customers_ClassName_DateEnd);
            cmd.Parameters.AddWithValue("@ScoId", MClass.Customers_ClassName_ScoId);
            cmd.Parameters.AddWithValue("@ServerName", MClass.Customers_ClassName_ServerName);
            cmd.Parameters.AddWithValue("@MeetingAccess", MClass.Customers_ClassName_MeetingAccess);
                
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            int modified = (int)cmd.ExecuteScalar();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            cmd.Connection.Close();
            cmd.Dispose();

            return modified;  
        }


        public int Create_Customers_ClassDayTime(int ClassNameId, int DayTimeId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Adobe].[SP_Insert_Customers_ClassDayTime]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassNameId", ClassNameId);
            cmd.Parameters.AddWithValue("@DayTimeId", DayTimeId);       
            
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            int modified = (int)cmd.ExecuteScalar();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            cmd.Connection.Close();
            cmd.Dispose();

            return modified;  
        }


        public int Create_Customers_Meeting(int ClassId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Adobe].[SP_Insert_Customers_Meeting]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClassId", ClassId);       
            
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            int modified = (int)cmd.ExecuteScalar();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            cmd.Connection.Close();
            cmd.Dispose();

            return modified;  
        }


        public int Create_Customers_Users(string Name,string Family,string LatinName,string LatinFamily
            , string UserMobile, string Email, string UserName, string NationalCode, int Sex, int CustomerId
            , string UserType, string UserId, string IdNumber, string UserPass)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Adobe].[SP_Insert_Customers_Users]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Family", Family);
            cmd.Parameters.AddWithValue("@LatinName", LatinName);
            cmd.Parameters.AddWithValue("@LatinFamily", LatinFamily);
            cmd.Parameters.AddWithValue("@UserMobile", UserMobile);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@NationalCode", NationalCode);
            cmd.Parameters.AddWithValue("@Sex", Sex);
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
            cmd.Parameters.AddWithValue("@UserType", UserType);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@IdNumber", IdNumber);
            cmd.Parameters.AddWithValue("@UserPass", UserPass);
                    
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            int modified = (int)cmd.ExecuteScalar();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            cmd.Connection.Close();
            cmd.Dispose();

            return modified;  
        }



        public int Create_Customers_UserMeeting(int IdUser, int IdMeeting, string UserAccess)
        {
            SqlCommand cmdexamclassSave = new SqlCommand();
            cmdexamclassSave.Connection = conn;
            cmdexamclassSave.CommandText = "[Adobe].[SP_Insert_Customers_UserMeeting]";
            cmdexamclassSave.CommandType = CommandType.StoredProcedure;

            cmdexamclassSave.Parameters.AddWithValue("@IdUser", IdUser);
            cmdexamclassSave.Parameters.AddWithValue("@IdMeeting", IdMeeting);
            cmdexamclassSave.Parameters.AddWithValue("@UserAccess", UserAccess);
    
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                cmdexamclassSave.ExecuteNonQuery();

                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }




        public int Customers_ClassName_RejectReason(int ClassNameId, string Text)
        {
            SqlCommand cmdexamclassSave = new SqlCommand();
            cmdexamclassSave.Connection = conn;
            cmdexamclassSave.CommandText = "[Adobe].[SP_Insert_Customers_ClassName_RejectReason]";
            cmdexamclassSave.CommandType = CommandType.StoredProcedure;

            cmdexamclassSave.Parameters.AddWithValue("@ClassNameId", ClassNameId);
            cmdexamclassSave.Parameters.AddWithValue("@Text", Text);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                cmdexamclassSave.ExecuteNonQuery();

                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();                

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }







        #endregion

        #region Update

        public void Update_Customers_ClassName_Scoid_ById(int ScoId,int Id )
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Update_Customers_ClassName_ByScoId_Id]", conn);
            cmd.CommandType = CommandType.StoredProcedure;         
            cmd.Parameters.Add("@ScoId", SqlDbType.Int);
            cmd.Parameters["@ScoId"].Value = ScoId;
            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = Id;  
            rdr = cmd.ExecuteReader();
           
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();  
        }

      

    
        /// <summary>
        ///  را بروز رسانی می کند UserAdobe & UserPass باشد،  Status=1  اگر 
        ///  را بروز رسانی می کند ScoName & ScoId باشد،  Status=2  اگر   
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="Id"></param>
        /// <param name="UserAdobe"></param>
        /// <param name="UserPass"></param>
        /// <param name="ScoId"></param>
        /// <param name="ScoName"></param>
        public void Update_Customers_ById(int Status,int Id, string UserAdobe, string UserPass
            ,int ScoId,string ScoName)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Update_Customers_ById]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@UserAdobe", UserAdobe);
            cmd.Parameters.AddWithValue("@UserPass", UserPass);
            cmd.Parameters.AddWithValue("@ScoId", ScoId);
            cmd.Parameters.AddWithValue("@ScoName", ScoName); 
            rdr = cmd.ExecuteReader();

            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }


        public void Update_Customers_Meeting_ById(int Id, int ScoId, string ScoName)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Update_Customers_Meeting_ById]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@ScoId", ScoId);
            cmd.Parameters.AddWithValue("@ScoName", ScoName);
            rdr = cmd.ExecuteReader();

            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

        }


         
        public void Update_Customers_UserMeeting_ById(long Id, int Active, long IdUser)
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand("[Adobe].[SP_Update_Customers_UserMeeting_ById]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Active", Active);
            cmd.Parameters.AddWithValue("@IdUser", IdUser);
            rdr = cmd.ExecuteReader();

            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();

            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

        }




        #endregion







    }
}
