using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Students;
using System.Data;

namespace IAUEC_Apps.Business.university.Students
{
    
     public class PaymentStatusAbdEnrollment
    {
         PaymentStatusAndEnrollment PSAE = new PaymentStatusAndEnrollment();

     public DataTable GetStatusPayStu(string Term, int StatusStu, int Daneshkade, int Field, int Degree)
     {
         return PSAE.GetStatusPayStu(Term, StatusStu, Daneshkade, Field, Degree);
     }

     public DataTable GetStatusRegistrationStu(string Term, int StatusStu, int Daneshkade, int Field, int Degree)
     {
         return PSAE.GetStatusRegistrationStu(Term, StatusStu, Daneshkade, Field, Degree);
     }
     public DataTable GetStudentRegistrationReport(int DaneshId)
     {
         return PSAE.GetStudentRegistrationReport(DaneshId);
     }

    }
}
