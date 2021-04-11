using IAUEC_Apps.DAC.Connections;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ResourceControl.DAL
{
    public class ShortTermDBAccess
    {
        ShortTermConnection ShortConn = null;

        public ShortTermDBAccess()
        {
            ShortConn = new ShortTermConnection();
        }

        public List<Course> GetShortTermByProfId(int profId)
        {
            List<Course> courselist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@depId",profId)
            };
            //need sp to fetch course list by teacher id
            using (DataTable table = SqlDBHelper_ShortCourcse.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetListOfThisTermClassOfOstad]", CommandType.StoredProcedure, parameters))
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
                        course.Capacity = Convert.ToInt32(row["zarfporm"]);
                        course.saatklass = row["saatklass"] as string;
                        course.DaneshID = Convert.ToInt32(row["iddanesh"]);

                        courselist.Add(course);
                    }
                }
            }

            return courselist;
        }

        public DataTable GetShortTermProfByDepId(int depId)
        {
            DataTable userlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DEPARTMENT_CODE",depId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[dbo].[SP_Get_AllProfessor_By_DepartmentCode]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {

                    userlist = table.Copy();
                }
            }

            return userlist;
        }

        public DataTable GetShortCourseList()
        {
            DataTable courselist = null;

            int depcode = 0;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@depcode", depcode)
            };

            using (DataTable table = SqlDBHelper_ShortCourcse.ExecuteParamerizedSelectCommand("[dbo].[SP_Get_AllProfessor]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    courselist = table.Copy();
                }
            }

            return courselist;
        }
    }
}
