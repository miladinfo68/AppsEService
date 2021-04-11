using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class SynchronizeSystem : System.Web.UI.Page
    {
        AssetBusiness assetb = new AssetBusiness();
        protected void Page_Load(object sender, EventArgs e)
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
            AccessControl1.MenuId = menuId;
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
        }
        protected void btn_synch_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            //assetb.GetAllUrlsByTerm(ConfigurationManager.AppSettings["Term"].ToString(), int.Parse(drp_Day.SelectedValue), txt_Date.Text);
            assetb.GetAllUrlsByTerm(ConfigurationManager.AppSettings["Term"].ToString(), date_input_1.Value.ToString(), date_input_2.Value.ToString());
            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 26, "");

        }
    }
}