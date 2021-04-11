using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;

namespace ResourceControl.PL.Admin
{
    public partial class AdminEditOption : System.Web.UI.Page
    {
        List<Option> optlist = new List<Option>();
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
            List<string> v = new List<string>();

            OptionHandler opt = new OptionHandler();
            optlist = opt.GetOptionList();
            drpChooseOption.DataSource = optlist;
            drpChooseOption.DataTextField = "name";
            drpChooseOption.DataValueField = "ID";
            drpChooseOption.DataBind();
        }
        protected void btnEditOption_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Option opti = new Option();
                opti.ID = Convert.ToInt32(drpChooseOption.SelectedValue);
                opti.Name = txtOptionNewName.Text;
                OptionHandler opt = new OptionHandler();
                var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                opt.UpdateOption(opti, userId);
                BindData();
                txtOptionNewName.Text = "";
            }
        }

        protected void btnDeleteOption_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Option opti = new Option();
                opti.ID = Convert.ToInt32(drpChooseOption.SelectedValue);
                OptionHandler opt = new OptionHandler();
                var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                opt.DeleteOption(opti.ID, userId);
                BindData();
                txtOptionNewName.Text = "";
            }
        }
    }
}