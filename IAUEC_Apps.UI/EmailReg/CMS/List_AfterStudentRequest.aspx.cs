using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;
using System.Data;
using IAUEC_Apps.DTO.EmailClasses;

using IAUEC_Apps.Business.university.Support;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class List_AfterStudentRequest : System.Web.UI.Page
    {
        StudentBuisiness SB = new StudentBuisiness();
        PassProfessorBusiness EmailBusiness = new PassProfessorBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        CommonBusiness EmailCommonBusiness = new CommonBusiness();
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
            }
            AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();

           
        }
        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            Email_ClassBusiness em = new Email_ClassBusiness();
            grd_ListAfterStdRequest.DataSource = em.GiveList_Status(1);
           
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "ok")
            {
                Session["RequestID"] = e.CommandArgument;
                RadWindowManager1.RadConfirm("آیا مطمئن هستید پست الکترونیکی ثبت شود؟"
               , "CallBackConfirm", 250, 50, null, "پیام");

             
            }
            else if (e.CommandName == "Notok")
            {
                Session["RequestID"] = e.CommandArgument;
                Session["Admin"] = "IAdmin";
                Response.Redirect("RequestRejected.aspx");
            }
        }

        protected void ExportToExcelImg_Click(object sender, ImageClickEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();

            string alternateText = (sender as ImageButton).AlternateText;
            grd_ListAfterStdRequest.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            grd_ListAfterStdRequest.ExportSettings.IgnorePaging = true;
            grd_ListAfterStdRequest.ExportSettings.ExportOnlyData = true;
            grd_ListAfterStdRequest.ExportSettings.OpenInNewWindow = true;
            grd_ListAfterStdRequest.ExportSettings.UseItemStyles = true;
            grd_ListAfterStdRequest.ExportSettings.FileName = "FailedListReport";
            grd_ListAfterStdRequest.MasterTableView.ExportToExcel();
            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 19, "لیست ایمیل ها پس از درخواست دانشجو در سامانه ایمیل");
        }
    }
}