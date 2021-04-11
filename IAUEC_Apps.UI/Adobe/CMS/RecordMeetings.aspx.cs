using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Adobe;
using System.Data;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class RecordMeetings : System.Web.UI.Page
    {
        CommonBusiness cmb = new CommonBusiness();
        RecordsBusiness rb = new RecordsBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rcbTerm.DataTextField = "Term";
                rcbTerm.DataValueField = "Term";
                rcbTerm.DataSource = cmb.getActiveTerm_AdobeConnection();
                rcbTerm.DataBind();
                rcbTerm.Items.Insert(0, new RadComboBoxItem("انتخاب نمایید"));
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
                Session[sessionNames.menuID] = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            }

        }
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            //int time = int.Parse(txt_TimeOver.Text);
            if (txt_Date.Text != "" && txt_Code.Text != "")
            {
                var code = (txt_Code.Text);
                grd_Meetings.DataSource = rb.MakeOffLineClassWithLessonCodeByCodeClassAndTime(code, txt_Date.Text,  rcbTerm.SelectedValue);
                grd_Meetings.Rebind();
            }
            else
            if (txt_Date.Text != "")
            {
                grd_Meetings.DataSource = rb.MakeOffLineClassWithLessonCodeByCodeClassAndTime("0", txt_Date.Text, rcbTerm.SelectedValue);
                grd_Meetings.Rebind();
            }
            else
            {
                var code = (txt_Code.Text);

                grd_Meetings.DataSource = rb.MakeOffLineClassWithLessonCodeByCodeClassAndTime(code, "nd",  rcbTerm.SelectedValue);
                grd_Meetings.Rebind();
            }

        }
        protected void grd_Meetings_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (txt_Date.Text != "" && txt_Code.Text != "")
            {
                var code = (txt_Code.Text);
                grd_Meetings.DataSource = rb.MakeOffLineClassWithLessonCodeByCodeClassAndTime(code, txt_Date.Text, rcbTerm.SelectedValue);
               
            }
            else
                if (txt_Date.Text != "")
                {

                    grd_Meetings.DataSource = rb.MakeOffLineClassWithLessonCodeByCodeClassAndTime("0", txt_Date.Text, rcbTerm.SelectedValue);
                   
                }
                else if (txt_Code.Text != "")
                {
                    var code = (txt_Code.Text);

                    grd_Meetings.DataSource = rb.MakeOffLineClassWithLessonCodeByCodeClassAndTime(code, "nd", rcbTerm.SelectedValue);
                   
                }
        }
        protected void ExportToExcelImg_Click(object sender, ImageClickEventArgs e)
        {
            string alternateText = (sender as ImageButton).AlternateText;
            grd_Meetings.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            grd_Meetings.ExportSettings.IgnorePaging = true;
            grd_Meetings.ExportSettings.ExportOnlyData = true;
            grd_Meetings.ExportSettings.OpenInNewWindow = true;
            grd_Meetings.ExportSettings.UseItemStyles = true;
            grd_Meetings.ExportSettings.FileName = "ConfirmListReport";
            grd_Meetings.MasterTableView.ExportToExcel();
        }
    }
}