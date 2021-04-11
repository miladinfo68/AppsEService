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
    public partial class List_EmailNotOk : System.Web.UI.Page
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

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Email_ClassBusiness em = new Email_ClassBusiness();

            grd_ListEmailNotOk.DataSource = em.GiveList_Status(2);
                
        }
        protected void ExportToExcelImg_Click(object sender, ImageClickEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();

            string alternateText = (sender as ImageButton).AlternateText;
            grd_ListEmailNotOk.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            grd_ListEmailNotOk.ExportSettings.IgnorePaging = true;
            grd_ListEmailNotOk.ExportSettings.ExportOnlyData = true;
            grd_ListEmailNotOk.ExportSettings.OpenInNewWindow = true;
            grd_ListEmailNotOk.ExportSettings.UseItemStyles = true;
            grd_ListEmailNotOk.ExportSettings.FileName = "FailedListReport";
            grd_ListEmailNotOk.MasterTableView.ExportToExcel();

            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 19, "لیست ایمیل های تایید نشده در سامانه ایمیل");

        }
    }
}