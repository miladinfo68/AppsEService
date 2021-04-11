using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class List_EmailOk : System.Web.UI.Page
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

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            Email_ClassBusiness em = new Email_ClassBusiness();
            grd_ListEmailOk.DataSource = em.GiveList_Status(3);
            
        }
        protected void ExportToExcelImg_Click(object sender, ImageClickEventArgs e)
        {

            CommonBusiness cmnb = new CommonBusiness();

            string alternateText = (sender as ImageButton).AlternateText;
            grd_ListEmailOk.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            grd_ListEmailOk.ExportSettings.IgnorePaging = true;
            grd_ListEmailOk.ExportSettings.ExportOnlyData = true;
            grd_ListEmailOk.ExportSettings.OpenInNewWindow = true;
            grd_ListEmailOk.ExportSettings.UseItemStyles = true;
            grd_ListEmailOk.ExportSettings.FileName = "ConfirmListReport";
            grd_ListEmailOk.MasterTableView.ExportToExcel();

            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 19, "لیست ایمیل های تایید شده در سامانه ایمیل");

        }
    }
}