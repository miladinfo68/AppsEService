using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.CMS
{
    public partial class Customers_Create : System.Web.UI.Page
    {
        ManagementPanelBusiness MPB = new ManagementPanelBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_CreateCustomer_Click(object sender, EventArgs e)
        {
            lbl_UserAdobe.Text ="";
            lbl_UserPass.Text = "";
            CustomerInfo.Visible = false;

            string Message = Check();
            if (Message == "")
            {
                if(Create())
                    RadWindowManager1.RadAlert("اطلاعات با موفقیت ثبت گردید", 500, 200, "پیام", "");
                else
                    RadWindowManager1.RadAlert("لطفا دوباره سعی کنید", 500, 200, "پیام", "");
            }                
            else
                RadWindowManager1.RadAlert(Message, 500, 200, "پیام", "");


        }


        public bool Create()
        {
            // Check Html Info
            string CustomerName = txt_CustomerName.Text;
            string CustomerTel = txt_CustomerTel.Text;
            string CustomerFax = txt_CustomerFax.Text;
            string CustomerEmail = txt_CustomerEmail.Text;
            string CustomerAddress = txt_CustomerAddress.Text;
            string CustomerUser = txt_CustomerUser.Text;
            string CustomerUserMobile = txt_CustomerUserMobile.Text;

            int Resualt= MPB.Create_Customer(CustomerName, CustomerTel, CustomerFax, CustomerEmail, CustomerAddress
                , CustomerUser, CustomerUserMobile);

            if (Resualt != 0)
            {
                string UserPass = CommonBusiness.RandomString(10, true);
                // Adobe ساختن کاربر در 
                string DomainAddress = "kadobe.iauec.ac.ir";
                string DomainLogin = "h@razavi.com";
                string DomainPassword = "P@ssw0rd";
                Cookie DomainCookies = MPB.Adobe_Login(DomainAddress, DomainLogin, DomainPassword);
                string DomainCookiesValue = DomainCookies.Value;

                MPB.Adobe_Create_User(DomainAddress, CustomerName, "Admin", "customer" + Resualt.ToString()
                    , UserPass, DomainCookiesValue);

                // بروز رسانی در جدول مشتری
                MPB.Update_Customers_ById(1, Resualt, "customer" + Resualt.ToString(), UserPass, 0, "");

                // نمایش در صفحه
                lbl_UserAdobe.Text = "نام کاربری این مشتری در آدابی: " + "customer" + Resualt.ToString();
                lbl_UserPass.Text = "کلمه عبور این مشتری در آدابی: " + UserPass;

                CustomerInfo.Visible = true;

                return true;
            }

            return false;            
        }


        public string Check()
        {
            if (!CommonBusiness.IsNumeric(txt_CustomerTel.Text))
                return "لطفا شماره تلفن را درست وارد نمایید";
            else if (!CommonBusiness.IsNumeric(txt_CustomerFax.Text))
                return "لطفا شماره فکس را درست وارد نمایید";
            else if (!CommonBusiness.ValidateMobile(txt_CustomerUserMobile.Text))
                return "لطفا شماره موبایل را درست وارد نمایید";
            else if (!CommonBusiness.ValidateEmail(txt_CustomerEmail.Text))
                return "لطفا آدرس ایمیل را درست وارد نمایید";
            else if (MPB.Check_CustomerName(txt_CustomerName.Text))
                return "نام موسسه تکراری می باشد، لطفا اطلاعات را بررسی نمایید";

            return "";
        }

        protected void btn_SendInfo_Click(object sender, EventArgs e)
        {
            string Mobile = txt_CustomerEmail.Text;
            string SMS = txt_CustomerUserMobile.Text;
            string UserName = lbl_UserAdobe.Text;
            string UserPass = lbl_UserPass.Text;

            if(rbtn_SendType.SelectedValue=="Email")
            {
                // ارسال ایمیل

            }
            else if(rbtn_SendType.SelectedValue=="SMS")
            {
                // ارسال اس ام اس

            }
            else
            {
                // ارسال به هر دو روش

            }



        }





    }
}