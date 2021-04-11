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
    public class CheckOutRefahBusiness
    {
        CheckOutDebitDAO DebitDAO = new CheckOutDebitDAO();
        CommonBusiness CommonBusiness = new CommonBusiness();
       
        public DataTable GetAllDebitTypes()
        {
            return DebitDAO.GetAllDebitTypes();
        }

        public void InsertUpdateDebit(string userID, string stcode, string loanNumber, string loanAmount, string strTasvieAmount, string description, int debitType, string fishNumber, string fishDate)
        {
            try
            {
                DebitDAO.InsertUpdateDebit(stcode, loanNumber, loanAmount, strTasvieAmount, description, debitType, fishNumber, fishDate);
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 74, "ثبت بدهی " + loanNumber + "دانشجو" + stcode + "شماره فیش" + fishNumber, Convert.ToInt32(loanNumber));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetAllDebitByStcode(string stcode)
        {
            return DebitDAO.GetAllDebitByStcode(stcode);
        }

        public void AddFishInfo(string userID, int DebitID, string fishNumber, string fishDate, string Note)
        {
            try
            {
                DebitDAO.AddFishInfo(DebitID, fishNumber, fishDate, Note);
                CommonBusiness.InsertIntoStudentLog(userID, DateTime.Now.ToString("HH:mm"), 12, 75, "ثبت فیش شماره" + fishNumber + "بدهی شماره" + DebitID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetLastUniInfo(string stcode)
        {
            try
            {
                return DebitDAO.GetLastUniInfo(stcode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddStudentLastUniInfo(DTO.University.Request.CheckOutStLastUniInfo lastInfo)
        {
            DebitDAO.InsertStudentLastUniInfo(lastInfo);
        }

        public void AddStudentAddressInfo(DTO.University.Request.CheckOutStLastUniInfo AddressInfo)
        {
            DebitDAO.InsertStudentAddress(AddressInfo);
        }

        public int GetStudentPastMaghta(string stCode)
        {
            return DebitDAO.GetStudentPastMaghta(stCode);
        }

        public bool HasAddress(string stCode)
        {
            DataTable dt = DebitDAO.GetStudentAddress(stCode);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasThisMaghta(string stcode, int maghta)
        {
            DataTable dt = DebitDAO.GetMaghtaInfo(stcode, maghta);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetStudentAddress(string stcode)
        {
            return DebitDAO.GetStudentAddress(stcode);
        }

        public int UpdateDebit(StudentCheckOutDebit debit, int userId)
        {
            CommonBusiness.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), 12, 75, "ویرایش فیش شماره" + debit.DebitNumber, Convert.ToInt32(debit.DebitNumber));
            return DebitDAO.UpdateDebit(debit);
        }

        public bool DeleteDebit(long RefID, int userId, string stcode, int debitTypeID, string amount)
        {           
            DataTable dt = new DataTable();
            dt= GetAllDebitByStcode(stcode);
            CommonBusiness.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), 12, 113, "بدهی " + getDebitType(debitTypeID) + " برای دانشجو با شماره دانشجویی " + stcode + " به مبلغ " + amount + " حذف گردید");
            return DebitDAO.DeleteDebit(RefID);
        }

        private string getDebitType(int id)
        {
            switch (id)
            {
                case 1:
                    return "وام وزارت علوم";
                    
                case 2:
                    return "چک برگشتی";
                case 3:
                    return "وام ازدواج";
                case 4:
                    return "وام تامین شهریه";
                case 5:
                    return "وام کمک هزینه تحصیلی";
                case 6:
                    return "وام مسکن" ;
                case 7:
                    return "وام ضروری";
                case 8:
                    return "بستانکاری";
                case 9:
                    return "شهریه";
                 case 10:
                    return "وام بلند مدت";
                default:
                    return string.Empty;
            }
        }
    }
}
