using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DAO.University.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.university.Request
{
    public class CheckOutPajooheshBusiness
    {
        CheckOutPajooheshDAO PajoheshDAO = new CheckOutPajooheshDAO();
        CommonBusiness CommonBusiness = new CommonBusiness();

        public DataTable GetStudentInfoForPajohesh(string stcode)
        {
            return PajoheshDAO.GetCheckOutInfoByStCodeForPajohesh(stcode);
        }


        public DataTable GetStudentsNaghs(string stcode)
        {
            return PajoheshDAO.GetCheckOutInfoByStCodeNaghs(stcode);
        }
        public void UpdateStudentsReqIDinNaghs(string stcode, int reqID)
        {
             PajoheshDAO.UpdateStudentsReqIDinNaghs(stcode,reqID);
        }

        public string Add_Def_Date(string userID, string defdate, string stcode)
        {
            if (PajoheshDAO.Add_Def_Date(defdate, stcode))
            {
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 79, "ثبت تاریخ دفاع برای دانشجو " + stcode);
                return "تاریخ دفاع با موفقیت ثبت شد.";
            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }

        public string Add_Cancel_Date(string userID, string stcode, string canceldate)
        {
            if (PajoheshDAO.Add_Cancel_Date(stcode, canceldate))
            {
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 80, "ثبت تاریخ انصراف از مقاله " + stcode);
                return "تاریخ انصراف از مقاله با موفقیت ثبت شد.";
            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }

        public string Add_Receive_Date(string userID, string stcode, string receivedate)
        {
            if (PajoheshDAO.Add_Receive_Date(stcode, receivedate))
            {
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, (int)(DTO.eventEnum.ثبت_تاریخ_دریافت_مدارک_دانشجو), "ثبت تاریخ دریافت مدارک دانشجو " + stcode);
                return "تاریخ دریافت مدارک با موفقیت ثبت شد.";
            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }

        public string Add_Send_Date(string userID, string stcode, string senddate)
        {
            if (PajoheshDAO.Add_Send_Date(stcode, senddate))
            {
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, (int)(DTO.eventEnum.درج_تاریخ_ارسال_مدارک), "درج تاریخ ارسال مدارک " + stcode);
                return "تاریخ ارسال مدارک با موفقیت ثبت شد.";
            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }
        public string Add_Send_Date_for_pajoohesh(string userID, string stcode, string senddate)
        {
            if (PajoheshDAO.Add_Send_Date_paj(stcode, senddate))
            {
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, (int)(DTO.eventEnum.درج_تاریخ_ارسال_مدارک), "درج تاریخ ارسال مدارک " + stcode);
                return "تاریخ ارسال مدارک با موفقیت ثبت شد.";
            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }

        public string Add_DefPoint(string userID, string stcode, string defpoint)
        {
            if (PajoheshDAO.Add_DefPoint(stcode, defpoint))
            {
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, (int)(DTO.eventEnum.درج_نمره_دفاع), "درج نمره دفاع " + stcode);
                return "نمره دفاع با موفقیت ثبت شد.";
            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }
        public string Add_HasCancelForm(string userID, string stcode, bool HasCancelForm)
        {
            if (PajoheshDAO.Add_HasCancelForm(stcode, HasCancelForm))
            {
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 179, "درج وضعیت فرم انصراف از مقاله" + stcode);
                return "وضعیت فرم انصراف از مقاله ثبت شد.";
            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }
        public string Add_DeadLineDateOrCancelDate(string userID, string stcode, string DeadLineDate, string cancelDate)
        {
            if (PajoheshDAO.Add_DeadLineDateOrCancelDate(stcode, DeadLineDate, cancelDate))
            {
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 180, "درج مهلت ارسال مقاله و یا تاریخ انصراف از مقاله" + stcode);
                return "مهلت ارسال مقاله و یا تاریخ انصراف ثبت شد.";
            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }
        public string Add_EditThesDetail(string userID, string stcode, string editThes,int ReqLogId,int isDeleted)
        {
            
            if (PajoheshDAO.Add_EditThesDetail(stcode, editThes , ReqLogId , isDeleted))
            {
                if (editThes != null)
                {
                    CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 181, "درج توضیحات اصلاحات پایان نامه " + stcode);
                    return "توضیحات اصلاحات پایان نامه ثبت شد.";
                }
                else
                {
                    CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 182, "حذف توضیحات اصلاحات پایان نامه " + stcode);
                    return "توضیحات اصلاحات پایان نامه حذف شد.";
                }

            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }
        public string Add_EditThesDetailWithEditForm(string userID, string stcode, string editThes, int ReqLogId)
        {
            if (PajoheshDAO.Add_EditThesDetailWithEditForm(stcode, editThes, ReqLogId))
            {
                if (editThes != null)
                {
                    CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 183, "درج توضیحات اصلاحات پایان نامه و دریافت فرم اصلاح " + stcode);
                    return "توضیحات اصلاحات پایان نامه ثبت شد.";
                }
                else
                {
                    CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 180, "حذف توضیحات اصلاحات پایان نامه  " + stcode);
                    return "توضیحات اصلاحات پایان نامه حذف شد.";
                }

            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }
        public string FinizlizePajoohesh(string userID, CheckOutPajoheshDTO oPajoohesh)
        {
            bool flag = PajoheshDAO.FinizlizePajoohesh(oPajoohesh);

            if (flag)
            {
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 84, "تایید نهایی دانشجو در بخش پژوهش " + oPajoohesh.StCode);
                return "این دانشجو با موفقیت تایید نهایی شد.";
            }
            else
            {
                return "خطایی در سیستم اتقاق افتاده لطفا مجددا سعی کنید.";
            }
        }

        public bool IsFinilized(string stCode)
        {
            if (PajoheshDAO.IsFinilized(stCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetListOfFinalizedStudent(string startDate, string endDate)
        {
            return PajoheshDAO.GetListOfFinalizedStudentByDate(startDate, endDate);
        }

        public DataTable GetConfirmStudentAfter48H(string stcode)
        {
            return PajoheshDAO.GetConfirmStudentAfter48H(stcode);
        }
    }
}
