using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using System.Data;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutDenyRequestReport : System.Web.UI.Page
    {
        CommonBusiness business = new CommonBusiness();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                //    Session["MenuId"] = menuId;
                //}
                //AccessControl.MenuId = Session["MenuId"].ToString();
                //AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();   
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sDate = txtSdate.Text;
            string eDate = txtEdate.Text;
            int appID = 0;
            sDate = sDate.Substring(2, sDate.Length - 2);
            eDate = eDate.Substring(2, eDate.Length - 2);
            dt = business.getDenyRequestInfo(sDate, eDate, appID);
            grdResult.DataSource = dt;
            grdResult.DataBind();
        }
    }
}