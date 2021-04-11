using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class DlAnswerSheet : System.Web.UI.Page
    {

        DataTable ExaminerExamPlace = new DataTable();
        bool IsTehranExaminer = false;
        ExamBusiness EBusiness = new ExamBusiness();
        CommonBusiness CB = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtExamDate = new DataTable();
                dtExamDate = EBusiness.Get_Exam_dateexam(true);
                ddl_ExamDate.DataSource = dtExamDate;
                ddl_ExamDate.DataTextField = "dateexam";
                ddl_ExamDate.DataValueField = "dateexam";
                ddl_ExamDate.DataBind();
                ddl_ExamDate.Items.Insert(0, new ListItem("انتخاب کنید"));
            }
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
                Session[sessionNames.menuID] = menuId;
            }
            AccessControl.MenuId = Session[sessionNames.menuID].ToString();
            AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
            IsTehranExaminer = EBusiness.ListExaminerExamPlace(ConfigurationManager.AppSettings["Exam_Term"].ToString(), Convert.ToInt32(Session[sessionNames.userID_Karbar]))
                               .AsEnumerable()
                               .Where(w => w.Field<int>("ExamPlaceID") == 1 || w.Field<int>("ExamPlaceID") == 41).Count() > 0;
        }
        protected void grd_CourseList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        protected void grd_CourseList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                GridDataItem item = (GridDataItem)e.Item;
                //Button btn_Dl = e.Item.FindControl("btn_Dl") as Button;
                Button btn_printExamSheet = e.Item.FindControl("btn_printExamSheet") as Button;
                //#############################
                Button btn_printExamSheetByA4Format = e.Item.FindControl("btn_printExamSheetByA4Format") as Button;
                //#############################
                //Label lbl_Password = e.Item.FindControl("lbl_Password") as Label;
                //HiddenField hdn_Pass = e.Item.FindControl("hdn_Pass") as HiddenField;
                CheckBox chk2 = e.Item.FindControl("chk2") as CheckBox;
                CheckBox chk3 = e.Item.FindControl("chk3") as CheckBox;
                Button btn_printtestSheet = e.Item.FindControl("btn_chk") as Button;
                Label lblNoAnswerSheet = e.Item.FindControl("lblNoAnswerSheet") as Label;

                //if (chk2.Checked && chk3.Checked)
                //{
                //    //btn_printtestSheet.Visible = true;
                //    btn_printExamSheet.Visible = true;
                //}
                if (chk2.Checked)
                {
                    btn_printExamSheet.Visible = true;
                    btn_printExamSheetByA4Format.Visible = true;
                }
                else
                    lblNoAnswerSheet.Visible = true;
                //if (chk3.Checked)
                //    btn_printtestSheet.Visible = true;
                //TableCell cell = dataItem["did"];
                //string tt = (hdn_Pass.Value.ToString());
                //byte[] str = Convert.FromBase64String(tt);

                //string pass = EncryptionClass.DecryptRJ256(str);
                //lbl_Password.Text = pass;
                //btn_Dl.CommandArgument = cell.Text;
                btn_printExamSheet.CommandArgument = dataItem["did"].Text; //cell.Text;
                btn_printExamSheetByA4Format.CommandArgument = dataItem["did"].Text;

                if (!IsTehranExaminer)
                    btn_printExamSheet.Visible = false;
            }
        }


        protected void grd_CourseList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            ExamBusiness EBusiness = new ExamBusiness();
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            var ip = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
            && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
           ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
           : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (ip.Contains(","))
                ip = ip.Split(',').First();
            //DataTable dtdet = new DataTable();
            // dtdet = EBusiness.Get_ExamdetailbyDid(int.Parse(e.CommandArgument.ToString()));

            if (e.CommandName == "ExamSheet" || e.CommandName == "ExamSheetA4")
            {


                var did = e.CommandArgument.ToString();

                var whichBtn = (e.CommandSource as Button).ID;


                DataTable dt1 = new DataTable();
                dt1 = EBusiness.ExamAnswerSheetbyDid(did, int.Parse(Session[sessionNames.userID_Karbar].ToString()));

                StiReport rpt = new StiReport();

                string targetReport = "";
                if (whichBtn == "btn_printExamSheet")
                {
                    targetReport = Server.MapPath("../Reports/AnswerSheet1.mrt");
                }
                else if (whichBtn == "btn_printExamSheetByA4Format")
                {
                    targetReport = Server.MapPath("../Reports/AnswerSheetA4.mrt");
                }
                rpt.Load(targetReport);
                rpt.Dictionary.Databases.Clear();
                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Supplementary", CB.ReportConnection.ToString()));


                if (whichBtn == "btn_printExamSheet")
                {
                    rpt.ReportName = $"{did}-{ddl_ExamDate.SelectedValue}-{ddl_Saate.SelectedValue}-A3";
                }
                else if (whichBtn == "btn_printExamSheetByA4Format")
                {
                    rpt.ReportName = $"{did}-{ddl_ExamDate.SelectedValue}-{ddl_Saate.SelectedValue}-A4";
                }
                rpt.ReportCacheMode = StiReportCacheMode.Off;
                rpt.Compile();
                rpt.CompiledReport.DataSources["[Exam].[SP_ExamAnswerSheetbyDid]"].Parameters["@did"].ParameterValue = did;
                rpt.CompiledReport.DataSources["[Exam].[SP_ExamAnswerSheetbyDid]"].Parameters["@ExaminerID"].ParameterValue = int.Parse(Session[sessionNames.userID_Karbar].ToString());
                rpt.CompiledReport.DataSources["[Exam].[SP_ExamAnswerSheetbyDid]"].Parameters["@examDate"].ParameterValue = ddl_ExamDate.SelectedValue;
                rpt.CompiledReport.DataSources["[Exam].[SP_ExamAnswerSheetbyDid]"].Parameters["@examTime"].ParameterValue = ddl_Saate.SelectedValue;

                rpt.RegData(dt1);




                StiReportResponse.ResponseAsPdf(this.Page, rpt, true);


                CommonBusiness cmnb = new CommonBusiness();
                CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 68, "چاپ پاسخنامه", int.Parse(e.CommandArgument.ToString()));



            }

        }
        //######################################
        protected void btn_Show_Click(object sender, EventArgs e)
        {
            if (ddl_ExamDate.SelectedIndex != 0)
            {
                ExamBusiness EBusiness = new ExamBusiness();
                DataTable dt = new DataTable();
                dt = EBusiness.AnswerSheetForDLByDate_Saat(int.Parse(Session[sessionNames.userID_Karbar].ToString()), ddl_Saate.SelectedValue.ToString(), ddl_ExamDate.SelectedValue.ToString());//IDshahr dade shavad
                if (dt.Rows.Count > 0)
                {
                    grd_CourseList.DataSource = dt;
                    grd_CourseList.DataBind();
                }
                else { rwm.RadAlert("دراین سانس پاسخنامه موجود نیست", null, 100, "پیام", ""); }
            }
            else
            {
                rwm.RadAlert("روز امتحان را انتخاب نمایید", null, 100, "پیام", "");
            }


        }

        protected void ddl_ExamDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExamBusiness EBusiness = new ExamBusiness();
            DataTable dtsaat = new DataTable();
            dtsaat = EBusiness.GetSaatExamByDateExam(ddl_ExamDate.SelectedValue ,true);
            ddl_Saate.DataSource = dtsaat;
            ddl_Saate.DataTextField = "saatexam";
            ddl_Saate.DataBind();
        }

    }
}