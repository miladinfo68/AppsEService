using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;

namespace ResourceControl.PL.Admin
{
    public partial class AdminEditCategory : System.Web.UI.Page
    {
        List<Category> allcats = new List<Category>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                ViewState.Add("catlist", allcats);
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
            CategoryHandler cth = new CategoryHandler();
            allcats = cth.GetCategoryList();
            ViewState["catlist"] = allcats;
            drpChooseCategory.ClearSelection();
            drpChooseCategory.DataTextField = "name";
            drpChooseCategory.DataValueField = "ID";
            drpChooseCategory.DataSource = allcats;

            drpChooseCategory.DataBind();
            drpChooseCategory.Items.Insert(0, "انتخاب کنید");
        }

        protected void drpChooseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpChooseCategory.SelectedIndex != 0)
            {
                DataBind();
                allcats = ViewState["catlist"] as List<Category>;
                if (allcats == null) return;
                var cat = allcats.Find(item => item.ID == (Convert.ToInt32(drpChooseCategory.SelectedValue)));
                txtDescriptionOld.Text = cat.Description;
            }
            else
            {
                txtDescriptionOld.Text = "";
            }
        }

        protected void btnEditCategory_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (!string.IsNullOrWhiteSpace(txtDecriptionNew.Text) && !string.IsNullOrWhiteSpace(txtNameNew.Text))
                {
                    Category cat = new Category();
                    cat.ID = Convert.ToInt32(drpChooseCategory.SelectedValue);
                    cat.Name = txtNameNew.Text;
                    cat.Description = txtDecriptionNew.Text;
                    CategoryHandler cth = new CategoryHandler();
                    var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                    cth.UpdateCategory(cat, userId);

                    BindData();
                    txtNameNew.Text = "";
                    txtDecriptionNew.Text = "";
                    txtDescriptionOld.Text = "";
                }
                else
                {
                    throw new Exception("name field must be filled");
                }
            }
        }
    }
}
