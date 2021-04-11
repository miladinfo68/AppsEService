using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Education;
namespace IAUEC_Apps.Business.university.Education
{
    public class EducationReportBusiness
    {
        EducationReportDAO ERD = new EducationReportDAO();
        #region Read
        //public DataTable SelectAllTerm()
        //{
        //    return ERD.SellectAllTerm();
        //}
        public DataTable SelectAllLocatoionClass()
        {
            return ERD.SelectAllLocationClass();
        }
        public DataTable SelectRowLocationClass(int LocationClass)
        {
            return ERD.SelectRowLocationClass(LocationClass);
        }
        public DataTable SelectListClass(int Number, string Term, int Location)
        {
            return ERD.SelectListClass(Number, Term, Location);
        }
        public DataTable SelectAllDegree()
        {
            return ERD.SelectAllDegree();
        }
        public DataTable SelectAllField()
        {
            return ERD.SelectAllField();
        }
        public DataTable GetNameClass(int NumberClass)
        {
            return ERD.GetNameClass(NumberClass);
        }
        public DataTable GetListClassBarAsasRuz(string Term, int Day, string Saatstart, string Saatend, int Daneshkade, int Field, int Degree)
        {
            return ERD.GetListClassBarAsasRuz(Term, Day, Saatstart, Saatend, Daneshkade, Field, Degree);
        }
        public DataTable GetListClassHazfi(string Term, int Day, string Saatstart, string Saatend, int Daneshkade, int Field, int Degree)
        {
            return ERD.GetListClassHazfi(Term, Day, Saatstart, Saatend, Daneshkade, Field, Degree);
        }
        public DataTable GetListklasTarikhNomre(string Term, int Daneshkade, string FromDate, string ToDate, int Field, int Day, string Saatstart, string Saatend, int Degree)
        {
            return ERD.GetListklassTarikhTahvilNomre(Term, Daneshkade, FromDate, ToDate, Field,  Day,  Saatstart,  Saatend,  Degree);
        }
        public DataTable GetListTadakholSaat(string Term, string AzSaat, string TaSaat, string Saatstart, string Saatend, int Daneshkade, int Field ,int Day, int Degree)
        {
            return ERD.GetListTadakholBaSaatKhas(Term, AzSaat, TaSaat, Saatstart, Saatend, Daneshkade, Field ,  Day,  Degree);
        }
        public DataTable GetListHadeNesabKlas(string Term, int MinCapacity,int MaxCapacity, int Vahed, int Daneshkade, int Field, string Saatstart, string Saatend, int Day, int Degree)
        {
            return ERD.GetListClassToLimitSupply(Term, MinCapacity, MaxCapacity, Vahed, Daneshkade, Field, Saatstart,  Saatend,  Day,  Degree);
        }
        public DataTable GetListMoghayerat(int AzMoshakhase, int TaMoshakhase, int Field, string Term)
        {
            return ERD.GetListMoghayerat(AzMoshakhase, TaMoshakhase, Field, Term);
        }
        public DataTable GetLisAdamSabtenam(string Term, string StCode, int Degree, int Education, int Sex, string SalVorud, int Field)
        {
            return ERD.GetListAdamMojavez(Term , StCode, Degree, Education, Sex, SalVorud, Field);
        }
        public DataTable GetSelectAllDepartman()
        {
            return ERD.GetSelectAllDepartman();
        }
        public DataTable GetListBarnameHaftegi(string Term, int Day, int LocationClass, int Field, int Departman, int Daneshkade, int Degree, string SaatStart, string SaatEnd)
        {
            return ERD.GetListBarnameHaftegi(Term, Day, LocationClass, Field, Departman, Daneshkade, Degree , SaatStart , SaatEnd);
        }
        public DataTable GetListDorus()
        {
            return ERD.GetListDorus();
        }
        public DataTable GetListDorusGhabuli(string Term , int Field, int Lesson , int Geraesh , int Degree)
        {
            return ERD.GetListDorusGhabuli(Term,Field, Lesson, Geraesh , Degree);
        }
        public DataTable GetListDorusGhabuliBarAsasTerm(string Term, int Field, int Lesson,int Geraesh,int Degree)
        {
            return ERD.GetListDorusGhabuliBarAsasTerm(Term, Field, Lesson,Geraesh, Degree );
        }
        public DataTable GetListEshteghalBeTahsil(string FromDate, string ToDate, string Term)
        {
            return ERD.GetListEshteghalBeTahsil(FromDate, ToDate, Term);
        }
        public DataTable GetReshByDaneshkade(int Daneshkade)
        {
            return ERD.GetReshByDaneshkade(Daneshkade);
        }
        public DataTable GetLessonByField(int Field)
        {
            return ERD.GetLessonByField(Field);
        }
       public DataTable GetNameLesson( int Lesson)
       {
           return ERD.GetNameLesson( Lesson);
       }
       public DataTable GetNameField(int Field)
       {
           return ERD.GetNameField(Field);
       }
       public DataTable SelectFieldByDegree(int degree)
       {
           return ERD.SelectFieldByDegree(degree);
       }
       public DataTable GetGeraeshByField(int Field)
       {
           return ERD.GetGeraeshByField(Field);
       }
       public DataTable GetInfoAllStudent(string StCode, string Family, string NameEp, string IdMeli, int Degree, int StatusStu, int Field)
       {
           return ERD.GetInfoAllStudent(StCode, Family, NameEp, IdMeli, Degree, StatusStu, Field);
       }
       public DataTable Getthesis(string Term, int Daneshkade)
       {
           return ERD.Getthesis(Term, Daneshkade);
       }
        #endregion Read
    }
}
