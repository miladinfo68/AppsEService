using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.Business.Common;

using System.Data;

namespace IAUEC_Apps.Business.Common
{
    public class LoginBusiness
    {
        LoginDAO LoginDAO = new LoginDAO();
        CommonDAO cma = new CommonDAO();

        #region Read
        public DataTable StHasNaghs(string stcode)
        {
            return LoginDAO.StHasNaghs(stcode);
        }

        public DataTable GetStIdVaz(string stcode)
        {
            return LoginDAO.GetStIdVaz(stcode);
        }
        public LoginDTO User_Login(string UserName)//, string Password
        {
            /// Login via Amozesh
            /// return LoginDAO.User_Login(UserName, Password);
            /// 

            /// Login via Supplementary
            return LoginDAO.User_Login(UserName);
            /// 

        }
        public bool IsUserForbidenToLogin(string stcode)
        {
            DataTable dt = new DataTable();
            dt = LoginDAO.IsUserForbidenToLogin(stcode);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public StuImg User_Img(string stcode)
        {
            DataTable dt = new DataTable();
            dt = LoginDAO.User_Img(stcode);
            StuImg st = new StuImg();
            if (dt.Rows.Count > 0)
            {

                st.img = (byte[])dt.Rows[0]["stu_pic"];
            }
            else
                st.img = null;
            return st;
        }
        public LoginDTO Get_StInfo(string stcode)
        {
            DataTable dt = new DataTable();
            dt = LoginDAO.Get_StInfo(stcode);
            LoginDTO userInfo = new LoginDTO();
            if (dt != null && dt.Rows.Count > 0)
            {
                userInfo.Name = dt.Rows[0]["name"].ToString();
                userInfo.LastName = dt.Rows[0]["family"].ToString();
                userInfo.StReshte = dt.Rows[0]["nameresh"].ToString();
            }
            return userInfo;
        }

        public List<LoginDTO> Get_UserLogin(string username, string password)
        {
            LoginDTO lngDTO = new LoginDTO();
            List<LoginDTO> lngDTOlst = new List<LoginDTO>();
            DataTable dt = new DataTable();
            dt = LoginDAO.Get_UserLogin(username);
            if (dt.Rows.Count > 0)
            {
                if (CommonBusiness.DecryptPass(dt.Rows[0]["Password"].ToString()) == password)
                {
                    lngDTO.UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
                    lngDTO.Name = dt.Rows[0]["Name"].ToString();
                    lngDTO.Password = dt.Rows[0]["Password"].ToString();
                    lngDTO.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"]);
                    lngDTO.Enable = Convert.ToBoolean(dt.Rows[0]["Enable"].ToString());
                    lngDTO.sectionId = Convert.ToInt32(dt.Rows[0]["SectionId"]);
                    foreach (DataRow dr in dt.Rows)
                        lngDTO.RoleName += dr["RoleName"].ToString() + ",";
                    lngDTO.RoleName = lngDTO.RoleName.Substring(0, (lngDTO.RoleName.Length > 0 ? lngDTO.RoleName.Length - 1 : 0));
                    lngDTO.UserName = username;
                }
                else
                {
                    lngDTO.UserId = 0;
                    lngDTO.Name = "";
                    lngDTO.Password = "";
                    lngDTO.Enable = false;
                }
                //lngDTO.RoleId = int.Parse(dt.Rows[0]["RoleId"].ToString());

            }
            else
            {
                //lngDTO.RoleId = 0;
                lngDTO.UserId = 0;
                lngDTO.Name = "";
                lngDTO.Password = "";
                lngDTO.Enable = false;
            }
            lngDTOlst.Add(lngDTO);
            return lngDTOlst;

        }
        public DataTable Get_Menu_ByUserIdAndAppId(int AppId, int UserId, int sectionId)
        {
            return LoginDAO.Get_Menu_ByUserIdAndAppId(AppId, UserId, sectionId);
        }
        public DataTable Get_Menu_ByUserIdAndAppId(int menuId)
        {
            return LoginDAO.Get_Menu_ByUserIdAndAppId(menuId);
        }
        public DataTable Get_MenuPermission(int MenuId, int UserId)
        {
            return LoginDAO.Get_MenuPermission(MenuId, UserId);
        }
        public DataTable Get_UserRoles(string userID)
        {
            return LoginDAO.GetUserRoles(userID);
        }
        public DataTable SendUserPassword(string username, string idd_meli, string idd, bool isstudent)
        {
            return LoginDAO.SendUserPassword(username, idd_meli, idd, isstudent);
        }

        public DataTable GetUserLoginByUserCode(string userCode, bool isStudent)
        {
            return LoginDAO.GetUserLoginByUserCode(userCode, isStudent);
        }

        public List<int> GetUserIdsByRoleId(int roleId)
        {
            return LoginDAO.GetUserIdsByRoleId(roleId).AsEnumerable().Select(r => r.Field<int>("UserId")).ToList();

        }
        #endregion

        #region Update
        public void changePassword(string pass, int userId)
        {
            cma.changePassword(pass, userId);
        }
        public void ChangePasswordAndEnable(string pass, int userId)
        {
            cma.ChangePasswordAndEnable(pass, userId);
        }
        public void UpdateExaminerPalcePasswordByExaminerID(string pass, int userId)
        {
            cma.UpdateExaminerPalcePasswordByExaminerID(pass, userId);
        }
        public bool SetChangePasswordToken(string userCode, bool isStudent, string token, DateTime expDate)
        {
            return LoginDAO.SetChangePasswordToken(userCode, isStudent, token, expDate) > 0;
        }
        public DataTable ResendChangePasswordToken(string userCode, DateTime expDate, bool isStudent)
        {
            return LoginDAO.ResendChangePasswordToken(userCode, expDate, isStudent);
        }

        public bool updatePortalStudentInfo(string stcode)
        {
            return LoginDAO.updatePortalStudentInfo(stcode);

        }
        #endregion
    }
}
