using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.ResourceControl
{
    public partial class AdminAddLocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
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

        private void BindGrid()
        {
            LocationHandler loch = new LocationHandler();
            List<Location> loclist = loch.GetAllLocation();
            grdLocationList.DataSource = loclist;
            grdLocationList.DataBind();
            ViewState.Add("loclist", loclist);

        }

        protected void btnAddLocation_Click1(object sender, EventArgs e)
        {
            var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            if (IsValid)
            {
                List<Location> loclist = (List<Location>)ViewState["loclist"];
                Location loc = new Location();
                loc.Name = txtLocationName.Text;
                loc.Address = txtAddress.Text;
                bool found = false;
                if (grdLocationList.Rows.Count > 0)
                {
                    foreach (GridViewRow row in grdLocationList.Rows)
                    {
                        if (row.Cells[0].Text == loc.Name || row.Cells[1].Text == loc.Address)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('این محل از قبل موجود است !');", true);
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        LocationHandler locH = new LocationHandler();

                        locH.AddNewLocation(loc, userId);
                        txtLocationName.Text = "";
                        txtAddress.Text = "";
                        BindGrid();
                    }
                }
                else
                {
                    LocationHandler locH = new LocationHandler();
                    locH.AddNewLocation(loc, userId);
                    txtLocationName.Text = "";
                    txtAddress.Text = "";
                    BindGrid();
                }
            }
        }
    }
}