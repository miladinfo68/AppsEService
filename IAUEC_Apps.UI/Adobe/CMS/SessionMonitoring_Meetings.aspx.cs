using IAUEC_Apps.Business.Adobe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class SessionMonitoring_Meetings : System.Web.UI.Page
    {
        SessionMonitoringBusiness SessionMonitoringBusiness = new SessionMonitoringBusiness();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    DataTable DT0 = SessionMonitoringBusiness.GetMeetingsOnline();
                    if (DT0.Rows.Count == 0)
                    {
                        Response.Write("no records");

                    }
                    else
                    { 
                      grd_Session.DataSource = DT0;
                    grd_Session.DataBind();
                    CountOF(DT0);
                    }
                  

                    string mId = Request.QueryString["id"].ToString();
                    string[] id = mId.ToString().Split(new char[] { '@' });
                    string menuId = "";
                    for (int i = 0; i < id[1].Length; i++)
                    {
                        string s = id[1].Substring(i + 1, 1);
                        if (s != "-")
                            menuId += s;
                        else
                            break;
                    }

                    Session[sessionNames.menuID] = menuId;
                }
                catch (Exception esession)
                {

                   Response.Write(esession.Message);
                }
               
            }
            AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            


        }

        protected void btn_Filter_Click(object sender, EventArgs e)
        {
            long MinMinuteFilter=long.Parse(txt_MinMinute.Text);
            long MaxMinuteFilter=long.Parse(txt_MaxMinute.Text);
            long MinUserFilter = long.Parse(txt_MinUser.Text);
            long MaxUserFilter = long.Parse(txt_MaxUser.Text);                      

            DataTable DT = SessionMonitoringBusiness.GetMeetingsOnline_WithFilter(MinMinuteFilter
                , MaxMinuteFilter, MinUserFilter, MaxUserFilter);


            grd_Session.DataSource = DT;
            grd_Session.DataBind();

            CountOF(DT);
        }


        public void CountOF(DataTable DT)
        {
            int CountOfMeetings=DT.Rows.Count;
            int CountOfUsers=0;

            for (int i = 0; i < DT.Rows.Count; i++)
                CountOfUsers = CountOfUsers + int.Parse(DT.Rows[i]["CountOFUser"].ToString());
            
            lbl_CountOfOnlineClass.Text = "مجموع کلاس های آنلاین: " + CountOfMeetings.ToString();
            lbl_CountOfUserOnlineInClass.Text = "مجموع کاربران آنلاین: " + CountOfUsers.ToString();            
        }

        protected void grd_Session_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable DT0 = SessionMonitoringBusiness.GetMeetingsOnline();
            if (DT0.Rows.Count == 0)
            {
                Response.Write("no records");

            }
            else
            {
                grd_Session.DataSource = DT0;
                
                CountOF(DT0);
            }
        }



    }
}