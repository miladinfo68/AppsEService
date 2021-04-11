using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.MasterPages
{
    public partial class PagesMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
            else
            {
                if (!IsPostBack)
                {
                    LoginBusiness lgnB = new LoginBusiness();
                    //StuImg st = lgnB.User_Img(Session["user"].ToString());
                    //PersonalImage.DataValue = st.img;
                    LoginDTO stInfo = lgnB.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                    stName.InnerText = stInfo.Name + " " + stInfo.LastName;

                }
               
                //if (Session["StApp"].ToString() == "5")
                //{
                //    a_card.Visible = true;
                //}
                //if (Session["StApp"].ToString() == "4")
                //{
                //    a_Edit.Visible = true;
                //}
                //if (Session["StApp"].ToString() == "3")
                //{
                //    a_eshteghal.Visible = true;
                //    a_Vaziat.Visible = true;
                //}
                //if (Session["StApp"].ToString() == "12")
                //{
                //    a_tasvieh.Visible = true;
                //}
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
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
    }
}