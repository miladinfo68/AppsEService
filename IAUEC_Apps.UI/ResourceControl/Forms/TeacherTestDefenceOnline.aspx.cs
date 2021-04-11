using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.DTO.AdobeClasses;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class TeacherTestDefenceOnline : System.Web.UI.Page
    {
        const string passAdobeUser = "4sx0pvauo4nleowu5ugvkkx9l0bpsbe";
        Business.Adobe.AdobeBusiness adobeBusiness = new Business.Adobe.AdobeBusiness();
        AdobeConnectDTO adobeConnectDTO = new AdobeConnectDTO();
        RequestHandler requestHandler = new RequestHandler();
        string user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }
        }

        protected void btnTesti_Click(object sender, EventArgs e)
        {

            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_9";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);
        }

        protected void btnTesti2_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_2";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);

        }

        protected void btnTesti3_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_3";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);
        }

        protected void btnTesti4_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_4";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);

        }

        protected void btnTesti5_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_5";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);
        }

        protected void btnTesti6_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_6";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);
        }
    }
}