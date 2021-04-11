using ResourceControl.DAL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceControl.BLL
{
    public class RC_UserHandler
    {
        RC_UserDBAccess RC_UserDB = null;

        public RC_UserHandler()
        {
            RC_UserDB = new RC_UserDBAccess();
        }

        // public List<RC_User> GetRC_UserList()
        //{
        //    return RC_UserDB.GetRC_UserList();
        //}

        // public bool UpdateRC_User(RC_User user)
        //{
        //    return RC_UserDB.UpdateRC_User(user);
        //}

         public RC_User GetRC_UserDetails(int userID)
        {
            return RC_UserDB.GetUserDetails(userID);
        }

        public RC_User Get_Ostad_Details(int ostadID)
        {
            return RC_UserDB.Get_Ostad_Details(ostadID);
        }

        public List<RC_User> GetOstadListByDaneshID(int daneshID)
        {
            return RC_UserDB.GetOstadListByDaneshID(daneshID);
        }

        //public bool DeleteRC_User(int userID)
        //{
        //    return RC_UserDB.DeleteRC_User(userID);
        //}

        // public bool AddNewRC_User(RC_User user)
        //{
        //    return RC_UserDB.AddNewRC_User(user);
        //}

        public RC_User Get_Ostad_DetailsByCourseId(int CourseId)
        {
            return RC_UserDB.Get_Ostad_DetailsByCourseId(CourseId);
        }
    }
}
