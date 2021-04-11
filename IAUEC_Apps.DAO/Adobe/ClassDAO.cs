using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace IAUEC_Apps.DAO.CommonDAO
{
    public class ClassDAO
    {

        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);


        #region Create

        #endregion


        #region Read

        public List<ClassListDTO> Show_Class_List(string stcode, string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_StudentClass";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@term", term);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            List<ClassListDTO> list = new List<ClassListDTO>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ClassListDTO classDTO = new ClassListDTO();
                classDTO.ClassCode = dt.Rows[i]["classID"].ToString();
                classDTO.CourseName = dt.Rows[i]["courseName"].ToString();
                classDTO.ProfName = dt.Rows[i]["Ost_Name"].ToString();
                classDTO.ClassDateTime = dt.Rows[i]["Klas_Day"].ToString();
                classDTO.ClassStartTime = dt.Rows[i]["ClassTime"].ToString();//classtime
                list.Add(classDTO);
                //list.Insert(i, classDTO);

            }
            conn.Close();
            return list;

        }


        public List<ClassListDTO> Show_SimilarClass_List(string did, string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Adobe].[SP_SimilarClass]";
            cmd.Parameters.AddWithValue("@Did", did);
            cmd.Parameters.AddWithValue("@StCode", stcode);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            List<ClassListDTO> list = new List<ClassListDTO>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ClassListDTO classDTO = new ClassListDTO();
                classDTO.ClassCode = dt.Rows[i]["did"].ToString();
                classDTO.CourseName = dt.Rows[i]["namedars"].ToString();
                classDTO.ProfName = dt.Rows[i]["Ost_Name"].ToString();
                classDTO.ClassDateTime = dt.Rows[i]["Klas_Day"].ToString();
                classDTO.ClassStartTime = dt.Rows[i]["ClassTime"].ToString();//classtime
               // classDTO.MergeCode = dt.Rows[i]["Merge_Code"].ToString();
                list.Add(classDTO);
                //list.Insert(i, classDTO);

            }
            conn.Close();
            return list;

        }
        public DataTable getProfName()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_Allostad]";
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            conn.Close();
            return dt;
        }

        public DataTable getActiveProfName()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_AllActiveOstad]";
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            conn.Close();
            return dt;
        }

        public DataTable getMergeClass(string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_select_MergeClassByterm]";
            cmd.Parameters.AddWithValue("@term", term);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            conn.Close();
            return dt;
        }

        public DataTable getMergeCodeClasses(string term, int mergeCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_Select_ClassByterm-Merge_Code]";
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@Merge_Code", mergeCode);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            conn.Close();
            return dt;
        }

        public DataTable CheckMergeCode(ClassListDTO mrgClass)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_getMergeCode]";
            cmd.Parameters.AddWithValue("@term", mrgClass.Semester);
            cmd.Parameters.AddWithValue("@Merge_Code", mrgClass.MergeCode);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            conn.Close();
            return dt;
        }

        public DataTable Show_Class_List_byTerm(string term, int code, string name)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_class_Bycodedars_namedars]";
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@codeclass", code);
            cmd.Parameters.AddWithValue("@namedars", name);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            conn.Close();
            return dt;
        }
        public List<ClassListDTO> Show_Merge_Class_List(string stcode, string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_StudentMergeClasses";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@term", term);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            List<ClassListDTO> list = new List<ClassListDTO>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ClassListDTO classDTO = new ClassListDTO();
                classDTO.ClassCode = dt.Rows[i]["classID"].ToString();
                classDTO.MergeCode = dt.Rows[i]["Merge_code"].ToString();
                classDTO.CourseName = dt.Rows[i]["courseName"].ToString();
                classDTO.ProfName = dt.Rows[i]["Ost_Name"].ToString();
                classDTO.ClassDateTime = dt.Rows[i]["Klas_Day"].ToString();
                classDTO.ClassStartTime = dt.Rows[i]["ClassTime"].ToString();//time
                list.Add(classDTO);
                //list.Insert(i, classDTO);

            }
            conn.Close();
            return list;

        }
   public List<ClassListDTO> Show_similar_Merge_Class_List(List<string> listOfDid)
        {

            List<ClassListDTO> list = new List<ClassListDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (var item in listOfDid)
            {
                string did = item;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[SP_StudentMergeSimilarClasses]";
                cmd.Parameters.AddWithValue("@did", did);
               // cmd.Parameters.AddWithValue("@stCode", stcode);
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClassListDTO classDTO = new ClassListDTO();
                    classDTO.ClassCode = dt.Rows[i]["did"].ToString();
                    classDTO.CourseName = dt.Rows[i]["namedars"].ToString();
                    classDTO.ProfName = dt.Rows[i]["Ost_Name"].ToString();
                    classDTO.ClassDateTime = dt.Rows[i]["Klas_Day"].ToString();
                    classDTO.ClassStartTime = dt.Rows[i]["ClassTime"].ToString();//time
                    classDTO.MergeCode = dt.Rows[i]["Merge_Code"].ToString();
                    list.Add(classDTO);
                    //list.Insert(i, classDTO);

                }
                conn.Close();
            }
            return list;

        }



        public DataTable GetAllClassesByTerm(string term, int day)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_GetAllClassByTerm";
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@day", day);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        public DataTable getProfMergeName()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_GetAllostadMergeNameAndCodeOstad]";
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);

            conn.Close();
            return dt;
        }
        public DataTable CheckMergeCode(string mergeCode)
        {
            ///atarodi
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_CheckMergeCode]";
            cmd.Parameters.AddWithValue("@merge_Code", mergeCode);
            conn.Open();
            DataTable res = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            res.Load(rdr);
            conn.Close();
            return res;
        }
        public void MergeClass(ClassListDTO cls)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_insertToMergeClass]";
            cmd.Parameters.AddWithValue("@term", cls.Semester);
            cmd.Parameters.AddWithValue("@classCode", cls.ClassCode);
            cmd.Parameters.AddWithValue("@ClassName", cls.ClassName);
            cmd.Parameters.AddWithValue("@date_first_session", cls.FirstSession);
            cmd.Parameters.AddWithValue("@count_sessions", cls.SessionCount);
            cmd.Parameters.AddWithValue("@merge_Code", cls.MergeCode);
            cmd.Parameters.AddWithValue("@idostad", cls.ProfID);
            cmd.Parameters.AddWithValue("@lessonCode", cls.CourseCode);
            cmd.Parameters.AddWithValue("@SaatStart", cls.ClassStartTime);
            cmd.Parameters.AddWithValue("@SaatEnd", cls.ClassEndTime);
            cmd.Parameters.AddWithValue("@IdRoz", cls.ClassDay);
            cmd.Parameters.AddWithValue("@ClassCount", cls.ClassCount);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void EditMergeClass(ClassListDTO cls)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_Update_MergeClassByterm-Merge_Code]";
            cmd.Parameters.AddWithValue("@term", cls.Semester);
            cmd.Parameters.AddWithValue("@date_first_session", cls.FirstSession);
            cmd.Parameters.AddWithValue("@count_sessions", cls.SessionCount);
            cmd.Parameters.AddWithValue("@merge_Code", cls.MergeCode);
            cmd.Parameters.AddWithValue("@idostad", cls.ProfID);
            cmd.Parameters.AddWithValue("@lessonCode", cls.CourseCode);
            cmd.Parameters.AddWithValue("@SaatStart", cls.ClassStartTime);
            cmd.Parameters.AddWithValue("@SaatEnd", cls.ClassEndTime);
            cmd.Parameters.AddWithValue("@IdRoz", cls.ClassDay);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public int DeleteFromMergeClass(string term, int codedars)
        {
            int val;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_delete_MergeClassByterm-classCode]";
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@classCode", codedars);
            conn.Open();
            val = cmd.ExecuteNonQuery();
            conn.Close();
            return val;
        }

        public void DeleteMergeClasses(string term, int mergeCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_delete_MergeClassByterm-kolli]";
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@Merge_Code", mergeCode);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        #endregion


        #region Update
        #endregion


        #region Delete
        public void OpenClass(string stcode)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_OpenAccess";
            cmd.Parameters.AddWithValue("@stcode", stcode);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();

        }
        #endregion
    }
}
