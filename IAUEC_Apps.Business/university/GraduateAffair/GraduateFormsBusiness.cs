using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAO.University.GraduateAffair;
using IAUEC_Apps.DTO.University.Graduate;

namespace IAUEC_Apps.Business.university.GraduateAffair
{
    public class GraduateFormsBusiness
    {
        GraduateFormsDAO gfo = new GraduateFormsDAO();

        #region get
        public DataTable getStudentInfo(GraduateFormsDTO gfd)
        {
            return gfo.GetStudentInfo(gfd);
        }

        public DataTable searchStudentInfo_FeraghatDocument(string stcode, string family)
        {
            return gfo.searchStudentInfo_FeraghatDocument(stcode, family);
        }

        public DataTable getStudentRequestes(string stcode)
        {
            return gfo.getAllStudentRequest(stcode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stcode"></param>
        /// <param name="inquiryType">1:govahi 2:daneshname</param>
        /// <returns></returns>
        public DataTable getStudentInquiry(string stcode,string nationalCode,int inquiryType)
        {
            return gfo.getInquiry(stcode,nationalCode, inquiryType);
        }
        public DataTable getStudentInquiry(string stcode,int inquiryType)
        {
            return gfo.getInquiry(stcode,"", inquiryType);
        }
        public void deleteInquiry(int inquiryID)
        {
            gfo.deleteInquiry(inquiryID);
        }
        public DataTable getStudentInquiry(string stcode)
        {
            return getStudentInquiry(stcode,"",0);
        }
        public DataTable getStudentInquiry(string nationalCode,string stcode="")
        {
            return getStudentInquiry(stcode,nationalCode,0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inquiryType">1:govahi 2:daneshname</param>
        /// <returns></returns>
        public DataTable getStudentInquiry(int inquiryType)
        {
            return getStudentInquiry("","",inquiryType);
        }
        public DataTable getStudentInquiry()
        {
            return getStudentInquiry("","",0);
        }

        public int insertStudentInquiry(Inquiry inquiry)
        {
            return gfo.insertInquiry(inquiry);
        }

        public bool updateStudentInquiry(Inquiry inquiry)
        {
            return gfo.updateInquiry(inquiry)>0;
        }

        public StudentFeraghatDocument getStudentFeraghatDocument(string stcode)
        {
            StudentFeraghatDocument sfd = new StudentFeraghatDocument();
            DataTable dt = gfo.getStudentFeraghatDocument(stcode);
            if (dt.Rows.Count > 0)
            {
                sfd.stcode = dt.Rows[0]["stcode"].ToString();
                sfd.RequestID = Convert.ToInt32(dt.Rows[0]["StudentRequestID"]);
                sfd.RequestStatus = Convert.ToInt32(dt.Rows[0]["RequestLogID"]);
                sfd.RequestStatus_String = dt.Rows[0]["RequestLogName"].ToString();
                sfd.ArchivesCode = dt.Rows[0]["ArchivesCode"].ToString();

                sfd.dateDaneshnameSodur = dt.Rows[0]["DateSodoorDaneshname"].ToString();
                sfd.dateDaneshnameTahvil = dt.Rows[0]["DateDaneshDeliver"].ToString();
                sfd.dateDaneshnameVorud = dt.Rows[0]["DateVoroodDaneshname"].ToString();
                sfd.dateGovahiSodur = dt.Rows[0]["DateSodoorGovahi"].ToString();
                sfd.dateGovahiTahvil = dt.Rows[0]["DateGovahiDeliver"].ToString();
                sfd.dateGovahiVorud = dt.Rows[0]["DateVoroodGovahi"].ToString();
                sfd.dateRiznomreSodur = dt.Rows[0]["DateSodoorRizNomre"].ToString();
                sfd.dateRiznomreTahvil = dt.Rows[0]["DateRizNomreDeliver"].ToString();
                sfd.dateRiznomreVorud = dt.Rows[0]["DateVoroodRizNomre"].ToString();
                sfd.dateRiznomreErsal = dt.Rows[0]["DateErsalRizNomre"].ToString();

                sfd.genderValue = Convert.ToInt32(dt.Rows[0]["sex"]);
                sfd.genderName = sfd.genderValue == 1 ? "مرد" : "زن";

                sfd.HasStamp = Convert.ToBoolean(dt.Rows[0]["HasStamp"]);
                sfd.HasPaymentReceipt = Convert.ToBoolean(dt.Rows[0]["HasPaymentReceipt"]);

                sfd.name = dt.Rows[0]["name"].ToString();
                sfd.family = dt.Rows[0]["family"].ToString();
                sfd.idd_meli = dt.Rows[0]["idd_meli"].ToString();
                sfd.lastTerm = dt.Rows[0]["lastTerm"].ToString();
                sfd.reshte = dt.Rows[0]["nameresh"].ToString();
                sfd.gerayesh = dt.Rows[0]["gerayesh"].ToString();
                sfd.maghta = dt.Rows[0]["magh"].ToString();
                sfd.pName = dt.Rows[0]["namep"].ToString();
                sfd.mobile = dt.Rows[0]["mobile"].ToString();
                sfd.mashmul = Convert.ToBoolean(dt.Rows[0]["mashmul"]);
                sfd.mashmulName = dt.Rows[0]["NezamName"].ToString();
                sfd.docArchiveID = dt.Rows[0]["archiveDoc_ID"].ToString();

                //sfd.archiveID_Movaghat = Convert.ToInt64(dt.Rows[0]["archiveCode"] == DBNull.Value ? 0 : dt.Rows[0]["archiveCode"]);
                //sfd.archiveID_Daneshname = Convert.ToInt64(dt.Rows[0]["archiveCode"] == DBNull.Value ? 0 : dt.Rows[0]["archiveCode"]);
                //sfd.archiveID_Riznomre = Convert.ToInt64(dt.Rows[0]["archiveCode"] == DBNull.Value ? 0 : dt.Rows[0]["archiveCode"]);


                sfd.archiveID_Movaghat = Convert.ToInt64(dt.Rows[0]["govahiMovaghat_ID"] == DBNull.Value ? 0 : dt.Rows[0]["govahiMovaghat_ID"]);
                sfd.archiveID_Daneshname = Convert.ToInt64(dt.Rows[0]["daneshname_ID"] == DBNull.Value ? 0 : dt.Rows[0]["daneshname_ID"]);
                sfd.archiveID_Riznomre = Convert.ToInt64(dt.Rows[0]["riznomre_ID"] == DBNull.Value ? 0 : dt.Rows[0]["riznomre_ID"]);

                sfd.serialNumber_Movaghat= dt.Rows[0]["govahiMovaghat_SerialNumber"].ToString();
                sfd.serialNumber_Daneshname = dt.Rows[0]["daneshname_SerialNumber"].ToString();
                sfd.documentNumber_Daneshname = dt.Rows[0]["daneshname_DocumentNumber"].ToString();
                sfd.documentNumber_Movaghat = dt.Rows[0]["govahiMovaghat_DocumentNumber"].ToString();


                sfd.SpecialTips = dt.Rows[0]["SpecialTips"].ToString();
                sfd.vahedSodur = dt.Rows[0]["vahedSodurMadrak"].ToString();
                sfd.vamdar = Convert.ToInt32(dt.Rows[0]["vamdar"]) > 0 ? true : false;
                sfd.PaiedToVahedSodoor = Convert.ToBoolean(dt.Rows[0]["PaiedToVahedSodoor"]);
                if (isNotNull(dt.Rows[0]["studentImage"]))
                    sfd.StudentImage = Common.CommonBusiness.objectToByteArray(dt.Rows[0]["studentImage"]);
                sfd.ResultRejToDep = dt.Rows[0]["ResultRejToDep"].ToString();
                sfd.DateRejToDep = dt.Rows[0]["DateRejToDep"].ToString();
                DataView dv = dt.DefaultView;
                dv.Sort = "UserLogId desc";
                DataTable dtTemp = dv.ToTable();
                DataRow[] dr205_23 = dtTemp.Select("eventID=205");// + (int)DTO.eventEnum.تایید_وضعیت_تسویه + " and descr like '%به مرحله ورود پرونده به معاونت دانشجویی%'");

                if (dr205_23.Length > 0)
                {
                    sfd.DateEnterStudentDep = dr205_23[0]["eventDate"].ToString();
                    sfd.countEnterStudentDep = dr205_23.Length;
                }


                DataRow[] dr207_24 = dtTemp.Select("eventID=207");// + (int)DTO.eventEnum.تایید_وضعیت_تسویه + " and descr like '%تایید در مرحله ارسال پرونده جهت صدور مدرک ارسال%'");
                if (dr207_24.Length > 0)
                {
                    sfd.dateSendPack = dr207_24[0]["eventDate"].ToString();
                }
                sfd.isOnline = Convert.ToBoolean(dt.Rows[0]["isOnline"]);

                //DataRow[] dr36_25 = dtTemp.Select("eventID=" + (int)DTO.eventEnum.تایید_وضعیت_تسویه + " and descr like '%ارسال پرونده جهت صدور%'");
                //if (dr36_25.Length > 0)
                //{
                //    sfd.DateEnterStudentDep = dr36_25[0]["eventDate"].ToString();
                //}


                //DataRow[] dr37 = dtTemp.Select("eventID=" + (int)DTO.eventEnum.ارسال_پیام_تسویه );
                //if (dr37.Length > 0)
                //{
                //    sfd.ResultRejToDep = dr37[0]["descr"].ToString();
                //    sfd.DateRejToDep = dr37[0]["eventDate"].ToString();
                //}
            }
            return sfd;
        }

        public bool IsEquivalentTwoYearsGraduated(string stcode)
        {
            var studentInf = getStudentInfo(new GraduateFormsDTO { stCode = stcode, family="", iddMeli="" });
            if (studentInf.Rows.Count > 0 && studentInf.Rows[0]["idvazkol"].ToString() == "5" && studentInf.Rows[0]["dateFar"].ToString() != "")
                return true;
            return false;
        }
        public bool UpdateFeraghatTahsil_GraduateDocument(StudentFeraghatDocument SFD, bool mashmulChanged = false)
        {
            return gfo.UpdateFeraghatTahsil_GraduateDocument(SFD.stcode,SFD.RequestID, SFD.HasStamp, SFD.HasPaymentReceipt, SFD.mashmul ? 7 : 6, SFD.SpecialTips, mashmulChanged,
                SFD.documentNumber_Movaghat ,SFD.documentNumber_Daneshname,SFD.serialNumber_Movaghat,SFD.serialNumber_Daneshname);
        }

        public int insertDocArchiveId(Int64 studentRequest)
        {
            return gfo.insertDocArchiveId(studentRequest);
        }
        public void freeDocArchiveId(Int64 studentRequest)
        {
            gfo.freeDocArchiveId(studentRequest);
        }

        private bool isNotNull(object o)
        {
            if (o != DBNull.Value)
                return true;
            return false;
        }

        public DataTable getStatusReportInfo(GraduateFormsDTO gfd)
        {
            if (gfd.family == null)
            {
                gfd.family = "";
            }
            return gfo.GetStatusReportInfo(gfd);
        }

        public DataTable getDrafReportInfo(GraduateFormsDTO gfd)
        {
            if (gfd.family == null)
            {
                gfd.family = "";
            }
            return gfo.GetDraftReportInfo(gfd);
        }

        public DataTable getCourseReportInfo(GraduateFormsDTO gfd)
        {
            if (gfd.family == null)
            {
                gfd.family = "";
            }
            return gfo.GetCoursePassesReportInfo(gfd);
        }

        public DataTable getMarkListReportInfo(GraduateFormsDTO gfd)
        {
            if (gfd.family == null)
            {
                gfd.family = "";
            }
            return gfo.GetMarkListReportInfo(gfd);
        }
        public DataTable getDocumentArchive()
        {
            return gfo.getDocumentArchive();
        }
        public DataTable getDocumentArchive_naghs()
        {
            return gfo.getDocumentArchive_naghs();
        }
        #endregion

        #region insert/update


        #endregion
    }
}
