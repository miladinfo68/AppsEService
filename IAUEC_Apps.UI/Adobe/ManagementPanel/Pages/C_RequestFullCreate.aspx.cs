using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Globalization;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.Pages
{
    public partial class C_RequestFullCreate : System.Web.UI.Page
    {
        ManagementPanelBusiness MPB = new ManagementPanelBusiness();
        TABusiness TABusiness = new TABusiness();
        CommonBusiness cmnB = new CommonBusiness();

        // CustomerId نیاز به اصلاح دارد
        //  مشتری باشد  id عدد 3 حتما باید بروز رسانی شود و باتوجه به
        public int CustomerId = 3;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                for (int i = 1; i < 51; i++)
                    ddl_ClassCount.Items.Add(i.ToString());

                
                // برای جلسات درسی
                DataTable DTDayTime = MPB.GetDayTime();
                for (int i = 0; i < DTDayTime.Rows.Count; i++)
                {
                    RadComboBoxItem cmbitem = new RadComboBoxItem();
                    cmbitem.Text = DTDayTime.Rows[i]["BEGIN_HOUR"].ToString() + "الی" + DTDayTime.Rows[i]["END_HOUR"].ToString() 
                        + " - " + DTDayTime.Rows[i]["DayName"].ToString();
                    cmbitem.Value = DTDayTime.Rows[i]["Id"].ToString();
                    cmbitem.Font.Name = "Tahoma";
                    cmbitem.Font.Size = 8;
                    ddl_ClassDayTime.Items.Add(cmbitem);
                
                }

                // برای جلسه دفاع
                for (int i = 0; i < DTDayTime.Rows.Count; i++)
                {
                    RadComboBoxItem cmbitem = new RadComboBoxItem();
                    cmbitem.Text = DTDayTime.Rows[i]["BEGIN_HOUR"].ToString() + "الی" + DTDayTime.Rows[i]["END_HOUR"].ToString()
                        + " - " + DTDayTime.Rows[i]["DayName"].ToString();
                    cmbitem.Value = DTDayTime.Rows[i]["Id"].ToString();
                    cmbitem.Font.Name = "Tahoma";
                    cmbitem.Font.Size = 8;
               
                    ddl_ClassDayTime2.Items.Add(cmbitem);
                }





                //date_input_1.Value.ToString(), date_input_2.Value.ToString()

                // مجوز حضور مهمان در کلاس
                ddl_UserType.Items.Add("بله");
                ddl_UserType.Items.Add("خیر");

                // نوع کلاس
                ddl_ClassType.Items.Add("جلسه دفاع");
                ddl_ClassType.Items.Add("کلاس درس");
                //ddl_ClassType.Items.Add("کلاس درس");
                //ddl_ClassType.Items.Add("سمینار");




                //======================================================================
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorGuide") != null)
                    lbl_ProfessorGuide.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorGuide");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorAdvisor") != null)
                    lbl_ProfessorAdvisor.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorAdvisor");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorRefereeOne") != null)
                    lbl_ProfessorRefereeOne.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorRefereeOne");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorRefereeTwo") != null)
                    lbl_ProfessorRefereeTwo.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ProfessorRefereeTwo");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("Student") != null)
                    lbl_Student.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("Student");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("Type") != null)
                    lbl_Type.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("Type");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("UserId") != null)
                    lbl_UserId.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("UserId");

                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassName") != null)
                    txt_ClassName.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassName");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("LatinClassName") != null)
                    txt_ClassNameLatin.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("LatinClassName");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("CountOfClass") != null)
                    ddl_ClassCount.SelectedIndex= int.Parse(HttpUtility.ParseQueryString(this.ClientQueryString).Get("CountOfClass"));
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("CountOfUser") != null)
                    txt_UserCount.Value = HttpUtility.ParseQueryString(this.ClientQueryString).Get("CountOfUser");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateStart") != null)
                    date_input_1.Value = HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateStart");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateEnd") != null)
                    date_input_2.Value = HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateEnd");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateDef") != null)
                    date_input_3.Value = HttpUtility.ParseQueryString(this.ClientQueryString).Get("DateDef");
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassType") != null)
                    ddl_ClassType.SelectedIndex =int.Parse( HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassType"));
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("UserType") != null)
                    ddl_UserType.SelectedIndex =int.Parse(HttpUtility.ParseQueryString(this.ClientQueryString).Get("UserType"));
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClasDayTime") != null)
                {
                    string DayTime = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClasDayTime");
                    string[] str = DayTime.Split(',');
                    for (int i = 0; i < str.Count()-1; i++)
			        {
                        ddl_ClassDayTime.Items.ElementAt(int.Parse(str[i].ToString())).Checked = true;
			        }
                    
                 
                }

                string x = lbl_ProfessorRefereeOne.Text;
                x=x.Replace(" ", "");
                lbl_ProfessorRefereeOne.Text = x;

                if(ddl_ClassType.SelectedValue=="جلسه دفاع")
                {
                    EnterUserId(lbl_Type.Text);
                    DataEntery();
                }
                    

                
                

            }

        }



        protected void btn_CreateRequest_Click(object sender, EventArgs e)
        {
            if (txt_ClassName.Text == "" || txt_ClassNameLatin.Text == "" || !CommonBusiness.IsEnglishLetter(txt_ClassNameLatin.Text))
                RadWindowManager1.RadAlert("لطفا نام کلاس و نام لاتین کلاس را درست وارد نمایید", 500, 200, "پیام", "");           
            else if (ddl_ClassType.SelectedValue == "جلسه دفاع")
            {
                if (date_input_3.Value.ToString() == "")
                    RadWindowManager1.RadAlert("تاریخ جلسه دفاع را مشخص کنید", 500, 200, "پیام", "");
                else if(!ConvertPersianDateToGregorian(date_input_3.Value.ToString()))
                    RadWindowManager1.RadAlert(" امکان سرویس دهی پس از 7 روز آینده می باشد", 500, 200, "پیام", "");
                else if (!Check_ThesisDefense_User())
                    RadWindowManager1.RadAlert("ثبت دانشجور، استاد راهنما و داور1 الزامی می باشد، و کاربران نمی توانند تکرار شوند", 500, 200, "پیام", "");
                else if (lbl_ProfessorRefereeTwo.Text != "" && lbl_ProfessorRefereeOne.Text == "")
                    RadWindowManager1.RadAlert("ابتدا داور یک را مشخص نمایید", 500, 200, "پیام", "");
                else
                    GetParameterFromPage_ThesisDefense();
            }
            else if (ddl_ClassDayTime.CheckedItems.Count == 0)
                RadWindowManager1.RadAlert("روز و ساعت کلاس را مشخص کنید", 500, 200, "پیام", "");
            else
            {
                string ProfFileName = Path.GetFileName(fi_ProfUsers.PostedFile.FileName);
                string StudentFileName = Path.GetFileName(fi_StudentUser.PostedFile.FileName);

                if (ddl_ClassDayTime.CheckedItems.Count > int.Parse(ddl_ClassCount.SelectedValue))
                    RadWindowManager1.RadAlert("تعداد جلسه انتخابی نمی تواند کمتر از تعداد روز برگزاری کلاس باشد", 500, 200, "پیام", "");
                else if(!ConvertPersianDateToGregorian(date_input_1.Value.ToString()))
                    RadWindowManager1.RadAlert(" امکان سرویس دهی پس از 7 روز آینده می باشد", 500, 200, "پیام", "");
                else if (!CheckDate())
                    RadWindowManager1.RadAlert("تاریخ شروع نمی تواند کوچکتر از تاریخ اتمام باشد...!!!", 500, 200, "پیام", "");
                else if (date_input_1.Value.ToString() == "" || date_input_2.Value.ToString() == "")
                    RadWindowManager1.RadAlert("تاریخ ها را با دقت پر نمایید", 500, 200, "پیام", "");
                else if (fi_ProfUsers.PostedFile.FileName.ToString() == "" || fi_StudentUser.PostedFile.FileName.ToString() == "")
                    RadWindowManager1.RadAlert("فایل اکسل کاربران را آپلود کنید", 500, 200, "پیام", "");
                else if (!ProfFileName.Contains(".xlsx") || !StudentFileName.Contains(".xlsx"))
                    RadWindowManager1.RadAlert("فایل ها باید فرمت اکسلی داشته باشند", 500, 200, "پیام", "");
                else if (fi_ProfUsers.PostedFile.FileName.ToString() == fi_StudentUser.PostedFile.FileName.ToString())
                    RadWindowManager1.RadAlert("نام فایل های اکسل نمی تواند یکسان می باشد", 500, 200, "پیام", "");
                else
                {
                    DataTable DTCustomers_Users_FromDB = MPB.Get_Customers_Users_ByCustomerId(CustomerId);
                    List<Customers_Users> CUList_Prof = CheckExcelFile_Prof(DTCustomers_Users_FromDB);
                    List<Customers_Users> CUList_Student = CheckExcelFile_Student(DTCustomers_Users_FromDB);

                    if (CUList_Prof.Count == 0 || CUList_Student.Count == 0)
                        RadWindowManager1.RadAlert("لطفا اطلاعات فایل های اکسل ارسالی را بررسی کرده و مجددا تلاش کنید", 500, 200, "پیام", "");
                    //else if(CUList_Prof.Count+CUList_Student.Count>int.Parse(ddl_UserCount.SelectedValue))
                    else if (CUList_Prof.Count + CUList_Student.Count > int.Parse(txt_UserCount.Value))
                        RadWindowManager1.RadAlert("تعداد کاربران ارسالی بیشتر از تعداد کاربر درخواستی می باشد...!!", 500, 200, "پیام", "");
                    else
                        GetParameterFromPage(CUList_Prof, CUList_Student);
                }
            }
            
          
             
        }
        
  

        // ============== پروژه حتما چک شود
        public void GetParameterFromPage(List<Customers_Users> CUList_Prof, List<Customers_Users> CUList_Student)
        {
            MPanelDTO MClass = new MPanelDTO();
            List<Customers_ClassDayTime> List_Customers_ClassDayTime = new List<Customers_ClassDayTime>();
            List<Customers_Meeting> List_Customers_Meeting = new List<Customers_Meeting>();
            DataTable DTProfUser = new DataTable();
            DataTable DTStudentUser = new DataTable();


            // Customers_ClassName اضافه کردن در جدول
            MClass.Customers_ClassName_Name = txt_ClassName.Text;
            MClass.Customers_ClassName_CustomerId = CustomerId; //*
            MClass.Customers_ClassName_NameLatin = txt_ClassNameLatin.Text;
            MClass.Customers_ClassName_SessionCount = int.Parse(ddl_ClassCount.SelectedValue);
            //MClass.Customers_ClassName_UserCount = int.Parse(ddl_UserCount.SelectedValue);
            MClass.Customers_ClassName_UserCount = int.Parse(txt_UserCount.Value);
            MClass.Customers_ClassName_DateStart = date_input_1.Value.ToString();
            MClass.Customers_ClassName_DateEnd = date_input_2.Value.ToString();
            MClass.Customers_ClassName_ScoId = 0;
            MClass.Customers_ClassName_ServerName = "";
            if (ddl_UserType.SelectedValue == "بله")
                MClass.Customers_ClassName_MeetingAccess = "view-hidden";
            else
                MClass.Customers_ClassName_MeetingAccess = "denied";
            MClass.Customers_ClassName_Id = MPB.Create_Customers_ClassName(MClass); 
            // END
           
            //Customers_ClassDayTime اضافه کردن در جدول           
            //   های آن کلاس را بر می گرداند، جهت پرکردن جدول بعدی Id سپس یک لیست از 
            for (int Counter = 0; Counter < ddl_ClassDayTime.CheckedItems.Count; Counter++)
            {
                Customers_ClassDayTime CDT = new Customers_ClassDayTime();
                CDT.Customers_ClassDayTime_ClassNameId = MClass.Customers_ClassName_Id;
                CDT.Customers_ClassDayTime_DayTimeId = int.Parse(ddl_ClassDayTime.CheckedItems[Counter].Value);
                CDT.Customers_ClassDayTime_Id = MPB.Create_Customers_ClassDayTime(CDT.Customers_ClassDayTime_ClassNameId
                    , CDT.Customers_ClassDayTime_DayTimeId);

                List_Customers_ClassDayTime.Add(CDT);
            }
            //END


            //  که براساس تعداد جلسات درخواستی ساخته می شود Customers_Meeting پرکردن جدول            
            for (int i = 0; i < int.Parse(ddl_ClassCount.SelectedValue); i++)
            {
                Customers_Meeting CM = new Customers_Meeting();
                CM.Customers_Meeting_ClassId = MClass.Customers_ClassName_Id;
                CM.Customers_Meeting_Id = MPB.Create_Customers_Meeting(MClass.Customers_ClassName_Id);
                List_Customers_Meeting.Add(CM);
            }

            
            //// خواندن اطلاعات کاربران با دسترسی استاد           
            for (int i = 0; i < CUList_Prof.Count; i++)
            {
                DataTable DT5=MPB.Get_Customers_Users_ByCustomerIdWithNationalCode
                    (CUList_Prof.ElementAt(i).Customers_Users_CustomerId
                        , CUList_Prof.ElementAt(i).Customers_Users_NationalCode);
                if(DT5.Rows.Count>0)
                {
                //if(CUList_Prof.ElementAt(i).Customers_Users_IsDuplicate)// DB کاربران موجود در 
                //{
                    CUList_Prof.ElementAt(i).Customers_Users_Id = int.Parse(DT5.Rows[0]["Id"].ToString()); 
                }
                else
                {
                    CUList_Prof.ElementAt(i).Customers_Users_Id = MPB.Create_Customers_Users
                        (CUList_Prof.ElementAt(i).Customers_Users_Name
                        , CUList_Prof.ElementAt(i).Customers_Users_Family
                        , CUList_Prof.ElementAt(i).Customers_Users_LatinName
                        , CUList_Prof.ElementAt(i).Customers_Users_LatinFamily
                        , CUList_Prof.ElementAt(i).Customers_Users_UserMobile
                        , CUList_Prof.ElementAt(i).Customers_Users_Email
                        , CUList_Prof.ElementAt(i).Customers_Users_UserName
                        , CUList_Prof.ElementAt(i).Customers_Users_NationalCode
                        , CUList_Prof.ElementAt(i).Customers_Users_Sex
                        , CUList_Prof.ElementAt(i).Customers_Users_CustomerId
                        ,"",""
                        , CUList_Student.ElementAt(i).Customers_Users_IdNumber
                        , CommonBusiness.RandomString(10,true));
                }                
            }

        

            // خواندن اطلاعات کاربران با دسترسی دانشجو         
            for (int i = 0; i < CUList_Student.Count; i++)
            {
                DataTable DT6 = MPB.Get_Customers_Users_ByCustomerIdWithNationalCode
                    (CUList_Student.ElementAt(i).Customers_Users_CustomerId
                        , CUList_Student.ElementAt(i).Customers_Users_NationalCode);
                if (DT6.Rows.Count > 0)
                {                    
                    CUList_Student.ElementAt(i).Customers_Users_Id = int.Parse(DT6.Rows[0]["Id"].ToString());

                    //if (CUList_Student.ElementAt(i).Customers_Users_IsDuplicate)// DB کاربران موجود در 
                    //{
                    //    CUList_Student.ElementAt(i).Customers_Users_Id = int.Parse
                    //        (MPB.Get_Customers_Users_ByCustomerIdWithNationalCode
                    //        ( CUList_Student.ElementAt(i).Customers_Users_CustomerId
                    //        , CUList_Student.ElementAt(i).Customers_Users_NationalCode).Rows[0]["Id"].ToString());               
                    //}
                }               
                else
                {
                    CUList_Student.ElementAt(i).Customers_Users_Id = MPB.Create_Customers_Users
                        (CUList_Student.ElementAt(i).Customers_Users_Name
                        , CUList_Student.ElementAt(i).Customers_Users_Family
                        , CUList_Student.ElementAt(i).Customers_Users_LatinName
                        , CUList_Student.ElementAt(i).Customers_Users_LatinFamily
                        , CUList_Student.ElementAt(i).Customers_Users_UserMobile
                        , CUList_Student.ElementAt(i).Customers_Users_Email
                        , CUList_Student.ElementAt(i).Customers_Users_UserName
                        , CUList_Student.ElementAt(i).Customers_Users_NationalCode
                        , CUList_Student.ElementAt(i).Customers_Users_Sex
                        , CUList_Student.ElementAt(i).Customers_Users_CustomerId
                        ,"",""
                        , CUList_Student.ElementAt(i).Customers_Users_IdNumber
                        , CommonBusiness.RandomString(10, true));
                }                
            }

                        
            //    Customers_UserMeeting پرکردن جدول 
            int Counter11 = 0;
            for (int i = 0; i < int.Parse(ddl_ClassCount.SelectedValue); i++)
            {
                for (int j = 0; j < CUList_Prof.Count; j++)
                {
                    Counter11 = Counter11 + MPB.Create_Customers_UserMeeting
                        (CUList_Prof.ElementAt(j).Customers_Users_Id
                        , List_Customers_Meeting.ElementAt(i).Customers_Meeting_Id
                        , "host");
                }

                for (int j = 0; j < CUList_Student.Count; j++)
                {
                    Counter11 = Counter11 + MPB.Create_Customers_UserMeeting
                        (CUList_Student.ElementAt(j).Customers_Users_Id
                        , List_Customers_Meeting.ElementAt(i).Customers_Meeting_Id
                        , "view");
                }

            }

            
            // آماده کردن کاربران تکراری جهت نمایش
            DataTable DTShowDuplicate = new DataTable();
            DTShowDuplicate.Columns.Add("Name", typeof(string));
            DTShowDuplicate.Columns.Add("Family", typeof(string));
            DTShowDuplicate.Columns.Add("NationalCode", typeof(string));
            DTShowDuplicate.Columns.Add("Access", typeof(string));

            for (int i = 0; i < CUList_Prof.Count; i++)
            {
                if (CUList_Prof.ElementAt(i).Customers_Users_IsDuplicate)
                {
                    DataRow row = DTShowDuplicate.NewRow();
                    row["Name"] = CUList_Prof.ElementAt(i).Customers_Users_Name;
                    row["Family"] = CUList_Prof.ElementAt(i).Customers_Users_Family;
                    row["NationalCode"] = CUList_Prof.ElementAt(i).Customers_Users_NationalCode;
                    row["Access"] = "استاد";
                    DTShowDuplicate.Rows.Add(row);
                }                
            }

            for (int i = 0; i < CUList_Student.Count; i++)
            {
                if (CUList_Student.ElementAt(i).Customers_Users_IsDuplicate)
                {                    
                    DataRow row = DTShowDuplicate.NewRow();
                    row["Name"] = CUList_Student.ElementAt(i).Customers_Users_Name;
                    row["Family"] = CUList_Student.ElementAt(i).Customers_Users_Family;
                    row["NationalCode"] = CUList_Student.ElementAt(i).Customers_Users_NationalCode;
                    row["Access"] = "دانشجو";
                    DTShowDuplicate.Rows.Add(row);
                }
            }

            RadGrid1.DataSource = DTShowDuplicate;
            RadGrid1.DataBind();





            ////=======  OK =======



            int z = 0;

        }



        /// <summary>
        /// دریافت فایل اکسل اطلاعات اساتید
        /// </summary>
        public List<Customers_Users> ReadExcelFile_Prof()
        {
            //DataTable ExcelUsers = new DataTable();
            List<Customers_Users> CUList = new List<Customers_Users>();


            if (fi_ProfUsers.PostedFile != null)
            {       
                // Give File
                // دریافت اکسل فایل از کاربر
                string serverFileName = Path.GetFileName(fi_ProfUsers.PostedFile.FileName);

                string FilePath = HttpContext.Current.Server.MapPath("~");
                //Delete File              
                File.Delete(FilePath + serverFileName);

                //Save File
                // ذخیره فایل روی سرور
                //string FilePath = HttpContext.Current.Server.MapPath("~") + "UploadFiles\\";                
                fi_ProfUsers.PostedFile.SaveAs(MapPath("~") + serverFileName);

                //Read Excel File
                // DataTable خواندن محتوا و ریختن روی 
                CUList = MPB.ReadExcelFile(FilePath + serverFileName);

                //Delete File
                // حذف فایل اکسل از سرور
                File.Delete(FilePath + serverFileName);

                return CUList;

             
               
            }
            else
                return CUList;
        }


        /// <summary>
        /// دریافت فایل اکسل اطلاعات دانشجویان
        /// </summary>
        public List<Customers_Users> ReadExcelFile_Student()
        {
            //DataTable ExcelUsers = new DataTable();
            List<Customers_Users> CUList = new List<Customers_Users>();

            if (fi_StudentUser.PostedFile != null)
            {
                // Give File
                // دریافت اکسل فایل از کاربر
                string serverFileName = Path.GetFileName(fi_StudentUser.PostedFile.FileName);

                string FilePath = HttpContext.Current.Server.MapPath("~");
                //Delete File              
                File.Delete(FilePath + serverFileName);

                //Save File
                // ذخیره فایل روی سرور
                //string FilePath = HttpContext.Current.Server.MapPath("~") + "UploadFiles\\";                
                fi_StudentUser.PostedFile.SaveAs(MapPath("~") + serverFileName);

                //Read Excel File
                // DataTable خواندن محتوا و ریختن روی 
                CUList = MPB.ReadExcelFile(FilePath + serverFileName);

                //Delete File
                // حذف فایل اکسل از سرور
                File.Delete(FilePath + serverFileName);


                return CUList;               
            }
            else
                return CUList;


        }


        /// <summary>
        /// چک کردن تاریخ های شمسی باهم
        /// +
        /// چک کردن طول تاریخ
        /// 1394/01/01 = 10 کاراکتر
        /// </summary>
        /// <returns></returns>
        public bool CheckDate()
        {
            string PersianDate1 = date_input_1.Value.ToString();
            string PersianDate2 = date_input_2.Value.ToString();
            int Year1 = int.Parse(PersianDate1.Substring(0, PersianDate1.IndexOf('/')));
            int Year2 = int.Parse(PersianDate2.Substring(0, PersianDate2.IndexOf('/')));
            int Month1 = int.Parse(PersianDate1.Substring(PersianDate1.IndexOf('/') + 1, 2));
            int Month2 = int.Parse(PersianDate2.Substring(PersianDate2.IndexOf('/') + 1, 2));
            int Day1 = int.Parse(PersianDate1.Substring(PersianDate1.LastIndexOf('/') + 1, 2));
            int Day2 = int.Parse(PersianDate2.Substring(PersianDate2.LastIndexOf('/') + 1, 2));

            if (PersianDate1.Count() != 10 || PersianDate2.Count() != 10)
                return false;
            else if (Year1 > Year2)
                return false;
            else if (Year1 == Year2 && Month1 > Month2)
                return false;
            else if (Month1 == Month2 && Day1 > Day2)
                return false;             
            else
                return true;
        }
        
        public List<Customers_Users> CheckExcelFile_Prof(DataTable DTCustomers_Users_FromDB)
        {
            List<Customers_Users> CUList_Prof = new List<Customers_Users>();
            CUList_Prof = ReadExcelFile_Prof();

            //=============================================================================================
            //    این خط باید اصلاح گردد
            // بروز رسانی کاربران که مربوط به کدام موسسه هستند
            for (int i = 0; i < CUList_Prof.Count; i++)
                CUList_Prof.ElementAt(i).Customers_Users_CustomerId = CustomerId;
            
            // چک موبایل
            for (int i = 0; i < CUList_Prof.Count; i++)
            {
                if(!CommonBusiness.ValidateMobile(CUList_Prof.ElementAt(i).Customers_Users_UserMobile)
                    || !CommonBusiness.IsEnglishLetter(CUList_Prof.ElementAt(i).Customers_Users_UserName))
                {
                    List<Customers_Users> CUList_Prof1 = new List<Customers_Users>();
                    return CUList_Prof1;
                }                
            }
               

            // چک کردن داده های تکراری
            //  می شود Customers_Users_IsDuplicate=True اگر تکراری باشد، مقدار  
            for (int i = 0; i < CUList_Prof.Count; i++)
            {
                CUList_Prof.ElementAt(i).Customers_Users_IsDuplicate = false;
                for (int j = 0; j < DTCustomers_Users_FromDB.Rows.Count; j++)
                {
                    // کاربر کدملی دارد
                    if (CUList_Prof.ElementAt(i).Customers_Users_NationalCode != ""
                        &&
                        CUList_Prof.ElementAt(i).Customers_Users_NationalCode == DTCustomers_Users_FromDB.Rows[j]["NationalCode"].ToString()
                        )
                    {
                        CUList_Prof.ElementAt(i).Customers_Users_IsDuplicate = true;
                        break;
                    }
                    // این کاربر شماره شناسنامه دارد
                    else if (CUList_Prof.ElementAt(i).Customers_Users_IdNumber != ""
                        &&
                        CUList_Prof.ElementAt(i).Customers_Users_IdNumber == DTCustomers_Users_FromDB.Rows[j]["IdNumber"].ToString()
                        && CUList_Prof.ElementAt(i).Customers_Users_Name == DTCustomers_Users_FromDB.Rows[j]["Name"].ToString()
                        && CUList_Prof.ElementAt(i).Customers_Users_Family == DTCustomers_Users_FromDB.Rows[j]["Family"].ToString()
                        )
                    {
                        CUList_Prof.ElementAt(i).Customers_Users_IsDuplicate = true;
                        break;
                    }
                    // این کاربر هیچ کدام را  ندارد - مجهول الهویه بود
                    else if (CUList_Prof.ElementAt(i).Customers_Users_UserMobile != ""
                        &&
                        CUList_Prof.ElementAt(i).Customers_Users_UserMobile == DTCustomers_Users_FromDB.Rows[j]["UserMobile"].ToString()
                        && CUList_Prof.ElementAt(i).Customers_Users_Name == DTCustomers_Users_FromDB.Rows[j]["Name"].ToString()
                        && CUList_Prof.ElementAt(i).Customers_Users_Family == DTCustomers_Users_FromDB.Rows[j]["Family"].ToString()
                        )
                    {
                        CUList_Prof.ElementAt(i).Customers_Users_IsDuplicate = true;
                        break;
                    }
                }

                // چک کردن جنسیت کاربر
                if (CUList_Prof.ElementAt(i).Customers_Users_Sex != 1
                    && CUList_Prof.ElementAt(i).Customers_Users_Sex != 2)
                {
                    CUList_Prof.ElementAt(i).Customers_Users_Sex = 0;
                }

            }









            return CUList_Prof;
        }

        public List<Customers_Users> CheckExcelFile_Student(DataTable DTCustomers_Users_FromDB)
        {
            List<Customers_Users> CUList_Student = new List<Customers_Users>();
            CUList_Student = ReadExcelFile_Student();

            //=============================================================================================
            //    این خط باید اصلاح گردد
            // بروز رسانی کاربران که مربوط به کدام موسسه هستند
            for (int i = 0; i < CUList_Student.Count; i++)
                CUList_Student.ElementAt(i).Customers_Users_CustomerId = CustomerId;

            // چک موبایل
            for (int i = 0; i < CUList_Student.Count; i++)
            {
                if (!CommonBusiness.ValidateMobile(CUList_Student.ElementAt(i).Customers_Users_UserMobile)
                    || !CommonBusiness.IsEnglishLetter(CUList_Student.ElementAt(i).Customers_Users_UserName))
                {
                    List<Customers_Users> CUList_Prof1 = new List<Customers_Users>();
                    return CUList_Prof1;
                }
            }
            
            
            // چک کردن داده های تکراری
            //  می شود Customers_Users_IsDuplicate=True اگر تکراری باشد، مقدار 
            for (int i = 0; i < CUList_Student.Count; i++)
            {
                CUList_Student.ElementAt(i).Customers_Users_IsDuplicate = false;
                for (int j = 0; j < DTCustomers_Users_FromDB.Rows.Count; j++)
                {
                    // کاربر کدملی دارد
                    if (CUList_Student.ElementAt(i).Customers_Users_NationalCode!=""
                        &&
                        CUList_Student.ElementAt(i).Customers_Users_NationalCode == DTCustomers_Users_FromDB.Rows[j]["NationalCode"].ToString()
                        )
                    {
                        CUList_Student.ElementAt(i).Customers_Users_IsDuplicate = true;
                        break;
                    }
                    // این کاربر شماره شناسنامه دارد
                    else if (CUList_Student.ElementAt(i).Customers_Users_IdNumber!=""
                        &&
                        CUList_Student.ElementAt(i).Customers_Users_IdNumber == DTCustomers_Users_FromDB.Rows[j]["IdNumber"].ToString()
                        && CUList_Student.ElementAt(i).Customers_Users_Name == DTCustomers_Users_FromDB.Rows[j]["Name"].ToString()
                        && CUList_Student.ElementAt(i).Customers_Users_Family == DTCustomers_Users_FromDB.Rows[j]["Family"].ToString()
                        )
                    {
                        CUList_Student.ElementAt(i).Customers_Users_IsDuplicate = true;
                        break;
                    }
                    // این کاربر هیچ کدام را  ندارد - مجهول الهویه بود
                    else if (CUList_Student.ElementAt(i).Customers_Users_UserMobile != ""
                        &&
                        CUList_Student.ElementAt(i).Customers_Users_UserMobile == DTCustomers_Users_FromDB.Rows[j]["UserMobile"].ToString()
                        && CUList_Student.ElementAt(i).Customers_Users_Name == DTCustomers_Users_FromDB.Rows[j]["Name"].ToString()
                        && CUList_Student.ElementAt(i).Customers_Users_Family == DTCustomers_Users_FromDB.Rows[j]["Family"].ToString()
                        )
                    {
                        CUList_Student.ElementAt(i).Customers_Users_IsDuplicate = true;
                        break;
                    }                                                           
                }


                if (CUList_Student.ElementAt(i).Customers_Users_Sex != 1 
                    && CUList_Student.ElementAt(i).Customers_Users_Sex != 2)
                {
                    CUList_Student.ElementAt(i).Customers_Users_Sex = 0;
                }    

            }










            
            return CUList_Student;
        }

        // وقتی که نوع درس را انتخاب می کند، مثل جلسه دفاع یا کلاس درس
        protected void ddl_ClassType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddl_ClassType.SelectedItem.Value=="جلسه دفاع")
            {
                UploadFile1.Visible = true;
                UploadFile2.Visible=false;
            }
            else
            {
                UploadFile1.Visible = false;
                UploadFile2.Visible = true;
            }            
        }


        //=================================================================================
        //=====================   نوع کلاس بصورت دفاع   ==================================
        // انتخاب دانشجو
        protected void btn_SelectStudent_Click(object sender, EventArgs e)
        {
            string DayTime = "";
            for (int Counter = 0; Counter < ddl_ClassDayTime.CheckedItems.Count; Counter++)    
                DayTime += ddl_ClassDayTime.CheckedItems[Counter].Index.ToString()+",";
                          
            DataTrasfer DT = EnterUserId("Student");
            Response.Redirect("C_FindUser.aspx?ProfessorGuide=" + DT.ProfessorGuide 
                + "&ProfessorAdvisor=" + DT.ProfessorAdvisor
                + "&ProfessorRefereeOne=" + DT.ProfessorRefereeOne 
                + "&ProfessorRefereeTwo=" + DT.ProfessorRefereeTwo
                + "&Student=" + DT.Student 
                + "&Type=" + DT.Type
                + "&ClassName=" + txt_ClassName.Text
                + "&LatinClassName=" + txt_ClassNameLatin.Text
                + "&CountOfClass=" + ddl_ClassCount.SelectedIndex.ToString()
                + "&CountOfUser=" + txt_UserCount.Value
                + "&DateStart=" + date_input_1.Value
                + "&DateEnd=" + date_input_2.Value
                + "&DateDef=" + date_input_3.Value
                + "&ClassType=" + ddl_ClassType.SelectedIndex.ToString()
                + "&UserType=" + ddl_UserType.SelectedIndex.ToString()
                + "&ClasDayTime=" + DayTime);



        }

        // انتخاب استاد راهنما
        protected void btn_SelectProfessorGuide_Click(object sender, EventArgs e)
        {
            string DayTime = "";
            for (int Counter = 0; Counter < ddl_ClassDayTime.CheckedItems.Count; Counter++)
                DayTime += ddl_ClassDayTime.CheckedItems[Counter].Index.ToString() + ",";

            DataTrasfer DT = EnterUserId("ProfessorGuide");
            Response.Redirect("C_FindUser.aspx?ProfessorGuide=" + DT.ProfessorGuide
                + "&ProfessorAdvisor=" + DT.ProfessorAdvisor
                + "&ProfessorRefereeOne=" + DT.ProfessorRefereeOne 
                + "&ProfessorRefereeTwo=" + DT.ProfessorRefereeTwo
                + "&Student=" + DT.Student 
                + "&Type=" + DT.Type
                + "&ClassName=" + txt_ClassName.Text
                + "&LatinClassName=" + txt_ClassNameLatin.Text
                + "&CountOfClass=" + ddl_ClassCount.SelectedIndex.ToString()
                + "&CountOfUser=" + txt_UserCount.Value
                + "&DateStart=" + date_input_1.Value
                + "&DateEnd=" + date_input_2.Value
                + "&DateDef=" + date_input_3.Value
                + "&ClassType=" + ddl_ClassType.SelectedIndex.ToString()
                + "&UserType=" + ddl_UserType.SelectedIndex.ToString()
                + "&ClasDayTime=" + DayTime);
        }

        // انتخاب مشاور 
        protected void btn_SelectProfessorAdvisor_Click(object sender, EventArgs e)
        {
            string DayTime = "";
            for (int Counter = 0; Counter < ddl_ClassDayTime.CheckedItems.Count; Counter++)
                DayTime += ddl_ClassDayTime.CheckedItems[Counter].Index.ToString() + ",";

            DataTrasfer DT = EnterUserId("ProfessorAdvisor");
            Response.Redirect("C_FindUser.aspx?ProfessorGuide=" + DT.ProfessorGuide
                + "&ProfessorAdvisor=" + DT.ProfessorAdvisor
                + "&ProfessorRefereeOne=" + DT.ProfessorRefereeOne 
                + "&ProfessorRefereeTwo=" + DT.ProfessorRefereeTwo
                + "&Student=" + DT.Student 
                + "&Type=" + DT.Type
                + "&ClassName=" + txt_ClassName.Text
                + "&LatinClassName=" + txt_ClassNameLatin.Text
                + "&CountOfClass=" + ddl_ClassCount.SelectedIndex.ToString()
                + "&CountOfUser=" + txt_UserCount.Value
                + "&DateStart=" + date_input_1.Value
                + "&DateEnd=" + date_input_2.Value
                + "&DateDef=" + date_input_3.Value
                + "&ClassType=" + ddl_ClassType.SelectedIndex.ToString()
                + "&UserType=" + ddl_UserType.SelectedIndex.ToString()
                + "&ClasDayTime=" + DayTime);       
        }

        // انتخاب داور اول
        protected void btn_SelectProfessorRefereeOne_Click(object sender, EventArgs e)
        {
            string DayTime = "";
            for (int Counter = 0; Counter < ddl_ClassDayTime.CheckedItems.Count; Counter++)
                DayTime += ddl_ClassDayTime.CheckedItems[Counter].Index.ToString() + ",";

            DataTrasfer DT = EnterUserId("ProfessorRefereeOne");
            Response.Redirect("C_FindUser.aspx?ProfessorGuide=" + DT.ProfessorGuide
                + "&ProfessorAdvisor=" + DT.ProfessorAdvisor
                + "&ProfessorRefereeOne=" + DT.ProfessorRefereeOne 
                + "&ProfessorRefereeTwo=" + DT.ProfessorRefereeTwo
                + "&Student=" + DT.Student 
                + "&Type=" + DT.Type
                + "&ClassName=" + txt_ClassName.Text
                + "&LatinClassName=" + txt_ClassNameLatin.Text
                + "&CountOfClass=" + ddl_ClassCount.SelectedIndex.ToString()
                + "&CountOfUser=" + txt_UserCount.Value
                + "&DateStart=" + date_input_1.Value
                + "&DateEnd=" + date_input_2.Value
                + "&DateDef=" + date_input_3.Value
                + "&ClassType=" + ddl_ClassType.SelectedIndex.ToString()
                + "&UserType=" + ddl_UserType.SelectedIndex.ToString()
                + "&ClasDayTime=" + DayTime);
        }

        // انتخاب داور دوم
        protected void btn_SelectProfessorRefereeTwo_Click(object sender, EventArgs e)
        {
            string DayTime = "";
            for (int Counter = 0; Counter < ddl_ClassDayTime.CheckedItems.Count; Counter++)
                DayTime += ddl_ClassDayTime.CheckedItems[Counter].Index.ToString() + ",";

            DataTrasfer DT = EnterUserId("ProfessorRefereeTwo");
            Response.Redirect("C_FindUser.aspx?ProfessorGuide=" + DT.ProfessorGuide
                + "&ProfessorAdvisor=" + DT.ProfessorAdvisor
                + "&ProfessorRefereeOne=" + DT.ProfessorRefereeOne 
                + "&ProfessorRefereeTwo=" + DT.ProfessorRefereeTwo
                + "&Student=" + DT.Student 
                + "&Type=" + DT.Type
                + "&ClassName=" + txt_ClassName.Text
                + "&LatinClassName=" + txt_ClassNameLatin.Text
                + "&CountOfClass=" + ddl_ClassCount.SelectedIndex.ToString()
                + "&CountOfUser=" + txt_UserCount.Value
                + "&DateStart=" + date_input_1.Value
                + "&DateEnd=" + date_input_2.Value
                + "&DateDef=" + date_input_3.Value
                + "&ClassType=" + ddl_ClassType.SelectedIndex.ToString()
                + "&UserType=" + ddl_UserType.SelectedIndex.ToString()
                + "&ClasDayTime=" + DayTime);
        }

       
        public DataTrasfer EnterUserId(string Type)
        {
            DataTrasfer DT = new DataTrasfer();


            // اگر کاربر دکمه حذف را بزند، دوباره یه همین صفحه باز می گردد و 
            // Type=Remove
            // می شود، در غیر اینصورت یکی از حالت های موجود در 
            // Else
            // را می گیرد
            if (Type == "Remove")
            {
                if (lbl_Student.Text == lbl_UserId.Text)
                    lbl_Student.Text = "";
                else if (lbl_ProfessorGuide.Text == lbl_UserId.Text)
                    lbl_ProfessorGuide.Text = "";
                else if (lbl_ProfessorAdvisor.Text == lbl_UserId.Text)
                    lbl_ProfessorAdvisor.Text = "";
                else if (lbl_ProfessorRefereeOne.Text == lbl_UserId.Text)
                    lbl_ProfessorRefereeOne.Text = "";
                else if (lbl_ProfessorRefereeTwo.Text == lbl_UserId.Text)
                    lbl_ProfessorRefereeTwo.Text = "";

                DT.ProfessorGuide = lbl_ProfessorGuide.Text;
                DT.ProfessorAdvisor = lbl_ProfessorAdvisor.Text;
                DT.ProfessorRefereeOne = lbl_ProfessorRefereeOne.Text;
                DT.ProfessorRefereeTwo = lbl_ProfessorRefereeTwo.Text;
                DT.Student = lbl_Student.Text;
                DT.Type = Type;
            }
            else
            {
                if (lbl_Type.Text == "Student")
                    lbl_Student.Text = lbl_UserId.Text;
                else if (lbl_Type.Text == "ProfessorGuide")
                    lbl_ProfessorGuide.Text = lbl_UserId.Text;
                else if (lbl_Type.Text == "ProfessorAdvisor")
                    lbl_ProfessorAdvisor.Text = lbl_UserId.Text;
                else if (lbl_Type.Text == "ProfessorRefereeOne")
                    lbl_ProfessorRefereeOne.Text = lbl_UserId.Text;
                else if (lbl_Type.Text == "ProfessorRefereeTwo")
                    lbl_ProfessorRefereeTwo.Text = lbl_UserId.Text;

                DT.ProfessorGuide = lbl_ProfessorGuide.Text;
                DT.ProfessorAdvisor = lbl_ProfessorAdvisor.Text;
                DT.ProfessorRefereeOne = lbl_ProfessorRefereeOne.Text;
                DT.ProfessorRefereeTwo = lbl_ProfessorRefereeTwo.Text;
                DT.Student = lbl_Student.Text;
                DT.Type = Type;
            }

            return DT;
        }

        public class DataTrasfer
        {
            public string ProfessorGuide { set; get; }
            public string ProfessorAdvisor { set; get; }
            public string ProfessorRefereeOne { set; get; }
            public string ProfessorRefereeTwo { set; get; }
            public string Student { set; get; }
            public string Type { set; get; }
        }


        public void DataEntery()
        {
            DataTable DTDate = new DataTable();
            DataTable DTSource = new DataTable();
            DTSource.Columns.Add("UserId", typeof(string));//StudentCode Or ProfCode
            DTSource.Columns.Add("Name", typeof(string)); // Name
            DTSource.Columns.Add("Family", typeof(string)); // Family
            DTSource.Columns.Add("FatherName", typeof(string)); // FatherName
            DTSource.Columns.Add("NationalCode", typeof(string)); // کدملی
            DTSource.Columns.Add("IdNumber", typeof(string)); // شماره شناسنامه
            DTSource.Columns.Add("Mobile", typeof(string)); // موبایل
            DTSource.Columns.Add("Field", typeof(string)); // رشته تحصیلی
            DTSource.Columns.Add("Type", typeof(string)); // نوع کاربر
            DTSource.Columns.Add("Type2", typeof(string)); // نوع عملیات
            DTSource.Columns.Add("ProfessorGuide", typeof(string));
            DTSource.Columns.Add("ProfessorAdvisor", typeof(string));
            DTSource.Columns.Add("ProfessorRefereeOne", typeof(string));
            DTSource.Columns.Add("ProfessorRefereeTwo", typeof(string));
            DTSource.Columns.Add("Student", typeof(string));
            DTSource.Columns.Add("Email", typeof(string));

            // پرکردن دانشجو
            DTDate = MPB.Get_Customers_UserInfo_ThesisDefense(lbl_Student.Text, 1);
            if (DTDate.Rows.Count > 0)
            {
                DataRow row = DTSource.NewRow();
                row["ProfessorGuide"] = lbl_ProfessorGuide.Text;
                row["ProfessorAdvisor"] = lbl_ProfessorAdvisor.Text;
                row["ProfessorRefereeOne"] = lbl_ProfessorRefereeOne.Text;
                row["ProfessorRefereeTwo"] = lbl_ProfessorRefereeTwo.Text;
                row["Student"] = lbl_Student.Text;
                row["Type2"] = "Remove";

                row["UserId"] = DTDate.Rows[0]["UserId"].ToString();
                row["Name"] = DTDate.Rows[0]["name"].ToString();
                row["Family"] = DTDate.Rows[0]["family"].ToString();
                row["FatherName"] = DTDate.Rows[0]["namep"].ToString();
                row["NationalCode"] = DTDate.Rows[0]["idd_meli"].ToString();
                row["IdNumber"] = DTDate.Rows[0]["idd"].ToString();
                row["Mobile"] = DTDate.Rows[0]["mobile"].ToString();
                row["Field"] = DTDate.Rows[0]["nameresh"].ToString();
                row["Email"] = DTDate.Rows[0]["Email"].ToString();
                row["Type"] = "Student";

                DTSource.Rows.Add(row);
            }

            // پرکردن استاد راهنما
            DTDate = MPB.Get_Customers_UserInfo_ThesisDefense(lbl_ProfessorGuide.Text, 2); 
            if (DTDate.Rows.Count > 0)
            {
                DataRow row = DTSource.NewRow();
                row["ProfessorGuide"] = lbl_ProfessorGuide.Text;
                row["ProfessorAdvisor"] = lbl_ProfessorAdvisor.Text;
                row["ProfessorRefereeOne"] = lbl_ProfessorRefereeOne.Text;
                row["ProfessorRefereeTwo"] = lbl_ProfessorRefereeTwo.Text;
                row["Student"] = lbl_Student.Text;
                row["Type2"] = "Remove";

                row["UserId"] = DTDate.Rows[0]["UserId"].ToString();
                row["Name"] = DTDate.Rows[0]["name"].ToString();
                row["Family"] = DTDate.Rows[0]["family"].ToString();
                row["FatherName"] = DTDate.Rows[0]["namep"].ToString();
                row["NationalCode"] = DTDate.Rows[0]["idd_meli"].ToString();
                row["IdNumber"] = DTDate.Rows[0]["idd"].ToString();
                row["Mobile"] = DTDate.Rows[0]["mobile"].ToString();
                row["Field"] = DTDate.Rows[0]["nameresh"].ToString();
                row["Email"] = DTDate.Rows[0]["Email"].ToString();
                row["Type"] = "ProfessorGuide";

                DTSource.Rows.Add(row);
            }

            // پرکردن مشاور
            DTDate = MPB.Get_Customers_UserInfo_ThesisDefense(lbl_ProfessorAdvisor.Text, 2);
            if (DTDate.Rows.Count > 0)
            {
                DataRow row = DTSource.NewRow();
                row["ProfessorGuide"] = lbl_ProfessorGuide.Text;
                row["ProfessorAdvisor"] = lbl_ProfessorAdvisor.Text;
                row["ProfessorRefereeOne"] = lbl_ProfessorRefereeOne.Text;
                row["ProfessorRefereeTwo"] = lbl_ProfessorRefereeTwo.Text;
                row["Student"] = lbl_Student.Text;
                row["Type2"] = "Remove";

                row["UserId"] = DTDate.Rows[0]["UserId"].ToString();
                row["Name"] = DTDate.Rows[0]["name"].ToString();
                row["Family"] = DTDate.Rows[0]["family"].ToString();
                row["FatherName"] = DTDate.Rows[0]["namep"].ToString();
                row["NationalCode"] = DTDate.Rows[0]["idd_meli"].ToString();
                row["IdNumber"] = DTDate.Rows[0]["idd"].ToString();
                row["Mobile"] = DTDate.Rows[0]["mobile"].ToString();
                row["Field"] = DTDate.Rows[0]["nameresh"].ToString();
                row["Email"] = DTDate.Rows[0]["Email"].ToString();
                row["Type"] = "ProfessorAdvisor";

                DTSource.Rows.Add(row);
            }

            // پرکردن داور1
            DTDate = MPB.Get_Customers_UserInfo_ThesisDefense(lbl_ProfessorRefereeOne.Text, 2); 
            if (DTDate.Rows.Count > 0)
            {
                DataRow row = DTSource.NewRow();
                row["ProfessorGuide"] = lbl_ProfessorGuide.Text;
                row["ProfessorAdvisor"] = lbl_ProfessorAdvisor.Text;
                row["ProfessorRefereeOne"] = lbl_ProfessorRefereeOne.Text;
                row["ProfessorRefereeTwo"] = lbl_ProfessorRefereeTwo.Text;
                row["Student"] = lbl_Student.Text;
                row["Type2"] = "Remove";

                row["UserId"] = DTDate.Rows[0]["UserId"].ToString();
                row["Name"] = DTDate.Rows[0]["name"].ToString();
                row["Family"] = DTDate.Rows[0]["family"].ToString();
                row["FatherName"] = DTDate.Rows[0]["namep"].ToString();
                row["NationalCode"] = DTDate.Rows[0]["idd_meli"].ToString();
                row["IdNumber"] = DTDate.Rows[0]["idd"].ToString();
                row["Mobile"] = DTDate.Rows[0]["mobile"].ToString();
                row["Field"] = DTDate.Rows[0]["nameresh"].ToString();
                row["Email"] = DTDate.Rows[0]["Email"].ToString();
                row["Type"] = "ProfessorRefereeOne";

                DTSource.Rows.Add(row);
            }

            // پرکردن داور2
            DTDate =  MPB.Get_Customers_UserInfo_ThesisDefense(lbl_ProfessorRefereeTwo.Text, 2); 
            if (DTDate.Rows.Count > 0)
            {
                DataRow row = DTSource.NewRow();
                row["ProfessorGuide"] = lbl_ProfessorGuide.Text;
                row["ProfessorAdvisor"] = lbl_ProfessorAdvisor.Text;
                row["ProfessorRefereeOne"] = lbl_ProfessorRefereeOne.Text;
                row["ProfessorRefereeTwo"] = lbl_ProfessorRefereeTwo.Text;
                row["Student"] = lbl_Student.Text;
                row["Type2"] = "Remove";

                row["UserId"] = DTDate.Rows[0]["UserId"].ToString();
                row["Name"] = DTDate.Rows[0]["name"].ToString();
                row["Family"] = DTDate.Rows[0]["family"].ToString();
                row["FatherName"] = DTDate.Rows[0]["namep"].ToString();
                row["NationalCode"] = DTDate.Rows[0]["idd_meli"].ToString();
                row["IdNumber"] = DTDate.Rows[0]["idd"].ToString();
                row["Mobile"] = DTDate.Rows[0]["mobile"].ToString();
                row["Field"] = DTDate.Rows[0]["nameresh"].ToString();
                row["Email"] = DTDate.Rows[0]["Email"].ToString();
                row["Type"] = "ProfessorRefereeTwo";

                DTSource.Rows.Add(row);
            }

            RadGrid2.DataSource = DTSource;
            RadGrid2.DataBind();
        }


        public void GetParameterFromPage_ThesisDefense()
        {
            DataTrasfer DT = EnterUserId(lbl_Type.Text);
            DataTable DTDate = new DataTable();
            DataTable DTMeetingInfo = new DataTable();         

            //ThesisDefenseName = ThesisDefenseName.Replace(" ", "");
         
            //=========================  START  =========================
            // پرکردن داده های کاربران حاضر در جلسه دفاع

            DataTable DTSource = new DataTable();
            DTSource.Columns.Add("Id", typeof(string));//ID
            DTSource.Columns.Add("UserId", typeof(string));//StudentCode Or ProfCode
            DTSource.Columns.Add("Name", typeof(string)); // Name
            DTSource.Columns.Add("Family", typeof(string)); // Family
            DTSource.Columns.Add("FatherName", typeof(string)); // FatherName
            DTSource.Columns.Add("NationalCode", typeof(string)); // کدملی
            DTSource.Columns.Add("IdNumber", typeof(string)); // شماره شناسنامه
            DTSource.Columns.Add("Mobile", typeof(string)); // موبایل
            DTSource.Columns.Add("Email", typeof(string)); //  ایمیل کاربر
            DTSource.Columns.Add("Field", typeof(string)); // رشته تحصیلی
            DTSource.Columns.Add("Type", typeof(string)); // نوع کاربر     
            DTSource.Columns.Add("IsDuplicate", typeof(bool)); // چک کردن کاربر جهت تکراری بودن    
            DTSource.Columns.Add("Sex", typeof(string)); // جنسیت کاربر 


            //================================ پرکردن داده ها برای ارسال و ساخت در دیتابیس
            // پرکردن دانشجو
            DTDate = MPB.Get_Customers_UserInfo_ThesisDefense(lbl_Student.Text, 1);
            if (DTDate.Rows.Count > 0)
            {
                DataRow row = DTSource.NewRow();
                row["Id"] = "";
                row["UserId"] = DTDate.Rows[0]["UserId"].ToString();
                row["Name"] = DTDate.Rows[0]["name"].ToString();
                row["Family"] = DTDate.Rows[0]["family"].ToString();
                row["FatherName"] = DTDate.Rows[0]["namep"].ToString();
                row["NationalCode"] = DTDate.Rows[0]["idd_meli"].ToString();
                row["IdNumber"] = DTDate.Rows[0]["idd"].ToString();
                row["Mobile"] = DTDate.Rows[0]["mobile"].ToString();
                row["Field"] = DTDate.Rows[0]["nameresh"].ToString();
                row["Email"] = DTDate.Rows[0]["Email"].ToString();
                row["Type"] = "Student";
                row["IsDuplicate"] = false;
                row["Sex"] = DTDate.Rows[0]["Sex"].ToString();

                DTSource.Rows.Add(row);
            }

            // پرکردن استاد راهنما
            DTDate = MPB.Get_Customers_UserInfo_ThesisDefense(lbl_ProfessorGuide.Text, 2);
            if (DTDate.Rows.Count > 0)
            {
                DataRow row = DTSource.NewRow();
                row["Id"] = "";
                row["UserId"] = DTDate.Rows[0]["UserId"].ToString();
                row["Name"] = DTDate.Rows[0]["name"].ToString();
                row["Family"] = DTDate.Rows[0]["family"].ToString();
                row["FatherName"] = DTDate.Rows[0]["namep"].ToString();
                row["NationalCode"] = DTDate.Rows[0]["idd_meli"].ToString();
                row["IdNumber"] = DTDate.Rows[0]["idd"].ToString();
                row["Mobile"] = DTDate.Rows[0]["mobile"].ToString();
                row["Field"] = DTDate.Rows[0]["nameresh"].ToString();
                row["Email"] = DTDate.Rows[0]["Email"].ToString();
                row["Type"] = "ProfessorGuide";
                row["IsDuplicate"] = false;
                row["Sex"] = DTDate.Rows[0]["Sex"].ToString();

                DTSource.Rows.Add(row);
            }

            // پرکردن مشاور
            DTDate = MPB.Get_Customers_UserInfo_ThesisDefense(lbl_ProfessorAdvisor.Text, 2);
            if (DTDate.Rows.Count > 0)
            {
                DataRow row = DTSource.NewRow();
                row["Id"] = "";
                row["UserId"] = DTDate.Rows[0]["UserId"].ToString();
                row["Name"] = DTDate.Rows[0]["name"].ToString();
                row["Family"] = DTDate.Rows[0]["family"].ToString();
                row["FatherName"] = DTDate.Rows[0]["namep"].ToString();
                row["NationalCode"] = DTDate.Rows[0]["idd_meli"].ToString();
                row["IdNumber"] = DTDate.Rows[0]["idd"].ToString();
                row["Mobile"] = DTDate.Rows[0]["mobile"].ToString();
                row["Field"] = DTDate.Rows[0]["nameresh"].ToString();
                row["Email"] = DTDate.Rows[0]["Email"].ToString();
                row["Type"] = "ProfessorAdvisor";
                row["IsDuplicate"] = false;
                row["Sex"] = DTDate.Rows[0]["Sex"].ToString();

                DTSource.Rows.Add(row);
            }

            // پرکردن داور1
            DTDate = MPB.Get_Customers_UserInfo_ThesisDefense(lbl_ProfessorRefereeOne.Text, 2);
            if (DTDate.Rows.Count > 0)
            {
                DataRow row = DTSource.NewRow();
                row["Id"] = "";
                row["UserId"] = DTDate.Rows[0]["UserId"].ToString();
                row["Name"] = DTDate.Rows[0]["name"].ToString();
                row["Family"] = DTDate.Rows[0]["family"].ToString();
                row["FatherName"] = DTDate.Rows[0]["namep"].ToString();
                row["NationalCode"] = DTDate.Rows[0]["idd_meli"].ToString();
                row["IdNumber"] = DTDate.Rows[0]["idd"].ToString();
                row["Mobile"] = DTDate.Rows[0]["mobile"].ToString();
                row["Field"] = DTDate.Rows[0]["nameresh"].ToString();
                row["Email"] = DTDate.Rows[0]["Email"].ToString();
                row["Type"] = "ProfessorRefereeOne";
                row["IsDuplicate"] = false;
                row["Sex"] = DTDate.Rows[0]["Sex"].ToString();

                DTSource.Rows.Add(row);
            }

            // پرکردن داور2
            DTDate = MPB.Get_Customers_UserInfo_ThesisDefense(lbl_ProfessorRefereeTwo.Text, 2);
            if (DTDate.Rows.Count > 0)
            {
                DataRow row = DTSource.NewRow();
                row["Id"] = "";
                row["UserId"] = DTDate.Rows[0]["UserId"].ToString();
                row["Name"] = DTDate.Rows[0]["name"].ToString();
                row["Family"] = DTDate.Rows[0]["family"].ToString();
                row["FatherName"] = DTDate.Rows[0]["namep"].ToString();
                row["NationalCode"] = DTDate.Rows[0]["idd_meli"].ToString();
                row["IdNumber"] = DTDate.Rows[0]["idd"].ToString();
                row["Mobile"] = DTDate.Rows[0]["mobile"].ToString();
                row["Field"] = DTDate.Rows[0]["nameresh"].ToString();
                row["Email"] = DTDate.Rows[0]["Email"].ToString();
                row["Type"] = "ProfessorRefereeTwo";
                row["IsDuplicate"] = false;
                row["Sex"] = DTDate.Rows[0]["Sex"].ToString();

                DTSource.Rows.Add(row);
            }
            DataTable DTCustomers_Users_FromDB = MPB.Get_Customers_Users_ByCustomerId(CustomerId);

            
            //NationalCode
            for (int i = 0; i < DTSource.Rows.Count; i++)
            {
                for (int j = 0; j < DTCustomers_Users_FromDB.Rows.Count; j++)
                {
                    // این کاربر کدملی دارد
                    if (DTSource.Rows[i]["NationalCode"].ToString()!=""
                        &&
                        DTSource.Rows[i]["NationalCode"].ToString()== DTCustomers_Users_FromDB.Rows[j]["NationalCode"].ToString())                         
                    {
                        DTSource.Rows[i]["IsDuplicate"] = true;
                        break;
                    }
                    // این کاربر شماره شناسنامه دارد
                    else if (DTSource.Rows[i]["IdNumber"].ToString() != ""
                        && 
                        DTSource.Rows[i]["Name"].ToString() == DTCustomers_Users_FromDB.Rows[j]["Name"].ToString()
                        && DTSource.Rows[i]["Family"].ToString() == DTCustomers_Users_FromDB.Rows[j]["Family"].ToString()
                        && DTSource.Rows[i]["IdNumber"].ToString() == DTCustomers_Users_FromDB.Rows[j]["IdNumber"].ToString())                     
                    {
                        DTSource.Rows[i]["IsDuplicate"] = true;
                        break;
                    }
                    // این کاربر هیچ کدام را  ندارد - مجهول الهویه بود
                    else if (DTSource.Rows[i]["Mobile"].ToString() != ""
                        &&
                        DTSource.Rows[i]["Name"].ToString() == DTCustomers_Users_FromDB.Rows[j]["Name"].ToString()
                        && DTSource.Rows[i]["Family"].ToString() == DTCustomers_Users_FromDB.Rows[j]["Family"].ToString()
                        && DTSource.Rows[i]["Mobile"].ToString() == DTCustomers_Users_FromDB.Rows[j]["UserMobile"].ToString())
                    {
                        DTSource.Rows[i]["IsDuplicate"] = true;
                        break;
                    }

                }
            }





            //=========================  END  =========================
      
     


            MPanelDTO MClass = new MPanelDTO();
            List<Customers_ClassDayTime> List_Customers_ClassDayTime = new List<Customers_ClassDayTime>();
            List<Customers_Meeting> List_Customers_Meeting = new List<Customers_Meeting>();
            DataTable DTProfUser = new DataTable();
            DataTable DTStudentUser = new DataTable();


            // Customers_ClassName اضافه کردن در جدول
            MClass.Customers_ClassName_Name = txt_ClassName.Text;
            MClass.Customers_ClassName_CustomerId = CustomerId; 
            MClass.Customers_ClassName_NameLatin = txt_ClassNameLatin.Text;
            MClass.Customers_ClassName_SessionCount = 1;
            MClass.Customers_ClassName_UserCount = 5;
            MClass.Customers_ClassName_DateStart = date_input_3.Value.ToString();// چون جلسه دفاع در یک روز برگزار می شود پس یک تاریخ داریم
            MClass.Customers_ClassName_DateEnd = date_input_3.Value.ToString();// چون جلسه دفاع در یک روز برگزار می شود پس یک تاریخ داریم
            MClass.Customers_ClassName_ScoId = 0;
            MClass.Customers_ClassName_ServerName = "";
            if (ddl_UserType.SelectedValue == "بله")
                MClass.Customers_ClassName_MeetingAccess = "view-hidden";
            else
                MClass.Customers_ClassName_MeetingAccess = "denied";
            MClass.Customers_ClassName_Id = MPB.Create_Customers_ClassName(MClass);
            // END


            //Customers_ClassDayTime اضافه کردن در جدول           
            //    آن کلاس را بر می گرداند، جهت پرکردن جدول بعدی Id سپس یک عدد از 
            
            Customers_ClassDayTime CDT = new Customers_ClassDayTime();
            CDT.Customers_ClassDayTime_ClassNameId = MClass.Customers_ClassName_Id;
            CDT.Customers_ClassDayTime_DayTimeId = int.Parse(ddl_ClassDayTime2.SelectedValue);            
            CDT.Customers_ClassDayTime_Id = MPB.Create_Customers_ClassDayTime(CDT.Customers_ClassDayTime_ClassNameId
                , CDT.Customers_ClassDayTime_DayTimeId);

            List_Customers_ClassDayTime.Add(CDT);
            
            //END

            //  که براساس جلسه درخواستی ساخته می شود Customers_Meeting پرکردن جدول      
            Customers_Meeting CM = new Customers_Meeting();
            CM.Customers_Meeting_ClassId = MClass.Customers_ClassName_Id;
            CM.Customers_Meeting_Id = MPB.Create_Customers_Meeting(MClass.Customers_ClassName_Id);
            List_Customers_Meeting.Add(CM);
            
            
            for (int i = 0; i < DTSource.Rows.Count; i++)
            {
                DataTable DTUser = MPB.Get_Customers_Users_ByNameAndFamily(DTSource.Rows[i]["Name"].ToString(), DTSource.Rows[i]["Family"].ToString());

                if (DTUser.Rows.Count>0)
                {                    
                    DTSource.Rows[i]["Id"] = DTUser.Rows[0]["Id"].ToString();
                }
                else
                {                        
                    DTSource.Rows[i]["Id"] = MPB.Create_Customers_Users
                        (DTSource.Rows[i]["Name"].ToString(), DTSource.Rows[i]["Family"].ToString()
                        , "", "", DTSource.Rows[i]["Mobile"].ToString()
                        , DTSource.Rows[i]["Email"].ToString()
                        , DTSource.Rows[i]["UserId"].ToString()
                        , DTSource.Rows[i]["NationalCode"].ToString()
                        , int.Parse(DTSource.Rows[i]["Sex"].ToString())
                        , CustomerId
                        , DTSource.Rows[i]["Type"].ToString()
                        , DTSource.Rows[i]["UserId"].ToString()
                        , DTSource.Rows[i]["IdNumber"].ToString()
                        , CommonBusiness.RandomString(10, true));
                }     

            }

            

            //    Customers_UserMeeting پرکردن جدول 
            int Counter11 = 0;
            for (int i = 0; i < int.Parse(ddl_ClassCount.SelectedValue); i++)
            {
                for (int j = 0; j < DTSource.Rows.Count; j++)
                {
                    Counter11 = Counter11 + MPB.Create_Customers_UserMeeting
                        (int.Parse(DTSource.Rows[j]["Id"].ToString())
                        , List_Customers_Meeting.ElementAt(i).Customers_Meeting_Id
                        , "view");
                }

            }


            // آماده کردن کاربران تکراری جهت نمایش
            DataTable DTShowDuplicate = new DataTable();
            DTShowDuplicate.Columns.Add("Name", typeof(string));
            DTShowDuplicate.Columns.Add("Family", typeof(string));
            DTShowDuplicate.Columns.Add("NationalCode", typeof(string));
            DTShowDuplicate.Columns.Add("IdNumber", typeof(string));
            DTShowDuplicate.Columns.Add("Access", typeof(string));

            for (int i = 0; i < DTSource.Rows.Count; i++)
            {
                if (bool.Parse(DTSource.Rows[i]["IsDuplicate"].ToString()))
                {
                    DataRow row = DTShowDuplicate.NewRow();
                    row["Name"] = DTSource.Rows[i]["Name"].ToString();
                    row["Family"] = DTSource.Rows[i]["Family"].ToString();
                    row["NationalCode"] = DTSource.Rows[i]["NationalCode"].ToString();
                    row["IdNumber"] = DTSource.Rows[i]["IdNumber"].ToString();
                    row["Access"] = "کاربر";
                    DTShowDuplicate.Rows.Add(row);
                }
            }
            
            RadGrid1.DataSource = DTShowDuplicate;
            RadGrid1.DataBind();                       
        }


        // کاربران استفاده شده در دفاع نمی توانند تکراری باشند
        public bool Check_ThesisDefense_User()
        {
            // دانشجو، استاد راهنما و داور1 حتما باید ثبت گردند         
            if (lbl_Student.Text == "" || lbl_ProfessorGuide.Text == "" || lbl_ProfessorRefereeOne.Text == "")
                return false;
            // راهنما وداور1 یکی نمی تواند باشد
            else if (lbl_ProfessorGuide.Text == lbl_ProfessorRefereeOne.Text)
                return false;
            // اگر مشاور ثبت شده باشد
            else if(lbl_ProfessorAdvisor.Text!="")
            {
                // نمی تواند هم نام با راهنما یا داور1 باشد
                if(lbl_ProfessorAdvisor.Text==lbl_ProfessorRefereeOne.Text 
                    || lbl_ProfessorAdvisor.Text==lbl_ProfessorGuide.Text )
                    return false;
            }
            // اگر داور2 ثبت شده باشد
            else if (lbl_ProfessorRefereeTwo.Text != "")
            {
                // نمی تواند هم نام با راهنما و داور1 باشد
                if (lbl_ProfessorRefereeTwo.Text == lbl_ProfessorRefereeOne.Text
                    || lbl_ProfessorRefereeTwo.Text == lbl_ProfessorGuide.Text)
                    return false;
                // اگر مشاور هم ثبت شده باشد، نمی تواند هم نام با مشاور باشد
                else if(lbl_ProfessorAdvisor.Text!="" 
                    && lbl_ProfessorRefereeTwo.Text == lbl_ProfessorAdvisor.Text)
                    return false;
            }
            
            return true;
        }
      
        
        public bool ConvertPersianDateToGregorian(string PersianDate)
        {
            // بعد میتوان این قسمت را در کل پروژه استفاده کرد
            // و نیاز به بهینه سازی وجود دارد

            // این قسمت به این دلیل اضافه گردیده تا مشتری مجبور باشد،
            //تاریخ شروع درخواست خود را حداقل برای 7 روز آینده اعلام کند تا
            //همکاران ما امکان بررسی و پاسخگویی را داشته باشند
  
            string PersianDate1 = PersianDate;        
            
            int Year = int.Parse(PersianDate1.Substring(0, PersianDate1.IndexOf('/')));
            int Month = int.Parse(PersianDate1.Substring(PersianDate1.IndexOf('/') + 1, 2));
            int Day = int.Parse(PersianDate1.Substring(PersianDate1.LastIndexOf('/') + 1, 2));

            DateTime DateNow = DateTime.Now;// دریافت تاریخ امروز
            DateNow = DateNow.AddDays(7); // برای اینکه یک کلاس ایجاد شود، حداقل باید برای 7 روز آینده باشد
            
            // تاریخ شمسی را تبدیل میکند به تاریخ میلادی            
            DateTime Epoch = new DateTime(Year, Month, Day, new PersianCalendar());

            if (DateNow > Epoch)
                return false;
            else
                return true;

        }








    }
}