using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.CommonDAO;
using System.Data;

namespace IAUEC_Apps.Business.Common
{

    public class UniversityBusiness
    {
        UniversityDAO UD = new UniversityDAO();

        public DataTable GetStudentPic(string stcode)
        {
            return UD.GetStudentPic(stcode);
        }
        public DataTable GetTop50Student()
        {
            return UD.GetTop50Student();
        }

        public DataTable GetTermJary()
        {
            return UD.GetTermJary();
        }

        public DataTable GetNimsalJary()
        {
            return UD.GetNimsalJary();
        }
        
        public DataTable GetFieldByDepartman(int Departman)
        {
            return UD.GetFieldByDepartman(Departman);
        }
    }
}
