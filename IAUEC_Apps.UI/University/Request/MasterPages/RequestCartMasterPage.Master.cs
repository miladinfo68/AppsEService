using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.CommonClasses;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Request.MasterPages
{
    public partial class RequestCartMasterPage : System.Web.UI.MasterPage
    {
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();
        protected void Page_init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
            if (!IsPostBack)
            {
                LoginBusiness lgnB = new LoginBusiness();
                StuImg st = lgnB.User_Img(Session[sessionNames.userID_StudentOstad].ToString());
                PersonalImage.DataValue = st.img;
                LoginDTO stInfo = lgnB.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                stName.InnerText = stInfo.Name + " " + stInfo.LastName;

            }


            if (Session[sessionNames.appID_StudentOstad].ToString() == "5")
            {
                a_card.Visible = true;
            }
            if (Session[sessionNames.appID_StudentOstad].ToString() == "4")
            {
                a_Edit.Visible = true;
            }
            if (Session[sessionNames.appID_StudentOstad].ToString() == "3")
            {
                a_eshteghal.Visible = true;
                a_Vaziat.Visible = true;
            }
            if (Session[sessionNames.appID_StudentOstad].ToString() == "12")
            {
                a_tasvieh.Visible = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
            //if (!IsPostBack)
            //{
            //    LoginBusiness lgnB = new LoginBusiness();
            //    StuImg st = lgnB.User_Img(Session["user"].ToString());
            //    PersonalImage.DataValue = st.img;
            //    LoginDTO stInfo = lgnB.Get_StInfo(Session["user"].ToString());
            //    stName.InnerText = stInfo.Name + " " + stInfo.LastName;

            //}
            //if (Session["StApp"].ToString() == "3")
            //    a_Vaziat.Visible = true;



            //if (Session["StApp"].ToString() == "3" || Session["StApp"].ToString() == "5")
            //li_Edite.Visible = true; 
        }

        /// <summary>
        /// Handles the Click event of the exitButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void exitButton_Click(object sender, EventArgs e)
        {

        }

        protected void btn_home_Click(object sender, EventArgs e)
        {

        }
        //protected void changePass_ServerClick(object sender, EventArgs e)
        //{
        //    RadWindowManager windowManager = new RadWindowManager();
        //    RadWindow widnow1 = new RadWindow();
        //    // Set the window properties   
        //    widnow1.NavigateUrl = "../../../CommonUI/ChangePassword.aspx";
        //    widnow1.ID = "RadWindow1";
        //    widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
        //    windowManager.Windows.Add(widnow1);
        //    this.form1.Controls.Add(widnow1);
        //}

        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../../CommonUI/IntroPage.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            //if (Session["user"].ToString() == "99900999")
            //{


            Session[sessionNames.userID_StudentOstad] = null;
            Session["Password"] = null;
            Session["LogStatus"] = "0-0";
            LogStatus.Value = Session["LogStatus"].ToString();
            Response.Redirect("../../../University/LMS/Pages/LmsMain.aspx");

            //}
            //else
            //{
            //    Session["user"] = null;
            //    Session["Password"] = null;
            //    Response.Redirect("~/CommonUI/login.aspx",false);
            //}
        }

    }
}