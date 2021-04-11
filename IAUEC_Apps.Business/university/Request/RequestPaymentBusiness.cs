using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Request;
using IAUEC_Apps.DTO.CommonClasses;
using System.Data;
using IAUEC_Apps.Business.ir.shaparak.bpm1;

namespace IAUEC_Apps.Business.university.Request
{
    public class RequestPaymentBusiness
    {
        PaymentRequestDAO pDAO = new PaymentRequestDAO();
        public List<PaymentDTO> Get_Student_Payment(string stcode)
        {
            List<PaymentDTO> lstPayment = new List<PaymentDTO>();

            DataTable dt = new DataTable();
            dt = pDAO.Get_Student_Payment(stcode);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PaymentDTO payDTO = new PaymentDTO();
                payDTO.OrderId = int.Parse(dt.Rows[i]["OrderID"].ToString());
                payDTO.Amount = long.Parse(dt.Rows[i]["AmountTrans"].ToString());
                payDTO.stcode = dt.Rows[i]["StudentCode"].ToString();
                payDTO.tterm = dt.Rows[i]["tterm"].ToString();
                payDTO.PersianDate = dt.Rows[i]["PersianDate"].ToString();
                if (dt.Rows[i]["TraceNumber"].ToString() != "")
                    payDTO.TraceNumber = long.Parse(dt.Rows[i]["TraceNumber"].ToString());
                else
                    payDTO.TraceNumber = 0;
                payDTO.AppStatus = dt.Rows[i]["paystatus"].ToString();
                payDTO.PayType = int.Parse(dt.Rows[i]["paystatus"].ToString());
                payDTO.Description = dt.Rows[i]["Description"].ToString();
                lstPayment.Add(payDTO);
            }
            return lstPayment;
        }
        public List<PaymentDTO> GetAllPayment()
        {
            List<PaymentDTO> lstPayment = new List<PaymentDTO>();

            DataTable dt = new DataTable();
            dt = pDAO.GetAllPayment();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PaymentDTO payDTO = new PaymentDTO();
                payDTO.OrderId = int.Parse(dt.Rows[i]["OrderID"].ToString());
                payDTO.Amount = long.Parse(dt.Rows[i]["AmountTrans"].ToString());
                payDTO.stcode = dt.Rows[i]["StudentCode"].ToString();
                payDTO.tterm = dt.Rows[i]["tterm"].ToString();
                payDTO.PersianDate = dt.Rows[i]["PersianDate"].ToString();
                if (dt.Rows[i]["TraceNumber"].ToString() != "")
                    payDTO.TraceNumber = long.Parse(dt.Rows[i]["TraceNumber"].ToString());
                else
                    payDTO.TraceNumber = 0;
                payDTO.AppStatus = dt.Rows[i]["paystatus"].ToString();
                payDTO.PayType = int.Parse(dt.Rows[i]["paystatus"].ToString());
                payDTO.Description = dt.Rows[i]["Description"].ToString();
                lstPayment.Add(payDTO);
            }
            return lstPayment;
        }

        public void UpdatePaymentStatus(PaymentDTO PaymentDTO)
        {
            PaymentRequestDAO payDAO = new PaymentRequestDAO();
            payDAO.UpdateMellatPaymentStatus(PaymentDTO);

        }
        public DataTable GetStudentPaymentInquiry(DTO.University.Request.PayType payType ,string stcode)
        {
            return pDAO.GetStudentPaymentInquiry(stcode,(int)payType);
        }
        public DataTable GetStudentPaymentInquiry(DTO.University.Request.PayType payType)
        {
            return pDAO.GetStudentPaymentInquiry("all",(int)payType);
        }
        public void checkRollbackPayments(DTO.University.Request.PayType payType,string stcode="all")
        {
            var allRollbackPayments = GetStudentPaymentInquiry(payType,stcode);
            DTO.CommonClasses.PaymentDTO pay;
            Business.Payment.bmp_PaymentBusiness bmp = new Business.Payment.bmp_PaymentBusiness();
            int result;
            foreach (DataRow dr in allRollbackPayments.Select("AppStatus='rollback' and persianDate>='" + DateTime.Now.AddDays(-30).ToPeString() + "'"))
            {
                string resultText = PaymentStatusInquery(Convert.ToInt64(dr["orderID"]), out result);
                if (result == 0)
                {
                    pay = new DTO.CommonClasses.PaymentDTO();
                    pay.OrderId = Convert.ToInt64(dr["orderID"]);
                    pay.ReqKey = dr["RetrivalRefNo"].ToString();
                    pay.AppStatus = "commit";
                    pay.TraceNumber = Convert.ToInt64(dr["TraceNumbers"]);
                    pay.Result = result;
                    bmp.UpdateMellatRquestPayment(pay);
                }
            }
        }



        public string PaymentStatusInquery(Int64 orderID, out int result)
        {
            result = -1;
            var pay = GetPaymentInfoByOrderID(orderID);
            PaymentGatewayImplService bpService = new PaymentGatewayImplService();
            string resultInquery = "";


            resultInquery = bpService.bpInquiryRequest(Int64.Parse(System.Configuration.ConfigurationManager.AppSettings["Mellat_TerminalId"]),
                System.Configuration.ConfigurationManager.AppSettings["UserName"],
                System.Configuration.ConfigurationManager.AppSettings["UserPassword"], 
                pay.OrderId, 
                pay.OrderId, 
                pay.TraceNumber);

            string resultText = "";
            
            if (Business.Common.CommonBusiness.IsNumeric(resultInquery))
            {
                result = Convert.ToInt32(resultInquery);
                switch (result)
                {
                    case 0:
                        resultText = "تراكنش با موفقيت انجام شد";
                        break;
                    case 11:
                        resultText = "ﺷﻤﺎﺭﻩ ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ ";
                        break;
                    case 12:
                        resultText = "ﻣﻮﺟﻮﺩﻱ ﻛﺎﻓﻲ ﻧﻴﺴﺖ ";
                        break;
                    case 13:
                        resultText = "ﺭﻣﺰ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ ";
                        break;
                    case 14:
                        resultText = "ﺗﻌﺪﺍﺩ ﺩﻓﻌﺎﺕ ﻭﺍﺭﺩ ﻛﺮﺩﻥ ﺭﻣﺰ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ ";
                        break;
                    case 15:
                        resultText = "ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case 16:
                        resultText = "ﺩﻓﻌﺎﺕ ﺑﺮﺩﺍﺷﺖ ﻭﺟﻪ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ";
                        break;
                    case 17:
                        resultText = "ﻛﺎﺭﺑﺮ ﺍﺯ ﺍﻧﺠﺎﻡ ﺗﺮﺍﻛﻨﺶ ﻣﻨﺼﺮﻑ ﺷﺪﻩ ﺍﺳﺖ ";
                        break;
                    case 18:
                        resultText = "ﺗﺎﺭﻳﺦ ﺍﻧﻘﻀﺎﻱ ﻛﺎﺭﺕ ﮔﺬﺷﺘﻪ ﺍﺳﺖ";
                        break;
                    case 19:
                        resultText = "ﻣﺒﻠﻎ ﺑﺮﺩﺍﺷﺖ ﻭﺟﻪ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ";
                        break;
                    case 111:
                        resultText = "ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case 112:
                        resultText = "ﺧﻄﺎﻱ ﺳﻮﻳﻴﭻ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ";
                        break;
                    case 113:
                        resultText = "ﭘﺎﺳﺨﻲ ﺍﺯ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ﺩﺭﻳﺎﻓﺖ ﻧﺸﺪ";
                        break;
                    case 114:
                        resultText = "ﺩﺍﺭﻧﺪﻩ ﻛﺎﺭﺕ ﻣﺠﺎﺯ ﺑﻪ ﺍﻧﺠﺎﻡ ﺍﻳﻦ ﺗﺮﺍﻛﻨﺶ ﻧﻴﺴﺖ";
                        break;
                    case 21:
                        resultText = "ﭘﺬﻳﺮﻧﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case 23:
                        resultText = "ﺧﻄﺎﻱ ﺍﻣﻨﻴﺘﻲ ﺭﺥ ﺩﺍﺩﻩ ﺍﺳﺖ";
                        break;
                    case 24:
                        resultText = "ﺍﻃﻼﻋﺎﺕ ﻛﺎﺭﺑﺮﻱ ﭘﺬﻳﺮﻧﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case 25:
                        resultText = "ﻣﺒﻠﻎ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case 31:
                        resultText = "ﭘﺎﺳﺦ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ ";
                        break;
                    case 32:
                        resultText = "ﻓﺮﻣﺖ ﺍﻃﻼﻋﺎﺕ ﻭﺍﺭﺩ ﺷﺪﻩ ﺻﺤﻴﺢ ﻧﻤﻲ ﺑﺎﺷﺪ";
                        break;
                    case 33:
                        resultText = "ﺣﺴﺎﺏ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case 34:
                        resultText = "ﺧﻄﺎﻱ ﺳﻴﺴﺘﻤﻲ";
                        break;
                    case 35:
                        resultText = "ﺗﺎﺭﻳﺦ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case 36:
                        resultText = "ﺷﻤﺎﺭﻩ ﺩﺭﺧﻮﺍﺳﺖ ﺗﻜﺮﺍﺭﻱ ﺍﺳﺖ ";
                        break;
                    case 42:
                        resultText = "ﻳﺎﻓﺖ ﻧﺸﺪ Sale ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case 43:
                        resultText = " ﺩﺍﺩﻩ ﺷﺪﻩ ﺍﺳﺖ Verify ﻗﺒﻼ ﺩﺭﺧﻮﺍﺳﺖ ";
                        break;
                    case 44:
                        resultText = " ﻳﺎﻓﺖ ﻧﺸﺪ Verfiy ﺩﺭﺧﻮﺍﺳﺖ ";
                        break;
                    case 45:
                        resultText = "  ﺷﺪﻩ ﺍﺳﺖ Settle ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case 46:
                        resultText = "  ﻧﺸﺪﻩ ﺍﺳﺖ Settle ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case 47:
                        resultText = "ﻳﺎﻓﺖ ﻧﺸﺪ Settle ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case 48:
                        resultText = "  ﺷﺪﻩ ﺍﺳﺖ Reverse ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case 49:
                        resultText = "ﻳﺎﻓﺖ ﻧﺸﺪ Refund ﺗﺮﺍﻛﻨﺶ ";
                        break;
                    case 412:
                        resultText = "ﺷﻨﺎﺳﻪ ﻗﺒﺾ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ";
                        break;
                    case 413:
                        resultText = "ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ";
                        break;
                    case 414:
                        resultText = "ﺳﺎﺯﻣﺎﻥ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻗﺒﺾ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case 415:
                        resultText = "ﺯﻣﺎﻥ ﺟﻠﺴﻪ ﻛﺎﺭﻱ ﺑﻪ ﭘﺎﻳﺎﻥ ﺭﺳﻴﺪﻩ ﺍﺳﺖ";
                        break;
                    case 416:
                        resultText = " ﺧﻄﺎ ﺩﺭ ﺛﺒﺖ ﺍﻃﻼﻋﺎﺕ";
                        break;
                    case 417:
                        resultText = "ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﻛﻨﻨﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case 418:
                        resultText = "ﺍﺷﻜﺎﻝ ﺩﺭ ﺗﻌﺮﻳﻒ ﺍﻃﻼﻋﺎﺕ ﻣﺸﺘﺮﻱ";
                        break;
                    case 419:
                        resultText = "ﺗﻌﺪﺍﺩ ﺩﻓﻌﺎﺕ ﻭﺭﻭﺩ ﺍﻃﻼﻋﺎﺕ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﮔﺬﺷﺘﻪ ﺍﺳﺖ";
                        break;
                    case 421:
                        resultText = "ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ IP";
                        break;
                    case 51:
                        resultText = "ﺗﺮﺍﻛﻨﺶ ﺗﻜﺮﺍﺭﻱ ﺍﺳﺖ ";
                        break;
                    case 54:
                        resultText = "ﺗﺮﺍﻛﻨﺶ ﻣﺮﺟﻊ ﻣﻮﺟﻮﺩ ﻧﻴﺴﺖ";
                        break;
                    case 55:
                        resultText = "ﺗﺮﺍﻛﻨﺶ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                        break;
                    case 61:
                        resultText = "ﺧﻄﺎ ﺩﺭ ﻭﺍﺭﻳﺰ";
                        break;
                }
            }
            return resultText;
        }
        public DataTable GetStudentGovahiPaymentInquiry(string stcode)
        {
            return pDAO.GetStudentGovahiPaymentInquiry(stcode);
        }
        public PaymentDTO GetPaymentInfoByOrderID(long OrderID)
        {
            return pDAO.GetPaymentInfoByOrderID(OrderID);
        }
        public DataTable GetPaymentByRequestID(Int64 ReqId,int payType)
        {
            return pDAO.GetPaymentByRequestID(ReqId,payType);
        }
        public DataTable GetDefencePaymentByStcode(string stcode)
        {
            return pDAO.GetDefencePaymentByStcode(stcode);
        }
        public bool studentShouldPayStampAmount(Int64 requestID)
        {

            return false;
        }
    }
}
