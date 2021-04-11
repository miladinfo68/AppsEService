using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DAO.University.Research;

namespace IAUEC_Apps.Business.university.Research
{
   public class ResearchBusiness
    {
        ResearchDAO DAO = new ResearchDAO();
        //public DataTable StHasThesis(string stcode)
        //{
        //    return DAO.StHasThesis( stcode);
        //}
        public DataTable StInSomFields(int idresh)
        {
         return DAO.StInSomFields(idresh);
        }
        public void insert_Ostad(string name_user, int oscode, string name_, string famili, string resh, int groh, string mail, string cli_ip, string date_sabt, string time_sabt, int tay_groh, int rotbe, string user_pass, int magta, int fall, int z_rah, int z_mosh, int tham, int jens, int payeh, int tayed_os, int lock_mad, string date_b, string mahal_b, string mahal_s, int sh_sh, int ostan, string bimeh, int bank, string hesab, int groh_k, int ozv_shora, string estkh, string code_sazmani, string mob_os, string address_os, string code_meli, int result_code)
        {
            DAO.insert_Ostad(name_user, oscode, name_, famili, resh, groh, mail, cli_ip, date_sabt, time_sabt, tay_groh, rotbe, user_pass, magta, fall, z_rah, z_mosh, tham, jens, payeh, tayed_os, lock_mad, date_b, mahal_b, mahal_s, sh_sh, ostan, bimeh, bank, hesab, groh_k, ozv_shora, estkh, code_sazmani, mob_os, address_os, code_meli,result_code);
        }

        public DataTable GetProfInfoByID(int id_os)
        {
            return DAO.GetProfInfoByID(id_os);
        }
        public void InsertIntoOstan(int id_ostan, string name_ostan)
        {
             DAO.InsertIntoOstan(id_ostan,name_ostan);
        }

        public DataTable GetProfInfo()
        {
            return DAO.GetProfInfo();
        }

        public bool permitStudentToEnterPortal(string stcode)
        {
            return DAO.changePortalEntryPermit(stcode, true);
        }
        
        public bool dontPermitStudentToEnterPortal(string stcode)
        {
            return DAO.changePortalEntryPermit(stcode, false);
        }
        public DataTable PortalEntryPermition()
        {
            return DAO.getPortalEntryPermition();
        }

        public DataTable GetTeacherUserPass(int ocode)//, string password
        {
           return DAO.GetTeacherUserPass(ocode);//,password
        }

        public string getTeacherName_Fostad(int ocode)
        {
            DataTable dt = DAO.GetTeacherName(ocode);
            string name = "";
            if (dt.Rows.Count == 1)
            {
                name = string.Format("{0} {1}", dt.Rows[0]["name"].ToString().Trim(), dt.Rows[0]["family"].ToString().Trim());
            }
            return name;
        }


        //public void InsertIntoOS_TB(int id_os, string famili_os, string name_os, string resh, int rotbe,  string code_meli, string mob_os, string mail, string date_sabt, string time_sabt, int type_ham, int jens, string mahal_t, string tarikh_t, string mahal_sodor, string sh_shn)
       
        //{
        //    DAO.InsertIntoOS_TB(id_os,famili_os,name_os,resh,rotbe,code_meli,mob_os,mail,date_sabt,time_sabt,type_ham,jens,mahal_t,tarikh_t,mahal_sodor,sh_shn);
        //}
        public DataTable GetAllOstadInfo()
        {
            return DAO.GetAllOstadInfo();
        }
        public DataTable GetOstadInfoByCodeOstad(int CodeOstad)
        {
            return DAO.GetOstadInfoByCodeOstad(CodeOstad);
        }

        public DataTable GetStudentFromStudentsByStcode(int stcode)
        {
            return DAO.GetStudentFromStudentsByStcode(stcode);
        }
        public void InsertStudentInfoToStudents(int stcode, string name, string famili, int vrodi, string code_meli, int resh,int groh, string date_sabt, string time_sabt, int maghta, string mob_stu, string address_stu, int term,string mail, int jens, string pic_stu)
        {
            DAO.InsertStudentInfoToStudents(stcode, name, famili, vrodi, code_meli, resh, groh, date_sabt, time_sabt, maghta, mob_stu, address_stu, term, mail, jens, pic_stu);
        }

        public void InsertIntoGrouh(int id_groh, string name_groh, int id_college)
        {
            DAO.InsertIntoGrouh(id_groh,name_groh,id_college);
        }
        public DataTable GetGrohInfobyId(int id_groh)
        {
            return DAO.GetGrohInfobyId(id_groh);
        }

        public DataTable CheckStInSecondTerm(string stcode)
        {
            return DAO.CheckStInSecondTerm(stcode);
        }
    }
}
