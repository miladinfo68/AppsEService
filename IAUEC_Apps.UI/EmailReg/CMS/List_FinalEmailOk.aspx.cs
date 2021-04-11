using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class List_FinalEmailOk : System.Web.UI.Page
    {
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
            AccessControl1.MenuId = menuId;
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();

        }

        protected void grd_ListFinalEmailOk_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            Email_ClassBusiness EmailBusiness = new Email_ClassBusiness();
            grd_ListFinalEmailOk.DataSource = EmailBusiness.GiveList_Status(4);
            
        }
    }
}