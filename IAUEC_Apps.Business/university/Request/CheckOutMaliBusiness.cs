using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Request;
using System.Data;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.Business.university.Request
{
    public class CheckOutMaliBusiness
    {
        CheckOutMaliDAO MaliDAO = new CheckOutMaliDAO();
        CommonBusiness CommonBusiness = new CommonBusiness();

        public string AddBankMeliAcountInfo(string stCode, string BankMeliID, string AcountOwner, string phoneNumber)
        {
            try
            {
                if (MaliDAO.InsertBankAcount(stCode, BankMeliID, "", AcountOwner, phoneNumber, "بانک ملی"))
                {
                    CommonBusiness.InsertIntoStudentLog(stCode, DateTime.Now.ToString("HH:mm"), 12, 76, "درج مشخصات حساب بانکی دانشجوی بستانکار");
                    return "اطلاعات شما با موفقیت ثبت شد.";
                }
                else
                {
                    return "مشکلی پیش آمده ، لطفا دوباره سعی کنید.";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string AddBankOtherAcountInfo(string stCode, string Sheba, string BankName, string Owner, string Phone)
        {
            try
            {
                if (MaliDAO.InsertBankAcount(stCode, "", Sheba, Owner, Phone, BankName))
                {
                    CommonBusiness.InsertIntoStudentLog(stCode, DateTime.Now.ToString("HH:mm"), 12, 76, "درج مشخصات حساب بانکی دانشجوی بستانکار");
                    return "اطلاعات شما با موفقیت ثبت شد.";
                }
                else
                {
                    return "مشکلی پیش آمده ، لطفا دوباره سعی کنید.";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool HasAcountNo (string stCode)
        {
            DataTable dt = MaliDAO.HasAcountNo(stCode);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasDebitRefah(string stCode)
        {
            DataTable dt = MaliDAO.HasDebitRefah(stCode);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasDebitRefahVezarat(string stCode)
        {
            DataTable dt = MaliDAO.HasDebitRefahVezarat(stCode);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasAnyRefahDebit(string stCode)
        {
            DataTable dt = MaliDAO.getAllDebit(stCode);
            return dt.Rows.Count > 0;
        }

        public bool HasDebitAcountNo(string stCode)
        {
            DataTable dt = MaliDAO.HasDebitAcountNo(stCode);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasAcountInfo(string stCode)
        {
            DataTable dt = MaliDAO.HasAcountInfo(stCode);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetAllMaliDebitByStcode(string stcode)
        {
            return MaliDAO.GetAllMaliDebitByStcode(stcode);
        }

        public void InsertUpdateDebit(string userID, string stcode, string strNumber, string strAmount, string strNote, int debitTypeId, string strFishNumber, string strFishDate)
        {
            try
            {
                MaliDAO.InsertUpdateDebit(stcode, strNumber, strAmount, strNote, debitTypeId, strFishNumber, strFishDate);
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 77, "درج بدهی مالی " + strNumber + " برای دانشجو" + stcode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateDebit(string stcode, int debitID, string fishNumber, string fishDate, string Note)
        {
            try
            {
                MaliDAO.AddFishInfo(debitID, fishNumber, fishDate, Note);
                CommonBusiness.InsertIntoStudentLog(stcode, DateTime.Now.ToString("HH:mm"), 12, 78, "درج فیش شماره " + fishNumber + "برای بدهی شماره" + debitID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CheckMaliCheckOut(string stcode)
        {
            return MaliDAO.CheckMaliCheckOut(stcode);
        }

        public DataTable CheckMaliCheckOutVezarat(string stcode)
        {
            return MaliDAO.CheckMaliCheckOutVezarat(stcode);
        }

        public int RemoveVahedInTerm(string userID, string stCode)
        {
            int count = 0;
            try
            {
                count = MaliDAO.RemoveVahedInTerm(stCode);
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 86, "حذف انتخاب واحد دانشجو" + stCode);
            }
            catch (Exception)
            {

                throw;
            }
            return count;
        }

        public DataTable GetNewStCode(string stcode)
        {
            return MaliDAO.GetStudentNewCode(stcode);
        }

        public int UpdateMaliDebit(DTO.University.Request.StudentCheckOutDebit debit , int userId)
        {
            CommonBusiness.InsertIntoUserLog(userId, DateTime.Now.ToPeString(), 12, 78, "ویرایش فیش شماره" + debit.DebitNumber, Convert.ToInt32(debit.DebitNumber));
            return MaliDAO.UpdateMaliDebit(debit);
        }

        public bool DeleteMaliDebit(long RefID)
        {
            return MaliDAO.DeleteDebit(RefID);
        }
    }
}
