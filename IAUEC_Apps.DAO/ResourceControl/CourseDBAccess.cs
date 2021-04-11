using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ResourceControl.Entity;

namespace ResourceControl.DAL
{
    public class CourseDBAccess
    {
        public Course GetCourseDetails(int corID)
        {
            Course course = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@courseID", corID)
            };
            //need an stored procedure to get course data , no sp exist yet !!!!!!!!
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("SP_GetCourseZarfDanesh", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];

                    course = new Course();
                    course.DID = Convert.ToInt32(row["dcode"]);
                    course.Name = row["namedars"] as string;
                    course.Capacity = Convert.ToInt32(row["zarfporm"]);
                    course.DaneshID = Convert.ToInt32(row["daneshid"]);
                }
            }
            return course;
        }

        public List<Course> GetCourseListByUserID(int userID)
        {
            List<Course> courselist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ostadID",userID)
            };
            //need sp to fetch course list by teacher id
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetListOfThisTermClassOfOstad]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    courselist = new List<Course>();

                    foreach (DataRow row in table.Rows)
                    {
                        Course course = new Course();
                        course.DID = Convert.ToInt32(row["did"]);
                        string kstime = "";
                        if (row["saatklass"] != System.DBNull.Value)
                        {
                            kstime = (row["saatklass"] as string).Replace("شماره کلاس کلاس آنلاينعادي", "");
                            kstime = kstime.Replace("شماره کلاسکلاس آنلاينعادي", "");
                        }
                        course.Name = row["namedars"] as string + " - " + kstime;
                        course.Capacity = Convert.ToInt32(row["zarfporm"].ToString());
                        course.saatklass = row["saatklass"] as string;
                        course.DaneshID = Convert.ToInt32(row["iddanesh"].ToString());
                        var catId = row["catID"].ToString();
                        if (!(string.IsNullOrEmpty(catId) || string.IsNullOrWhiteSpace(catId))) 
                            course.catID = Convert.ToInt32(row["catID"].ToString());
                        var status = row["status"].ToString();
                        if (!(string.IsNullOrEmpty(status) || string.IsNullOrWhiteSpace(status)))
                            if (row["status"] != null)
                            course.status = Convert.ToInt32(row["status"].ToString());

                        courselist.Add(course);
                    }
                }
            }

            return courselist;
        }

        public DataTable GetShortCourseList()
        {
            DataTable courselist = null;

            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("@depcode", 0)
             };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[dbo].[SP_Get_AllProfessor]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    courselist = table.Copy();
                }
            }

            return courselist;
        }

        public Course GetCourseDetails2(int classID)
        {
            Course course = null;

            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("@classID", classID)
             };
            //need an stored procedure to get course data
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];

                    course = new Course();
                    course.Name = row["CourseName"] as string;
                    course.Capacity = Convert.ToInt32(row["FILL_CAPACITY"]);
                    course.DaneshID = Convert.ToInt32(row["DepCode"]);
                }
            }
            return course;
        }
    }
}