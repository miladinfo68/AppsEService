using IAUEC_Apps.DAO.University.Students;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.university.Students
{
    public class CardQuizTemporaryStudentsBusiness
    {
        CardQuizTemporaryStudentsDAO CQTS = new CardQuizTemporaryStudentsDAO();
        public DataTable CardQuizStudents(int Daneshkade, int Field, int Degree, int Dorpar, int Sex, string SalVorod, int MablaghAz, int MablaghTa, string TarikhSodor, string ModatEtebar, string Semat, string FamilySemat, int Mojaz, string Term, int CtrlCheck, int TypeAction , string stCode)
        {
            return CQTS.CardQuizStudents(Daneshkade, Field, Degree, Dorpar, Sex, SalVorod, MablaghAz, MablaghTa, TarikhSodor, ModatEtebar, Semat, FamilySemat, Mojaz, Term, CtrlCheck, TypeAction , stCode);
        }
        public DataTable GetListStudentsNotImage(int Daneshkade, int Field, int Degree, int Dorpar, int Sex, string SalVorod, int MablaghAz, int MablaghTa, int Mojaz, string Term, int CtrlCheck, string stCode)
        {
            return CQTS.GetListStudentsNotImage(Daneshkade, Field, Degree, Dorpar, Sex, SalVorod, MablaghAz, MablaghTa, Mojaz, Term, CtrlCheck, stCode);
        }
    }
}
