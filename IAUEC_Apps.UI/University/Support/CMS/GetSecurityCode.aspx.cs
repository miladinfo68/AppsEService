using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Support.CMS
{
    public partial class GetSecurityCode : System.Web.UI.Page
    {
        CommonBusiness ProfCommonBusiness = new CommonBusiness();
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
            AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
        }

        protected void btn_FindSecurityCode_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(txtCodeMeli.Text))
                {
                    DataTable FetchSecurityCodRow = ProfCommonBusiness.GetInfoPeoByCodeMeliINAspNetUsers(txtCodeMeli.Text);
                    if (FetchSecurityCodRow.Rows.Count>0)
                    {
                        divSecurityCode.Visible = true;
                        lblName.Text = FetchSecurityCodRow.Rows[0]["name"].ToString();
                        lblFamily.Text = FetchSecurityCodRow.Rows[0]["family"].ToString();
                        lblCodeMeli.Text = FetchSecurityCodRow.Rows[0]["idd_meli"].ToString();
                        lblSecuritCode.Text = FetchSecurityCodRow.Rows[0]["Securityid"].ToString();
                    }
                    else
                    {
                        divAlarm.Visible = true;
                        if (divSecurityCode.Visible == true)
                        {
                            divSecurityCode.Visible = false;
                        }
                    }
                }

            }
        }
    }
}