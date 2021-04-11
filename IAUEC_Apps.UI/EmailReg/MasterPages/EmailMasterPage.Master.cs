using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Email;

namespace IAUEC_Apps.UI.EmailReg.MasterPages
{
    public partial class EmailMasterPage : System.Web.UI.MasterPage
    {
        StudentBuisiness EmailBuss = new StudentBuisiness();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session[sessionNames.userID_StudentOstad] != null)
            {
                 DataTable dt = new DataTable();
            dt = EmailBuss.GetEmailRequestStatus(Session[sessionNames.userID_StudentOstad].ToString());
                if(dt.Rows.Count>0 &&  dt.Rows[dt.Rows.Count-1]["Status"].ToString()=="2")
                {
                    newrequest.Visible=true;
                }
                if (dt.Rows.Count > 0 && dt.Rows[dt.Rows.Count - 1]["Status"].ToString() == "4")
                {
                    gotoemail.Visible = true;
                    ChangePass.Visible = true;
                    //UserName.InnerText = dt.Rows[dt.Rows.Count - 1]["Email_Address"].ToString();
                    //Usernameli.Visible = true;
                }
                if (!IsPostBack)
                {
                    LoginBusiness lgnB = new LoginBusiness();
                    StuImg st = lgnB.User_Img(Session[sessionNames.userID_StudentOstad].ToString());
                    PersonalImage.DataValue = st.img;
                    LoginDTO stInfo = lgnB.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                    stName.InnerText = stInfo.Name + " " + stInfo.LastName;
                    //stName.InnerText = stInfo.LastName;

                }
            }
            else
                Response.Redirect("~/CommonUI/login.aspx",false);
        }

        protected void exitButton_Click(object sender, EventArgs e)
        {
            //if (Session["user"].ToString() == "99900999")
            //{



            //}
            //else
            //{
            //    Session["User"] = null;
            //    Session["Password"] = null;
            //    Response.Redirect("~/CommonUI/login.aspx",false);
            //}
        }


        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../CommonUI/IntroPage.aspx");

        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {

            Session[sessionNames.userID_StudentOstad] = null;

            Session["Password"] = null;
            Session["LogStatus"] = "0-0";
            LogStatus.Value = Session["LogStatus"].ToString();
            form1.Action = "http://lms.iauec.ac.ir/Authentication.php";
            ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);
        }
    }
}