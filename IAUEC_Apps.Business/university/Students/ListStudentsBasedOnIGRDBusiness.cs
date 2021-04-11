using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DAO.University.Students;

namespace IAUEC_Apps.Business.university.Students
{
     public class ListStudentsBasedOnIGRDBusiness
    {
         ListStudentsBasedOnIGRDDAO LSBI = new ListStudentsBasedOnIGRDDAO();
         public DataTable GetVaznom()
         {
             return LSBI.GetVazNom();
         }
         public DataTable GetNameVaznom(int StatusScore)
         {
             return LSBI.GetNameVazNom(StatusScore);
         }
         public DataTable GetReportIGRDInfo(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Order, int Sex, int Vazkol, int Isargar, int TypeAccepted, int IS)
         {
             return LSBI.GetReportIGRDInfo(Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Order, Sex, Vazkol, Isargar, TypeAccepted , IS);
         }
         public DataTable GetReportGuestStudents(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Order, int Sex, DataTable Vazkol,  int TypeAccepted, string Term)
         {
             return LSBI.GetReportGuestStudents(Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Order, Sex, Vazkol, TypeAccepted, Term);
         }
         public DataTable GetReportTransferStudents(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, DataTable Vazkol, int TypeAccepted,string Term)
         {
             return LSBI.GetReportTransferStudents(Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Sex, Vazkol, TypeAccepted, Term);
         }
         public DataTable GetReportAddressTelStudents(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Order, int Sex, int Vazkol, int Isargar, int TypeAccepted, int IS, string Term)
         {
             return LSBI.GetReportAddressTelStudents(Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Order, Sex, Vazkol, Isargar, TypeAccepted, IS, Term);
         }
         public DataTable GetReportLackOfStudents(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex,  int TypeAccepted, string Term, DataTable Vazkol, int type)
         {
             return LSBI.GetReportLackOfStudents(Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Sex, TypeAccepted, Term , Vazkol , type);
         }
         public DataTable GetIncompleteStudents(DataTable Vazkol,int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int TypeAccepted, int naghsType, string naghsDate, int termstatus, string term)
         {
             return LSBI.GetIncompleteStudents(Vazkol,Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Sex, TypeAccepted, naghsType, naghsDate, termstatus, term);
         }
         public DataTable GetstudentsGeneralInfo(DataTable Vazkol, int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int TypeAccepted)
         {
             return LSBI.GetstudentsGeneralInfo(Vazkol, Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Sex, TypeAccepted);
         }
         public DataTable Moadelsazi(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int TypeAccepted, DataTable Vazkol, string term)
         {
             return LSBI.Moadelsazi(Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Sex, TypeAccepted, Vazkol,term);
         }
         public DataTable Sabtenambatakhir(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int TypeAccepted, DataTable Vazkol, string term,int vazeterm)
         {
             return LSBI.Sabtenambatakhir(Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Sex, TypeAccepted, Vazkol, term, vazeterm);
         }
         public DataTable GetStatusScoreStudent(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int Isargar, int TypeAccepted, int IS, int Vazkol, int VaziatNomre, string Term)
         {
             return LSBI.GetStatusScoreStudent(Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Sex, Isargar, TypeAccepted, IS, Vazkol, VaziatNomre, Term);
         }
         public DataTable GetStatusTermStudent(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex,  int TypeAccepted,  DataTable Vazkol, string Term, int VaziatTerm)
         {
             return LSBI.GetStatusTermStudent(Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Sex, TypeAccepted, Vazkol, Term, VaziatTerm);
         }
         public DataTable GetFormStudentsGuest(int Daneshkade, int Field, string Salvorod, string stcode, int NimsalVorod, int Dorpar, int Degree, int Sex, int Isargar, int TypeAccepted, int IS, int Vazkol, string Term, string ShomareName, string TarikhName)
         {
             return LSBI.GetFormStudentsGuest(Daneshkade, Field, Salvorod, stcode, NimsalVorod, Dorpar, Degree, Sex, Isargar, TypeAccepted, IS, Vazkol, Term, ShomareName, TarikhName);
         }
         public DataTable GetStudentBehzisti(string stcode, int Degree, int Sex, int Education, int Field, string SalVorod, int StatusStu)
         {
             return LSBI.GetStudentBehzisti(stcode, Degree, Sex, Education, Field, SalVorod, StatusStu);
         }
         public DataTable GetStudentIsargar(string stcode, int Degree, int Sex, int Education, int Field, string SalVorod, int StatusStu)
         {
             return LSBI.GetStudentIsargar(stcode, Degree, Sex, Education, Field, SalVorod, StatusStu);
         }
         public DataTable ConvertNumberToWord(string Mark)
         {
             return LSBI.ConvertNumberToWord(Mark);
         }
        
    }
}
