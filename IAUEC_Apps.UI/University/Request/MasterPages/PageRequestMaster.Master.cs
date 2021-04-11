using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;
using System;


namespace IAUEC_Apps.UI.University.Request.MasterPages
{
    public partial class PageRequestMaster : System.Web.UI.MasterPage
    {
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();
        protected void Page_init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    LoginBusiness lgnB = new LoginBusiness();
                    StuImg st = lgnB.User_Img(Session[sessionNames.userID_StudentOstad].ToString());
                    PersonalImage.DataValue = st.img;
                    LoginDTO stInfo = lgnB.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                    stName.InnerText = stInfo.Name + " " + stInfo.LastName;

                }
                //برای زمانی که از رزرواسیون به طور مستقیم می یاد


                if (Session["ComeFromRes"] != null && Session["ComeFromRes"].ToString() == "ok")
                {
                    Session[sessionNames.appID_StudentOstad] = "4";
                    Session["ComeFromRes"] = "no";
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
        

        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../../CommonUI/IntroPage.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {

            Session[sessionNames.userID_StudentOstad] = null;
            Session["LogStatus"] = "0-0";
                LogStatus.Value = Session["LogStatus"].ToString();
                Response.Redirect("../../../University/LMS/Pages/LmsMain.aspx");
            

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");
        }
    }
}