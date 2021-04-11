using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class professorsByStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            manageAccessControl();
                getProfessors();
                grdProfessors.DataBind();
                PersiaFiltering();
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
            else
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
        }


        private void getProfessors()
        {
            if (ddlEvent.SelectedItem.Value != "")
            {
                Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
                var dt=bsn.getProfessorsByModifyType(Convert.ToInt32(ddlEvent.SelectedItem.Value), fromDate.Text, toDate.Text);
                grdProfessors.DataSource = dt;
            }
        }
        protected void PersiaFiltering()
        {
            Telerik.Web.UI.GridFilterMenu menu = grdProfessors.FilterMenu;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getProfessors();

            grdProfessors.DataBind();
        }

        protected void grdProfessors_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() != "" && e.CommandName== "showDetail")
            {
                Response.Redirect("ShowDetailInfo.aspx?ID=" + int.Parse(e.CommandArgument.ToString()));
            }
        }

        protected void grdProfessors_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getProfessors();
        }
    }
}