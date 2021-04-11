using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Students;
using System.Data;

namespace IAUEC_Apps.Business.university.Students
{
    
    public class InformationRationBusiness
    {
        InformationRationDAO IRD = new InformationRationDAO();

        public DataTable GetInfoSahmie(string Term, int Sex, int StatusStu, int Degree)
        {
            return IRD.GetInfoSahmie(Term, Sex, StatusStu, Degree);
        }
        public DataTable GetInfoSahmie2(string Term, int Sex, int Degree, int StatusStu)
        {
            return IRD.GetInfoSahmie2(Term, Sex, Degree, StatusStu);
        }

        public DataTable GetInfoSahmiePermitted(string Term, int Sex, int Degree, int StatusStu)
        {
            return IRD.GetInfoSahmiePermitted(Term, Sex, Degree, StatusStu);
        }
    }
}
