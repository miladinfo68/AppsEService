
using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Resource = ResourceControl.Entity.Resource;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class ResourceMonitor : System.Web.UI.Page
    {
        int catID;
        int LocID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int roleId = Convert.ToInt32(Session["RoleId"]);

                if (roleId == 1 || roleId == 7 || roleId == 50)
                {
                    AdminMode();
                }
                else
                {
                    UserMode(roleId);
                }
                pcal1.Text = DateTime.Now.ToPeString();
                Label1.Text = string.Format("لیست کلاسهای رزرو شده در تاریخ : {0} ", pcal1.Text);
                BindGrid();
                BindCategory();
            }


        }

        private void AdminMode()
        {
            LocationHandler locHandler = new LocationHandler();
            List<Location> locList = locHandler.GetAllLocation();

            drpLocation.DataSource = locList;
            drpLocation.DataTextField = "Name";
            drpLocation.DataValueField = "Id";
            drpLocation.DataBind();
            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "همه موارد";
            drpLocation.Items.Insert(0, item);
        }

        private void UserMode(int roleId)
        {
            LocationHandler locHandler = new LocationHandler();
            List<Location> locList = locHandler.GetLocationByUserRoleId(roleId);

            drpLocation.DataSource = locList;
            drpLocation.DataTextField = "Name";
            drpLocation.DataValueField = "Id";
            drpLocation.DataBind();

            if (locList.Count > 1)
            {
                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = "همه موارد";
                drpLocation.Items.Insert(0, item);
            }
            else
            {
                lblLocation.Visible = false;
                drpLocation.Visible = false;
            }
        }

        private void BindCategory()
        {
            CategoryHandler catHandler = new CategoryHandler();
            List<Category> catList = catHandler.GetCategoryList();
            drpCategory.DataSource = catList;
            drpCategory.DataTextField = "name";
            drpCategory.DataValueField = "ID";
            drpCategory.DataBind();
            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "همه موارد";
            drpCategory.Items.Insert(0, item);

        }

        private void BindGrid()
        {
            int roleId = Convert.ToInt32(Session["RoleId"]);
            ResourceHandler rs = new ResourceHandler();
            List<Resource> reslist = rs.GetResourceList();
            //
            if (roleId == 38 || roleId == 37)
            {
                reslist = rs.GetResourceList().Where(w => w.Location == "ملاصدرا").ToList();
            }
            if (roleId == 39 || roleId == 40)
            {
                reslist = rs.GetResourceList().Where(w => w.Location == "ساختمان رام").ToList();
            }
            if (chkbLocationByRole.Checked)
            {
                reslist = rs.GetResourceList();
            }
            //if (drpLocation.SelectedIndex == 1)
            //{
            //    reslist = rs.GetResourceList().Where(w => w.Location == "ملاصدرا").ToList();
            //}

            //if (drpLocation.SelectedIndex == 2)
            //{
            //    reslist = rs.GetResourceList().Where(w => w.Location == "ساختمان رام").ToList();
            //}

            //  
            grdResourceList.DataSource = reslist;
            grdResourceList.DataBind();
        }

        private void BindGrid(string category)
        {
            LocID = Convert.ToInt32(ViewState["LocID"]);
            catID = Convert.ToInt32(ViewState["CatID"]);
            List<Resource> reslist;
            ResourceHandler rs = new ResourceHandler();
            if (LocID == 0)
            {
                reslist = rs.GetResourceListByCatID(catID);
            }
            else
            {
                reslist = rs.GetResourceListByCatIDandLocation(Convert.ToInt32(category), LocID);
            }

            grdResourceList.DataSource = reslist;
            grdResourceList.DataBind();
        }

        private void BindGrid(string category, List<Telerik.Web.UI.RadComboBoxItem> selected)
        {
            ResourceHandler rs = new ResourceHandler();
            List<Resource> reslist = rs.GetResourceList();
            Resource res = null;
            List<Resource> newResList = new List<Resource>();
            foreach (RadComboBoxItem item in RadComboBox1.Items)
            {
                if (item.Checked == true)
                {
                    res = rs.GetResourceDetails(Convert.ToInt32(item.Value));
                    newResList.Add(res);
                }
            }
            //foreach (var item in selected)
            //{
            //    res = rs.GetResourceDetails(Convert.ToInt32(item.Value));
            //    newResList.Add(res);
            //}
            grdResourceList.DataSource = newResList;
            grdResourceList.DataBind();
        }

        protected void grdResourceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (drpLocation.SelectedIndex != 0)
                {
                    Label lblLocation = (Label)e.Row.FindControl("lblLocation");
                    if (lblLocation.Text == drpLocation.SelectedItem.ToString())
                    {
                        LoadRowsContents(e);
                    }
                    else
                    {
                        e.Row.Visible = false;
                    }
                }
                else
                {
                    LoadRowsContents(e);
                }
            }
        }

        private void LoadRowsContents(GridViewRowEventArgs e)
        {
            int resID = Convert.ToInt32(grdResourceList.DataKeys[e.Row.RowIndex].Value);
            BulletedList bltoptions = (BulletedList)e.Row.FindControl("bltResOptions");
            OptionHandler opt = new OptionHandler();
            List<Option> optlist = opt.GetOptionListByResID(resID);
            bltoptions.DataSource = optlist;
            bltoptions.DataTextField = "name";
            bltoptions.DataBind();

            RequestHandler reqH = new RequestHandler();
            GridView grdRequestsPerResource = (GridView)e.Row.FindControl("grdRequestsPerResource");
            //DataTable reqList = reqH.GetRequestListBySessionDateResID(pcal1.Text, resID);
            DataTable reqList1 = reqH.GetRequestListBySessionDateResID1(pcal1.Text, resID);

            if (cbNotShowEmpty.Checked == true)
            {
                if (reqList1 != null)
                {
                    grdRequestsPerResource.DataSource = reqList1;
                    grdRequestsPerResource.DataBind();
                }
                else
                {
                    e.Row.Visible = false;
                }
            }
            else
            {
                grdRequestsPerResource.DataSource = reqList1;
                grdRequestsPerResource.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (drpCategory.SelectedIndex == 0 && RadComboBox1.SelectedIndex == -1)
                {
                    BindGrid();
                }
                if (drpCategory.SelectedIndex != 0 && RadComboBox1.SelectedIndex == -1)
                {
                    BindGrid(drpCategory.SelectedValue);
                }
                if (drpCategory.SelectedIndex != 0 && RadComboBox1.SelectedIndex != -1)
                {
                    List<Telerik.Web.UI.RadComboBoxItem> selected = RadComboBox1.Items.Cast<Telerik.Web.UI.RadComboBoxItem>()
                                                              .Where(li => li.Selected)
                                                              .ToList()
                                                              ;
                    BindGrid(drpCategory.SelectedValue, selected);

                }

                Label1.Text = string.Format("لیست کلاسهای رزرو شده در تاریخ : {0} ", pcal1.Text);
                string scrp = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1', {extraInputID: 'ContentPlaceHolder1_pcal1',extraInputFormat: 'yyyy/mm/dd'}); ";
                ScriptManager.RegisterStartupScript(this, GetType(), ClientID, scrp, true);
            }
        }

        public string GetImage(int value)
        {
            if (value == 5)
            {
                return ResolveUrl("~/ResourceControl/Images/Messaging-Question-icon.png");
            }
            if (value == 4)
            {
                return ResolveUrl("~/ResourceControl/Images/informed.png");
            }
            if (value == 3)
            {
                return ResolveUrl("~/ResourceControl/Images/deny.png");
            }
            if (value == 2)
            {
                return ResolveUrl("~/ResourceControl/Images/Approved-icon.png");
            }
            if (value == 1)
            {
                return ResolveUrl("~/ResourceControl/Images/send-file-32.png");
            }
            if (value == 0)
            {
                return ResolveUrl("~/ResourceControl/Images/waiting.jpg");
            }
            else
            {
                return ResolveUrl("~/ResourceControl/Images/red_trans_question.png");
            }
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            catID = Convert.ToInt32(drpCategory.SelectedValue);
            ViewState.Add("CatID", catID);
            if (drpLocation.SelectedIndex != 0)
            {
                LocID = Convert.ToInt32(ViewState["LocID"]);
                ResourceHandler resHandler = new ResourceHandler();
                List<Resource> resList = resHandler.GetResourceListByCatIDandLocation(catID, Convert.ToInt32(drpLocation.SelectedValue));
                if (resList != null)
                {
                    RadComboBox1.DataSource = resList;
                    RadComboBox1.DataTextField = "name";
                    RadComboBox1.DataValueField = "ID";
                    RadComboBox1.DataBind();
                }
                else
                {
                    RadComboBox1.Items.Clear();
                }
            }
            else
            {
                int roleId = Convert.ToInt32(Session["RoleId"]);
                LocID = Convert.ToInt32(ViewState["LocID"]);
                ResourceHandler resHandler = new ResourceHandler();
                List<Resource> resList = new List<Resource>();

                if (roleId == 37 || roleId == 38)//edari molasadra
                {
                    resList = resHandler.GetResourceListByCatIDandLocation(catID, 1);
                }
                if (roleId == 39 || roleId == 40)//edari ram
                {
                    resList = resHandler.GetResourceListByCatIDandLocation(catID, 2);

                }
                if (resList != null)
                {
                    RadComboBox1.DataSource = resList;
                    RadComboBox1.DataTextField = "name";
                    RadComboBox1.DataValueField = "ID";
                    RadComboBox1.DataBind();
                }
                else
                {
                    RadComboBox1.Items.Clear();
                }
            }
            string scrp = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1', {extraInputID: 'ContentPlaceHolder1_pcal1',extraInputFormat: 'yyyy/mm/dd'}); ";
            ScriptManager.RegisterStartupScript(this, GetType(), ClientID, scrp, true);
        }

        protected void drpLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocID = Convert.ToInt32(drpLocation.SelectedValue);
            ViewState.Add("LocID", LocID);
            if (drpCategory.SelectedIndex != 0)
            {
                catID = Convert.ToInt32(ViewState["CatID"]);
                ResourceHandler resHandler = new ResourceHandler();
                List<Resource> resList = resHandler.GetResourceListByCatIDandLocation(catID, Convert.ToInt32(drpLocation.SelectedValue));
                if (resList != null)
                {
                    RadComboBox1.DataSource = resList;
                    RadComboBox1.DataTextField = "name";
                    RadComboBox1.DataValueField = "ID";
                    RadComboBox1.DataBind();
                }
                else
                {
                    RadComboBox1.Items.Clear();
                }
            }
            string scrp = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1', {extraInputID: 'ContentPlaceHolder1_pcal1',extraInputFormat: 'yyyy/mm/dd'}); ";
            ScriptManager.RegisterStartupScript(this, GetType(), ClientID, scrp, true);
        }
    }
}