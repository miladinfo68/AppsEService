using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.ResourceControl.Admin
{
    public partial class AdminEditLocation : System.Web.UI.Page
    {
        List<Location> loclist = new List<Location>();
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
            LocationHandler locH = new LocationHandler();
            List<Location> loclist = locH.GetAllLocation();
            drpChooseLocation.DataSource = loclist;
            drpChooseLocation.DataTextField = "name";
            drpChooseLocation.DataValueField = "name";
            drpChooseLocation.DataBind();
            drpChooseLocation.Items.Insert(0, "انتخاب کنید");
            ViewState.Add("loclist", loclist);
        }

        protected void drpChooseLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpChooseLocation.SelectedIndex!=0)
	        {
                loclist = ViewState["loclist"] as List<Location>;
                Location loc = new Location();
                loc = loclist.Find(item => item.Name == drpChooseLocation.SelectedValue);
                txtDescriptionOld.Text = loc.Address;
	        }
        }

        protected void btnEditLocation_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (!string.IsNullOrWhiteSpace(txtDecriptionNew.Text) && !string.IsNullOrWhiteSpace(txtNameNew.Text))
                {
                    Location loc = new Location();
                    loc.Name = txtNameNew.Text;
                    loc.Address = txtDecriptionNew.Text;
                    LocationHandler locH = new LocationHandler();
                    string oldname = drpChooseLocation.SelectedValue;
                    locH.UpdateLocation(loc, oldname);
                    BindData();
                    //string scrp = "Alert('');"
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClientID, scrp, true);
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