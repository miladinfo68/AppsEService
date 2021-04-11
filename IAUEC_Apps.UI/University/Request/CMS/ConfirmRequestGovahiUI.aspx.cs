using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Stimulsoft.Report;
using System.Drawing;
using IAUEC_Apps.Business.university.GraduateAffair;

using Stimulsoft.Report.Dictionary;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class ConfirmRequestGovahiUI : System.Web.UI.Page
    {
        //CommonDAO dao = new CommonDAO();
        RequestStudentCartBusiness business = new RequestStudentCartBusiness();
        /// <summary>
        ///ایجاد نموده ایم CommonBusiness یک شئ از کلاس 
        /// </summary>
        CommonBusiness CB = new CommonBusiness();
        /// <summary>
        ///درخواست های ارسال گواهی در این جدول ذخیره می گردد
        /// </summary>
        DataTable dt = new DataTable();
        DataTable mData = new DataTable();
        /// <summary>
        ///ایجاد نموده ایم RequestGovahiBusiness یک شئ از کلاس
        /// </summary>
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        /// <summary>
        ///ایجاد نموده ایم RequestStudentCartBusiness یک شئ از کلاس
        /// </summary>
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();
        UniversityBusiness unibusiness = new UniversityBusiness();
        GraduateReportBusiness GraduateBusiness = new GraduateReportBusiness();

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
                AccessControl1.MenuId = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
        }
        /// <summary>
        /// این متد محتوای گیرید که درخواست ارسال گواهی می باشد را فراهم می نماید
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void grd_GovahiRequest_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            dt = GovahiBusiness.GetGovahiRequest(7, 6);
            if (dt.Rows.Count > 0)
            {
                grd_GovahiRequest.DataSource = dt;
            }
            else
            {
                grd_GovahiRequest.Visible = false;
                rwmValidations.RadAlert("درخواستی موجود نیست", null, 100, "پیغام", "");
                StiWebViewer1.Visible = false;
            }
            GridFilterMenu menu = grd_GovahiRequest.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }

                }
            }

        }
        /// <summary>
        /// با فشردن کلید تایید یا رد درخواست ارسال گواهی اشتغال به تحصیل، این متد فعال می گردد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void grd_GovahiRequest_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            if (e.CommandName == "radedarkhast")
            {
                RadWindowManager windowManager = new RadWindowManager();
                RadWindow widnow1 = new RadWindow();
                widnow1.NavigateUrl = "../CMS/InsertRejectDescription.aspx?params=" + e.CommandArgument.ToString();
                widnow1.ID = "RadWindow1";
                windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(500);
                windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                widnow1.VisibleOnPageLoad = true;
                windowManager.Windows.Add(widnow1);
                ContentPlaceHolder mpContentPlaceHolder;
                mpContentPlaceHolder =
                 (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                mpContentPlaceHolder.Controls.Add(widnow1);
            }

            if (e.CommandName == "printgovahi")
            {



                this.StiWebViewer1.ResetReport();
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Session["stcode"] = commandArgs[0];
                Session["RequestTypeID"] = commandArgs[1];
                Session["StudentRequestID"] = commandArgs[2];
                Session["Erae_Be"] = commandArgs[3];

                var studentRegisterInCurrentTerm = GovahiBusiness.GetStRegisterd(Session["stcode"].ToString());
                

                var studentsInfo = CartBusiness.GetStudentsInfo(Session["stcode"].ToString());
                
                var studentRequest = GovahiBusiness.GetRequestByRequestID(int.Parse(Session["StudentRequestID"].ToString()));

                var currentTerm = unibusiness.GetTermJary();

                var mashmulNumber = studentRequest?.Rows[0]["MashmulNumber"].ToString();


                TextBox txt_EraeBe = (TextBox)e.Item.FindControl("txt_EraeBe");
                if (studentRegisterInCurrentTerm.Rows.Count == 0)
                {

                    CartBusiness.UpdateStudentRequestLogID(Session["stcode"].ToString(), 5, 3, int.Parse(Session["StudentRequestID"].ToString()));
                    GovahiBusiness.UpdateStudentPOstNumber(Session["stcode"].ToString(), " عدم ثبت نام در ترم جاری", 3, int.Parse(Session["StudentRequestID"].ToString()));
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 31,"ارائه به : " + txt_EraeBe.Text, Convert.ToInt32(Session["StudentRequestID"].ToString()));

                    GovahiBusiness.UpdateAmount(Convert.ToInt32(Session["StudentRequestID"]), int.Parse(Session["StudentRequestID"].ToString()));

                    rwm_Validations.RadAlert("به دلیل عدم ثبت نام در ترم جدید امکان چاپ گواهی وجود ندارد و درخواست مورد نظر رد می شود", 500, 100, "خطا", null);
                    grd_GovahiRequest.Rebind();
                    return;
                }
                else
                {
                    if (currentTerm.Rows[0]["termCode"].ToString() == studentRequest.Rows[0]["Term"].ToString())
                    {
                        CartBusiness.UpdateStudentRequestLogID(Session["stcode"].ToString(), 7, 3, int.Parse(Session["StudentRequestID"].ToString()));
                        cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 33, "ارائه به : " + txt_EraeBe.Text, Convert.ToInt32(Session["StudentRequestID"].ToString()));
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Reports/eshteghal.mrt"));
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Request].[SP_Get_StudentInfo]"].Parameters["@stcode"].ParameterValue = Session["stcode"].ToString();
                        rpt.CompiledReport.DataSources["[Request].[SP_GetRequestByRequestID]"].Parameters["@StudentRequestID"].ParameterValue = int.Parse(Session["StudentRequestID"].ToString());
                        rpt.RegData(studentsInfo);
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;

                    }
                    else
                    {


                        var requestID = int.Parse(business.InsertInToStudentRequest(Session["stcode"].ToString(), 3, 7, Session["Erae_Be"].ToString(), mashmulNumber, 1).ToString());


                        CartBusiness.UpdateStudentRequestLogID(Session["stcode"].ToString(), 5, 3, int.Parse(Session["StudentRequestID"].ToString()));
                        GovahiBusiness.UpdatePayment(Convert.ToInt32(Session["StudentRequestID"]), requestID);
                        GovahiBusiness.UpdateStudentPOstNumber(Session["stcode"].ToString(), " عدم ثبت نام در ترم جاری", 3, int.Parse(Session["StudentRequestID"].ToString()));
                        cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 31, "ارائه به : " + txt_EraeBe.Text, Convert.ToInt32(Session["StudentRequestID"].ToString()));

                        rwm_Validations.RadAlert("درخواست مورد نظر به دلیل ثبت در ترم قبل رد و درخواست جدیدی در ترم جاری ثبت می گردد همچنین دانشجو در ترم جاری ثبت نام کرده است", 400, 100, "خطا", null);
                      
                        


                        CartBusiness.UpdateStudentRequestLogID(Session["stcode"].ToString(), 7, 3, requestID);
                        cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 33, "ارائه به : " + txt_EraeBe.Text, Convert.ToInt32(Session["StudentRequestID"].ToString()));
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Reports/eshteghal.mrt"));
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Request].[SP_Get_StudentInfo]"].Parameters["@stcode"].ParameterValue = Session["stcode"].ToString();
                        rpt.CompiledReport.DataSources["[Request].[SP_GetRequestByRequestID]"].Parameters["@StudentRequestID"].ParameterValue = requestID;
                        rpt.RegData(studentsInfo);
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                    }
                }



            }
            if (e.CommandName == "Edit_EraeBe")
            {
                Session["StudentRequestID"] = e.CommandArgument.ToString();
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    TextBox txt_EraeBe = e.Item.FindControl("txt_EraeBe") as TextBox;
                    Session["EraeBe"] = txt_EraeBe.Text;
                    GovahiBusiness.UpdateEraeBe(Session["EraeBe"].ToString(), int.Parse(Session["StudentRequestID"].ToString()));
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 34, "ارائه به : " + txt_EraeBe.Text, Convert.ToInt32(Session["StudentRequestID"].ToString()));
                }
            }
        }
        /// <summary> 
        /// برای اعمال تغییرات در ردیف های مختلف گیریدویو از این متد استفاده می نماییم
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridItemEventArgs"/> instance containing the event data.</param>
        protected void grd_GovahiRequest_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                Button btn_govahi = e.Item.FindControl("btn_Govahi") as Button;
                Button btn_Rad = e.Item.FindControl("btn_Rad") as Button;
                TextBox txt_EraeBe = e.Item.FindControl("txt_EraeBe") as TextBox;
                Button btn_Edit = e.Item.FindControl("btn_Edit") as Button;
                DataTable st = new DataTable();
                dt = GovahiBusiness.GetGovahiRequest(7, 6);
                btn_Edit.CommandArgument = dt.Rows[e.Item.ItemIndex]["StudentRequestId"].ToString();
                btn_govahi.CommandArgument = dt.Rows[e.Item.ItemIndex]["stcode"].ToString() + "," + dt.Rows[e.Item.ItemIndex]["RequestTypeID"].ToString() + "," + dt.Rows[e.Item.ItemIndex]["StudentRequestId"].ToString() + "," + dt.Rows[e.Item.ItemIndex]["Erae_Be"].ToString();
                btn_Rad.CommandName = "radedarkhast";
                btn_Rad.CommandArgument = dt.Rows[e.Item.ItemIndex]["stcode"].ToString() + "," + dt.Rows[e.Item.ItemIndex]["StudentRequestId"].ToString();
                var items = (GridDataItem)e.Item;
                txt_EraeBe.Text = dt.Rows[e.Item.ItemIndex]["Erae_Be"].ToString();
                if (dt.Rows[e.Item.ItemIndex]["nezam"].ToString() == "7" && (dt.Rows[e.Item.ItemIndex]["MashmulNumber"].ToString() == "" || dt.Rows[e.Item.ItemIndex]["MashmulTarikh"].ToString() == "") && dt.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() != "1")
                {
                    items.BackColor = Color.Bisque;
                    btn_govahi.Enabled = false;
                }
                if (dt.Rows[e.Item.ItemIndex]["nezam"].ToString() == "7" && (dt.Rows[e.Item.ItemIndex]["MashmulNumber"].ToString() == "" || dt.Rows[e.Item.ItemIndex]["MashmulTarikh"].ToString() == "") && dt.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() == "1")
                {
                    items.BackColor = Color.Bisque;
                }
                if (dt.Rows[e.Item.ItemIndex]["nezam"].ToString() != "7" && dt.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() != "1")
                {
                    btn_govahi.Enabled = true;
                }
                if (dt.Rows[e.Item.ItemIndex]["nezam"].ToString() == "7" && (dt.Rows[e.Item.ItemIndex]["MashmulNumber"].ToString() == "" || dt.Rows[e.Item.ItemIndex]["MashmulTarikh"].ToString() == "") && dt.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() == "1")
                {
                    items.BackColor = Color.Bisque;
                }
                if (dt.Rows[e.Item.ItemIndex]["nezam"].ToString() == "7" && dt.Rows[e.Item.ItemIndex]["MashmulNumber"].ToString() != "" && dt.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() != "1")
                {
                    items.BackColor = Color.Bisque;
                    btn_govahi.Enabled = true;

                }
                if (dt.Rows[e.Item.ItemIndex]["nezam"].ToString() == "7" && dt.Rows[e.Item.ItemIndex]["MashmulTarikh"].ToString() != "" && dt.Rows[e.Item.ItemIndex]["MashmulNumber"].ToString() != "" && dt.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() == "1")
                {
                    items.BackColor = Color.Bisque;
                    btn_govahi.Enabled = true;
                }
                if (dt.Rows[e.Item.ItemIndex]["nezam"].ToString() == "7" && dt.Rows[e.Item.ItemIndex]["MashmulTarikh"].ToString() != "" && dt.Rows[e.Item.ItemIndex]["MashmulNumber"].ToString() != "" && dt.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() != "1")
                {
                    items.BackColor = Color.Bisque;
                    btn_govahi.Enabled = true;
                }
                if (dt.Rows[e.Item.ItemIndex]["nezam"].ToString() == "7" && dt.Rows[e.Item.ItemIndex]["MashmulTarikh"].ToString() != "" && dt.Rows[e.Item.ItemIndex]["RequestLogID"].ToString() != "1")
                {
                    items.BackColor = Color.Bisque;
                    btn_govahi.Enabled = true;
                }
                if (dt.Rows[e.Item.ItemIndex]["nezam"].ToString() == "7")
                {
                    items.BackColor = Color.FromName("#A173FF");
                }
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {

                DataTable dt = new DataTable();
                dt = GovahiBusiness.GetGovahiRequest(7, 6);
                grd_GovahiRequest.MasterTableView.Dispose();
                grd_GovahiRequest.DataSource = dt;
                grd_GovahiRequest.DataBind();
            }
        }

        protected void grd_GovahiRequest_NeedDataSource1(object sender, GridNeedDataSourceEventArgs e)
        {
            dt = GovahiBusiness.GetGovahiRequest(7, 6);
            if (dt.Rows.Count > 0)
            {
                grd_GovahiRequest.DataSource = dt;
            }
            else
            {
                grd_GovahiRequest.Visible = false;
                rwmValidations.RadAlert("درخواستی موجود نیست", null, 100, "پیغام", "");
                StiWebViewer1.Visible = false;
            }
            GridFilterMenu menu = grd_GovahiRequest.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }

                }
            }
        }
    }
}

