using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.EmailReg.MasterPages
{
    public partial class PageEmailMaster : System.Web.UI.MasterPage
    {
        StudentBuisiness EmailBuss = new StudentBuisiness();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);

            else
            {

                if (Session[sessionNames.userID_StudentOstad] != null)
                {
                    DataTable dt = new DataTable();
                    dt = EmailBuss.GetEmailRequestStatus(Session[sessionNames.userID_StudentOstad].ToString());
                    if (dt.Rows.Count > 0 && dt.Rows[dt.Rows.Count - 1]["Status"].ToString() == "2")
                    {
                        newrequest.Visible = true;
                    }
                    if (dt.Rows.Count > 0 && dt.Rows[dt.Rows.Count - 1]["Status"].ToString() == "4")
                    {
                        gotoemail.Visible = true;
                        ChangePass.Visible = true;
                        
                    }
                    if (!IsPostBack)
                    {
                        LoginBusiness lgnB = new LoginBusiness();
                        StuImg st = lgnB.User_Img(Session[sessionNames.userID_StudentOstad].ToString());
                        PersonalImage.DataValue = st.img;
                        LoginDTO stInfo = lgnB.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                        stName.InnerText = stInfo.Name + " " + stInfo.LastName;
                        

                    }
                }
                else
                    Response.Redirect("~/CommonUI/login.aspx",false);
            }
        }

        protected void exitButton_Click(object sender, EventArgs e)
        {
            
        }


        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../CommonUI/IntroPage.aspx");

        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {

            Session[sessionNames.userID_StudentOstad] = null;

            Session["LogStatus"] = "0-0";
            LogStatus.Value = Session["LogStatus"].ToString();
            Response.Redirect("../../University/LMS/Pages/LmsMain.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
        }
    }
}