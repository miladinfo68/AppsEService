using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ResourceControl.BLL;
using System.Data;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using View = DocumentFormat.OpenXml.Wordprocessing.View;

namespace ResourceControl.PL.Admin
{
    public partial class AdminReports : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        RequestHandler rq = new RequestHandler();

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                AccessControl.MenuId = menuId;
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
                ViewState.Add("sDate", null);
                ViewState.Add("Edate", null);
                ViewState["type"] = -1;

            }
        }

        private void BindData()
        {
            dt = rq.GetAllRequestByTypeAndDate(Convert.ToInt32(ViewState["type"]), ViewState["sDate"].ToString(), ViewState["Edate"].ToString());
            grdRequestLists.DataSource = dt;
            grdRequestLists.DataBind();
        }

        //this event is just for debuging!!!
        protected void drpReportChoose_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void btnAddDate_Click(object sender, EventArgs e)
        {
            ViewState["sDate"] = txtSdate.Text;
            ViewState["Edate"] = txtEdate.Text;
            ViewState["type"] = Convert.ToInt32(drpReportChoose.SelectedValue);

            BindData();
        }

        protected void grdRequestLists_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Convert.ToInt32(ViewState["type"]) != -1 && ViewState["sDate"] != null && ViewState["Edate"] != null)
            {
                dt = rq.GetAllRequestByTypeAndDate(Convert.ToInt32(ViewState["type"]), ViewState["sDate"].ToString(), ViewState["Edate"].ToString());
                grdRequestLists.DataSource = dt;

            }
        }

        protected void grdRequestLists_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "History")
            {
                GridDataItem itemAmount = (GridDataItem)e.Item;
                int ID;
                ID = Convert.ToInt32(e.CommandArgument);

                string userID = itemAmount["issuerID"].Text;
                string username = itemAmount["issuerName"].Text;
                string reqDate = itemAmount["issue_time"].Text;
                string reqID = itemAmount["ID"].Text;
                CommonBusiness cmb = new CommonBusiness();

                lst_history.DataSource = cmb.GetUserLogByModifyId(int.Parse(e.CommandArgument.ToString()), 11);
                lst_history.DataBind();
                info1.InnerText = "نام درخواست کننده:" + username;
                info2.InnerText = "شماره درخواست:" + reqID;
                info3.InnerText = "تاریخ درخواست:" + reqDate;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
    }
}