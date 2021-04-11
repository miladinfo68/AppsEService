using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.University.Request;
using IAUEC_Apps.DAO.University.Request;
using System.Data;

namespace IAUEC_Apps.Business.university.Request
{
    public class FeraghatTahsilBusiness
    {
        FeraghatTahsilDAO daoFeraghat = null;

        public int UpdateMadarekStatus(FeraghatTahsilDTO oFeraghat, int userID, bool Loan, bool sendSms = true)
        {
            daoFeraghat = new FeraghatTahsilDAO();
            int counter = daoFeraghat.UpdateMadarekStatus(oFeraghat);
            if (counter > 0)
            {
                oFeraghat.Id = counter;
                //if(oFeraghat.GovahiMovaghat==1)
                //setMadrakArchiveCode(oFeraghat.StudentRequestId, 1);
                //if (oFeraghat.DaneshNameh == 1)
                //    setMadrakArchiveCode(oFeraghat.StudentRequestId, 2);
                //if (oFeraghat.RizNomarat == 1)
                //    setMadrakArchiveCode(oFeraghat.StudentRequestId,3);


                try
                {
                    insertVoroodDate(oFeraghat);
                    if (sendSms)
                        SendSms(oFeraghat, userID, Loan);

                }
                catch (Exception e)
                {
                    throw;
                }
                return counter;
            }
            else
            {
                return 0;
            }
        }

        public void setMadrakArchiveCode(int studentRequestID, int archiveTypeID)
        {
            daoFeraghat = new FeraghatTahsilDAO();
            daoFeraghat.setMadrakArchiveCode(studentRequestID, archiveTypeID);
        }

        private static void SendSms(FeraghatTahsilDTO oFeraghat, int userID, bool Loan)
        {
            Common.CommonBusiness oCommon = new Common.CommonBusiness();
            DataTable smsStatus = new DataTable();
            FeraghatTahsilDAO daoFeraghat = new FeraghatTahsilDAO();
            string pasdaranAddress = "";
            var address = oCommon.getBasicInformation((int)DTO.basicType.آدرس, 2);
            if (address.Rows.Count > 0 && address.Rows[0]["value"] != DBNull.Value)
                pasdaranAddress = address.Rows[0]["value"].ToString();

            smsStatus = daoFeraghat.getSmsStatus(oFeraghat.Stcode);
            bool condition;
            string smsBody = "";
            string result = "";
            bool sentSMS;
            if (oFeraghat.RizNomarat > 0)
            {
                if (smsStatus.Rows.Count <= 0)
                    condition = false;
                else
                    condition = smsStatus.Rows[smsStatus.Rows.Count - 1]["SendRizNomarat"] == DBNull.Value ? false : (bool)smsStatus.Rows[smsStatus.Rows.Count - 1]["SendRizNomarat"];
                if (!condition)
                {

                    //smsBody = "دانشجوی گرامی ریز نمرات شما درمحل دانشگاه واقع در  "+pasdaranAddress+" دانشگاه آزاد اسلامی آماده تحویل می باشد. تحویل ریز نمرات فقط به خود فارغ التحصیل یا وکیل قانونی وی امکان پذیر است، لطفاً قبل از مراجعه به تقويم كاري دانشگاه (سايت واحد) توجه نماييد.";
                    smsBody = @"دانش آموخته گرامی، باتوجه به شیوع بیماری اپیدمی کرونا و هشدارهای وزارت بهداشت مبنی بر قرنطینه در منزل، لطفا در صورتیکه به مدرک خود نیاز ضروری ندارید از مراجعه حضوری خودداری نموده و دریافت مدرک خود را به زمان مناسب دیگری موکول نمایید.

ضمناً ریزنمره شمادر محل دانشگاه واقع در " + pasdaranAddress + " دانشگاه آزاد اسلامی آماده تحویل می باشد. تحویل ریزنمره فقط به خود فارغ التحصیل یا وکیل قانونی وی امکان پذیر است.";

                    //result = oCommon.SendSMSByUserIdAndType(smsBody, oFeraghat.Stcode, 1);

                    string smsStatusText;
                    oCommon.sendSMS(1, oFeraghat.Stcode, smsBody, out sentSMS, out smsStatusText);

                    //result = oCommon.sendSMS(1, oFeraghat.Stcode,smsBody, out sentSMS);
                    daoFeraghat.UpdateSmsStatus(oFeraghat.Stcode, 1, true);

                    oCommon.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 110, "تایید آماده بودن ریز نمرات", oFeraghat.StudentRequestId);
                }
            }
            if (oFeraghat.GovahiMovaghat > 0)
            {
                if (smsStatus.Rows.Count <= 0)
                    condition = false;
                else condition = smsStatus.Rows[smsStatus.Rows.Count - 1]["SendGovahiMovaghat"] == DBNull.Value ? false : (bool)smsStatus.Rows[smsStatus.Rows.Count - 1]["SendGovahiMovaghat"];

                if (!condition)
                {
                    DataTable Mashmool = new DataTable();
                    Mashmool = daoFeraghat.getMashmoolInfo(oFeraghat.Stcode);
                    int Male;
                    Male = daoFeraghat.getMaleInfo(oFeraghat.Stcode);

                    if (Male == 1)//Male
                    {
                        //smsBody = "دانشجوی گرامی گواهینامه موقت شما درمحل دانشگاه واقع در  " + pasdaranAddress + " دانشگاه آزاد اسلامی آماده تحویل می باشد. تحویل گواهینامه موقت فقط به خود فارغ التحصیل یا وکیل قانونی وی و در صورت ارائه ی برگه اعزام به خدمت بدون غیبت، گواهی اشتغال به تحصیل در مقاطع بالاتر و یا همراه داشتن کارت پایان خدمت، امکان پذیر است،  لطفاً قبل از مراجعه به تقويم كاري دانشگاه (سايت واحد) توجه نماييد";//mard
                        smsBody = @"دانش آموخته گرامی، باتوجه به شیوع بیماری اپیدمی کرونا و هشدارهای وزارت بهداشت مبنی بر قرنطینه در منزل، لطفا در صورتیکه به مدرک خود نیاز ضروری ندارید از مراجعه حضوری خودداری نموده و دریافت مدرک خود را به زمان مناسب دیگری موکول نمایید.
ضمناً گواهینامه موقت شما در محل دانشگاه واقع در آدرس " + pasdaranAddress + " دانشگاه آزاد اسلامی آماده تحویل می باشد. تحویل گواهینامه موقت فقط به خود فارغ التحصیل یا وکیل قانونی وی و در صورت ارائه برگه اعزام به خدمت بدون غیبت، گواهی اشتغال به تحصیل در مقاطع بالاتر و با همراه داشتن کارت پایان خدمت، امکان پذیراست.";


                    }
                    else if (Male == 0)//Female
                    {
                        //smsBody = "دانشجوی گرامی گواهینامه موقت شمادرمحل دانشگاه واقع در  "+pasdaranAddress+" دانشگاه آزاد اسلامی آماده تحویل می باشد. تحویل گواهینامه موقت فقط به خود فارغ التحصیل یا وکیل قانونی وی امکان پذیر است، لطفاً قبل از مراجعه به تقويم كاري دانشگاه (سايت واحد) توجه نماييد";
                        smsBody = @"دانش آموخته گرامی، باتوجه به شیوع بیماری اپیدمی کرونا و هشدارهای وزارت بهداشت مبنی بر قرنطینه در منزل، لطفا در صورتیکه به مدرک خود نیاز ضروری ندارید از مراجعه حضوری خودداری نموده و دریافت مدرک خود را به زمان مناسب دیگری موکول نمایید.

ضمناً گواهینامه موقت شمادر محل دانشگاه واقع در " + pasdaranAddress + " دانشگاه آزاد اسلامی آماده تحویل می باشد. تحویل گواهینامه موقت فقط به خود فارغ التحصیل یا وکیل قانونی وی امکان پذیر است.";
                    }

                    string smsStatusText;
                    oCommon.sendSMS(1, oFeraghat.Stcode, smsBody, out sentSMS, out smsStatusText);

                    daoFeraghat.UpdateSmsStatus(oFeraghat.Stcode, 2, true);

                    oCommon.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 111, "تایید آماده بودن گواهی موقت", oFeraghat.StudentRequestId);
                }
            }
            if (oFeraghat.DaneshNameh > 0)
            {
                if (smsStatus.Rows.Count <= 0)
                    condition = false;
                else
                    condition = smsStatus.Rows[smsStatus.Rows.Count - 1]["SendDaneshnameh"] == DBNull.Value ? false : (bool)smsStatus.Rows[smsStatus.Rows.Count - 1]["SendDaneshnameh"];
                if (!condition)
                {
                    DataTable loan = new DataTable();
                    loan = daoFeraghat.getLoanInfo(oFeraghat.Stcode);

                    if (loan.Rows.Count > 0 || Loan)//vam dar
                    {
                        //smsBody = "دانشجوی گرامی دانشنامه شمادرمحل دانشگاه واقع در  " + pasdaranAddress + " دانشگاه آزاد اسلامی آماده تحویل می باشد. تحویل دانشنامه فقط به خود فارغ التحصیل یا وکیل قانونی وی در صورت تسویه وام و بعد از تحویل گواهینامه موقت امکان پذیر خواهد بود، لطفاً قبل از مراجعه به تقويم كاري دانشگاه (سايت واحد) توجه نماييد";//vamdar  
                        smsBody = @"دانش آموخته گرامی، باتوجه به شیوع بیماری اپیدمی کرونا و هشدارهای وزارت بهداشت مبنی بر قرنطینه در منزل، لطفا در صورتیکه به مدرک خود نیاز ضروری ندارید از مراجعه حضوری خودداری نموده و دریافت مدرک خود را به زمان مناسب دیگری موکول نمایید.

ضمناً دانشنامه شما در محل دانشگاه واقع در آدرس "+pasdaranAddress+ " دانشگاه آزاد اسلامی آماده تحویل می باشد. تحویل دانشنامه فقط به خود فارغ التحصیل یا وکیل قانونی وی در صورت تسویه وام و بعد از تحویل گواهینامه موقت، امکان پذیراست.";
                    }
                    else//bedoone vam
                    {
                        //smsBody = "دانشجوی گرامی دانشنامه شمادرمحل دانشگاه واقع در  " + pasdaranAddress + " دانشگاه آزاد اسلامی آماده تحویل می باشد. تحویل دانشنامه فقط به خود فارغ التحصیل یا وکیل قانونی وی در صورت تسویه وام و بعد از تحویل گواهینامه موقت امکان پذیر خواهد بود، لطفاً قبل از مراجعه به تقويم كاري دانشگاه (سايت واحد) توجه نماييد";
                        smsBody = @"دانش آموخته گرامی، باتوجه به شیوع بیماری اپیدمی کرونا و هشدارهای وزارت بهداشت مبنی بر قرنطینه در منزل، لطفا در صورتیکه به مدرک خود نیاز ضروری ندارید از مراجعه حضوری خودداری نموده و دریافت مدرک خود را به زمان مناسب دیگری موکول نمایید.

ضمناً دانشنامه شمادر محل دانشگاه واقع در " + pasdaranAddress + " دانشگاه آزاد اسلامی آماده تحویل می باشد. تحویل دانشنامه فقط به خود فارغ التحصیل یا وکیل قانونی وی در صورت تسویه وام و بعد از تحویل گواهینامه موقت امکان پذیر است.";
                    }
                    //result = oCommon.SendSMSByUserIdAndType(smsBody, oFeraghat.Stcode, 1);
                    //result = oCommon.sendSMS(1, oFeraghat.Stcode,smsBody, out sentSMS);
                    string smsStatusText;
                    result = oCommon.sendSMS(1, oFeraghat.Stcode, smsBody, out sentSMS, out smsStatusText);
                    daoFeraghat.UpdateSmsStatus(oFeraghat.Stcode, 3, true);

                    oCommon.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 112, "تایید آماده بودن دانشنامه", oFeraghat.StudentRequestId);
                }
            }
        }
        private  void insertVoroodDate(FeraghatTahsilDTO oFeraghat)
        {
            FeraghatTahsilDAO daoFeraghat = new FeraghatTahsilDAO();

            bool condition;
            string _dateVoroodDaneshname = oFeraghat.dateVoroodDaneshname;
            string _dateVoroodRizNomre = oFeraghat.dateVoroodRizNomre;
            string _dateVoroodGovahiMovaghat = oFeraghat.dateVoroodGovahi;

            condition = oFeraghat.RizNomarat == 1 && (oFeraghat.dateVoroodRizNomre == "" || oFeraghat.dateVoroodRizNomre == "null");

            if (condition)
            {
                _dateVoroodRizNomre = DateTime.Now.ToPeString("yyyy/MM/dd");
            }
            else if (oFeraghat.RizNomarat == 0)
            {
                _dateVoroodRizNomre = "";
            }
            oFeraghat.dateVoroodRizNomre = _dateVoroodRizNomre;


            condition = oFeraghat.DaneshNameh == 1 && (oFeraghat.dateVoroodDaneshname == "" || oFeraghat.dateVoroodDaneshname == "null");

            if (condition)
            {
                _dateVoroodDaneshname = DateTime.Now.ToPeString("yyyy/MM/dd");
            }
            else if (oFeraghat.DaneshNameh == 0)
            {
                _dateVoroodDaneshname = "";
            }
            oFeraghat.dateVoroodDaneshname = _dateVoroodDaneshname;
            condition = oFeraghat.GovahiMovaghat == 1 && (oFeraghat.dateVoroodGovahi == "" || oFeraghat.dateVoroodGovahi == "null");

            if (condition)
            {
                _dateVoroodGovahiMovaghat = DateTime.Now.ToPeString("yyyy/MM/dd");
            }
            else if (oFeraghat.GovahiMovaghat == 0)
            {
                _dateVoroodGovahiMovaghat = "";
            }
            oFeraghat.dateVoroodGovahi = _dateVoroodGovahiMovaghat;


            daoFeraghat.UpdateMadarekStatus(oFeraghat);
        }
        public FeraghatTahsilDTO GetFeraghatMadrekStatus(int reqId)
        {
            daoFeraghat = new FeraghatTahsilDAO();

            return daoFeraghat.GetMadarekStatus(reqId);
        }

        public DataTable getStudentInfo(FeraghatTahsilDTO oFeraghat)
        {
            daoFeraghat = new FeraghatTahsilDAO();
            return daoFeraghat.getStudentInfo(oFeraghat);
        }

        public DataTable GetDateOfDef(string stCode)
        {
            daoFeraghat = new FeraghatTahsilDAO();
            return daoFeraghat.GetDateOfDef(stCode);
        }
        public int ExistRiz(int ReqId)
        {
            daoFeraghat = new FeraghatTahsilDAO();
            return daoFeraghat.ExistSignatureRiz(ReqId);
        }
        public int ExistGovahi(int ReqId)
        {
            daoFeraghat = new FeraghatTahsilDAO();
            return daoFeraghat.ExistSignatureGovahi(ReqId);
        }
        public int ExistDanesh(int ReqId)
        {
            daoFeraghat = new FeraghatTahsilDAO();
            return daoFeraghat.ExistSignatureDanesh(ReqId);
        }
        public void sendSmsForMsg(int ReqId, string Stcode)
        {
            Common.CommonBusiness oCommon = new Common.CommonBusiness();
            string smsBody;
            string result = "";
            bool sentSMS;

            smsBody = "دانشجوی محترم واحد الکترونیکی، با توجه به درج پیام توسط کارشناس واحد، خواهشمند است در اسرع وقت با مراجعه به سامانه تسویه حساب نسبت به اطلاع از پیام درج شده اقدام لازم را مبذول فرمایید. دانشگاه آزاد اسلامی واحد الکترونیکی";

            string smsStatusText;
            result = oCommon.sendSMS(1, Stcode, smsBody, out sentSMS, out smsStatusText);
        }
        public void sendSmsForUpload(int ReqId, string Stcode)
        {
            Common.CommonBusiness oCommon = new Common.CommonBusiness();
            string smsBody;
            string result = "";
            bool sentSMS;

            smsBody = "دانشجوی گرامی، جهت ادامه فرآیند فراغت از تحصيل، با مراجعه به سامانه تسويه حساب نسبت به بارگذاري فايل پايان­نامه خود مطابق آیین­نامه نگارش پایان­نامه در اسرع وقت اقدام نماييد.معاونت پژوهشی";

            string smsStatusText;
            result = oCommon.sendSMS(1, Stcode, smsBody, out sentSMS, out smsStatusText);
        }
        public void sendSmsForDenythes(int ReqId, string Stcode)
        {
            Common.CommonBusiness oCommon = new Common.CommonBusiness();
            string smsBody;
            string result = "";
            bool sentSMS;

            smsBody = "دانشجوی گرامی، فايل پايان نامه بارگذاري شده جنابعالي/ سركارعالي در سامانه تسويه حساب مورد تاييد قرار نگرفته و موارد لازم جهت انجام اصلاحات در پيام درج شده است.در اسرع وقت با مراجعه به سامانه و انجام اصلاحات مورد نياز نسبت به بارگذاري مجدد فايل پايان نامه اقدام نماييد.معاونت پژوهشی";

            string smsStatusText;
            result = oCommon.sendSMS(1, Stcode, smsBody, out sentSMS, out smsStatusText);
        }

    }
}
