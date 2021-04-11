using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.DTO.AdobeClasses;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class ViewFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                Session[sessionNames.menuID] = menuId;
                CommonBusiness cmb = new CommonBusiness();
                DDL_Term.DataTextField = "Term";
                DDL_Term.DataValueField = "Term";
                DDL_Term.DataSource = cmb.getActiveTerm_AdobeConnection();
                DDL_Term.DataBind();
            
              
            }
            AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            GridFilterMenu menu = grd_ViewFiles.FilterMenu;
            foreach (RadMenuItem item in menu.Items)
            {    //change the text for the "StartsWith" menu item  
                if (item.Text == "StartsWith")
                {
                    item.Text = "شروع با";
                    //item.Remove();
                }
            }
            int im = 0;
            while (im < menu.Items.Count)
            {
                if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo" || menu.Items[im].Text == "GreaterThan" || menu.Items[im].Text == "LessThan")
                {
                    im++;
                }
                else
                {
                    menu.Items.RemoveAt(im);
                }
            }
           
        }

       

        protected void grd_ViewFiles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<CMSViewFilesDTO> cmsvf = new List<CMSViewFilesDTO>();
            AssetBusiness adobebus = new AssetBusiness();

            cmsvf = adobebus.GetAllAssetByTermAndDaneshId(DDL_Term.SelectedValue, 0);
                grd_ViewFiles.DataSource = cmsvf;
            
        }

        protected void grdViewFiles_PreRender(object sender, EventArgs e)
        {
            //if (Session["RoleId"].ToString() == "1" || Session["RoleId"].ToString() == "6")
            //    grd_ViewFiles.MasterTableView.GetColumn("namedanesh").Visible = true;
            //else
            //    grd_ViewFiles.MasterTableView.GetColumn("namedanesh").Visible = false;
        
        }

        protected void DDLTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                //GridItem cmdItem = grd_ViewFiles.MasterTableView.GetItems(GridItemType.)[0];
       
        }

        protected void DDL_Term_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            List<CMSViewFilesDTO> cmsvf = new List<CMSViewFilesDTO>();
            AssetBusiness adobebus = new AssetBusiness();

            cmsvf = adobebus.GetAllAssetByTermAndDaneshId(DDL_Term.SelectedValue, 0);
                grd_ViewFiles.DataSource = cmsvf;
                grd_ViewFiles.DataBind();
        }
    }
}