using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DAO.University.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.university.Support
{
    public class ResetPasswordBusiness
    {
        #region Fileds
        ResetPasswordDAO _resetPassDAO = new ResetPasswordDAO();
        CommonBusiness _commonBusiness = new CommonBusiness();
        #endregion

        #region Professor
        public bool IsProfessorCodeExist(decimal professorCode)
        {
            return _resetPassDAO.IsProfessorCodeExist(professorCode);
        }
        public bool ChangeProfessorPassword(decimal professorCode, string newPassword)
        {
            var encryptNewPassword = _commonBusiness.Encrypt(newPassword, true);
            return _resetPassDAO.ChangeProfessorPassword(professorCode, encryptNewPassword);
        }
        #endregion

        #region Student
        public bool IsStudentCodeExist(decimal studentCode)
        {
            return _resetPassDAO.IsStudentCodeExist(studentCode);
        }
        public bool ChangeStudentPassword(decimal studentCode, string newPassword)
        {
            var encryptNewPassword = _commonBusiness.Encrypt(newPassword, true);
            return _resetPassDAO.ChangeStudentPassword(studentCode, encryptNewPassword);
        }
        #endregion
    }
}
