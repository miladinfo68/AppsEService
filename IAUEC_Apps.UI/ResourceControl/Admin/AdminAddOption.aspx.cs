using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;

namespace ResourceControl.PL.Admin
{
    public partial class AdminAddOptions : System.Web.UI.Page
    {
        List<Option> optlist = new List<Option>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                //string mId = Request.QueryString["id"].ToString();
                //string[] id = mId.ToString().Split(new char[] { '@' });
                //string menuId = "";
                //for (int i = 0; i < id[1].Length; i++)
                //{
                //    string s = id[1].Substring(i + 1, 1);
                //    if (s != "-")
                //        menuId += s;
                //    else
                //        break;
                //}
                //Session["menuId"] = menuId;
                //AccessControl1.MenuId = menuId;
                //AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
            optlist = ViewState["optlist"] as List<Option>;
        }
        private void BindData()
        {
            OptionHandler opt = new OptionHandler();
            optlist = opt.GetOptionList();
            grdOptionList.DataSource = optlist;
            grdOptionList.DataBind();
            ViewState.Add("optlist", optlist);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Option opti = new Option();
                optlist = ViewState["optlist"] as List<Option>;
                opti.Name = txtOptionName.Text;
                if (optlist != null)
                {
                    if (optlist.Exists(item => item.Name == opti.Name))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('این نام دسته بندی از قبل موجود است !');", true);
                        return;
                    }
                }

                OptionHandler opt = new OptionHandler();
                var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                opt.AddNewOption(opti, userId);
                //optlist.Add(opti);
                txtOptionName.Text = "";
                BindData();
            }
        }
    }
}