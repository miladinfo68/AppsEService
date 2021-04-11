using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.Pages
{
    public partial class Request_User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddl_TypeAccount.Items.Add("دانشجو");
                ddl_TypeAccount.Items.Add("استاد");
                ddl_TypeAccount.Items.Add("ناظر");
            }

        }

        protected void btn_RegisterRequest_Click(object sender, EventArgs e)
        {
            ManagementPanelBusiness MPB = new ManagementPanelBusiness();
            string Message=CheckPageParameter();
            if (Message!="")
                RadWindowManager1.RadAlert(Message, 500, 200, "پیام", "");
            else if (!MPB.Insert_CourseUser(InsertData()))
                RadWindowManager1.RadAlert("لطفا دوباره سعی کنید", 500, 200, "پیام", "");
            else
                RadWindowManager1.RadAlert("درخواست شما با موفقیت ثبت گردید", 500, 200, "پیام", "");

        }


        public ManagementPanelDTO InsertData()
        {
            ManagementPanelDTO MpClass = new ManagementPanelDTO();            

            MpClass.Id = 0;
            MpClass.Name = txt_NameFa.Text;
            MpClass.Family = txt_FamilyFa.Text;
            MpClass.LatinName=txt_NameEn.Text;
            MpClass.LatinFamily = txt_FamilyEn.Text;
            MpClass.NationalID = txt_NationalID.Text;
            MpClass.Mobile = txt_Mobile.Text;
            MpClass.EmailAddress = txt_EmailAddress.Text;
            MpClass.UserName = txt_UserName.Text;          
            MpClass.Password = CommonBusiness.EncryptPass(txt_Password.Text);
            MpClass.TypeAccount = int.Parse(ddl_TypeAccount.SelectedIndex.ToString());
            
            return MpClass;
        }

        public string CheckPageParameter()
        {
            CommonBusiness cmnB=new CommonBusiness();
            if (!CommonBusiness.IsEnglishLetter(txt_NameEn.Text) || !CommonBusiness.IsEnglishLetter(txt_FamilyEn.Text))
                return "لطفا نام و نام خانوادگی لاتین را بدرستی وارد نمایید";
            else if (!CommonBusiness.IsNumeric(txt_NationalID.Text))
                return "لطفا کد ملی را فقط بصورت عددی وارد نمایید";
            else if (!CommonBusiness.ValidateMobile(txt_Mobile.Text))
                return "لطفا شماره موبایل را درست وارد نمایید";
            else if (!CommonBusiness.ValidateEmail(txt_EmailAddress.Text))
                return "لطفا ایمیل را درست وارد نمایید";
            else if (!CommonBusiness.IsEnglishLetter(txt_UserName.Text))
                return "لطفا نام کاربری را درست وارد نمایید";           
            else if (!cmnB.CheckPasswordIsValidate(txt_Password.Text, txt_UserName.Text))
                return "لطفا کلمه عبور را درست وارد نمایید";
            else
                return "";
        }
      



    }
}