
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DAO;
using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using IAUEC_Apps.DAO.University.Request;

namespace IAUEC_Apps.Business.Payment
{
    public class bmp_PaymentBusiness
    {
        IAUEC_Apps.DAO.CommonDAO.PaymentDAO PaymentDAO = new DAO.CommonDAO.PaymentDAO();

        public void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate (
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }
        #region Create

        private long GenerateOrderId()
        {
            long orderid = 0;
            do
            {
                orderid = CommonBusiness.RandomNumber(100, 1000000);

            } while (PaymentDAO.CheckOrderId(orderid, 2));

            return orderid;
        }
        

        private string PayerIdGenerator(string stcode)
        {
            try
            {

                if (!CommonBusiness.IsNumeric(stcode))
                    return "0";
                //int a1, a2, a3, a4, a5, a6, a7, a8, a9;
                int sum=0, mm;
                string mmStr = "0";
                char[] code = new char[12];
                if (stcode.Length < 9)
                {
                    int zeroCount = 9 - stcode.Length;
                    for (int i = 0; i < zeroCount; i++)
                    {
                        stcode = "0" + stcode;
                    }
                }
                code = stcode.ToCharArray();
                int remain8 = 1;
                for(int i = 0; i < code.Length; i++)
                {
                    if (code.Length - i <= 8)
                    {
                        sum += Convert.ToInt32(code[i].ToString()) * remain8;
                        remain8++;
                    }
                    else
                    {
                        sum += Convert.ToInt32(code[i].ToString()) * (code.Length - i);
                    }
                }



                //a1 = Convert.ToInt32(code[0].ToString()) * 9;
                //a2 = Convert.ToInt32(code[1].ToString()) * 1;
                //a3 = Convert.ToInt32(code[2].ToString()) * 2;
                //a4 = Convert.ToInt32(code[3].ToString()) * 3;
                //a5 = Convert.ToInt32(code[4].ToString()) * 4;
                //a6 = Convert.ToInt32(code[5].ToString()) * 5;
                //a7 = Convert.ToInt32(code[6].ToString()) * 6;
                //a8 = Convert.ToInt32(code[7].ToString()) * 7;
                //a9 = Convert.ToInt32(code[8].ToString()) * 8;

                //sum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 + a9;
                mm = (sum % 99);
                if (mm.ToString().Length < 2)
                {
                    mmStr = "0" + mm.ToString();
                }
                else
                {
                    mmStr = mm.ToString();
                }
                return mmStr;

            }
            catch
            {
                return "0";
            }


        }

        public void CreateStudentPayment(PaymentDTO payInfo)
        {
            PaymentDAO.CreateStudentPayment(payInfo);
        }
        public void CreateStudentRequestPayment(PaymentRequest payInfo)
        {
            PaymentDAO.CreateRequestPayment(payInfo);
        }
        public void CreateRequestStudentPayment(PaymentDTO payInfo)
        {
            PaymentRequestDAO PaymentDAO = new PaymentRequestDAO();
            PaymentDAO.CreateStudentPayment(payInfo);
        }

        private Int64 createPayerID(string payerUserID, int appID,int payType)
        {
            string paymentCode = "";

            var payerId = long.Parse(paymentCode+payerUserID + (PayerIdGenerator(paymentCode + payerUserID)));
            return payerId;
        }


        public string pay(long amount, string payerUserID, out long orderID,int appID=0,int payType=0)
        {
            string CallBackUrl = appID==1? ConfigurationManager.AppSettings["Mellat_CallBackUrl"] : ConfigurationManager.AppSettings["Mellat_Request_CallBackUrl"];
            string TerminalId = ConfigurationManager.AppSettings["Mellat_TerminalId"];
            string UserName = ConfigurationManager.AppSettings["UserName"];
            string UserPassword = ConfigurationManager.AppSettings["UserPassword"];
            var bpService = new ir.shaparak.bpm1.PaymentGatewayImplService();
            orderID = GenerateOrderId();
            var PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            var PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
            BypassCertificateError();


            var payerId = createPayerID(payerUserID,appID,payType);


            var result = bpService.bpPayRequest(long.Parse(TerminalId),
               UserName,
               UserPassword,
               orderID,
               amount,
               PayDate,
               PayTime,
               payerUserID,
               CallBackUrl, payerId);

            return result;
        }

        public string TuitionPayment(PaymentDTO model, out long orderID)
        {
            string CallBackUrl = ConfigurationManager.AppSettings["TuitionPaymentCallback"];
            string TerminalId = ConfigurationManager.AppSettings["Mellat_TerminalId"];
            string UserName = ConfigurationManager.AppSettings["UserName"];
            string UserPassword = ConfigurationManager.AppSettings["UserPassword"];
            var bpService = new ir.shaparak.bpm1.PaymentGatewayImplService();
            var PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            var PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
            BypassCertificateError();
            
            orderID = PaymentDAO.CreateTuitionPayment(model);

            if (orderID > 0)
            {
                var payerId = createPayerID(model.stcode, 0, 0);
                
                var result = bpService.bpPayRequest(long.Parse(TerminalId),
                   UserName,
                   UserPassword,
                   orderID,
                   model.Amount,
                   PayDate,
                   PayTime,
                   model.stcode,
                   CallBackUrl, payerId);

                return result;
            }
            return "";
        }
        #endregion



        #region Read

        public string bmp_PaymentResult(int resCode)
        {
            switch (resCode.ToString())
            {

                case "11":
                    return "ﺷﻤﺎﺭﻩ ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ ";
                    
                case "12":
                    return "ﻣﻮﺟﻮﺩﻱ ﻛﺎﻓﻲ ﻧﻴﺴﺖ ";
                case "13":
                    return "ﺭﻣﺰ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ ";
                case "14":
                    return "ﺗﻌﺪﺍﺩ ﺩﻓﻌﺎﺕ ﻭﺍﺭﺩ ﻛﺮﺩﻥ ﺭﻣﺰ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ ";
                case "15":
                    return "ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                case "16":
                    return "ﺩﻓﻌﺎﺕ ﺑﺮﺩﺍﺷﺖ ﻭﺟﻪ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ";
                case "17":
                    return "ﻛﺎﺭﺑﺮ ﺍﺯ ﺍﻧﺠﺎﻡ ﺗﺮﺍﻛﻨﺶ ﻣﻨﺼﺮﻑ ﺷﺪﻩ ﺍﺳﺖ ";
                case "18":
                    return "ﺗﺎﺭﻳﺦ ﺍﻧﻘﻀﺎﻱ ﻛﺎﺭﺕ ﮔﺬﺷﺘﻪ ﺍﺳﺖ";
                case "19":
                    return "ﻣﺒﻠﻎ ﺑﺮﺩﺍﺷﺖ ﻭﺟﻪ ﺑﻴﺶ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﺍﺳﺖ";
                case "111":
                    return "ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                case "112":
                    return "ﺧﻄﺎﻱ ﺳﻮﻳﻴﭻ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ";
                case "113":
                    return "ﭘﺎﺳﺨﻲ ﺍﺯ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻛﺎﺭﺕ ﺩﺭﻳﺎﻓﺖ ﻧﺸﺪ";
                case "114":
                    return "ﺩﺍﺭﻧﺪﻩ ﻛﺎﺭﺕ ﻣﺠﺎﺯ ﺑﻪ ﺍﻧﺠﺎﻡ ﺍﻳﻦ ﺗﺮﺍﻛﻨﺶ ﻧﻴﺴﺖ";
                case "21":
                    return "ﭘﺬﻳﺮﻧﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                case "23":
                    return "ﺧﻄﺎﻱ ﺍﻣﻨﻴﺘﻲ ﺭﺥ ﺩﺍﺩﻩ ﺍﺳﺖ";
                case "24":
                    return "ﺍﻃﻼﻋﺎﺕ ﻛﺎﺭﺑﺮﻱ ﭘﺬﻳﺮﻧﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                case "25":
                    return "ﻣﺒﻠﻎ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                case "31":
                    return "ﭘﺎﺳﺦ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ ";
                case "32":
                    return "ﻓﺮﻣﺖ ﺍﻃﻼﻋﺎﺕ ﻭﺍﺭﺩ ﺷﺪﻩ ﺻﺤﻴﺢ ﻧﻤﻲ ﺑﺎﺷﺪ";
                case "33":
                    return "ﺣﺴﺎﺏ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                case "34":
                    return "ﺧﻄﺎﻱ ﺳﻴﺴﺘﻤﻲ";
                case "35":
                    return "ﺗﺎﺭﻳﺦ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                case "41":
                    return "ﺷﻤﺎﺭﻩ ﺩﺭﺧﻮﺍﺳﺖ ﺗﻜﺮﺍﺭﻱ ﺍﺳﺖ ";
                case "42":
                    return "ﻳﺎﻓﺖ ﻧﺸﺪ Sale ﺗﺮﺍﻛﻨﺶ ";
                case "43":
                    return " ﺩﺍﺩﻩ ﺷﺪﻩ ﺍﺳﺖ Verify ﻗﺒﻼ ﺩﺭﺧﻮﺍﺳﺖ ";
                case "44":
                    return " ﻳﺎﻓﺖ ﻧﺸﺪ Verfiy ﺩﺭﺧﻮﺍﺳﺖ ";
                case "45":
                    return "  ﺷﺪﻩ ﺍﺳﺖ Settle ﺗﺮﺍﻛﻨﺶ ";
                case "46":
                    return "  ﻧﺸﺪﻩ ﺍﺳﺖ Settle ﺗﺮﺍﻛﻨﺶ ";
                case "47":
                    return "ﻳﺎﻓﺖ ﻧﺸﺪ Settle ﺗﺮﺍﻛﻨﺶ ";
                case "48":
                    return "  ﺷﺪﻩ ﺍﺳﺖ Reverse ﺗﺮﺍﻛﻨﺶ ";
                case "49":
                    return "ﻳﺎﻓﺖ ﻧﺸﺪ Refund ﺗﺮﺍﻛﻨﺶ ";
                case "412":
                    return "ﺷﻨﺎﺳﻪ ﻗﺒﺾ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ";
                case "413":
                    return "ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﻧﺎﺩﺭﺳﺖ ﺍﺳﺖ";
                case "414":
                    return "ﺳﺎﺯﻣﺎﻥ ﺻﺎﺩﺭ ﻛﻨﻨﺪﻩ ﻗﺒﺾ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                case "415":
                    return "ﺯﻣﺎﻥ ﺟﻠﺴﻪ ﻛﺎﺭﻱ ﺑﻪ ﭘﺎﻳﺎﻥ ﺭﺳﻴﺪﻩ ﺍﺳﺖ";
                case "416":
                    return " ﺧﻄﺎ ﺩﺭ ﺛﺒﺖ ﺍﻃﻼﻋﺎﺕ";
                case "417":
                    return "ﺷﻨﺎﺳﻪ ﭘﺮﺩﺍﺧﺖ ﻛﻨﻨﺪﻩ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                case "418":
                    return "ﺍﺷﻜﺎﻝ ﺩﺭ ﺗﻌﺮﻳﻒ ﺍﻃﻼﻋﺎﺕ ﻣﺸﺘﺮﻱ";
                case "419":
                    return "ﺗﻌﺪﺍﺩ ﺩﻓﻌﺎﺕ ﻭﺭﻭﺩ ﺍﻃﻼﻋﺎﺕ ﺍﺯ ﺣﺪ ﻣﺠﺎﺯ ﮔﺬﺷﺘﻪ ﺍﺳﺖ";
                case "421":
                    return "ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ IP";
                case "51":
                    return "ﺗﺮﺍﻛﻨﺶ ﺗﻜﺮﺍﺭﻱ ﺍﺳﺖ ";
                case "54":
                    return "ﺗﺮﺍﻛﻨﺶ ﻣﺮﺟﻊ ﻣﻮﺟﻮﺩ ﻧﻴﺴﺖ";
                case "55":
                    return "ﺗﺮﺍﻛﻨﺶ ﻧﺎﻣﻌﺘﺒﺮ ﺍﺳﺖ";
                case "61":
                    return "ﺧﻄﺎ ﺩﺭ ﻭﺍﺭﻳﺰ";
                default:
                    return "";
            }

        }
        public PaymentDTO GetPaymentByRefID(string RefId)
        {
            return PaymentDAO.GetPaymentByRefID(RefId);
        }
        public PaymentDTO GetRequestPaymentByRefID(string RefId)
        {
            PaymentRequestDAO PaymentDAO = new PaymentRequestDAO();
            return PaymentDAO.GetPaymentByRefID(RefId);
        }
        public PaymentDTO GetTuitionPaymentByRefId(decimal refId)
        {
            return PaymentDAO.GetTuitionPaymentByRefId(refId);
        }
        #endregion

        #region Update

        public void UpdateMellatPayment(PaymentDTO payment)
        {
            PaymentDAO.UpdateMellatPayment(payment);
        }
        public void UpdateMellatRquestPayment(PaymentDTO payment)
        {
            PaymentRequestDAO PaymentDAO = new PaymentRequestDAO();
            PaymentDAO.UpdateMellatPayment(payment);
        }
        public void UpdateTuitionPayment(PaymentDTO payment)
        {
            PaymentDAO.UpdateTuitionPayment(payment);
        }

        #endregion

    }
}