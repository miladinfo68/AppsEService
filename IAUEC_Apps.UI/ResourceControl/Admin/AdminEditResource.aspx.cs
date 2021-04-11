using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResourceControl.PL.Admin
{
    public partial class AdminEditResource : System.Web.UI.Page
    {
        private List<Category> allcats = new List<Category>();
        private List<Resource> reslist = new List<Resource>();
        private List<Location> loclist = new List<Location>();
        int catID;
        int LocID;
        private List<Option> optlist = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
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
            }
        }

        private void BindData()
        {
            OptionHandler opt = new OptionHandler();
            optlist = opt.GetOptionList();
            LoadListControl<Option>(chkblResourceNewOptions, optlist, "name", "ID");

            LocationHandler locH = new LocationHandler();
            loclist = locH.GetAllLocation();
            LoadListControl<Location>(drpResourceNewLocation, loclist, "name", "ID");
        }

        protected void drpChooseToEditCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            catID = Convert.ToInt32(drpChooseToEditCategory.SelectedValue);
            ViewState.Add("CatID", catID);
            if (drpChooseToEditCategory.SelectedIndex != 0)
            {
                LocID = Convert.ToInt32(ViewState["LocID"]);
                ResourceHandler resHandler = new ResourceHandler();
                List<Resource> resList = resHandler.GetResourceListByCatIDandLocation(catID, Convert.ToInt32(drpResourceNewLocation.SelectedValue));
                LoadListControl<Resource>(drpChoosToEditResource, resList, "name", "ID");
                ViewState.Add("reslist", resList);
            }
        }

        protected void drpChoosToEditResource_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtResourceNewName.Text = "";
            // drpResourceNewLocation.ClearSelection();
            txtResourceNewDescription.Text = "";
            chkblResourceNewOptions.ClearSelection();
            rdbtnlistStatus.ClearSelection();
            reslist = ViewState["reslist"] as List<Resource>;
            if (drpChoosToEditResource.SelectedIndex != 0)
            {
                int resID = Convert.ToInt32(drpChoosToEditResource.SelectedValue);
                Resource res = reslist.Find(item => item.ID == resID);
                txtResourceNewName.Text = res.Name;

                txtCapacity.Text = res.Capacity.ToString();
                txtResourceNewDescription.Text = res.Description;

                //drpResourceNewLocation.SelectedItem.Text = res.Location;
                if (reslist.Find(item => item.ID == resID).IsDeleted == true)
                {
                    rdbtnlistStatus.Items[1].Selected = true;
                }
                else
                {
                    rdbtnlistStatus.Items[0].Selected = true;
                }
                List<Res_Opt_Junc> res_opt_junlist = new List<Res_Opt_Junc>();
                Res_Opt_JuncHandler rsopt = new Res_Opt_JuncHandler();
                res_opt_junlist = rsopt.GetRes_Opt_JuncListByResID(resID);
                if (res_opt_junlist != null)
                {
                    for (int i = 0; i < res_opt_junlist.Count; i++)
                    {
                        if (res_opt_junlist[i].IsActive == true)
                        {
                            chkblResourceNewOptions.Items.FindByValue(res_opt_junlist[i].Opt_id.ToString()).Selected = true;
                        }
                    }
                }
            }
        }

        protected void btnEditResource_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Resource res = new Resource();
                LocID = (int)ViewState["LocID"];
                optlist = (List<Option>)ViewState["optionList"];
                if (drpChoosToEditResource.Enabled == true)
                    res.ID = Convert.ToInt32(drpChoosToEditResource.SelectedValue);
                else
                    throw new Exception("نام قبلی منبع انتخاب نشده است");
                res.Name = txtResourceNewName.Text;
                res.Location = LocID.ToString();
                res.Capacity = Convert.ToInt32(txtCapacity.Text);
                res.Description = txtResourceNewDescription.Text;
                res.CategoryID = Convert.ToInt32(drpChooseToEditCategory.SelectedValue);
                res.IsDeleted = Convert.ToBoolean(rdbtnlistStatus.SelectedValue);
                ResourceHandler rs = new ResourceHandler();
                var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                if (optlist != null && chkblResourceNewOptions.SelectedIndex != -1)
                {
                    rs.UpdateResource(res, optlist, userId);
                }
                else if (optlist == null && chkblResourceNewOptions.SelectedIndex != -1)
                {
                    rs.UpdateResource(res, userId);
                }
                else
                {
                    throw new Exception();
                }
                ClearControl();
                BindData();
                string scrp = res.Name + "با موفقیت ویرایش یافت. ";
                RadWindowManager1.RadAlert(scrp, 0, 100, " پیام سیستم", "");
            }
        }

        protected void chkblResourceNewOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            optlist = new List<Option>();
            foreach (ListItem item in chkblResourceNewOptions.Items)
            {
                Option optstat = new Option();
                optstat.ID = Convert.ToInt32(item.Value);
                optstat.Name = item.Text;
                optstat.IsActive = item.Selected;
                optlist.Add(optstat);
            }
            ViewState.Add("optionList", optlist);
        }

        public void ClearControl()
        {
            drpResourceNewLocation.Items.Clear();
            drpChooseToEditCategory.Items.Clear();
            drpChoosToEditResource.Items.Clear();
            txtCapacity.Text = string.Empty;
            chkblResourceNewOptions.Items.Clear();
            txtResourceNewDescription.Text = string.Empty;
            txtResourceNewName.Text = string.Empty;
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (chkblResourceNewOptions.SelectedIndex != -1)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
        private void LoadListControl<T>(ListControl listcont, List<T> list, string txtfield, string valfield)
        {
            listcont.DataSource = list;
            listcont.DataTextField = txtfield;
            listcont.DataValueField = valfield;
            listcont.DataBind();
            ListItem item = new ListItem();
            item.Text = "انتخاب کنید";
            item.Value = "0";
            if (listcont is DropDownList)
            {
                listcont.Items.Insert(0, item);
            }
        }

        protected void drpResourceNewLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocID = Convert.ToInt32(drpResourceNewLocation.SelectedValue);
            ViewState.Add("LocID", LocID);
            if (drpResourceNewLocation.SelectedIndex != 0)
            {
                CategoryHandler catHandler = new CategoryHandler();
                List<Category> catList = catHandler.getCategoryListByLocId(LocID);

                LoadListControl<Category>(drpChooseToEditCategory, catList, "name", "ID");
            }
            else
            {

            }
        }

        protected void btnDeleteResource_OnClick(object sender, EventArgs e)
        {
            if (!IsValid) return;
            var resourceId = Convert.ToInt32(drpChoosToEditResource.SelectedValue);
            var resHandler = new ResourceHandler();
            var isUsed = resHandler.IsSourceUsed(resourceId);
            if (isUsed)
            {
                string scrp = "به دلیل استفاده شدن این منبع در درخواست ها امکان انجام این کار وجود ندارد";
                RadWindowManager1.RadAlert(scrp, 0, 100, " پیام سیستم", "");
            }
            else
            {
                var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                resHandler.DeleteResource(resourceId, userId);
                ClearControl();
                BindData();
                string scrp = "درخواست شما با موفقیت انجام پذیرفت";
                RadWindowManager1.RadAlert(scrp, 0, 100, " پیام سیستم", "");
            }
        }
    }
}