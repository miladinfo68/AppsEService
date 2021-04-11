using IAUEC_Apps.DAO.Adobe;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web.Security;


namespace IAUEC_Apps.Business.Adobe
{
    public class ManagementPanelBusiness
    {
        ManagementPanelDAO MpDAO = new ManagementPanelDAO();
        

        #region Old

        public bool Insert_CourseClass(ManagementPanelDTO MpClass)
        {
            return MpDAO.Insert_CourseClass(MpClass);
        }

        public bool Insert_CourseUser(ManagementPanelDTO MpDTO)
        {
            return MpDAO.Insert_CourseUser(MpDTO);
        }


        public DataTable Get_CourseType()
        {
            return MpDAO.Get_CourseType();
        }


        public DataTable GetCourseTimeClass()
        {
            return MpDAO.GetCourseTimeClass();
        }


        public DataTable GetGetAdobe_Ability()
        {
            return MpDAO.GetGetAdobe_Ability();
        }

        #endregion



        #region Get

        /// <summary>
        /// یک عدد تصادفی ایجاد می کند، با طول درخواستی شما
        /// طول عدد اگر کوچکتر از 6 و یا بزرگتر 30 باشد، متن تصادفی با طول 10 کاراکتر ایجاد می شود
        /// </summary>
        /// <param name="LengthofString"></param>
        /// <returns></returns>
       

        public bool Check_CustomerName(string CustomerName)
        {
            return MpDAO.Check_CustomerName(CustomerName);
        }

        public DataTable GetDayTime()
        {
            return MpDAO.GetDayTime();
        }


        /// <summary>
        /// خواندن فایل اکسل به شیوه مورد نیاز برای آدابی
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public List<Customers_Users> ReadExcelFile(string Path)
        {                                    
            List<Customers_Users> CuList = new List<Customers_Users>();

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(Path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            for (int i = 1; i <= 1000; i++)
            {
                if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)                
                {              
                    Customers_Users CU = new Customers_Users();
                    CU.Customers_Users_Name = xlRange.Cells[i, 1].Value2.ToString();

                    if (xlRange.Cells[i, 2].Value2 != null)
                        CU.Customers_Users_Family = xlRange.Cells[i, 2].Value2.ToString();
                    else
                        CU.Customers_Users_Family = "";

                    if (xlRange.Cells[i, 3].Value2 != null)
                        CU.Customers_Users_LatinName = xlRange.Cells[i, 3].Value2.ToString();
                    else
                        CU.Customers_Users_LatinName = "";

                    if (xlRange.Cells[i, 4].Value2 != null)
                        CU.Customers_Users_LatinFamily = xlRange.Cells[i, 4].Value2.ToString();
                    else
                        CU.Customers_Users_LatinFamily = "";

                    if (xlRange.Cells[i, 5].Value2 != null)
                        CU.Customers_Users_UserMobile = xlRange.Cells[i, 5].Value2.ToString();
                    else
                        CU.Customers_Users_UserMobile = "";

                    if (xlRange.Cells[i, 6].Value2 != null)
                        CU.Customers_Users_Email = xlRange.Cells[i, 6].Value2.ToString();
                    else
                        CU.Customers_Users_Email = "";

                    if (xlRange.Cells[i, 7].Value2 != null)
                        CU.Customers_Users_UserName = xlRange.Cells[i, 7].Value2.ToString();
                    else
                        CU.Customers_Users_UserName = "";

                    if (xlRange.Cells[i, 8].Value2 != null)
                        CU.Customers_Users_NationalCode = xlRange.Cells[i, 8].Value2.ToString();
                    else
                        CU.Customers_Users_NationalCode = "";

                    if (xlRange.Cells[i, 9].Value2 != null)
                        CU.Customers_Users_Sex = int.Parse(xlRange.Cells[i, 9].Value2.ToString());
                    else
                        CU.Customers_Users_Sex = 0;

                    if (xlRange.Cells[i, 10].Value2 != null)
                        CU.Customers_Users_IdNumber = xlRange.Cells[i, 10].Value2.ToString();
                    else
                        CU.Customers_Users_IdNumber = "";
                    
                    CuList.Add(CU);
                }
                else
                    break;
            }

            xlWorkbook.Close();
            return CuList;
        }

        public DataTable Get_Customers_Users_ByCustomerId(int CustomerId)
        {
            return MpDAO.Get_Customers_Users_ByCustomerId(CustomerId);
        }

        public DataTable Get_Customers_Users_ByCustomerIdWithNationalCode(int CustomerId, string NationalCode)
        {
            return MpDAO.Get_Customers_Users_ByCustomerIdWithNationalCode(CustomerId, NationalCode);
        }
        
        public DataTable Get_Customers()
        {
            return MpDAO.Get_Customers();
        }
         
        public DataTable Get_Customers_ClassNameByName(string Name, int ScoId, int CustomerId)
        {
            return MpDAO.Get_Customers_ClassNameByName(Name,ScoId,CustomerId);
        }

        public DataTable Get_Professor_ByName_Family_NationalCode(string Name, string Family, string NationalCode)
        {
            return MpDAO.Get_Professor_ByName_Family_NationalCode(Name, Family, NationalCode);
        }

        public DataTable Get_Student_ByName_Family_NationalCode(string Name, string Family, string NationalCode)
        {
            return MpDAO.Get_Student_ByName_Family_NationalCode(Name, Family, NationalCode);
        }

        public DataTable Get_Customers_UserInfo_ThesisDefense(string UserId, int Type)
        {
            return MpDAO.Get_Customers_UserInfo_ThesisDefense(UserId, Type);
        }


        public DataTable Get_Customers_Users_ByNameAndFamily(string Name, string Family)
        {
            return MpDAO.Get_Customers_Users_ByNameAndFamily(Name, Family);
        }

        public DataTable Get_Customers_Meeting_ByClassId(int ClassId)
        {
            return MpDAO.Get_Customers_Meeting_ByClassId(ClassId);
        }

        public DataTable Get_Customers_Users_InCustomerClass_ByClassId(int ClassId)
        {
            return MpDAO.Get_Customers_Users_InCustomerClass_ByClassId(ClassId);
        }

        public DataTable Get_Customers_ClassDayTime_ByClassId(int ClassId)
        {
            return MpDAO.Get_Customers_ClassDayTime_ByClassId(ClassId);        
        }

        public DataTable Get_Customers_FullParam(long Id,string Name,string UserName,string UserPass
            ,string ScoName,string Tel,string Fax,string UserMobile)
        {
            return MpDAO.Get_Customers_FullParam(Id, Name, UserName, UserPass, ScoName, Tel, Fax, UserMobile);
        }


        public DataTable Get_Customers_ClassName_ById(int Id)
        {
            return MpDAO.Get_Customers_ClassName_ById(Id);
        }


        public DataTable Get_Customers_Users_InCustomerClass_ByClassId2(int ClassId)
        {
            return MpDAO.Get_Customers_Users_InCustomerClass_ByClassId2(ClassId);
        }



        #endregion



        #region insert
         
        public int Create_Customer(string CustomerName, string CustomerTel, string CustomerFax, string CustomerEmail
               , string CustomerAddress, string CustomerUser, string CustomerUserMobile)
        {
            return MpDAO.Create_Customer(CustomerName, CustomerTel, CustomerFax, CustomerEmail, CustomerAddress
                , CustomerUser, CustomerUserMobile);
        }

        public int Create_Customers_ClassName(MPanelDTO MClass)
        {
            return MpDAO.Create_Customers_ClassName(MClass);
        }

        public int Create_Customers_ClassDayTime(int ClassNameId, int DayTimeId)
        {
            return MpDAO.Create_Customers_ClassDayTime(ClassNameId, DayTimeId);
        }


        public int Create_Customers_Meeting(int ClassId)
        {
            return MpDAO.Create_Customers_Meeting(ClassId);
        }

        public int Create_Customers_Users(string Name, string Family, string LatinName, string LatinFamily
            , string UserMobile, string Email, string UserName, string NationalCode, int Sex, int CustomerId
            , string UserType, string UserId, string IdNumber, string UserPass)
        {
            return MpDAO.Create_Customers_Users(Name, Family, LatinName, LatinFamily, UserMobile, Email, UserName
                , NationalCode, Sex, CustomerId, UserType, UserId, IdNumber, UserPass);
        }



        public int Create_Customers_UserMeeting(int IdUser, int IdMeeting, string UserAccess)
        {
            return MpDAO.Create_Customers_UserMeeting(IdUser,IdMeeting,UserAccess);
        }

        public int Customers_ClassName_RejectReason(int ClassNameId, string Text)
        {
            return MpDAO.Customers_ClassName_RejectReason(ClassNameId, Text);
        }

        public DataTable Get_Customers_ClassName_RejectReason(int ClassNameId)
        {
            return MpDAO.Get_Customers_ClassName_RejectReason(ClassNameId);
        }



        #endregion


        #region Update

        public void Update_Customers_ClassName_Rejection(int ScoId,int Id )
        {
            MpDAO.Update_Customers_ClassName_Scoid_ById(ScoId, Id);
        }

        public void Update_Customers_ById(int Status, int Id, string UserAdobe, string UserPass
            , int ScoId, string ScoName)
        {
            MpDAO.Update_Customers_ById(Status, Id, UserAdobe, UserPass, ScoId, ScoName);
        }

        public void Update_Customers_UserMeeting_ById(long Id, int Active, long IdUser)
        {
            MpDAO.Update_Customers_UserMeeting_ById(Id, Active, IdUser);        
        }





        #endregion


        


        public void Update_Customers_ClassName_Accept(int Id)
        {
            //Master FolderId=11003
            // این پوشه بصورت استاندارد به برنامه اضافه شود Id درآینده 

            string MasterFolderId = "11003";
            string ScoId_Customer = "";
            string ScoId_ClassName = "";
            string ScoId_Meeting = "";

            string DomainAddress = "kadobe.iauec.ac.ir";
            string DomainLogin = "h@razavi.com";
            string DomainPassword = "P@ssw0rd";
            DateTime BeginDate = new DateTime(2015, 1, 1);
            DateTime EndDate = new DateTime(2020, 1, 1);
            Cookie DomainCookies = Adobe_Login(DomainAddress, DomainLogin, DomainPassword);
            string DomainCookiesValue = DomainCookies.Value;



            // دریافت اطلاعات کلاس
            DataTable DT_CInfo = MpDAO.Get_Customers_ClassName_ById(Id);



            // ==============   بررسی بودن پوشه اصلی مشتری در آدابی   =========================
            // چک کردن اینکه پوشه اصلی مشتری وجود دارد یا خیر؟
            DataTable DTAdobe_CheckCustomerFolder = MpDAO.Get_SP_Get_ScosByName("Customer" + DT_CInfo.Rows[0]["CustomerId"].ToString());
            if (DTAdobe_CheckCustomerFolder.Rows.Count == 0)
            {
                // ساختن پوشه مادر
                Adobe_Create_Folder(DomainAddress, "Customer" + DT_CInfo.Rows[0]["CustomerId"].ToString()
                , MasterFolderId
                , BeginDate, EndDate, "Customer" + DT_CInfo.Rows[0]["CustomerId"].ToString()
                , DomainCookiesValue);

                // دریافت اطلاعات پوشه کلاس ساخته شده در آداب
                DTAdobe_CheckCustomerFolder = MpDAO.Get_SP_Get_ScosByName("Customer" + DT_CInfo.Rows[0]["CustomerId"].ToString());
                ScoId_Customer = DTAdobe_CheckCustomerFolder.Rows[0]["SCO_ID"].ToString();

                //  در جدول مشتری ScoID and ScoName  بروز کردن 
                MpDAO.Update_Customers_ById(2,int.Parse(DT_CInfo.Rows[0]["CustomerId"].ToString())
                    ,"",""
                    , int.Parse(ScoId_Customer)
                    , "Customer" + DT_CInfo.Rows[0]["CustomerId"].ToString());
            }
            else
            {
                ScoId_Customer = DTAdobe_CheckCustomerFolder.Rows[0]["SCO_ID"].ToString();
            }
            // ==============   بررسی بودن پوشه اصلی مشتری در آدابی   =========================



            //===============     ساختن پوشه کلاس درس در آدابی    ==========================
            Adobe_Create_Folder(DomainAddress, "ClassName" + DT_CInfo.Rows[0]["Id"].ToString()
                , ScoId_Customer
                , BeginDate, EndDate, "ClassName" + DT_CInfo.Rows[0]["Id"].ToString()
                , DomainCookiesValue);

            DTAdobe_CheckCustomerFolder = MpDAO.Get_SP_Get_ScosByName("ClassName" + DT_CInfo.Rows[0]["Id"].ToString());
            ScoId_ClassName = DTAdobe_CheckCustomerFolder.Rows[0]["SCO_ID"].ToString();

            MpDAO.Update_Customers_ClassName_Scoid_ById(int.Parse(ScoId_ClassName)
                , int.Parse(DT_CInfo.Rows[0]["Id"].ToString()));

            //===============     ساختن پوشه کلاس درس در آدابی    ==========================


            //===============    Meeting  ساخت جلسات کلاس  ===================================
           
            DataTable DTCustomer_Meeting = MpDAO.Get_Customers_Meeting_ByClassId(int.Parse(DT_CInfo.Rows[0]["Id"].ToString()));

            string[] CustomerMeetingScoId = new string[DTCustomer_Meeting.Rows.Count]; 

            for (int i = 0; i < DTCustomer_Meeting.Rows.Count; i++)
            {
                // ساخت جلسه کلاس
                Adobe_Create_Meeting(DomainAddress, "Meeting" + DTCustomer_Meeting.Rows[i]["Id"].ToString(), ScoId_ClassName, BeginDate
                    , EndDate, "Meeting" + DTCustomer_Meeting.Rows[i]["Id"].ToString(), DomainCookiesValue);

                // ساخته شده Meeting از  ScoId گرفتن 
                DTAdobe_CheckCustomerFolder = MpDAO.Get_SP_Get_ScosByName("Meeting" + DTCustomer_Meeting.Rows[i]["Id"].ToString());
                ScoId_Meeting = DTAdobe_CheckCustomerFolder.Rows[0]["SCO_ID"].ToString();

                //  Customer_Meeting بروز رسانی در جدول
                MpDAO.Update_Customers_Meeting_ById(int.Parse(DTCustomer_Meeting.Rows[i]["Id"].ToString())
                    , int.Parse(ScoId_Meeting)
                    , "Meeting" + DTCustomer_Meeting.Rows[i]["Id"].ToString());

                // پرکردن برای اینکه بعدا بتوان به راحتی کاربران را در کلاس ها اضافه کرد
                CustomerMeetingScoId[i] = ScoId_Meeting;
            }

            //===============    Meeting  ساخت جلسات کلاس  ===================================


            //===============    Adobe ساخت کاربران در    ==================================

            DataTable DTUser = MpDAO.Get_Customers_Users_InCustomerClass_ByClassId(
                int.Parse(DT_CInfo.Rows[0]["Id"].ToString()));

            //  کاربران حاضر در کلاس id لیست 
            string[] CustomerPrincipalsId = new string[DTUser.Rows.Count]; 

            for (int i = 0; i < DTUser.Rows.Count; i++)
            {
                DataTable DTAdobeUser = MpDAO.Get_PRINCIPALS_ByLOGIN("user" + DTUser.Rows[i]["Id"].ToString());

                if (DTAdobeUser.Rows.Count==0)
                {
                    string NationalCodeLengnt=DTUser.Rows[i]["NationalCode"].ToString();                    
                    // برای یکی کردن برنامه، پسوردی که به صورت پیش فرض در نظر گرفته شده
                    // پسورد سیستمی به عنوان پسورد در آداب قرار می گیرد  
                    Adobe_Create_User(DomainAddress, DTUser.Rows[i]["Name"].ToString()
                        , DTUser.Rows[i]["Family"].ToString()
                        , "user" + DTUser.Rows[i]["Id"].ToString()
                        , DTUser.Rows[i]["UserPass"].ToString(), DomainCookiesValue);

                
                    //// کاربر وجود نداشته باشد، باید کاربر ایجاد شود
                    //// یا کدملی درست وارد نشده باشد، یعنی کمتر از 8 کاراکتر باشد
                    //if (DTUser.Rows[i]["NationalCode"].ToString() == "" || NationalCodeLengnt.Length<8)
                    //{
                    //    //  اگر کدملی کاربر موجود نباشد
                    //    //پسورد پیش فرض که بصورت تصادفی از قبل ایجاد گردیده است را در نظر می گیرد                         
                    //    Adobe_Create_User(DomainAddress, DTUser.Rows[i]["Name"].ToString()
                    //        , DTUser.Rows[i]["Family"].ToString()
                    //        , "user" + DTUser.Rows[i]["Id"].ToString()
                    //        , DTUser.Rows[i]["UserPass"].ToString(), DomainCookiesValue);                      
                    //}
                    //else
                    //{
                    //    Adobe_Create_User(DomainAddress, DTUser.Rows[i]["Name"].ToString()
                    //        , DTUser.Rows[i]["Family"].ToString()
                    //        , "user" + DTUser.Rows[i]["Id"].ToString()
                    //        , DTUser.Rows[i]["NationalCode"].ToString(), DomainCookiesValue);
                    //}
                               
                }

                DTAdobeUser = MpDAO.Get_PRINCIPALS_ByLOGIN("user" + DTUser.Rows[i]["Id"].ToString()); 
                CustomerPrincipalsId[i] = DTAdobeUser.Rows[0]["PRINCIPAL_ID"].ToString();
            }

            //===============    Adobe ساخت کاربران در    ==================================



            //===============    Meeting اضافه کردن کاربران در    ==================================
            

            //=====  در کلاس های موسسه Host دریافت اطلاعات کاربر ادمین موسسه برای ایجاد دسترسی 
            DataTable DTCustomerInfo = MpDAO.Get_Customers_FullParam(long.Parse(DT_CInfo.Rows[0]["CustomerId"].ToString())
                , "", "", "", "", "", "", "");
            DataTable DTAdobeCustomerUser = MpDAO.Get_PRINCIPALS_ByLOGIN(DTCustomerInfo.Rows[0]["UserAdobe"].ToString());
            string CustomerPrincipalId = DTAdobeCustomerUser.Rows[0]["PRINCIPAL_ID"].ToString();
                    
            
            for (int i = 0; i < DTCustomer_Meeting.Rows.Count; i++)
            {
                // جلسه
                for (int j = 0; j < CustomerPrincipalsId.Count(); j++)
                {
                    if(j==0)
                    {
                        // کاربر اصلی مشتری
                        // این کاربری است که باید به همه کلاس ها دسترسی هاست داشته باشد
                        Adobe_Insert_UserInMeeting(DomainAddress
                        , CustomerPrincipalId
                        , CustomerMeetingScoId[i]
                        , "host"
                        , DomainCookiesValue);
                    }


                    // کاربران
                    Adobe_Insert_UserInMeeting(DomainAddress
                    , CustomerPrincipalsId[j]
                    , CustomerMeetingScoId[i]
                    , DTUser.Rows[j]["UserAccess"].ToString()
                    , DomainCookiesValue);
                }

                // نحوه دسترسی به کلاس بروز رسانی می شود
                // و براساس آنچه کاربر خواسته تغییر می کند
                Adobe_Update_Meeting_Access(DomainAddress
                    , CustomerMeetingScoId[i]
                    , DomainCookiesValue
                    , DT_CInfo.Rows[0]["MeetingAccess"].ToString());
            }




             
            




        }




        // بعدا به قسمت مخصوص آداب برده شود
        public Cookie Adobe_Login()
        {
            string DomainAddress = "kadobe.iauec.ac.ir";
            string DomainLogin = "h@razavi.com";
            string DomainPassword = "P@ssw0rd";
            Cookie DomainCookies = Adobe_Login(DomainAddress, DomainLogin, DomainPassword);
            return DomainCookies;
        }

        public Cookie Adobe_Login(string DomainAddress, string DomainLogin, string DomainPassword)
        {
            string serviceUrl = "http://" + DomainAddress + "/api/xml?action=login"
                + "&login=" + DomainLogin + "&password=" + DomainPassword;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Cookie Cookies = new System.Net.Cookie();
            foreach (Cookie cook in response.Cookies)
            {
                Cookies.Value = cook.Value;
                Cookies.Name = cook.Name;
                Cookies.Domain = cook.Domain;
                Cookies.Path = cook.Path;
                Cookies.Port = cook.Port;
                Cookies.Secure = cook.Secure;
                //Cookies.TimeStamp = cook.TimeStamp;
                Cookies.Expires = cook.Expires;
                Cookies.Expired = cook.Expired;
                Cookies.Discard = cook.Discard;
                Cookies.Comment = cook.Comment;
                Cookies.CommentUri = cook.CommentUri;
                Cookies.Version = cook.Version;
            }

            return Cookies;
        }

        // ایجاد پوشه
        public bool Adobe_Create_Folder(string DomainAddress, string FolderName, string FolderFolderId, DateTime FolderDataBegin
            , DateTime FolderDataEnd, string FolderUrlPath, string DomainCookies_Value)
        {
            bool Resualt = false;
            string serviceUrl = "http://" + DomainAddress + "/api/xml?action=sco-update&type=folder"
                + "&name=" + FolderName
                + "&folder-id=" + FolderFolderId
                + "&date-begin=" + FolderDataBegin.Year.ToString()
                    + "-" + FolderDataBegin.Month.ToString()
                    + "-" + FolderDataBegin.Day.ToString()
                + "&date-end=" + FolderDataEnd.Year.ToString()
                    + "-" + FolderDataEnd.Month.ToString()
                    + "-" + FolderDataEnd.Day.ToString()
                + "&url-path=" + FolderUrlPath
                + "&session=" + DomainCookies_Value;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            XmlReader reader = XmlReader.Create(response.GetResponseStream());
            XmlNodeType z;

            while (reader.Read())
            {
                reader.MoveToFirstAttribute();
                z = reader.NodeType;
                if (reader.Value == "ok")
                    Resualt = true;
            }

            return Resualt;
        }

        // meeting ایجاد 
        public bool Adobe_Create_Meeting(string DomainAddress, string MeetingName, string MeetingFolderId, DateTime MeetingDataBegin
            , DateTime MeetingDataEnd, string MeetingUrlPath, string DomainCookies_Value)
        {
            bool Resualt = false;
            string serviceUrl = "http://" + DomainAddress + "/api/xml?action=sco-update&type=meeting"
                + "&name=" + MeetingName
                + "&folder-id=" + MeetingFolderId
                + "&date-begin=" + MeetingDataBegin.Year.ToString()
                    + "-" + MeetingDataBegin.Month.ToString()
                    + "-" + MeetingDataBegin.Day.ToString()
                + "&date-end=" + MeetingDataEnd.Year.ToString()
                    + "-" + MeetingDataEnd.Month.ToString()
                    + "-" + MeetingDataEnd.Day.ToString()
                + "&url-path=" + MeetingUrlPath
                + "&session=" + DomainCookies_Value;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            XmlReader reader = XmlReader.Create(response.GetResponseStream());
            XmlNodeType z;

            while (reader.Read())
            {
                reader.MoveToFirstAttribute();
                z = reader.NodeType;
                if (reader.Value == "ok")
                    Resualt = true;
            }

            return Resualt;
        }

        // ساخت کاربر
        public bool Adobe_Create_User(string DomainAddress, string UserFirstName, string UserLastName, string UserLogin
            , string UserPassWord, string DomainCookies_Value)
        {
            string Text = "";
            string serviceUrl = "http://" + DomainAddress + "/api/xml?action=principal-update"
                + "&first-name=" + UserFirstName
                + "&last-name=" + UserLastName
                + "&login=" + UserLogin
                + "&password=" + UserPassWord
                + "&type=user&send-email=true&has-children=0"
                + "&email=" + UserLogin + "@" + DomainAddress
                + "&session=" + DomainCookies_Value;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            XmlReader reader = XmlReader.Create(response.GetResponseStream());
            XmlNodeType z;
            while (reader.Read())
            {
                reader.MoveToFirstAttribute();
                z = reader.NodeType;
                if (z == XmlNodeType.Attribute && (reader.Value == "ok" || reader.Value == "invalid"))
                    Text = reader.Value;
            }

            if (Text == "ok")
                return true;
            else
                return false;
        }


        // اضافه کردن یک کاربر به درون یک کلاس
        public bool Adobe_Insert_UserInMeeting(string DomainAddress, string PrincipalID, string ScoId
            , string MeetingPermissionType, string DomainCookies_Value)
        {
            //host = host
            //mini-host = Presenter
            //view = Particepent

            string Permission = "";
            switch (MeetingPermissionType)
            {
                case "host":
                    Permission = "host";
                    break;
                case "Presenter":
                    Permission = "mini-host";
                    break;
                default:
                    Permission = "view";
                    break;
            }

            string serviceUrl = "http://" + DomainAddress + "/api/xml?action=permissions-update"
                + "&principal-id=" + PrincipalID
                + "&acl-id=" + ScoId
                + "&permission-id=" + Permission
                + "&session=" + DomainCookies_Value;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            XmlReader reader = XmlReader.Create(response.GetResponseStream());
            XmlNodeType z;

            while (reader.Read())
            {
                reader.MoveToFirstAttribute();
                z = reader.NodeType;
                if (z == XmlNodeType.Attribute || z == XmlNodeType.Text)
                    if (reader.Value == "ok")
                        return true;
            }

            return false;
        }



        // دسترسی پیدا کنند Meeting چه کسانی بتوانند به 
        public bool Adobe_Update_Meeting_Access(string DomainAddress, string ScoId, string DomainCookies_Value
            , string PermissionId)
        {
            //view-hidden =  همه بتوانند وارد کلاس شوند
            //remove      =  کاربران مهمان با اجازه هاست میتوانند وارد کلاس شوند
            //denied      =  فقط اعضا میتوانند وارد کلاس شوند، کاربران سیستم با اجازه استاد میتوانند وارد شوند

            string serviceUrl = "http://" + DomainAddress + "/api/xml?action=permissions-update"
              + "&acl-id=" + ScoId
              + "&principal-id=public-access"
              + "&permission-id=" + PermissionId
              + "&session=" + DomainCookies_Value;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            XmlReader reader = XmlReader.Create(response.GetResponseStream());
            XmlNodeType z;

            while (reader.Read())
            {
                reader.MoveToFirstAttribute();
                z = reader.NodeType;
                if (reader.Value == "ok")
                    return true;
            }

            return false;
        }


        // this remove user from classlist of user 
        public bool Adobe_Remove_UserOfMeeting(string DomainAddress, string PrincipalID, string DomainCookies_Value
            , string ScoId)
        {           
            string serviceUrl = "http://" + DomainAddress + "/api/xml?action=permissions-update"
                + "&acl-id=" + ScoId
                + "&principal-id=" + PrincipalID
                + "&permission-id=remove"
                + "&session=" + DomainCookies_Value;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            XmlReader reader = XmlReader.Create(response.GetResponseStream());
            XmlNodeType z;

            while (reader.Read())
            {
                reader.MoveToFirstAttribute();
                z = reader.NodeType;
                if (z == XmlNodeType.Attribute || z == XmlNodeType.Text)
                {
                    if (reader.Value == "ok")
                        return true;
                }
            }

            return false;
        }



        public DataTable Adobe_Get_PRINCIPALS_ByLOGIN(string Login)
        {
            return MpDAO.Get_PRINCIPALS_ByLOGIN(Login);
        }

        public DataTable Adobe_Get_SP_Get_ScosByName(string Name)
        {
            return MpDAO.Get_SP_Get_ScosByName(Name);
        }
        
        



    }
}
