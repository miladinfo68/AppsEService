using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class AcceptGovahiReport : System.Web.UI.Page
    {
        RequestGovahiBusiness GBusiness = new RequestGovahiBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setFilterMenu(grd_AcceptReport.FilterMenu);
                manageAccessControl();
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
                AccessControl.MenuId = menuId;
                Session[sessionNames.menuID] = menuId;
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
        }

        protected void grd_AcceptReport_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtgov = new DataTable();
            dtgov = GBusiness.GetAcceptedGovahiReport(3);
            if (dtgov.Rows.Count > 0)
            {
                grd_AcceptReport.DataSource = dtgov;
                img_ExportToExcel.Visible = true;

            }
            else
            {
                grd_AcceptReport.Visible = false;
                img_ExportToExcel.Visible = false;
            }
        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {

            grd_AcceptReport.ExportSettings.FileName = "RejectGovahiReport";
            grd_AcceptReport.ExportSettings.IgnorePaging = true;
            grd_AcceptReport.MasterTableView.ExportToExcel();

        }
    }
}