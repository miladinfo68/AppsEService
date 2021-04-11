using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Exam;

namespace IAUEC_Apps.Business.university.Exam
{
    public class ExamShowClockBusiness
    {
      
        ExamShowClockDAO ShowNimsalTerm = new ExamShowClockDAO();

        public DataTable  ShowTerm()
        {
            return ShowNimsalTerm.Get_ShowNimsal();
        }

        public DataTable SelectTerm(string tterm)
        {

            return ShowNimsalTerm.ShowClockDateExam(tterm);
        }
       

    }
}
