using System;
using System.Data;
using IAUEC_Apps.Business.Conatct.Functions;
using IAUEC_Apps.DAO.ConatctDAO;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.Business.Conatct
{
    public static  class SendSmsContactBuisnes
    {
        #region check
        public static bool CheckSendedSmsToday(string idGrp)
        {
           DataTable dt;
           dt= SendSmsContactDAO.CheckSendedSms(idGrp);
            if (dt!=null&&dt.Rows.Count>0)
            {
                return true;
            }
            return false;
        }
        public static bool CheckTimeSendSms()
        {
            const int startTime = 8;//8 AM Start Send SMS
            const int endTime = 22;//10 PM END Send SMS
            if (DateTime.Now.Hour >=startTime&& DateTime.Now.Hour<endTime)
            {
                return true;
            }
  
            return false;
        }
        #endregion

        #region insert
        public static DataTable InsertSendSms(string idGrp)
        {
            DataTable DtInsert = null;

            if (idGrp != "" && idGrp != null && !(CheckSendedSmsToday(idGrp)))
            {
                DataTable dtOs;
                dtOs = ContactBuisnes.GetConatctOstads(idGrp);
                if (dtOs != null && dtOs.Rows.Count > 0)
                {

                    string persianDate = DatePersian.GetDateNow();
                    string Time = DatePersian.GetTimeNow12();
                    string fullNameSt = ContactBuisnes.FullNameSt(idGrp);
                    string textMsg = TextLogSms.GetTextLogSms(fullNameSt, idGrp);
                    DtInsert = SendSmsContactDAO.InsertLogSms(fullNameSt, idGrp, persianDate,
                       Time, textMsg, CheckTimeSendSms());   
                    if(CheckTimeSendSms())
                          SendSmsOS(dtOs, textMsg);

                }
            }
            return DtInsert;
            
        }
        public static DataTable UpdateWaitingSendSms(string idGrp, int waiting)
        {
            if (idGrp != "" && idGrp != null)
            {
                return SendSmsContactDAO.UpdateWaitingSendSms(idGrp, waiting);
            }
            return null;
        }
        #endregion

        #region get
        public static DataTable GetlogsNotSendSms(string idGrp="-1")
        {
            const int waiting= 0;
           return SendSmsContactDAO.SelectLogSms(idGrp, waiting);
            

        }
        #endregion

        #region sendsms
        public static void SendSmsOS(DataTable dt, string msg)
        {
            CommonBusiness cb = new CommonBusiness();
            foreach (DataRow dtRow in dt.Rows)
            {
                cb.sendSMS(dtRow["mobileOs"].ToString(), msg, out bool sentSMS, out string smsStatusText);
            }
        }
        public static void SendSmsSt(DataTable dt, string msg)
        {
            CommonBusiness cb = new CommonBusiness();
            foreach (DataRow dtRow in dt.Rows)
            {
                cb.sendSMS(dtRow["mobileSt"].ToString(), msg, out bool sentSMS, out string smsStatusText);
            }
        }
        public static void SendSmsSchedular()
        {
            DataTable dtLogSms = GetlogsNotSendSms();
            const int Approved = 1;
            DataTable dtOs ;
            foreach (DataRow dtRow in dtLogSms.Rows)
            {
                dtOs = ContactBuisnes.GetConatctOstads(dtRow["ID_Group"].ToString());
                string textMsg = TextLogSms.GetTextLogSms(dtRow["ID_Group"].ToString(),
                                    dtRow["FullName"].ToString());

                SendSmsOS(dtOs, textMsg);
                UpdateWaitingSendSms(dtRow["ID_Group"].ToString(), Approved);
            }
        }
        public static void SendSmsOsForOstadsDefence(string stcode,string stName,
                                    string dateStart,string dateEnd,string startTime,string endTime, bool edit=false)
        {

        DataTable dt;
        if(stcode=="99900999")
        {
        dt = new DataTable();
        dt.Columns.Add("mobileOs");
        DataRow dr = dt.NewRow();
        dr["mobileOs"] = "09192678116";
        dt.Rows.Add(dr);
        }
        else

        dt = ContactBuisnes.GetConatctOstads(stcode);
            if (dt != null && dt.Rows.Count > 0)
            {
                string msg = "";
                if (!edit)
                    msg = TextLogSms.GetTextSmsForAcceptedCommitmentDefencceOs(stName, dateStart, startTime, dateEnd.Substring(0, 10), endTime);
                else 
                    msg = TextLogSms.GetEditTextSmsForAcceptedCommitmentDefencceOs(stName, dateStart, startTime, dateEnd.Substring(0,10), endTime);

                SendSmsOS(dt, msg);
            }

        }
        public static void SendSmsStudent(string osName,string typeOs,string stCode)
        {
            DataTable dt = ContactBuisnes.GetContactAStudent(stCode);
            if(dt!=null&&dt.Rows.Count>0)
            {
                string msg = TextLogSms.GetTextSmsForRejectDefenceSt(osName, typeOs);
                SendSmsSt(dt, msg);
            }
        }
        public static void SendSmsStudentForRejectTechnichal(string stCode)
        {
            DataTable dt = ContactBuisnes.GetContactAStudent(stCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                string msg = TextLogSms.GetTextSmsStudentForRejectTechnichal();
                SendSmsSt(dt, msg);
            }
        }
        public static void SendSmsOstadForRejectTechnichal(string stCode,string stName)
        {
            DataTable dt = ContactBuisnes.GetConatctOstads(stCode);
            string msg = TextLogSms.GetTextSmsOstadForRejectTechnichal(stName);
            if (dt != null && dt.Rows.Count > 0)
            {
            
                SendSmsOS(dt, msg);
            }
        }


        #endregion
    }
}
