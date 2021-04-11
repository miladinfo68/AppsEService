using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class ConfirmStudentGovahiNotPrinted : System.Web.UI.Page
    {
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();
        RequestStudentCartBusiness business = new RequestStudentCartBusiness();
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        //CommonDAO dao = new CommonDAO();
        CommonBusiness CB = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                manageAccessControl();
                setFilterMenu(grd_GovahiRequest.FilterMenu);
            }

        }
        private void manageAccessControl()
        {
            if (Request.QueryString["id"] != null)
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
                AccessControl1.MenuId = menuId;
                Session[sessionNames.menuID] = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
        }


        private void setFilterMenu(Telerik.Web.UI.GridFilterMenu menu)
        {
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
                foreach (Telerik.Web.UI.RadMenuItem item in menu.Items)
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


        protected void grd_GovahiRequest_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            if (e.CommandName == "taeiddarkhast")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Session["stcode"] = commandArgs[0];
                Session["RequestTypeID"] = commandArgs[1];
                Session["StudentRequestID"] = commandArgs[2];
                CartBusiness.UpdateStudentRequestLogID(Session["stcode"].ToString(), 1, 3, int.Parse(Session["StudentRequestID"].ToString()));
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 6, "", Convert.ToInt32(Session["StudentRequestID"].ToString()));
                this.StiWebViewer1.ResetReport();
                DataTable dt = new DataTable();
                dt = GovahiBusiness.GetGovahiRequest(1, 7);
                grd_GovahiRequest.DataSource = dt;
                grd_GovahiRequest.DataBind();
                DataTable dt1 = new DataTable();
                dt1 = business.GetStudentsInfo(Session["stcode"].ToString());
                StiWebViewer1.Visible = true;
                StiReport rpt = new StiReport();
                rpt.Load(Server.MapPath("../Reports/Packet1.mrt"));
                rpt.Dictionary.Databases.Clear();
                rpt.Dictionary.Databases.Add(new StiSqlDatabase("SuppConnection", CB.ReportConnection.ToString()));
                rpt.Compile();
                rpt.CompiledReport.DataSources["[Request].[SP_Get_StudentAddress]"].Parameters["@stcode"].ParameterValue = Session["stcode"].ToString();

                rpt.RegData(dt1);
                StiWebViewer1.Report = rpt;
                StiWebViewer1.Visible = true;

            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {

                DataTable dt = new DataTable();
                dt = GovahiBusiness.GetGovahiRequest(1, 7);
                grd_GovahiRequest.MasterTableView.Dispose();
                grd_GovahiRequest.DataSource = dt;
                grd_GovahiRequest.DataBind();
            }
        }

        protected void grd_GovahiRequest_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GovahiBusiness.GetGovahiRequest(1, 7);
            if (dt.Rows.Count > 0)
            {
                grd_GovahiRequest.DataSource = dt;
            }
            else
            {
                grd_GovahiRequest.Visible = false;
                rwmValidations.RadAlert("درخواستی موجود نیست", null, 100, "پیغام", "");
                StiWebViewer1.Visible = false;
            }
        }

        protected void grd_GovahiRequest_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                GridDataItem dataItem = e.Item as GridDataItem;
                Button btn_Taeid = e.Item.FindControl("btn_Taeid") as Button;
                HiddenField hd_Field = e.Item.FindControl("hd_Field") as HiddenField;
                TableCell cell = dataItem["stcode"];
                btn_Taeid.CommandArgument = cell.Text + "," + "3" + "," + hd_Field.Value.ToString();

            }

        }
    }
}