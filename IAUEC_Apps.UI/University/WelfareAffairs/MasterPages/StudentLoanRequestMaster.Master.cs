using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Linq;

namespace IAUEC_Apps.UI.University.WelfareAffairs.MasterPages
{
    public partial class StudentLoanRequestMaster : System.Web.UI.MasterPage
    {
        protected void Page_init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null) Response.Redirect("~/CommonUI/login.aspx");
            else
            {
                LoginBusiness logBusiness = new LoginBusiness();
                System.Data.DataTable dt = new System.Data.DataTable();

                if (!IsPostBack)
                {
                    StuImg st = logBusiness.User_Img(Session[sessionNames.userID_StudentOstad].ToString());
                    PersonalImage.DataValue = st.img;
                    LoginDTO stInfo = logBusiness.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                    stName.InnerText = stInfo.Name + " " + stInfo.LastName;

                }
                //dt = logBusiness.Get_Menu_ByUserIdAndAppId(int.Parse(Session[sessionNames.appID_Karbar].ToString()), int.Parse(Session[sessionNames.userID_Karbar].ToString()), int.Parse(Session["SectionId"].ToString()));
                //dt = logBusiness.Get_Menu_ByUserIdAndAppId(14, int.Parse(Session[sessionNames.userID_StudentOstad].ToString()), int.Parse(Session["SectionId"].ToString()));
                //string cat = "";

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (cat == "")
                //    {
                //        cat = dt.Rows[i]["CategoryId"].ToString();
                //        lstreportMenu.Text += "<li><a>" + dt.Rows[i]["CategoryName"].ToString() + "<span class='fa fa-chevron-down'> </span></a><ul class='nav child_menu' style='display: none'>";

                //    }

                //    if (cat != dt.Rows[i]["CategoryId"].ToString())
                //    {

                //        lstreportMenu.Text += " </ul></li>";
                //        cat = dt.Rows[i]["CategoryId"].ToString();
                //        lstreportMenu.Text += "<li><a>" + dt.Rows[i]["CategoryName"].ToString() + "<span class='fa fa-chevron-down'> </span></a><ul class='nav child_menu' style='display: none'>";
                //        lstreportMenu.Text += "  <li><a href='" + "../../Exam/CMS/" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + generaterandomstr(8) + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr(8) + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";
                //    }
                //    else
                //        lstreportMenu.Text += "  <li><a href='" + "../../Exam/CMS/" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + generaterandomstr(8) + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr(8) + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";
                //}

                lstreportMenu.Text = "  <li><span class='fa fa-hand-o-left'></span><a href='" + "../Content/forms/help.pdf" + "?id=" + generaterandomstr(8) + "' target='_blank'>شرایط و راهنمای درخواست وام </a></li>";

                lstreportMenu.Text += "  <li><span class='fa fa-hand-o-left'></span><a href='" + "../Content/forms/shorttime.pdf" + "?id=" + generaterandomstr(8) + "' target='_blank'>فرم کوتاه مدت</a></li>";

                lstreportMenu.Text += "  <li><span class='fa fa-hand-o-left'></span><a href='" + "../Content/forms/midtime_taminshahriye.pdf" + "?id=" + generaterandomstr(8) + "' target='_blank'>فرم میان مدت یا تامین شهریه</a></li>";

            }

        }

        public string generaterandomstr(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
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
            Response.Redirect("~/CommonUI/IntroPage.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {

            Session[sessionNames.userID_StudentOstad] = null;
            Session["LogStatus"] = "0-0";
            LogStatus.Value = Session["LogStatus"].ToString();
            Response.Redirect("../../../CommonUI/login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");
        }
    }
}