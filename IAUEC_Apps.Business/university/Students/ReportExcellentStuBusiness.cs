using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Students;
using System.Data;

namespace IAUEC_Apps.Business.university.Students
{
     public class ReportExcellentStuBusiness
    {
         ReportExcellentStuDAO RES = new ReportExcellentStuDAO();

         public DataTable GetInfoSTU(int Daneshkade, int Field, int Education, string SalVorod, int Degree, int NimsalVorod, int Vaziatkol, int Sex, string FromDate, string ToDate)
         {
             return RES.GetInfoSTU(Daneshkade, Field, Education, SalVorod, Degree, NimsalVorod, Vaziatkol, Sex, FromDate, ToDate);
         }
         public DataTable ExcellentStudents(string Term, string SalVorod, int NimsalVorod, int Degree, int Field)
         {
             return RES.dtExcellentStudents(Term, SalVorod, NimsalVorod, Degree, Field);
         }
    }
}
