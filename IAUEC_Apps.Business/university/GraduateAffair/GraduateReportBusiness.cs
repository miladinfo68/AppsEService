using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAO.University.GraduateAffair;

namespace IAUEC_Apps.Business.university.GraduateAffair
{
    public class GraduateReportBusiness
    {
        GraduateReportDAO GraduateDAO = new GraduateReportDAO();


        // public DataTable GetTop50Student()
        //{
        //    return GraduateBusiness.GetTop50Student();
        //}
        public int InsertTaeidieTahsili(string stcode, int type_govahi, string num_namehaz, string date_namehaz, string name_bekoja)
        {
            return GraduateDAO.InsertTaeidieTahsili(stcode, type_govahi, num_namehaz, date_namehaz, name_bekoja);
        }
        public void DeleteTaeidieTahsili(int ID)
        {
            GraduateDAO.DeleteTaeidieTahsili(ID);
        }

        public bool deleteStudentFromThrMrkz(string studentCode)
        {
            return GraduateDAO.deleteStudentFromTehranMarkazList(studentCode);
        }

        //public DataTable Save_Serial_Govahi(int code)
        //{
        //    return GraduateBusiness.Save_Serial_Govahi(code);
        //}
        public DataTable GetGraduateStudentsByParams(string whereclause)
        {
            return GraduateDAO.GetGraduateStudentsByParams(whereclause);
        }

        public DataTable GetGraduateStudent()
        {
            return GraduateDAO.GetGraduateStudent();
        }
        public DataTable GetPishnevisElamFeraghat(string stcode)
        {
            return GraduateDAO.GetPishnevisElamFeraghat(stcode);
        }

        public DataTable GetStudentInfoByStcode(string stcode)
        {
            return GraduateDAO.GetStudentInfoByStcode(stcode);
        }

        public DataTable GetStudentGovahiByStcode(string stcode)
        {
            return GraduateDAO.GetStudentGovahiByStcode(stcode);
        }

        //public DataTable GetStudentGovahiByID(string stcode,int id)
        //{
        //    return GraduateBusiness.GetStudentGovahiByStcodeByID(stcode,id);
        //}
        //ramezanian
        public DataTable GetSeniorEncyclopedia(string stCode)
        {
            return GraduateDAO.GetSeniorEncyclopedia(stCode);
        }

        public DataTable GetStudentGovahiByID(int id)
        {
            return GraduateDAO.GetStudentGovahiByID(id);
        }
        public void InsertFar_TehranMarkaz(string stcode)
        {
            GraduateDAO.InsertFar_TehranMarkaz(stcode);
        }
        public DataTable GetTHRMarkaz(int type, int act)
        {
            return GraduateDAO.GetTHRMarkaz(type, act);
        }
        public DataTable GetTehranMarkaz()
        {
            return GraduateDAO.GetTehranMarkaz();
        }
        public void UpdateConvertExcel_Far_Stcode(DataTable lststcode)
        {
            GraduateDAO.UpdateConvertExcel_Far_Stcode(lststcode);
        }

        public bool updateExcelStatusToNotConverted(string studentCode)
        {
            return GraduateDAO.updateExcelStatusInTehranMarkazList(studentCode, 0);
        } 

        public bool updateExcelStatusToExported(string studentCode)
        {
            return GraduateDAO.updateExcelStatusInTehranMarkazList(studentCode, 1);
        }

        public DataTable getStudentBystCode(string stCode)
        {
            DataTable dtStudent = GraduateDAO.getStudentInf(stCode,"",-1);
            return dtStudent;
        }
        public DataTable getGraduatedStudentBystCode(string stCode)
        {
            DataTable dtStudent = GraduateDAO.getStudentInf(stCode, "", 7);
            return dtStudent;
        }
        public DataTable getStudentByIddMelli(string iddMeli)
        {
            DataTable dtStudent = GraduateDAO.getStudentInf("", iddMeli, -1);
            return dtStudent;
        }
        public DataTable getStudentsByVaziat(int vaziatTahsili)
        {
            DataTable dtStudent = GraduateDAO.getStudentInf("","",vaziatTahsili);
            return dtStudent;
        }
        public DataTable getStudentsInf()
        {
            DataTable dtStudent = GraduateDAO.getStudentInf("", "", -1);
            return dtStudent;
        }
    }
}
