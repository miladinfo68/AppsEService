using ResourceControl.DAL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ResourceControl.BLL
{
    public class CourseHandler
    {
        CourseDBAccess CourseDB = null;
        ShortTermDBAccess ShortTermDB = null;

        public CourseHandler()
        {
            CourseDB = new CourseDBAccess();
            ShortTermDB = new ShortTermDBAccess();
        }

        public Course GetCourseDetails(int corID)
        {
            return CourseDB.GetCourseDetails(corID);
        }

        public List<Course> GetCourseListByUserID(int userID)
        {
            return CourseDB.GetCourseListByUserID(userID);
        }


        public DataTable GetShortCourseList()
        {
            return ShortTermDB.GetShortCourseList();
        }

        public Course GetCourseDetails2(int courseID)
        {
            return CourseDB.GetCourseDetails2(courseID);
        }

        public List<Course> GetShortTermByProfId(int profId)
        {
            return ShortTermDB.GetShortTermByProfId(profId);
        }

        public DataTable GetShortTermProfByDepId(int depId)
        {
            return ShortTermDB.GetShortTermProfByDepId(depId);
        }
    }
}