using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DAO.Email;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.EmailClasses;

namespace IAUEC_Apps.Business.Email
{
    public class User_LoginBusiness
    {
        UserLoginDAO userLoginDAO = new UserLoginDAO();
        public void Insert_NewUser(string Name, string UserName, string Password, int RoleID)
        {
            userLoginDAO.Insert_NewUser(Name, UserName, Password, RoleID);
        }
        public List<User_LoginDTO> User_Login(string userName, string pass)
        {
            DataTable dt = new DataTable();
            User_LoginDTO loginDTO = new User_LoginDTO();
            List<User_LoginDTO> lstloginDTO = new List<User_LoginDTO>();
            UserAccessBusiness ub = new UserAccessBusiness();
            dt = userLoginDAO.User_Login(userName, pass);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ub.DecryptPass(dt.Rows[0]["Password"].ToString()) == pass)
                {
                    loginDTO.name = dt.Rows[i]["Name"].ToString();
                    loginDTO.UserName = dt.Rows[i]["UserName"].ToString();
                    loginDTO.RoleID = int.Parse(dt.Rows[i]["RoleID"].ToString());
                    loginDTO.Password = dt.Rows[i]["Password"].ToString();
                    loginDTO.id = int.Parse(dt.Rows[i]["id"].ToString());
                    lstloginDTO.Add(loginDTO);
                }
            }
            return lstloginDTO;
        }
    }
}

