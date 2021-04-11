using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.CommonClasses;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net;
using System.IO;
using IAUEC_Apps.DAO.University.Support;
using IAUEC_Apps.DAO.Email;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Net.Security;
using System.Configuration;
using IAUEC_Apps.DTO.ResourceControlClasses;
using Newtonsoft.Json;

namespace IAUEC_Apps.Business.Common
{
    public class CommonBusiness
    {

        public string ReportConnection => ReturnSqlConnection.GetSidaSqlConnectionString(); //{ get { return con_sida.ConnectionString; } }
        Isida4_webservice_main.Isida4_webservice_mainservice ms = new Isida4_webservice_main.Isida4_webservice_mainservice();


        public string SupplementaryReportConnection => ReturnSqlConnection.GetSidaSqlConnectionString();//conn.ConnectionString;
        Email_ClassDAO EmailMessage = new Email_ClassDAO();
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        PassProfessorDAO Prof = new PassProfessorDAO();
        CommonDAO cmdao = new CommonDAO();
        string username = ConfigurationManager.AppSettings["SMS_UserName"].ToString();
        string pass = ConfigurationManager.AppSettings["SMS_Password"].ToString();
        string source = ConfigurationManager.AppSettings["SMS_Source"].ToString();
        string uri = ConfigurationManager.AppSettings["SMS_uri"].ToString();
        string uriStatus = ConfigurationManager.AppSettings["SMS_uriStatus"].ToString();

        #region Validations


        public const string regexNumber = @"^([\d]+)$";
        public const string regexPhoneNumber = @"^[1-9][0-9]{6,8}$";
        public const string regexPreCodePhoneNumber = @"^0[1-9]{2,3}$";
        public const string regexMobile = @"^09\d{9}$";
        public const string regexPersianAlphabetical = @"^[\u0600-\u06FF\s]+$";
        public const string regexEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string regexEnglish = @"[A-Za-z0-9 _]";
        public const string regexNumber_Email = @"[0-9]";
        private const string regexAverage = @"^[1][0-9]+(\.[0-9]{1,2})?$";
        private const string regexZipCode = @"^[1-9][0-9]{9}$";
        private const string regexNomreh = @"^(([1-9]|1\d)(\.\d{1,2})?|20)$";
        //private const string regSymbol = @"^[\x40-\x47][\x58-\x64][-[\\:;,']]";


        public static bool ValidateNationalCode(string nationalCode)
        {
            try
            {
                if (nationalCode.Length != 10)
                    return false;
                if (!IsNumeric(nationalCode))
                    return false;
                int a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a, b;
                int sum, mm;
                char[] code = new char[12];
                if (nationalCode == "1111111111" || nationalCode == "2222222222" ||
                              nationalCode == "3333333333" || nationalCode == "4444444444" ||
                              nationalCode == "5555555555" || nationalCode == "6666666666" ||
                              nationalCode == "7777777777" || nationalCode == "8888888888" ||
                              nationalCode == "9999999999" || nationalCode == "0000000000")
                    return false;
                code = nationalCode.ToCharArray();

                a1 = Convert.ToInt32(code[0].ToString()) * 10;
                a2 = Convert.ToInt32(code[1].ToString()) * 9;
                a3 = Convert.ToInt32(code[2].ToString()) * 8;
                a4 = Convert.ToInt32(code[3].ToString()) * 7;
                a5 = Convert.ToInt32(code[4].ToString()) * 6;
                a6 = Convert.ToInt32(code[5].ToString()) * 5;
                a7 = Convert.ToInt32(code[6].ToString()) * 4;
                a8 = Convert.ToInt32(code[7].ToString()) * 3;
                a9 = Convert.ToInt32(code[8].ToString()) * 2;
                //------------------
                a10 = Convert.ToInt32(code[9].ToString());//رقم اخر

                sum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 + a9;
                mm = (sum % 11);
                if (mm < 2)
                    if (a10 == mm)
                        return true;
                if (mm >= 2)
                    if ((11 - mm) == a10)
                        return true;
                return false;
            }
            catch
            {
                return false;
            }


        }


        public static bool IsNumeric(string str)
        {
            return new Regex(regexNumber).IsMatch(str);
        }
        public static bool ValidateZipCode(string str)
        {
            return new Regex(regexZipCode).IsMatch(str);
        }
        public static bool ValidatePhoneNumber(string str)
        {
            return new Regex(regexPhoneNumber).IsMatch(str);
        }

        public static bool ValidateEmail(string str)
        {
            return new Regex(regexEmail).IsMatch(str);
        }
        public static bool ValidatePreCodePhoneNumber(string str)
        {
            return new Regex(regexPreCodePhoneNumber).IsMatch(str);
        }
        public static bool ValidateMobile(string mobile)
        {
            return new Regex(regexMobile).IsMatch(mobile);
        }

        public static bool ValidateNomreh(string nomreh)
        {
            return new Regex(regexNomreh).IsMatch(nomreh);
        }


        //public static bool IsSymbol(char password)
        //{
        //    return new Regex(regSymbol).IsMatch(password.ToString());
        //}

        public static bool IsEnglishLetter(string text)
        {
            return new Regex(regexEnglish).IsMatch(text);
        }
        public bool CheckLettersIsEnglishCharacters(string inputstring)
        {
            string firstChar = inputstring.ElementAt(0).ToString();

            if (new Regex(regexEnglish).IsMatch(inputstring)
                    && new Regex(regexNumber_Email).IsMatch(firstChar) == false)
                return true;
            else
                return false;
        }




        public bool CheckPasswordIsValidate(string password, string username)
        {
            bool isDigit = false;
            bool isLetter = false;
            bool isSymbol = false;

            if (password.Count() <= 7 || password.Count() >= 26)
                return false;

            if (password.ToLower().Contains(username.ToLower()))
                return false;

            foreach (char c in password)
            {
                if (char.IsDigit(c))
                    isDigit = true;
                if (char.IsLetter(c))
                    isLetter = true;
                if (((int)c > 32 && (int)c <= 47) || ((int)c >= 58 && (int)c <= 64))
                {
                    isSymbol = true;
                }

            }

            if (isDigit && isLetter && isSymbol)
                return true;
            else
                return false;
        }


        public static bool IsConnectedToInternet()
        {

            int Desc;
            return InternetGetConnectedState(out Desc, 0);

        }
        public static bool IsConnected()
        {
            System.Uri Url = new System.Uri("http://www.google.com");

            System.Net.WebRequest WebReq;
            System.Net.WebResponse Resp;
            WebReq = System.Net.WebRequest.Create(Url);

            try
            {
                Resp = WebReq.GetResponse();
                Resp.Close();
                WebReq = null;
                return true;
            }

            catch
            {
                WebReq = null;
                return false;
            }
        }


        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        public static byte[] objectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        public Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binForm = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }
        //Administrator

        public string GetIPAddress()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            string ipadd = "";
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipadd = ip.ToString();
                }

            }
            return ipadd;
        }

        #endregion

        #region Read
        public DataTable getBasicInformation(int infoType, int infoID = 0)
        {
            return cmdao.getBasicInformation(infoType, infoID);
        }
        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public DataTable GetUserLogByModifyId(int ModifyId, int AppId)
        {
            return cmdao.GetUserLogByModifyId(ModifyId, AppId);
        }
        public DataTable GetUserLogByStcode(int ModifyId, int AppId)
        {
            return cmdao.GetUserLogByModifyId(ModifyId, AppId);
        }

        public DataTable GetUserAndStudentLogModifyId(int ModifyId, int AppId)
        {

            return cmdao.GetStudentLogByModifyId(ModifyId, AppId);


        }

        public DataTable getUserAndStudentLogByAppId(int appId, int studentCode_HrID)
        {
            Business.university.Request.ProfessorRequestBusiness PRB = new university.Request.ProfessorRequestBusiness();
            DataTable dtStudent = new DataTable();
            DataTable dtUserLog = new DataTable();
            DataTable dtRequest = new DataTable();
            DataTable dtLogByRequest = new DataTable();
            DataTable dt = new DataTable();
            DataRow[] drSelect;

            dt.Columns.Add("rowId");
            dt.Columns.Add("Name");
            dt.Columns.Add("LogDate");
            dt.Columns.Add("LogTime");
            dt.Columns.Add("EventName");
            dt.Columns.Add("Description");
            dt.Columns.Add("reqId");


            dtUserLog = cmdao.GetUserLogByModifyId(studentCode_HrID, appId);
            drSelect = dtUserLog.Select("event in(69,70,71,72,73,87,88,89,90,91,134,150,157,158,159,160)");
            foreach (DataRow dr in drSelect)
            {
                if (Convert.ToInt32(dr["event"]) >= (int)DTO.eventEnum.امضای_قرارداد_توسط_مدیر_کارگزینی && Convert.ToInt32(dr["event"]) <= (int)DTO.eventEnum.رد_قرارداد_توسط_کارگزینی)
                {

                }
                DataRow drTemp = dt.NewRow();
                drTemp["Name"] = dr["name"];
                drTemp["LogDate"] = dr["LogDate"];
                drTemp["LogTime"] = dr["LogTime"];
                drTemp["eventName"] = dr["eventName"];
                drTemp["Description"] = dr["Description"];
                drTemp["reqId"] = "";
                dt.Rows.Add(drTemp);
            }


            dtRequest = PRB.GetAllRequestsByHRCode(studentCode_HrID);
            if (dtRequest.Rows.Count > 0)
            {
                dtLogByRequest = new DataTable();
                foreach (DataRow dr in dtRequest.Rows)
                {
                    try
                    {
                        dtLogByRequest.Merge(cmdao.GetUserLogByModifyId(Convert.ToInt32(dr["ProfessorRequestID"]), appId));

                    }
                    catch { }
                }
                if (dtLogByRequest.Rows.Count > 0)
                {
                    drSelect = dtLogByRequest.Select("event in(4,5,144,145,146,147,148,149,151,172)");
                    foreach (DataRow dr in drSelect)
                    {
                        DataRow drTemp = dt.NewRow();
                        drTemp["Name"] = dr["name"];
                        drTemp["LogDate"] = dr["LogDate"];
                        drTemp["LogTime"] = dr["LogTime"];
                        drTemp["eventName"] = dr["eventName"];
                        drTemp["Description"] = dr["Description"];
                        drTemp["reqId"] = "کد درخواست: " + dr["modifyId"];
                        dt.Rows.Add(drTemp);
                    }
                }
            }
            dtStudent = cmdao.getStudentLogByAppId(appId, studentCode_HrID);


            //foreach (DataRow dr in dtUserLog.Rows)
            //{
            //    DataRow drTemp=dt.NewRow();
            //    drTemp["Name"] = dr["name"];
            //    drTemp["LogDate"] = dr["LogDate"];
            //    drTemp["LogTime"] = dr["LogTime"];
            //    drTemp["eventName"] = dr["eventName"];
            //    drTemp["Description"] = dr["Description"];
            //    drTemp["reqId"] = dr["modifyId"];
            //    dt.Rows.Add(drTemp);
            //}
            foreach (DataRow dr in dtStudent.Rows)
            {
                DataRow drTemp = dt.NewRow();
                drTemp["Name"] = "استاد";
                drTemp["LogDate"] = dr["LogDate"];
                drTemp["LogTime"] = dr["LogTime"];
                drTemp["eventName"] = dr["eventName"];
                if (dr["event"].ToString() == "38")
                {
                    drTemp["description"] = dr["Description"].ToString();
                }
                else
                {
                    if (dr["Description"] != DBNull.Value)
                        drTemp["reqId"] = dr["Description"].ToString();
                }
                dt.Rows.Add(drTemp);
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "LogDate,LogTime";
            dt = new DataTable();
            dt = dv.ToTable();
            for (int i = 1; i <= dt.Rows.Count; i++)
                dt.Rows[i - 1]["rowId"] = i;
            return dt;
        }

        public DataTable getContractHistory(int hrID)
        {
            DataTable dtUser = new DataTable();
            DataTable dtTeacher = new DataTable();
            DataTable dtLog = new DataTable();
            dtLog.Columns.Add("name");
            dtLog.Columns.Add("logdate");
            dtLog.Columns.Add("logtime");
            dtLog.Columns.Add("eventname");
            dtLog.Columns.Add("description");
            dtLog.Columns.Add("userType");
            dtUser = cmdao.GetUserLogByModifyId(hrID, 13);
            DataRow[] drUser = dtUser.Select("event in(159,158,157,160)");
            dtTeacher = cmdao.getStudentLogByAppId(13, hrID);
            DataRow[] drTeacher = dtTeacher.Select("event=38");
            foreach (DataRow dr in drUser)
            {
                DataRow drLog = dtLog.NewRow();
                drLog["name"] = dr["name"];
                drLog["eventname"] = dr["eventname"];
                drLog["logdate"] = dr["logdate"];
                drLog["logtime"] = dr["logtime"];
                drLog["description"] = dr["description"];
                drLog["userType"] = 2;
                dtLog.Rows.Add(drLog);
            }
            foreach (DataRow dr in drTeacher)
            {

                DataRow drLog = dtLog.NewRow();
                drLog["name"] = "استاد";
                drLog["eventname"] = dr["eventname"];
                drLog["logdate"] = dr["logdate"];
                drLog["logtime"] = dr["logtime"];
                drLog["userType"] = 1;
                drLog["description"] = dr["description"];
                dtLog.Rows.Add(drLog);
            }
            DataView dv = dtLog.DefaultView;
            dv.Sort = "LogDate,LogTime,userType";
            dtLog = new DataTable();
            dtLog = dv.ToTable();
            return dtLog;
        }

        public DataTable getAgreementHistory(int hrID)
        {
            DataTable dtUser;
            DataTable dtTeacher;
            DataTable dtLog = new DataTable();
            dtLog.Columns.Add("name");
            dtLog.Columns.Add("logdate");
            dtLog.Columns.Add("logtime");
            dtLog.Columns.Add("eventname");
            dtLog.Columns.Add("description");
            dtUser = cmdao.GetUserLogByModifyId(hrID, 13);
            DataRow[] drUser = dtUser.Select("event in(216,217)");
            dtTeacher = cmdao.getStudentLogByAppId(13, hrID);
            DataRow[] drTeacher = dtTeacher.Select("event=50");
            foreach (DataRow dr in drUser)
            {
                DataRow drLog = dtLog.NewRow();
                drLog["name"] = dr["name"];
                drLog["eventname"] = dr["eventname"];
                drLog["logdate"] = dr["logdate"];
                drLog["logtime"] = dr["logtime"];
                drLog["description"] = dr["description"];
                dtLog.Rows.Add(drLog);
            }
            foreach (DataRow dr in drTeacher)
            {

                DataRow drLog = dtLog.NewRow();
                drLog["name"] = "استاد";
                drLog["eventname"] = dr["eventname"];
                drLog["logdate"] = dr["logdate"];
                drLog["logtime"] = dr["logtime"];
                drLog["description"] = dr["description"];
                dtLog.Rows.Add(drLog);
            }
            DataView dv = dtLog.DefaultView;
            dv.Sort = "LogDate,LogTime";
            dtLog = dv.ToTable();
            return dtLog;
        }

        public DataTable GetInfoPeoByCodeMeliINAspNetUsers(string codeMeli)
        {
            return cmdao.GetInfoPeoByCodeMeliINAspNetUsers(codeMeli);
        }

        public DataTable GetStudentInfoByStcode(string Stcode)
        {
            return cmdao.GetStudentInfoByStcode(Stcode);
        }
        public DataTable getStudentTuitional(string term, string stcode)
        {
            return cmdao.getStudentTuitional(term, stcode);
        }
        public void insertTuitional(string username, string stcode, string fishNumber, decimal amount, string term)
        {
            cmdao.insertTuitional(username, stcode, fishNumber, amount, term);
        }

        public bool insertTuitionFormol(string level, string currentYear, string CurrentTerm, string byYear, string LastTerm, decimal fix_Percent, decimal var_Perecnt, int insurance, Int64 services)
        {

            return cmdao.insertTuitionFormol(level, currentYear, CurrentTerm, byYear, LastTerm, (decimal)(100 + fix_Percent) / 100, (decimal)(100 + var_Perecnt) / 100, insurance, services);
        }
        public bool insertTuitionFormol_LastEntries(string CurrentTerm, string LastTerm, decimal var_Percent, int level)
        {

            return cmdao.insertTuitionFormol_LastEntries(CurrentTerm, LastTerm, (decimal)(100 + var_Percent) / 100, level);
        }


        public bool openCartAccess(string stcode)
        {
            return cmdao.openCartAccess(stcode);
        }
        public string getCurrentTerm()
        {
            return cmdao.GetCurrentTerm();
        }

        public string getLastTerm(string byTerm)
        {
            DataTable dtTerm = SelectAllTerm();
            var dr = dtTerm.Select("substring(tterm,7,1)<>'3' and tterm<'" + byTerm + "'").Max(c => c["tterm"]);
            if (dr != null)
                return dr.ToString();
            return "";
        }


        public DataTable GetAppIDMessage(int type, int AppID, int Category, int status)
        {
            return EmailMessage.GetTextMessage(type, AppID, Category, status);
        }
        //ramezanian-start
        public DataTable GetMessage1(string Text)
        {
            return cmdao.GetShowStatus(Text);
        }
        //ramezanian-end
        //ramezanian-start
        public DataTable GetMessage(string Text)
        {
            return Prof.GetShowStatus(Text);
        }
        //ramezanian-end
        public DataTable GetAllTermByStudent(string stcode)
        {
            return cmdao.GetAllTermByStudent(stcode);
        }
        public DataTable getActiveTerm_AdobeConnection()
        {
            string term = "99-99-9";
            System.Data.DataTable dt = GetAllAdobeConnectionTerms();
            term = dt.Compute("max(term)", "").ToString();
            int sal = Convert.ToInt32(term.Substring(0, 2));
            int nimsal = Convert.ToInt32(term.Substring(term.Length - 1));
            term = string.Format("{0}-{1}-{2}", sal - 2, sal - 1, nimsal);
            System.Data.DataRow[] dr = dt.Select("term>'" + term + "'");
            System.Data.DataTable dtFinal = new System.Data.DataTable();
            dtFinal.Columns.Add("term");
            foreach (System.Data.DataRow r in dr)
            {
                System.Data.DataRow rnew = dtFinal.NewRow();
                rnew[0] = r[0];
                dtFinal.Rows.Add(rnew);
            }


            return dtFinal;
        }

        public DataTable GetAllAdobeConnectionTerms()
        {
            return cmdao.GetAllAdobeConnectionTerms();
        }

        public DataTable getShahrestan()
        {
            return getShahrestan(0, 0);
        }
        public DataTable getShahrestan(int ostanCode)
        {
            return getShahrestan(ostanCode, 0);
        }
        public DataTable getShahrestan(int ostanCode, int shahrCode)
        {
            return cmdao.getShahrestan(shahrCode, ostanCode);
        }
        public DataTable getCity(int StateID, int cityID)
        {
            return cmdao.getCity(StateID, cityID);
        }

        public DataTable getCity(int StateID)
        {
            return getCity(StateID, 0);
        }
        public DataTable getCitiesFromTblShahrestan(int StateID)
        {
            return cmdao.getCitiesFromTblShahrestan(StateID);
        }

        public DataTable GetOstan(int ostanID = 0)
        {
            return cmdao.GetOstan(ostanID);
        }

        public DataTable GetState(int stateID = 0)
        {
            return cmdao.getState(stateID);
        }

        public DataTable GetStateFromTblOstan(int stateID = 0)
        {
            return cmdao.GetStateFromTblOstan(stateID);
        }


        public DataTable getProfClasses(string term = "this")
        {
            return cmdao.getProfClasses(term);
        }

        public string CalculateMD5Hash(String PasswordPlainText)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(PasswordPlainText);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string EncryptPass(String PasswordPlainText)
        {
            string plainText = PasswordPlainText.Trim();
            string cipherText = "";                 // encrypted text
            string passPhrase = "Pas5pr@se";        // can be any string
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            EncryptionClass ThisKey =
            new EncryptionClass(passPhrase, initVector);
            cipherText = ThisKey.Encrypt(plainText);
            return cipherText;

        }
        public static string DecryptPass(String CipherText)
        {
            string plainText = "";
            string cipherText = CipherText;                 // encrypted text
            string passPhrase = "Pas5pr@se";        // can be any string
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes

            EncryptionClass ThisKey =
            new EncryptionClass(passPhrase, initVector);
            plainText = ThisKey.Decrypt(cipherText);

            return plainText;

        }
        public static string getFileExtension(string fileName)
        {
            string extension = "";

            char[] arr = fileName.ToCharArray();

            int index = 0;

            for (int i = 0; i < arr.Length; i++)
            {

                if (arr[i] == '.')
                {
                    index = i;
                }

            }

            for (int x = index + 1; x < arr.Length; x++)
            {
                extension = extension + arr[x];
            }

            return extension;

        }
        public static string getFileNameWithoutExtension(string fileName)
        {
            string name = "";

            char[] arr = fileName.ToCharArray();

            int index = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '.')
                {
                    index = i;
                }
            }

            for (int x = 0; x < index; x++)
            {
                name = name + arr[x];
            }
            return name;

        }

        public string GetTimeToken()
        {
            DateTime dt = DateTime.Now;
            byte[] b_dt = Encoding.UTF8.GetBytes(dt.AddDays(1).ToString());
            string code = Convert.ToBase64String(b_dt);
            return code;
        }
        public string PayerIdGenerator(string stcode)
        {
            try
            {
                if (!IsNumeric(stcode))
                    return "0";
                int a1, a2, a3, a4, a5, a6, a7, a8, a9;
                int sum, mm;
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

                a1 = Convert.ToInt32(code[0].ToString()) * 9;
                a2 = Convert.ToInt32(code[1].ToString()) * 1;
                a3 = Convert.ToInt32(code[2].ToString()) * 2;
                a4 = Convert.ToInt32(code[3].ToString()) * 3;
                a5 = Convert.ToInt32(code[4].ToString()) * 4;
                a6 = Convert.ToInt32(code[5].ToString()) * 5;
                a7 = Convert.ToInt32(code[6].ToString()) * 6;
                a8 = Convert.ToInt32(code[7].ToString()) * 7;
                a9 = Convert.ToInt32(code[8].ToString()) * 8;
                //------------------
                // a10 = Convert.ToInt32(code[9].ToString());//رقم اخر

                sum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 + a9;
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

        public bool SendEmail2(string strTo, string strSubject, string strBody)
        {

            MailMessage objMailMessage = new MailMessage();
            System.Net.NetworkCredential objSMTPUserInfo =
                new System.Net.NetworkCredential();
            SmtpClient objSmtpClient = new SmtpClient();
            string s_html = "<HTML><div dir='rtl' style='font-family:Tahoma'>";
            string e_html = "</div></HTML>";
            try
            {
                objMailMessage.From = new MailAddress("noreply.iauec@gmail.com");
                //objMailMessage.From = new MailAddress("noreply@iauec.ac.ir");
                objMailMessage.To.Add(new MailAddress(strTo));
                objMailMessage.Subject = strSubject;
                objMailMessage.Body = s_html + strBody + e_html;
                objMailMessage.IsBodyHtml = true;
                objSmtpClient = new SmtpClient("smtp.gmail.com");
                // objSmtpClient = new SmtpClient("192.168.1.5");
                //objSmtpClient.Port = 25;
                objSMTPUserInfo = new System.Net.NetworkCredential
               ("noreply.iauec", "A_rohani");
                //   ("mylms.iauec@gmail.com", "information110");
                objSmtpClient.Credentials = objSMTPUserInfo;
                objSmtpClient.Port = 587;
                objSmtpClient.UseDefaultCredentials = true;
                objSmtpClient.EnableSsl = true;

                objSmtpClient.Send(objMailMessage);
                objMailMessage = null;
                objSMTPUserInfo = null;
                objSmtpClient = null;
                return true;
            }
            catch (Exception ex)
            { throw ex; }



        }

        public bool SendEmail1(string strTo, string strSubject, string strBody)
        {

            try
            {

                using (MailMessage mm = new MailMessage("noreply.iauec@gmail.com", strTo))
                {
                    mm.Subject = strSubject;

                    string s_html = "<HTML><div dir='rtl' style='font-family:Tahoma'>";
                    string e_html = "</div></HTML>";

                    mm.Body = s_html + strBody + e_html;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    //  NetworkCredential NetworkCred = new NetworkCredential("testhamfekran@gmail.com", "<#mercuri#>");
                    NetworkCredential NetworkCred = new NetworkCredential("noreply.iauec@gmail.com", "A_rohani");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);

                    return true;
                    //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
                }


            }
            catch (Exception ex)
            { throw ex; }



        }


        public string SendEmail(string strTo, string strSubject, string strBody)
        {


            string Username = "noreply";
            string Password = "A*_*rohani";
            string SmtpServer = "mail.iauec.ac.ir";

            string From = Username + "@iauec.ac.ir";
            string s_html = "<HTML><div dir='rtl' style='font-family:Tahoma'>";
            string e_html = "</div></HTML>";
            try
            {
                SmtpClient s = new SmtpClient(SmtpServer);
                s.Port = 25;
                s.Credentials = new NetworkCredential(From, Password);
                s.DeliveryMethod = SmtpDeliveryMethod.Network;
                s.UseDefaultCredentials = true;
                s.EnableSsl = false;



                MailMessage m = new MailMessage(From, strTo);
                m.IsBodyHtml = true;
                m.Subject = strSubject;

                //LinkedResource backgroundLink = new LinkedResource("D:\\programs\\style_r1_c1.png");
                //backgroundLink.ContentId = "BackgroundImage";
                //backgroundLink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(m.Body, null, System.Net.Mime.MediaTypeNames.Text.Html);
                //htmlView.LinkedResources.Add(backgroundLink);
                //m.AlternateViews.Add(htmlView);

                m.Body = s_html + strBody + e_html;



                // m.Bcc.Add("a_rohani@iauec.ac.ir");
                try
                {
                    s.Send(m);
                }
                catch (Exception em)
                {

                    return em.Message;
                }

                return "ارسال ایمیل با موفقیت";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }




        #region SMS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userType">1:student 2:teacher</param>
        /// <param name="userID"></param>
        /// <param name="message"></param>
        /// <param name="sentSMS"></param>
        /// <param name="smsStatusText"></param>
        /// <returns></returns>
        public string sendSMS(int userType, string userID, string message, out bool sentSMS, out string smsStatusText)
        {
            string mobile = "";
            smsStatusText = "";
            sentSMS = false;

            UniversityDAO oUniversity = new UniversityDAO();
            if (userType == 1)//student
            {
                mobile = oUniversity.GetStudentMobileByStcode(userID);
            }
            else if (userType == 2)//professor
            {
                mobile = oUniversity.GetProfMobileByCode(userID);
            }
            if (string.IsNullOrEmpty(mobile))
            {
                smsStatusText = "شماره تلفن پیدا نشد";
                return "";
            }

            return sendSMS(mobile.Trim(), message, out sentSMS, out smsStatusText);
        }

        public string sendSMS(string Mobile, string message, out bool sentSMS, out string smsStatusText)
        {
            sentSMS = false; smsStatusText = ""; string asanakCode = "";
            Mobile = Mobile.Trim();
            if (Mobile.Length != 11 || !Mobile.StartsWith("09") || Mobile.Contains("-") || !IsNumeric(Mobile))
            {
                smsStatusText = "شماره تلفن همراه نامعتبر است";
                return asanakCode;
            }

            string resultSendSMS = _sendSMS(Mobile, message, out sentSMS);
            if (string.IsNullOrEmpty(resultSendSMS))
                sentSMS = false;
            if (sentSMS)
            {
                asanakCode = resultSendSMS.Substring(1, (resultSendSMS.Length) - 2);
                if (!IsNumeric(asanakCode))
                {
                    sentSMS = false;
                }
                smsStatusText = ShowStatusSMS_Text(asanakCode, out sentSMS);
            }
            insertLog_MobileApp(Mobile, message);
            return asanakCode;
        }

        private void insertLog_MobileApp(string Mobile, string message)
        {
            #region SendAppMessage
            try
            {
                var stCodeList = GetStCode(Mobile);
                var stCodeListIdentity = new List<string>();
                foreach (var stCode in stCodeList)
                {
                    var res = PersonIdentity(stCode);
                    if (res)
                        stCodeListIdentity.Add(stCode);
                    else
                        InsertLogAppSms(stCode, "عدم وجود در اپلیکیشن", false, Mobile);
                }
                var appsms = new AppSmsDTO
                {
                    Title = "سامانه خدمات",
                    Body = message,
                    CategoryId = 1001,
                    Receivers = stCodeListIdentity.ToArray(),
                    SendNotif = true
                };
                var sendRes = SendContent(appsms);
                stCodeListIdentity.ForEach(x => InsertLogAppSms(x, sendRes, true, Mobile));
            }
            catch (Exception ex) { }
            #endregion
        }

        private string _sendSMS(string Mobile, string message, out bool sentSMS)
        {
            try
            {
                var ret = "";
                // Create the web request
                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;

                // Set type to POST
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                // Create the data we want to send
                StringBuilder data = new StringBuilder();
                data.Append("username=" + username.Trim());
                data.Append("&password=" + pass.Trim());
                data.Append("&source=" + source.Trim());
                data.Append("&destination=" + Mobile);
                data.Append("&message=" + message);

                // Create a byte array of the data we want to send
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());

                // Set the content length in the request headers
                request.ContentLength = byteData.Length;

                // Write data
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                // Get response
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string Result = reader.ReadToEnd();
                    sentSMS = true;
                    ret = Result;
                }

                return ret;
            }
            catch (Exception ex)
            {
                sentSMS = false;
                throw ex;
            }

        }
        private List<string> GetStCode(string mobile)
        {
            var _commonDAO = new CommonDAO();
            return _commonDAO.GetStcode(mobile);
        }
        private static bool PersonIdentity(string stCode)
        {
            string html = string.Empty;
            string url = "http://mobile.iauec.ac.ir/Api/MainApi/PersonIdentity?code=" + stCode;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
                var res = JsonConvert.DeserializeObject<ResponceAppSms>(html);
                return res.Result == "true";
            }
        }
        private static string SendContent(AppSmsDTO model)
        {
            using (var client = new WebClient())
            {
                var encodedJson = JsonConvert.SerializeObject(model);
                client.Headers.Add("Content-Type:application/json");
                client.Encoding = Encoding.UTF8;
                var response = client.UploadString("http://mobile.iauec.ac.ir/Api/MainApi/SendContent", encodedJson);
                var res = JsonConvert.DeserializeObject<ResponceAppSms>(response);
                return res.Result;
            }
        }

        private static void InsertLogAppSms(string stCode, string message, bool isSend, string mobile)
        {
            var _commonDAO = new CommonDAO();
            _commonDAO.InsertLogAppSms(stCode, message, isSend, mobile);
        }

        public string ShowStatusSMS(string asanakCode)
        {
            bool sentSMS;
            return ShowStatusSMS_Text(asanakCode, out sentSMS);
        }

        public string ShowStatusSMS(string asanakCode, out bool sentSMS)
        {
            return ShowStatusSMS_Text(asanakCode, out sentSMS);
        }

        private string ShowStatusSMS_Text(string asanakCode, out bool sentSMS)
        {
            string statusQueries = SMSstatusQueries_FromAsanak(asanakCode);
            string resultSMS = getSMSstatus_text(statusQueries, out sentSMS);
            return resultSMS;
        }

        private string SMSstatusQueries_FromAsanak(string asanakCode)
        {
            HttpWebRequest request = WebRequest.Create(uriStatus) as HttpWebRequest;

            // Set type to POST
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Create the data we want to send
            StringBuilder data = new StringBuilder();
            data.Append("username=" + username.Trim());
            data.Append("&password=" + pass.Trim());
            data.Append("&msgid=" + asanakCode.Trim());
            //data.Append("&msgid=" + lbl_Resault.Substring(30 , (lbl_Resault.Text.Lenght)-73));

            // Create a byte array of the data we want to send
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());


            // Set the content length in the request headers
            request.ContentLength = byteData.Length;


            // Write data
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            // Get response
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string Status = reader.ReadToEnd();
                return Status;
            }
        }

        private string getSMSstatus_text(string statusQueries, out bool sentSMS)
        {
            DataTable dt = new DataTable();
            int code = _getAsanakStatusID(statusQueries);
            //sentSMS = true; //(code == 10 || code == 11 || code == 12 || code == 2 || code == 6 || code == 9);
            dt = GetMessage(code.ToString());
            if (dt.Rows.Count > 0)
            {
                sentSMS = true;
                return dt.Rows[0]["comment"].ToString();
            }
            else
            {
                sentSMS = false;
                return "پیامی ارسال نشده است";

            }
        }

        private int _getAsanakStatusID(string statusQuery)
        {
            string ss;
            if (statusQuery.Substring(12, (statusQuery.Length) - 15) == "NotFound")
            {
                ss = "-1";
            }
            else if (statusQuery.Length < 104)
            {
                ss = "-2";
            }
            else
            {
                ss = (statusQuery.Substring(32, (statusQuery.Length) - 104));
                ss = Regex.Replace(ss, @"[^\d]", "");

            }
            return Convert.ToInt32(ss);
        }

        public int getAsanakStatusID(string codeAsanak)
        {
            string statusQueries = SMSstatusQueries_FromAsanak(codeAsanak);
            int result = _getAsanakStatusID(statusQueries);
            return result;
        }

        public DataTable GetCodeAsanak(string Code, int IDRow)
        {
            return cmdao.GetCodeAsanak(Code, IDRow);
        }

        #endregion SMS


        public DataTable GetSearchStudentOrProf(string code)
        {
            return cmdao.GetSearchStudentOrProf(code);
        }
        //ramezanian
        public DataTable SelectAllTerm()
        {
            return cmdao.SellectAllTerm();
        }
        //ramezanian
        public DataTable SelectAllDaneshkade()
        {
            return cmdao.SelectAllDaneshkade(0);
        }
        public DataTable SelectAllDaneshkade(int daneshID)
        {
            return cmdao.SelectAllDaneshkade(daneshID);
        }


        //ramezanian
        public DataTable GetAllTypeCooperation()
        {
            return cmdao.GetAllTypeCooperation();
        }
        //ramezanian
        public DataTable GetAllInformationFaculty()
        {
            return cmdao.GetAllInformationFaculty();
        }
        //ramezanian
        public DataTable GetInformationFacultyByFilter(int CodeProf, string Family, string NameEp, int Cooperation)
        {
            return cmdao.GetInformationFacultyByFilter(CodeProf, Family, NameEp, Cooperation);
        }


        //ramezanian
        public DataTable GetAllDepartman()
        {
            return cmdao.GetAllDepartman(0);
        }
        public DataTable GetAllDepartman(int daneshId)
        {
            return cmdao.GetAllDepartman(daneshId);
        }
        //ramezanian
        //public DataTable GetTerm()
        //{
        //    return cmdao.GetTerm();
        //}
        //ramezanian
        public DataTable GetLesson()
        {
            return cmdao.GetLesson();
        }
        //ramezanian
        public DataTable GetDateMiladiToShamsi(DateTime Date)
        {
            return cmdao.GetDateMiladiToShamsi(Date);
        }
        //ramezanian
        public DataTable SelectAllField()
        {
            return cmdao.SelectAllField(0);
        }
        public DataTable SelectAllField(int daneshId)
        {
            return cmdao.SelectAllField(daneshId);
        }

        public DataTable SelectField_fcoding()
        {
            DataTable dt = GetCodingByTypeId(4);
            dt.Columns["namecoding"].ColumnName = "nameresh";
            return dt;
        }

        public DataTable GetTextMessage(int type, int AppID, int Category, int status)
        {
            return cmdao.GetTextMessage(type, AppID, Category, status);
        }

        public DataTable GetSystemAvailability(int AppID)
        {
            return cmdao.GetSystemAvailability(AppID);
        }

        public DataTable GetSystemAvailability(int AppID, int UserKind, int UserStatus)
        {
            return cmdao.GetSystemAvailability(AppID, UserKind, UserStatus);
        }

        public DataTable GetNameCity_fcoding()
        {
            DataTable dt = GetCodingByTypeId(13);
            dt.Columns["namecoding"].ColumnName = "title";
            return dt;
        }
        public DataTable GetNameUni_fcoding()
        {
            return GetCodingByTypeId(1);
        }
        public DataTable GetStatusMilitary_fcoding()
        {
            return GetCodingByTypeId(7);
        }
        public DataTable GetNameCountry_fcoding()
        {
            return GetCodingByTypeId(5);
        }


        public DataTable GetCodingByTypeId(int codingTypeId)
        {
            return cmdao.GetCodingByTypeId(codingTypeId);
        }

        /// <summary>
        /// تمام تاریخهای نوع ورودی برای تمام ترمها را دریافت میکند
        /// </summary>
        /// <param name="dateType"></param>
        /// <returns></returns>
        public DataTable getEducationCalender(int dateType)
        {
            return getEducationCalender("", dateType);
        }

        /// <summary>
        /// برای ترم ذکر شده تمام تاریخهای ثبت شده را می آورد
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public DataTable getEducationCalender(string term)
        {
            return getEducationCalender(term, 0);
        }

        /// <summary>
        /// تمام داده های جدول تقویم آموزشی را می آورد
        /// </summary>
        /// <returns></returns>
        public DataTable getEducationCalender()
        {
            return getEducationCalender("", 0);
        }

        /// <summary>
        /// برای ترم ذکر شده روز ذکر شده را می آورد
        /// </summary>
        /// <param name="term"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        public DataTable getEducationCalender(string term, int dateType)
        {
            return cmdao.getEducationCalender(term, dateType);
        }

        public DataTable getEducationCalender_currentTerm()
        {
            return getEducationCalender("this");
        }

        public DataTable getEducationCalender_currentTerm(int datetype)
        {
            return getEducationCalender("this", datetype);
        }

        public DataTable GetReshByDaneshkade(int Daneshkade)
        {
            return cmdao.GetReshByDaneshkade(Daneshkade);
        }
        #endregion

        #region Create
        public void InsertIntoUserLog(int UserID, string LogTime, int AppId, int eventId, string Description)
        {
            cmdao.InsertIntoUserLog(UserID, LogTime, AppId, eventId, GetIPAddress(), Description);
        }

        public void InsertIntoUserLogwithIP(int UserID, string LogTime, int AppId, int eventId, string ip, string Description)
        {
            cmdao.InsertIntoUserLog(UserID, LogTime, AppId, eventId, ip, Description);
        }
        public void InsertIntoUserLog(int UserID, string LogTime, int AppId, int eventId, string Description, int ModifyID)
        {
            cmdao.InsertIntoUserLog(UserID, LogTime, AppId, eventId, GetIPAddress(), Description, ModifyID);
        }
        public DataTable getDenyRequestInfo(string sDate, string eDate, int apps)
        {
            return cmdao.getDenyRequestInfo(sDate, eDate, apps);
        }

        public void InsertIntoUserLogwithIP(int UserID, string LogTime, int AppId, int eventId, string ip, string Description, int ModifyID)
        {
            cmdao.InsertIntoUserLog(UserID, LogTime, AppId, eventId, ip, Description, ModifyID);
        }
        public void InsertIntoStudentLog(string stcode, string LogTime, int AppId, int eventId, string Description)
        {
            cmdao.InsertIntoStudentLog(stcode, LogTime, AppId, eventId, GetIPAddress(), Description);
        }
        public void InsertIntoStudentLog(string stcode, string LogTime, int AppId, int eventId, string Description, int ModifyID)
        {

            cmdao.InsertIntoStudentLog(stcode, LogTime, AppId, eventId, GetIPAddress(), Description, ModifyID);
        }
        public int CheckDifenceCondition(string stcode)
        {
            return cmdao.CheckDifenceCondition(stcode);
        }
        // ------------------------------------
        public void InsertIntoDefenceInfo(DefenceInformation defenceInformation)
        {
            cmdao.InsertIntoDefenceInfo(defenceInformation);
        }


        public void UpdateIntoDefenceInfo(DefenceInformation defenceInformation)
        {
            cmdao.UpdateIntoDefenceInfo(defenceInformation);
        }
        //-------------------------------------

        public void LogStatusMessage(string code, string codeAsanak, string mobile, int statusmsg, string msgStatus, int id_msg)
        {
            Prof.InsertIntoLogMsgStatus(code, codeAsanak, mobile, statusmsg, msgStatus, id_msg);
        }
        #endregion

        #region Update


        /// <summary>
        /// Changes Made To Convert Passwords
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="Status"></param>
        public bool ChangeTeacherPassword(Int64 codeOstad, string newPassword)
        {
            if (cmdao.changeTeacherPassword(codeOstad, newPassword, EncryptPass(newPassword)))
                //if (ms.ctrl_elec(codeOstad.ToString(), newPassword, "2", "0", "97iauec1206") == "1")
                {
                    InsertIntoStudentLog(codeOstad.ToString(), DateTime.Now.ToShortTimeString(), 0, 44, "");
                    return true;
                }
            return false;
        }

        public bool ChangeStudentPassword(string stcode, string newPassword)
        {
            if (cmdao.changeStudentPassword(stcode, newPassword, EncryptPass(newPassword)))
            //if (ms.ctrl_elec(stcode, newPassword, "1", "0", "97iauec1206") == "1")
            {
                InsertIntoStudentLog(stcode, DateTime.Now.ToShortTimeString(), 0, 48, "");
                return true;
            }

            return false;
        }

        public bool insertProfessorPassword(string professorCode, string password)
        {
            return true;
            DAO.University.Faculty.CooperationRequestFaculty CRF = new DAO.University.Faculty.CooperationRequestFaculty();

            DataTable dt = CRF.CheckTeacherIsInWebUserOst(Convert.ToInt64(professorCode));
            if (dt.Rows.Count > 0)
                return true;
            return ms.ctrl_elec(professorCode, password, "2", "1", "97iauec1206") == "1";
        }

        public bool insertStudentPassword(string stcode, string password)
        {
            if (cmdao.insertStudentPassword(stcode, EncryptPass(password)))
                //if (ms.ctrl_elec(stcode, password, "1", "1", "97iauec1206") == "1")
                {
                    return true;
                }

            return false;
        }

        public void UpdateSystemAvailability(int AppID, bool Status)
        {
            cmdao.UpdateSystemAvailability(AppID, Status);
        }
        public void UpdateSystemAvailability(int AppID, int userKind, int userStatus, bool isOpen, string fromDate, string endDate, string startTime = "00:00", string endTime = "23:59")
        {
            cmdao.UpdateSystemAvailability(AppID, userKind, userStatus, isOpen, fromDate, endDate, startTime, endTime);
        }


        public void UpdateLogMessageStatus(string text, string msgStatus, string codeAsanak)
        {
            Prof.UpdateLogMessage(text, msgStatus, codeAsanak);
        }
        #endregion


        //public bool SendEmail(string Session["to"] , string Session["subject"],string Session["body"])
        //{
        //    throw new NotImplementedException();
        //}


        public string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            // Get the key from config file
            var key = ConfigurationManager.AppSettings["SecurityKey"];
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //Get your key from config file to open the lock!
            var key = ConfigurationManager.AppSettings["SecurityKey"];

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public void AddSignature(byte[] imageBytes, int identityNumber, int appId, int type)
        {
            cmdao.AddSignature(imageBytes, identityNumber, appId, type);
        }
        public List<SignautreDTO> GetSignature(int identityNumber, int appId, int type)
        {
            return cmdao.GetSignature(identityNumber, appId, type);
        }

        public List<GroupManger> GetGroupMangerInformation(Int64 professorId)
        {
            return cmdao.GetGroupMangerInformation(professorId);
        }
        public List<GroupManger> GetGroupMangerInformation(string professorUsrename)
        {
            return cmdao.GetGroupMangerInformation_ByUserName(professorUsrename);
        }

        public List<LoginDTO> GetProfessorUser(string userId)
        {
            var lngDto = new LoginDTO();
            var loginDao = new LoginDAO();
            var lngDtOlst = new List<LoginDTO>();
            var dt = loginDao.Get_UserLogin(userId);
            if (dt.Rows.Count > 0)
            {
                lngDto.UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
                lngDto.Name = dt.Rows[0]["Name"].ToString();
                lngDto.Password = dt.Rows[0]["Password"].ToString();
                lngDto.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"]);
                lngDto.Enable = Convert.ToBoolean(dt.Rows[0]["Enable"].ToString());
                lngDto.sectionId = Convert.ToInt32(dt.Rows[0]["SectionId"]);
                lngDto.UserName = dt.Rows[0]["UserName"].ToString();

            }
            else
            {
                //lngDTO.RoleId = 0;
                lngDto.UserId = 0;
                lngDto.Name = "";
                lngDto.Password = "";
                lngDto.Enable = false;
            }
            lngDtOlst.Add(lngDto);
            return lngDtOlst;
        }
    }
    public static class extentions
    {
        public static string changeArabicLetterToFarsi(this string text)
        {
            text = text.Replace('ي', 'ی');
            text = text.Replace('ك', 'ک');
            return text;
        }
        public static string formatDateString(this string date)
        {
            if (date.Length < 5 || !date.Contains("/"))
                return date;
            string year, month, day;
            year = date.Substring(0, date.IndexOf("/"));
            if (!year.StartsWith("13"))
                year = "13" + year;
            month = date.Substring(date.IndexOf("/") + 1, date.LastIndexOf("/") - date.IndexOf("/") - 1);
            day = date.Substring(date.LastIndexOf("/") + 1);
            string tempDate = string.Format("{0:0000}/{1:00}/{2:00}", Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
            return tempDate;
        }

        public static string improveFileName(this string fileName)
        {
            fileName = fileName.Replace("%", "");
            fileName = fileName.Replace("+", "");
            return fileName;
        }

        public static string changePersianNumberToLatinNumber(this string text)
        {
            foreach (char ch in text.ToCharArray())
            {
                if (DTO.CommonClasses.persianCharacters.persianDigits.ContainsKey(ch.ToString()))
                    text = text.Replace(ch.ToString(), DTO.CommonClasses.persianCharacters.persianDigits[ch.ToString()].ToString());
            }
            return text;
        }
    }
}
