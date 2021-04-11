using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.Pages
{
    public partial class C_RequestClassName_NewUser : System.Web.UI.Page
    {
        ManagementPanelBusiness MPB = new ManagementPanelBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassNameId") != null)
                    lbl_ClassId.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("ClassNameId");
                //else
                //    Response.Redirect("C_RequestClassName_EditUser.aspx");



                // ====== 02 START ======
                RadComboBoxItem cmbitem1 = new RadComboBoxItem();
                cmbitem1.Text = "مرد";
                cmbitem1.Value = "1";
                cmbitem1.Font.Name = "Tahoma";
                cmbitem1.Font.Size = 8;
                ddl_Sex.Items.Add(cmbitem1);
                RadComboBoxItem cmbitem2 = new RadComboBoxItem();
                cmbitem2.Text = "زن";
                cmbitem2.Value = "2";
                cmbitem2.Font.Name = "Tahoma";
                cmbitem2.Font.Size = 8;
                ddl_Sex.Items.Add(cmbitem2);
                RadComboBoxItem cmbitem3 = new RadComboBoxItem();
                cmbitem3.Text = "استاد";
                cmbitem3.Value = "1";
                cmbitem3.Font.Name = "Tahoma";
                cmbitem3.Font.Size = 8;
                ddl_Access.Items.Add(cmbitem3);
                RadComboBoxItem cmbitem4 = new RadComboBoxItem();
                cmbitem4.Text = "دانشجو";
                cmbitem4.Value = "2";
                cmbitem4.Font.Name = "Tahoma";
                cmbitem4.Font.Size = 8;
                ddl_Access.Items.Add(cmbitem4);
                // ====== 02 END ======

            }
        }

        protected void rbtn_NewUser_Click(object sender, EventArgs e)
        {

        }

        public void CheckInfo()
        {
            if (!CommonBusiness.IsEnglishLetter(txt_LatinName.Text)
                || !CommonBusiness.IsEnglishLetter(txt_LatinFamily.Text))
                RadWindowManager1.RadAlert("لطفا نام و نام خانوادگی لاتین را به طور صحیح وارد نمایید", 500, 200, "پیام", "");
            else if(!CommonBusiness.ValidateMobile(txt_Mobile.Text))
                RadWindowManager1.RadAlert("لطفا شماره موبایل را صحیح وارد نمایید", 500, 200, "پیام", "");
            else if (!CommonBusiness.ValidateEmail(txt_Email.Text))
                RadWindowManager1.RadAlert("لطفا ایمیل آدرس را صحیح وارد نمایید", 500, 200, "پیام", "");
            else if (!CommonBusiness.IsNumeric(txt_IdNumber.Text)
                || !CommonBusiness.IsNumeric(txt_NationalCode.Text))
                RadWindowManager1.RadAlert("لطفا شماره شناسنامه و شماره ملی را صحیح وارد نمایید", 500, 200, "پیام", "");
            else
            {
                // چک کردن کاربر جهت عدم تکراری بودن
                DataTable DTUser = MPB.Get_Customers_Users_ByNameAndFamily(txt_Name.Text, txt_Family.Text);
                if(DTUser.Rows.Count>0)
                    RadWindowManager1.RadAlert("این کاربر وجود دارد، لطفا اطلاعات وارد شده را بررسی نمایید", 500, 200, "پیام", "");
                else
                    AddUserinMeeting();
            }
                
            
        }

        public void AddUserinMeeting()
        {
            string UserPassWord=CommonBusiness.RandomString(10, true);
            // ایجاد کاربر
            int CustomerUserId = MPB.Create_Customers_Users(txt_Name.Text, txt_Family.Text, txt_LatinName.Text
                , txt_LatinFamily.Text, txt_Mobile.Text, txt_Email.Text, txt_UserName.Text, txt_NationalCode.Text
                , int.Parse(ddl_Sex.SelectedValue), 3, ddl_Access.SelectedValue, "-", txt_IdNumber.Text
                , UserPassWord);

            DataTable DTMeetingOfClass = MPB.Get_Customers_Meeting_ByClassId(int.Parse(lbl_ClassId.Text));

            string UserAccess=""; // دسترسی کاربر
            if(ddl_Access.SelectedValue=="1")
                UserAccess="host";
            else
                UserAccess="view";

            // اضافه کردن کاربر به درون کلاس ها
            for (int i = 0; i < DTMeetingOfClass.Rows.Count; i++)
            {
                MPB.Create_Customers_UserMeeting(CustomerUserId
                    , int.Parse(DTMeetingOfClass.Rows[i]["Id"].ToString())
                    , UserAccess);                    
            }


            // چک کردن کاربر در آدابی
            DataTable DTAdobeUser = MPB.Adobe_Get_PRINCIPALS_ByLOGIN("user" + CustomerUserId.ToString());
            string DomainCookiesValue = MPB.Adobe_Login().Value;
            string DomainAddress = "kadobe.iauec.ac.ir";

            ////  اضافه کردن کاربر به آدابی
            if (DTAdobeUser.Rows.Count == 0)
            {
                MPB.Adobe_Create_User(DomainAddress, txt_Name.Text, txt_Family.Text
                    , "user" + CustomerUserId.ToString(), UserPassWord, DomainCookiesValue);
            }

            string CustomerPrincipalsId = DTAdobeUser.Rows[0]["PRINCIPAL_ID"].ToString();

            //// اضافه کردن کاربر به درون کلاس در آداب
            //for (int j = 0; j < DTMeetingOfClass.Rows.Count; j++)
            //{
            //    //GetMeetingInfo_ByMeetingName
            //    string CustomerMeetingScoId=MPB.Adobe_Get_SP_Get_ScosByName()

            //    MPB.Adobe_Insert_UserInMeeting(DomainAddress
            //    , CustomerPrincipalsId
            //    , CustomerMeetingScoId[i]
            //    , UserAccess
            //    , DomainCookiesValue);


            //}




            //بازگشت به صفحه ویرایش کاربر 
            // + کدکلاس نیز ارسال شود تا در آن صفحه، اطلاعات همین کلاسی که تغییر کرده کاربرش نمایش داده شود


        }





    }
}