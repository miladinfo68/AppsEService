using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class StudentTestDefenceOnline : System.Web.UI.Page
    {
        const string passAdobeUser = "4sx0pvauo4nleowu5ugvkkx9l0bpsbe";

        Business.Adobe.AdobeBusiness adobeBusiness = new Business.Adobe.AdobeBusiness();
        AdobeConnectDTO adobeConnectDTO = new AdobeConnectDTO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }

        }

        protected void btnTesti_Click(object sender, EventArgs e)
        {

            LoginBusiness lgb = new LoginBusiness();

            var userAdobe = Session[sessionNames.userID_StudentOstad].ToString();
            LoginDTO stInfo = lgb.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
            string firtsName = stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
            string lastName = stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_9";



            string link = "http://" + adobeBusiness.OpenMeetingAsView(adobeConnectDTO);
            Response.Redirect(link);

        }

        protected void btnTesti2_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();

            var userAdobe = Session[sessionNames.userID_StudentOstad].ToString();
            LoginDTO stInfo = lgb.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
            string firtsName = stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
            string lastName = stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_2";



            string link = "http://" + adobeBusiness.OpenMeetingAsView(adobeConnectDTO);
            Response.Redirect(link);

        }

        protected void btnTesti3_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();

            var userAdobe = Session[sessionNames.userID_StudentOstad].ToString();
            LoginDTO stInfo = lgb.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
            string firtsName = stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
            string lastName = stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_3";



            string link = "http://" + adobeBusiness.OpenMeetingAsView(adobeConnectDTO);
            Response.Redirect(link);
        }

        protected void btnTesti4_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();

            var userAdobe = Session[sessionNames.userID_StudentOstad].ToString();
            LoginDTO stInfo = lgb.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
            string firtsName = stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
            string lastName = stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_4";



            string link = "http://" + adobeBusiness.OpenMeetingAsView(adobeConnectDTO);
            Response.Redirect(link);
        }

        protected void btnTesti5_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();

            var userAdobe = Session[sessionNames.userID_StudentOstad].ToString();
            LoginDTO stInfo = lgb.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
            string firtsName = stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
            string lastName = stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_5";



            string link = "http://" + adobeBusiness.OpenMeetingAsView(adobeConnectDTO);
            Response.Redirect(link);
        }

        protected void btnTesti6_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();

            var userAdobe = Session[sessionNames.userID_StudentOstad].ToString();
            LoginDTO stInfo = lgb.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
            string firtsName = stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
            string lastName = stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_6";



            string link = "http://" + adobeBusiness.OpenMeetingAsView(adobeConnectDTO);
            Response.Redirect(link);
        }
    }
}