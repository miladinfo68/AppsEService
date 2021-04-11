using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Conatct;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class TeacherReview : System.Web.UI.Page
                
    {
        private CommonBusiness commonBusiness = new CommonBusiness();
        private RequestHandler _requestHandler = new RequestHandler();
        private int _prev=200;
        string pTitle = "تایید یا رد دفاع دانشجو";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                drpRequestTypeList.SelectedValue = "0";
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Label lblRequestId = (Label)item.FindControl("lblRequestId");
            Label lblStCode= (Label)item.FindControl("lblstudentcode");
            Label lblIdTypeOstad= (Label)item.FindControl("lblIdTypeOstad");
            Label lblNameTypeOstad = (Label)item.FindControl("lblNameTypeOstad");
            Button btnReject = (Button)item.FindControl("btnReject");
            Button btnAccept = (Button)item.FindControl("btnAccept");

             string userName= Session[sessionNames.userName_StudentOstad].ToString();
                var oscode = Session[sessionNames.userID_StudentOstad].ToString();
            if (_requestHandler.DeleteStudentRequest(int.Parse(lblRequestId.Text.Trim())))
            {
                _requestHandler.UpdateFlagReject_DefenceMeeting(int.Parse(lblRequestId.Text.Trim()),
                                                            int.Parse(lblIdTypeOstad.Text.Trim()), false);
                commonBusiness.InsertIntoStudentLog(oscode, DateTime.Now.ToString("HH:mm")
                , 11, 52, "رد درخواست دفاع توسط استاد", int.Parse(lblRequestId.Text.Trim()));
            }

            RfrhGrdDisplayStundetDefence();
            grdDisplayStundetDefence.Rebind();

            btnReject.Enabled = false;
            btnAccept.Enabled = false;

            SendSmsContactBuisnes.SendSmsStudent(userName, lblNameTypeOstad.Text,lblStCode.Text);
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Button btnReject = (Button)item.FindControl("btnReject");
            Button btnAccept = (Button)item.FindControl("btnAccept");
            grdDisplayStundetDefence.Rebind();
            btnAccept.Enabled = false;
            btnReject.Enabled = true;
        }

        protected void grdDisplayStundetDefence_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int status = Convert.ToInt32(drpRequestTypeList.SelectedValue);
            RfrhGrdDisplayStundetDefence(status);
        }
        public void RfrhGrdDisplayStundetDefence(int status=0)
        { 
            
             var userid = _prev+Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dt = new DataTable();

            dt = _requestHandler.GetStudentsDefenceforAOstad(userid);
            //درخو.است های درخواست شده از dropdown
            if (status == 1)//درخواستهای  منتظر رد
            {
                var rows = dt.AsEnumerable()
                                .Where(row => row.Field<int>("status") != 3
                                && RequestHandler.WorkingDays12h(row.Field<DateTime>("DateRegistration"))>DateTime.Now);
                if (rows.Any())
                {
                    dt = rows.CopyToDataTable();
                }
                else
                {
                    dt = null;
                }
            }
            else if (status == 2)
            {
                var rows = dt.AsEnumerable()
                              .Where(row => row.Field<int>("status") == 3
                               && RequestHandler.WorkingDays12h(row.Field<DateTime>("DateRegistration")) > DateTime.Now).ToList();
                if (rows.Any())
                {
                    dt = rows.CopyToDataTable();
                }
                else
                {
                    dt = null;
                }
            }
            else
            {
                var rows = dt.AsEnumerable()
              .Where(row =>
                RequestHandler.WorkingDays12h(row.Field<DateTime>("DateRegistration")) > DateTime.Now).ToList();
                if (rows.Any())
                {
                    dt = rows.CopyToDataTable();
                }
                else
                {
                    dt = null;
                }
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                grdDisplayStundetDefence.DataSource = dt;
            }
            else
                grdDisplayStundetDefence.DataSource = string.Empty;

            GridFilterMenu menu = grdDisplayStundetDefence.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }


                }
            }
        }

        protected void drpRequestTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int status = Convert.ToInt32(drpRequestTypeList.SelectedValue);
            RfrhGrdDisplayStundetDefence(status);
            grdDisplayStundetDefence.Rebind();
        }
    }
}