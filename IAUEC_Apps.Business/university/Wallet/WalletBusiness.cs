using IAUEC_Apps.Business.ir.shaparak.bpm1;
using IAUEC_Apps.DAO.University.Wallet;
using IAUEC_Apps.DTO.University.Wallet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.Business.university.Wallet
{
    public class WalletBusiness
    {
        WalletDAO walletDAO = new WalletDAO();
        public PayRequestResultDTO AddPayment(PaymentDTO payment)
        {
            string log = DateTime.Now+" * ";
            var RequestKey = string.Empty;
            var Result = string.Empty;
            var gateway = string.Empty;
            var payDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            var payTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
            var PayerId = long.Parse(payment.stcode + payment.stcode.ToPayerId());
            log += "payerID=" + PayerId + " * ";
            Random random = new Random();
            long orderid;
            do
            {
                orderid = random.Next(100, 100000000);

            } while (walletDAO.SelectPayment(new PaymentDTO { OrderId = orderid, BankId = payment.BankId }).Count() > 0);
            log += "orderid=" + orderid + " * ";

            if (payment.BankId == Convert.ToInt32(BanksEnum.MellatBank))
            {
                //read config
                gateway = ConfigurationManager.AppSettings["PgwSite"];
                var callback = ConfigurationManager.AppSettings["Mellat_Wallet_CallBackURL"];
                var terminalId = Convert.ToInt64(ConfigurationManager.AppSettings["Mellat_TerminalId"]);
                var userName = ConfigurationManager.AppSettings["Username"];
                var password = ConfigurationManager.AppSettings["UserPassword"];

                //call web service
                PaymentGatewayImplService bpService = new PaymentGatewayImplService();
                var result = bpService.bpPayRequest(terminalId, userName, password, orderid, Convert.ToInt64(payment.Amount), payDate, payTime, payment.stcode, callback, PayerId);
                log += "result=" + result + " * ";
                var resultArray = result.Split(',');
                log += "resultArray=" + resultArray + " * ";
                Result = resultArray[0];
                if (resultArray.Length > 1)
                    RequestKey = resultArray[1];
                log += "RequestKey=" + RequestKey + " * ";
            }



            payment.OrderId = orderid;
            payment.CreateDate = DateTime.Now;
            payment.PayType = Convert.ToInt32(PayTypeEnum.OnlineTopUp);
            payment.RequestKey = RequestKey;
            payment.Result = -1;
            payment.RetrivalRefNo = RequestKey;
            payment.Status = (int)PaymentStatusEnum.New;
            log += "resultTranslate= " + TranslateMellatError(Convert.ToInt32(Result)) + " * ";
            log += "GateWay= " + gateway + " * ";
            System.IO.File.AppendAllText(@"D:\web-folder\IAUEC\University\wallet\walletError.txt", "\r\n" + log);
            if (walletDAO.InsertPayment(payment) > 0)
                return new PayRequestResultDTO { GateWay = gateway, RefId = RequestKey, Result = TranslateMellatError(Convert.ToInt32(Result)) };
            log = "--72--";
            System.IO.File.AppendAllText(@"D:\web-folder\IAUEC\University\wallet\walletError.txt", "\r\n" + log);

            return new PayRequestResultDTO { Result = "خطا در ثبت اطلاعات" };
        }

        public ConfirmPaymentDTO AcceptPayment(PaymentDTO payment)
        {
            if (payment.BankId == (int)BanksEnum.MellatBank)
            {
                var paymentObj = walletDAO.SelectPayment(new PaymentDTO { BankId = payment.BankId, RequestKey = payment.RequestKey })?.FirstOrDefault();
                if (paymentObj != null)
                {
                    if (payment.Result == 0)
                    {
                        PaymentGatewayImplService bpService = new PaymentGatewayImplService();
                        var res = bpService.bpVerifyRequest(
                            Convert.ToInt64(ConfigurationManager.AppSettings["Mellat_TerminalId"]),
                            ConfigurationManager.AppSettings["Username"],
                            ConfigurationManager.AppSettings["UserPassword"],
                            Convert.ToInt64(paymentObj.OrderId),
                            Convert.ToInt64(paymentObj.OrderId),
                            Convert.ToInt64(payment.TraceNumber)
                            );
                        if (res == "0")
                        {
                            paymentObj.Status = (int)PaymentStatusEnum.Commit;
                            paymentObj.TraceNumber = payment.TraceNumber;
                            paymentObj.Result = 0;
                            walletDAO.UpdatePayment(paymentObj);
                            walletDAO.InsertTransaction(new TransactionDTO
                            {
                                CurrentBalance = GetStudentCurrentBalance(paymentObj.stcode) + paymentObj.Amount,
                                stcode = paymentObj.stcode,
                                TransactionTypeId = (int)TransactionTypeEnum.Deposit,
                                Amount = paymentObj.Amount
                            });
                            //TODO: log
                            return new ConfirmPaymentDTO { Message = "ﺗﺮﺍﻛﻨﺶ ﺑﺎ ﻣﻮﻓﻘﻴﺖ ﺍﻧﺠﺎﻡ ﺷﺪ", Result = true, stcode = paymentObj.stcode };
                        }
                        else
                        {
                            paymentObj.Status = (int)PaymentStatusEnum.RollBack;
                            walletDAO.UpdatePayment(paymentObj);
                            return new ConfirmPaymentDTO { Result = false, Message = ".وجه مبلغ به حساب واحد دانشگاهی واریز نشد چنانچه پس از 72 ساعت آینده به حساب شما برگشت داده نشد مجددا پرداخت نمایید ", stcode = paymentObj.stcode };
                        }
                    }
                    else
                    {
                        paymentObj.Status = (int)PaymentStatusEnum.Failed;
                        walletDAO.UpdatePayment(paymentObj);
                        return new ConfirmPaymentDTO { Result = false, Message = TranslateMellatError(Convert.ToInt32(payment.Result)), stcode = paymentObj.stcode };
                        //return TranslateMellatError(Convert.ToInt32(result));
                    }
                }
                else
                    return new ConfirmPaymentDTO { Message = "شناسه پرداخت صحیح نیست", Result = false, stcode = "" };
            }
            return new ConfirmPaymentDTO { Message = "امکان برقراری ارتباط با بانک وجود ندارد", Result = false, stcode = "" };
        }


        public decimal GetStudentCurrentBalance(string stcode)
        {
            var balance = walletDAO.SelectTransaction(new TransactionDTO { stcode = stcode }).OrderByDescending(o => o.CreateDate)?.FirstOrDefault()?.CurrentBalance;
            return balance == null ? 0 : Convert.ToDecimal(balance);
        }
        public List<TransactionDTO> GetStudentTransactions(string stcode = null)
        {
            return walletDAO.SelectTransaction(new TransactionDTO { stcode = stcode });
        }
        public bool PayByWallet(TransactionDTO model, out string Message)
        {
            var balance = walletDAO.SelectTransaction(new TransactionDTO { stcode = model.stcode }).OrderByDescending(o => o.CreateDate)?.FirstOrDefault()?.CurrentBalance;
            if (balance == null || balance < model.Amount)
            {
                Message = "موجودی کیف پول شما کافی نیست، لطفاً ابتدا نسبت به افزایش اعتبار خود اقدام نمایید.";
                return false;
            }
            if (walletDAO.InsertTransaction(new TransactionDTO
            {
                Amount = -model.Amount,
                CurrentBalance = (decimal)balance - model.Amount,
                stcode = model.stcode,
                Description = model.Description,
                TransactionTypeId = model.TransactionTypeId > 0 ? model.TransactionTypeId : Convert.ToDecimal(TransactionTypeEnum.WalletPurchase)
            }) > 0)
            {
                Message = string.Empty;
                return true;
            }
            else
            {
                Message = "خطا در ثبت پرداخت.";
                return true;
            }
        }
        public long GenerateOrderIdForRequests()
        {
            long orderid = 0;
            Random r = new Random();
            IAUEC_Apps.DAO.CommonDAO.PaymentDAO PaymentDAO = new DAO.CommonDAO.PaymentDAO();
            do
            {
                orderid = Convert.ToInt64("700" + r.Next(100, 1000000).ToString()); //CommonBusiness.RandomNumber(100, 1000000);

            } while (PaymentDAO.CheckOrderId(orderid, 700));

            return orderid;
        }
        public bool CancelWalletPayment(decimal id)
        {
            var transaction = walletDAO.SelectTransaction(new TransactionDTO { Id = id })?.FirstOrDefault();
            var balance = walletDAO.SelectTransaction(new TransactionDTO { stcode = transaction.stcode }).OrderByDescending(o => o.CreateDate)?.FirstOrDefault()?.CurrentBalance;
            if (transaction != null)
            {
                return walletDAO.InsertTransaction(new TransactionDTO
                {
                    Amount = transaction.Amount,
                    CurrentBalance = (balance != null ? (decimal)balance : 0) + transaction.Amount,
                    Description = "بازگشت مبلغ تراکنش شناسه " + id,
                    stcode = transaction.stcode,
                    TransactionTypeId = Convert.ToDecimal(TransactionTypeEnum.CancelWalletPurchase)
                }) > 0;
            }
            return false;
        }

        public PaymentDTO GetPaymentByRequestKey(string requestKey)
        {
            return walletDAO.SelectPayment(new PaymentDTO { RequestKey = requestKey })?.FirstOrDefault();
        }

        private string TranslateMellatError(int errorCode)
        {
            switch (errorCode.ToString())
            {
                case "0":
                    return "0";
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

        public System.Data.DataTable getDebitTypes()
        {
            return walletDAO.getDebitTypes();
        }
        public System.Data.DataTable getStudentWalletInformation(string studentID)
        {
            return walletDAO.getStudentWalletInformation(studentID);
        }

    }
    public static class HelperMethods
    {
        public static string ToPayerId(this string personalCode)
        {
            try
            {
                if (!IsNumeric(personalCode))
                    return "0";
                if (personalCode.Length < 9)
                {
                    int zeroCount = 9 - personalCode.Length;
                    for (int i = 0; i < zeroCount; i++)
                        personalCode = "0" + personalCode;
                }
                var code = personalCode.ToCharArray();
                List<int> nums = new List<int>();
                string mmStr = "0";

                for(int i = 0; i <= personalCode.Length - 9; i++)
                    nums.Add(Convert.ToInt32(code[i].ToString()) * (code.Length - i));

                for (int i = 1; i < 9; i++)
                    nums.Add(Convert.ToInt32(code[personalCode.Length - 9 + i].ToString()) * i);
                var mm = (nums.Sum() % 99);
                if (mm.ToString().Length < 2)
                    mmStr = "0" + mm.ToString();
                else
                    mmStr = mm.ToString();
                return mmStr;
            }
            catch (Exception ex)
            {
                return "0";
            }
            //try
            //{
            //    if (!IsNumeric(personalCode))
            //        return "0";
            //    int a1, a2, a3, a4, a5, a6, a7, a8, a9;
            //    int sum, mm;
            //    string mmStr = "0";
            //    char[] code = new char[12];
            //    if (personalCode.Length < 9)
            //    {
            //        int zeroCount = 9 - personalCode.Length;
            //        for (int i = 0; i < zeroCount; i++)
            //        {
            //            personalCode = "0" + personalCode;
            //        }
            //    }
            //    code = personalCode.ToCharArray();

            //    a1 = Convert.ToInt32(code[0].ToString()) * 9;
            //    a2 = Convert.ToInt32(code[1].ToString()) * 1;
            //    a3 = Convert.ToInt32(code[2].ToString()) * 2;
            //    a4 = Convert.ToInt32(code[3].ToString()) * 3;
            //    a5 = Convert.ToInt32(code[4].ToString()) * 4;
            //    a6 = Convert.ToInt32(code[5].ToString()) * 5;
            //    a7 = Convert.ToInt32(code[6].ToString()) * 6;
            //    a8 = Convert.ToInt32(code[7].ToString()) * 7;
            //    a9 = Convert.ToInt32(code[8].ToString()) * 8;

            //    sum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 + a9;
            //    mm = (sum % 99);
            //    if (mm.ToString().Length < 2)
            //    {
            //        mmStr = "0" + mm.ToString();
            //    }
            //    else
            //    {
            //        mmStr = mm.ToString();
            //    }
            //    return mmStr;
            //}
            //catch
            //{
            //    return "0";
            //}
        }
        public static bool IsNumeric(string str)
        {
            var regexNumber = @"^([\d]+)$";
            return new Regex(regexNumber).IsMatch(str);
        }
    }
}
