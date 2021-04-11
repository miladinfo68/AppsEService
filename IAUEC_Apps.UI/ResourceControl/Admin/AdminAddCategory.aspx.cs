using System;
using System.Collections.Generic;
using ResourceControl.Entity;
using ResourceControl.BLL;

namespace ResourceControl.PL.Admin
{
    public partial class AdminAddCategory : System.Web.UI.Page
    {
        List<Category> allcats = new List<Category>();
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
            CategoryHandler cth = new CategoryHandler();
            allcats = cth.GetCategoryList();
            grdCategoryList.DataSource = allcats;
            grdCategoryList.DataBind();
            ViewState.Add("catlist", allcats);
        }

        protected void btnAddCategory_Click1(object sender, EventArgs e)
        {
            if (IsValid)
            {
                allcats = ViewState["catlist"] as List<Category>;
                Category cat = new Category();
                cat.Name = txtCategoryName.Text;
                cat.Description = txtDescription.Text;
                if (allcats != null)
                {
                    if (allcats.Exists(item => item.Name == cat.Name))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('این نام دسته بندی از قبل موجود است !');", true);
                        return;
                    }
                }

                CategoryHandler cth = new CategoryHandler();
                var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                cth.AddNewCategory(cat, userId);

                txtCategoryName.Text = "";
                txtDescription.Text = "";
                BindData();
            }
        }
    }
}