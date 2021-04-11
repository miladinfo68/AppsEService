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
using IAUEC_Apps.DTO.ResourceControlClasses;

namespace ResourceControl.PL.Forms
{
    public partial class TeacherDefencesList : System.Web.UI.Page
    {
        CommonBusiness cmb = new CommonBusiness();
        private RequestHandler _requestHandler = new RequestHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Session[sessionNames.userID_StudentOstad]
                // Session[sessionNames.userName_StudentOstad].ToString()
                BindGrid();
            }
        }

        protected void grdvTeacherDefenceList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.grdvTeacherDefenceList.DataSource = ViewState["ProfDefList"] as DataTable;
            GridFilterMenu menu = this.grdvTeacherDefenceList.FilterMenu;
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

        protected void grdvTeacherDefenceList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Filter" || e.CommandName == "Page") return;
                var profId = int.Parse(Session[sessionNames.userID_StudentOstad]?.ToString() ?? "-1");
                var reqId = int.Parse(e.CommandArgument?.ToString() ?? "-1");
                if (e.CommandName == "RejectRequest")
                {
                    Session["Cur_ReqId"] = reqId;
                    string scrp = "function f(){$find(\"" +RadWindow1.ClientID+ "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);                    
                }
                else if (e.CommandName == "History")
                {
                    var dtlog = cmb.GetUserAndStudentLogModifyId(reqId, 11);
                    var myLog = RequestHandler.ConvertDataTableToList<logDetail>(dtlog).OrderBy(O => O.LogDate.ToGregorian()).ThenBy(x => x.LogTime.TimeToTicks());

                    lst_history.DataSource = myLog;
                    lst_history.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        private void BindGrid()
        {
            var teacherId = int.Parse(Session[sessionNames.userID_StudentOstad].ToString() ?? "-1");
            ViewState["ProfDefList"] = _requestHandler.GetStudentDefenceListByProfossorCode(teacherId);

            grdvTeacherDefenceList.DataSource = ViewState["ProfDefList"] as DataTable;
            grdvTeacherDefenceList.DataBind();
        }

        protected void btnConfirmReject_Click(object sender, EventArgs e)
        {
            var profId = int.Parse(Session[sessionNames.userID_StudentOstad]?.ToString() ?? "-1");
            var reqId = int.Parse(Session["Cur_ReqId"]?.ToString() ?? "-1");
            var res = _requestHandler.UpdateRequest_RejectReason(reqId, txtRejectReason.Text.Trim());
            cmb.InsertIntoUserLog(profId, DateTime.Now.ToString("HH:mm"), 11, 220, string.Format("{0}", "رد درخواست جلسه دفاع توسط استاد"), reqId);
            //=============
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeRadWindow();", true);

            Session["Cur_ReqId"] = null;
            BindGrid();
        }
        //=-=================
    }
}