using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResourceControl.PL.Forms
{
    public partial class Admin : System.Web.UI.Page
    {
        private List<Resource> reslist = new List<Resource>();
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
            List<Category> allcats = new List<Category>();
            CategoryHandler cth = new CategoryHandler();
            allcats = cth.GetCategoryList();
            LoadListControl<Category>(drpChooseCategory,allcats  , "name", "ID");
                               
            List<Option> options = new List<Option>();
            OptionHandler opt = new OptionHandler();
            options = opt.GetOptionList();
            LoadListControl<Option>(chkblSelecetOptions, options, "name", "ID");

            LocationHandler locH = new LocationHandler();
            List<Location> loclist = locH.GetAllLocation();
            drpNewResLocation.DataSource = loclist;
            drpNewResLocation.DataTextField = "name";
            drpNewResLocation.DataValueField = "id";
            drpNewResLocation.DataBind();

            List<Resource> reslist = new List<Resource>();
            ResourceHandler rs = new ResourceHandler();
            reslist = rs.GetResourceList();
            grdResourceList.DataSource = reslist;
            grdResourceList.DataBind();        
        }
        private void LoadListControl<T>(ListControl listcont , List<T> list,string txtfield,string valfield) 
        {
            listcont.DataSource = list;
            listcont.DataTextField = txtfield;
            listcont.DataValueField = valfield;
            listcont.DataBind();
            if (listcont is DropDownList)
            {
                listcont.Items.Insert(0, "انتخاب کنید");
            }        
        }
        protected void btnAddResource_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var gridview = grdResourceList.Rows
                    .Cast<GridViewRow>()
                    .Where(item => item.Cells[1].Text == txtAddNewResource.Text 
                        && item.Cells[2].Text == drpNewResLocation.SelectedItem.ToString())
                    .Any();

                if (gridview == false)
                {
                    Resource res = new Resource();
                    res.Name = txtAddNewResource.Text;
                    res.Location = drpNewResLocation.SelectedValue;                    
                    res.Capacity = Convert.ToInt32(txtCapacity.Text);
                    res.Description = txtNewDescription.Text;
                    res.CategoryID = Convert.ToInt32(drpChooseCategory.SelectedValue);
                    List<Option> optlist = new List<Option>();
                    foreach (ListItem item in chkblSelecetOptions.Items)
                    {
                        if (item.Selected)
                        {
                            Option opt = new Option();
                            opt.ID = Convert.ToInt32(item.Value);
                            opt.Name = item.Text;
                            optlist.Add(opt);
                        }
                    }
                    ResourceHandler rs = new ResourceHandler();
                    var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                    rs.AddNewResource(res, optlist,userId);
                    ClearControl(this);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('این نام کلاس از قبل موجود است !');", true);
                    return;
                }
                
                BindData();
            }            
        }
        public static void ClearControl(Control control)
        {
            var textbox = control as TextBox;
            if (textbox != null)
                textbox.Text = string.Empty;

            var dropDownList = control as DropDownList;
            if (dropDownList != null)
                dropDownList.SelectedIndex = 0;

            var checkboxlist = control as CheckBoxList;
            if (checkboxlist != null)
                checkboxlist.ClearSelection();

            foreach (Control childControl in control.Controls)
            {
                ClearControl(childControl);
            }
        }
        protected void grdResourceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {               
                int resID = Convert.ToInt32(grdResourceList.DataKeys[e.Row.RowIndex].Value);
                OptionHandler opt = new OptionHandler();
                List<Option> optlist = opt.GetOptionListByResID(resID);
                if (optlist != null)
                {
                    BulletedList bltOption = (BulletedList)e.Row.FindControl("bltOption");
                    bltOption.DataSource = optlist;
                    bltOption.DataBind();
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (chkblSelecetOptions.SelectedIndex!=-1)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}