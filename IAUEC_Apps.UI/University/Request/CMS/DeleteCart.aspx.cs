using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class DeleteCart : System.Web.UI.Page
    {
        CommonBusiness cmnb = new CommonBusiness();
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_StNumber.Text != "")
            {
                CartBusiness.DeleteCard(txt_StNumber.Text);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()),
                    DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()),
                    6, txt_StNumber.Text);
                rwm.RadAlert("با موفقیت حذف شد", null, 100, "پیام", "");
            }

            else
            {
                rwm.RadAlert("لطفا شماره دانشجویی را وارد نمایید", null, 100, "پیام", "");
            }
        }
    }
}