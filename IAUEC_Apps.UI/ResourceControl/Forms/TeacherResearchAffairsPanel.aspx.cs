using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ResourceControl.Entity;
using ResourceControl.BLL;
using System.Data;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.ResourceControl;
using Telerik.Web.UI;

namespace ResourceControl.PL.Forms
{
    public partial class TeacherResearchAffairsPanel : System.Web.UI.Page
    {     
        CommonBusiness cmnb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
           // var userId = Session[sessionNames.userID_StudentOstad];
        }






        protected void a_PortalResearch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../University/Research/CMS/TeacherMainPage.aspx");
        }
        protected void a_StudentAndTheacherChatting_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../Contact/ContactOstad/ContactOstads.aspx");
        }

        protected void a_DefenceMeetingConcordance_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/TeacherReview.aspx");
        }

        protected void a_AudioAndVideoCommunication_ServerClick(object sender, EventArgs e)
        {

        }


        protected void a_SendMessageToTeacher_ServerClick(object sender, EventArgs e)
        {

        }
        protected void a_OnlineDefensePlayback_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/TeacherOnlineDefensePlayback.aspx");
        }



        protected void a_AcceptScore_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/TeacherAcceptScore.aspx");
        }

        protected void aTestDefence_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/ResourceControl/Forms/TeacherTestDefenceOnline.aspx");
        }
    }
}