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
    public partial class TeacherDefencePanel : System.Web.UI.Page
    {     
        CommonBusiness cmnb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {            
        }
        //=-=================
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //هماهنگی رزرو کلاس
        protected void a_ClassReservationConcordance_ServerClick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = cmnb.GetSystemAvailability(11);
            if (bool.Parse(dt.Rows[0]["IsOpen"].ToString()) == false)
            {

                rwm_message.RadAlert("سامانه راه اندازی نشده است", null, 100, "پیام", "");
            }
            else
            {
                Session[sessionNames.appID_StudentOstad] = 11;
                Response.Redirect("ProfessorReview.aspx");
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //هماهنگی رزرو دفاع
        protected void a_DefenceReservationConcordance_ServerClick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = cmnb.GetSystemAvailability(11);
            if (bool.Parse(dt.Rows[0]["IsOpen"].ToString()) == false)
            {

                rwm_message.RadAlert("سامانه راه اندازی نشده است", null, 100, "پیام", "");
            }
            else
            {
                Session[sessionNames.appID_StudentOstad] = 11;
                Response.Redirect("TeacherDefencesList.aspx");
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //گفتگوی استاد با دانشجو
        protected void a_ChattingTeacherToStudent_ServerClick(object sender, EventArgs e)
        {

        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //درخواست مساعدت هماهنگی دفاع
        protected void a_AssistanceRequestForDefenceConcordance_ServerClick(object sender, EventArgs e)
        {


        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    }
}