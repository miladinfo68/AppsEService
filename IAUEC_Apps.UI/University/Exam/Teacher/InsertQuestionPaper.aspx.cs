using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.university.Exam;
using System.IO;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using System.Text;
using System.Drawing;
using System.Configuration;

namespace IAUEC_Apps.UI.University.Exam.Teacher
{
    public partial class InsertQuestionPaper : System.Web.UI.Page
    {
        ExamBusiness eBusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        string Exam_Term =ConfigurationManager.AppSettings["Exam_Term"];

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");


            //if (!string.IsNullOrEmpty(Request.Form["tokenField"]))
            //{

            //}
        }

        protected void grd_Dars_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var dt = eBusiness.GetDidByCodeOstad(int.Parse(Session[sessionNames.userID_StudentOstad].ToString()));
            grd_Dars.DataSource = dt;
        }

        protected void grd_Dars_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var cmdName = e.CommandName;
            var cmdArgs = e.CommandArgument.ToString().Split('|');
            if (cmdName == "RebindGrid") return;


            var did = cmdArgs[0].ToString();
            var dateexam = cmdArgs[1].ToString();
            var saatexam = cmdArgs[2].ToString();
            int? cityId = null;
            var qID = cmdArgs[4].ToString();
            var statusQ2 = cmdArgs[5].ToString();
            var statusQ1 = int.Parse(cmdArgs[6].ToString());
            if (!string.IsNullOrEmpty(cmdArgs[3].ToString()))
                cityId = int.Parse(cmdArgs[3].ToString());

            if (cmdName == "Option")
            {
                if ((statusQ1 == 1 || statusQ1 == 2))
                {
                    var msg = "";
                    if (statusQ1 == 1)
                        msg = "کاربر محترم شما قبلا در زبانه ای دیگر شرایط آزمون را تعیین نموده اید لطفا پنجره مرورگر خود را بروز کرده یا ببندید";
                    if (statusQ1 == 2)
                        msg = "کاربر محترم شما قبلا در زبانه ای دیگر  تاعملیات دانلود یا آپلود سوالات پیش رفته اید لطفا پنجره مرورگر خود را بروز کرده یا ببندید";

                    rwm.RadAlert(msg, null, 100, "پیام", "");
                    return;
                }
                RadWindowManager windowManager = new RadWindowManager();
                RadWindow widnow1 = new RadWindow();
                widnow1.NavigateUrl = "../Teacher/InsertExamOption.aspx?did=" + did + "&examTime=" + saatexam + "&examDate=" + dateexam + "&cityId=" + cityId + "&qID=" + qID + "&statusQ2=" + statusQ2 + "&statusQ1=" + statusQ1.ToString();
                widnow1.ID = "RadWindow1";
                windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(800);
                windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(600);
                widnow1.VisibleOnPageLoad = true;
                windowManager.Windows.Add(widnow1);
                ContentPlaceHolder mpContentPlaceHolder;
                mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                mpContentPlaceHolder.Controls.Add(widnow1);

            }
            if (cmdName == "Upload")
            {
               
                if (statusQ1 == 2)
                {
                    //Response.Redirect(Request.RawUrl);
                    rwm.RadAlert("کاربر محترم شما قبلا در زبانه دیگر عملیات بار گذاری  سوال را انجام داده اید لطفا پنجره مرورگر خود را بروز کرده یا ببندید", null, 100, "پیام", "");
                    return;
                }
                int? cityIDQ2 = null;
                if (statusQ2 != "-1" && statusQ1 == 3)
                    cityIDQ2 = cityId;
                var dtpas = eBusiness.GetExamQuestionsbyDid(did, null, cityIDQ2);               
                if (bool.Parse(dtpas.Rows[0]["TemplateDownloaded"].ToString()) == true)
                {
                    RadWindowManager windowManager = new RadWindowManager();
                    RadWindow widnow1 = new RadWindow();
                    widnow1.NavigateUrl = "../Teacher/UploadExamQuestion.aspx?did=" + did + "&term=" + Exam_Term + "&ostad=" + Session[sessionNames.userID_StudentOstad].ToString();
                    widnow1.ID = "RadWindow1";
                    windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(800);
                    windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(700);
                    widnow1.VisibleOnPageLoad = true;
                    windowManager.Windows.Add(widnow1);
                    ContentPlaceHolder mpContentPlaceHolder;
                    mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                    mpContentPlaceHolder.Controls.Add(widnow1);
                }
                else
                {
                    rwm.RadAlert("لطفا ابتدا فرم سوالات را دانلود نمایید", null, 100, "خطا", "");
                }
            }

            if (cmdName == "Editing")
            {
                if (statusQ1 != 3 && statusQ1 != 4)
                {
                    rwm.RadAlert("کاربر محترم شما قبلا در زبانه دیگر عملیات بار گذاری مجدد سوال را انجام داده اید لطفا پنجره مرورگر خود را بروز کرده یا ببندید", null, 100, "پیام", "");
                    return;
                }
                RadWindowManager windowManager = new RadWindowManager();
                RadWindow widnow1 = new RadWindow();
                widnow1.NavigateUrl = "../Teacher/EditQuestionPaper.aspx?did=" + did + "&examTime=" + saatexam + "&examDate=" + dateexam + "&cityId=" + cityId + "&qID=" + qID + "&statusQ2=" + statusQ2 + "&statusQ1=" + statusQ1.ToString();              
                widnow1.ID = "RadWindow1";
                windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(800);
                windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(700);
                widnow1.VisibleOnPageLoad = true;
                windowManager.Windows.Add(widnow1);
                ContentPlaceHolder mpContentPlaceHolder;
                mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                mpContentPlaceHolder.Controls.Add(widnow1);
            }
            if (cmdName == "DL")
            {
                if (statusQ1 == 2)
                {
                    //Response.Redirect(Request.RawUrl);
                    rwm.RadAlert("کاربر محترم شما قبلا در زبانه دیگر عملیات دانلود سوال را انجام داده اید لطفا پنجره مرورگر خود را بروز کرده یا ببندید", null, 100, "پیام", "");
                    return;
                }
                int? cityIDQ2 = null;
                if (statusQ2 != "-1" && statusQ1 == 3)
                    cityIDQ2 = cityId;
                var dtQ = eBusiness.ShowQueizPaperByDid(did, cityIDQ2);
                string calc, Note, book = "";
                if (dtQ.Rows[0]["Calculator"].ToString() == "True") calc = "می باشد";
                else calc = "نمی باشد";
                if (dtQ.Rows[0]["Note"].ToString() == "True") Note = "باز می باشد";
                else Note = "بسته می باشد";
                if (dtQ.Rows[0]["LawBook"].ToString() == "True")
                {
                    book = "<tr><td  colspan='2' style='font-size: 11px;font-weight:bold;width:50%;padding: 2px 15px 2px 5px'>استفاده از کتاب قانون مجاز می باشد";
                    if (dtQ.Rows[0]["BookName"].ToString() != "")
                        book += "***" + dtQ.Rows[0]["BookName"].ToString() + "***</td></tr>";
                }
                else
                {
                    if (eBusiness.GetIdgroupBydid(did))
                        book = "<tr><td  colspan='2' style='font-size: 11px;font-weight:bold;width:50%;padding: 2px 15px 2px 5px'>استفاده از کتاب قانون مجاز نمی باشد";
                }

                var dt = eBusiness.Get_ExamdetailbyDid(did, null, cityIDQ2);
                if (dt == null || dt.Rows.Count == 0)
                {
                    Response.Redirect(Request.RawUrl);                    
                }
                //=========== make random code for questions
                //var quizSheetCode = new Random().Next(0, 999999).ToString("D6");
                eBusiness.TemplateDownloaded(did, true);
                //=========== 

                string term = dt.Rows[0]["tterm"].ToString().Substring(0, 5);
                string vorudi = dt.Rows[0]["tterm"].ToString().Substring(dt.Rows[0]["tterm"].ToString().Length - 1, 1);
                if (vorudi == "1") vorudi = "اول";
                if (vorudi == "2") vorudi = "دوم";
                string[] trm = term.ToString().Split(new char[] { '-' });
                string[] exdate = dt.Rows[0]["dateexam"].ToString().Split(new char[] { '/' });


                string content = "<body dir='rtl' style='margin-right:7px'> " +
                    "<table style='border-style: solid; border-width: 1px; width:99%; margin:10px; border-spacing: 0px;border-collapse: collapse;' border='1' dir='rtl' >" +
                    "<tr><td style='width:90%;padding-left:20px; padding-top: 0px;'>" +
                "<table style='border-style: solid; border-width: 1px; width:100%; margin-right:20px; margin-left:10px; margin-bottom:15px;border-spacing:0px; font-family: tahoma; font-size: 10px;border-collapse: collapse;' border='1' dir='rtl'>" +

                "<tr>" +
                "<td style='padding: 2px 5px 2px 5px'> امتحانات پایان ترم نیمسال : " + "<span style='font-weight:bold'>" + vorudi + "  سال تحصیلی  " + "<span style='font-weight:bold'>" + trm[1] + "</span><span style='font-weight:bold'>" + "-" + "</span><span sstyle='font-weight:bold'>" + trm[0] + "</span>" + "</td>" +
                "<td style='padding: 2px 5px 2px 5px'> کد برگه آزمون : <span style='font-weight:bold'></span></td>" +
                "</tr>" +

                "<tr>" +
                "<td style='padding: 2px 5px 2px 5px'>عنوان درس : " + "<span style='font-weight:bold'>" + dt.Rows[0]["namedars"].ToString() + "</span>" + "</td>" +
                "<td style='padding: 2px 5px 2px 5px'>مدت زمان برگزاری امتحان : " + "<span style='font-weight:bold'>" + dtQ.Rows[0]["ExamTime"].ToString() + " دقیقه " + "</span>" + "</td></tr>" +

                "<tr>" +
                "<td style='padding: 2px 5px 2px 5px'>نام و نام خانوادگی استاد : " + "<span style='font-weight:bold'>" + dt.Rows[0]["osname"].ToString() + "</span></td>" +
                "<td style='padding: 2px 5px 2px 5px'>شماره دانشجویی : " + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td style='padding: 2px 5px 2px 5px'>تاریخ برگزاری امتحان : " + "<span style='font-weight:bold;text-align: right' dir='rtl'>" + exdate[2] + "</span>/<span style='font-weight:bold;text-align: right' dir='rtl'>" + exdate[1] + "</span>/<span style='font-weight:bold;text-align: right' dir='rtl'>" + exdate[0] + "</span>" + "</td>" +
                "<td style='padding: 2px 5px 2px 5px'>نام دانشجو : " + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td style='padding: 2px 5px 2px 5px'>ساعت برگزاری امتحان : " + "<span style='font-weight:bold'>" + dt.Rows[0]["saatexam"].ToString() + "</span>" + "</td>" +
                "<td style='padding: 2px 5px 2px 5px'> نام خانوادگی دانشجو : " + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td style='padding: 2px 5px 2px 5px'> رشته تحصیلی : " + "<span style='font-weight:bold'>" + dt.Rows[0]["nameresh"].ToString() + "</span>" + "</td>" +
                "<td style='padding: 2px 5px 2px 5px'>مقطع تحصیلی  : " + "<span style='font-weight:bold'>" + dt.Rows[0]["magh"].ToString() + "</span>" + "</td>" +
                "</tr>" +
                "</table>" +

                "<table style='width:100%;font-family:Tahoma; font-size: small; margin-right: 15px;'>" +
                "<tr><td style='font-size: 10px;width:50%;padding: 2px 15px 2px 5px'>" + "مجاز به استفاده از ماشین حساب" + " " + "<span style='font-weight:bold'>" + calc + "</span>" + "</td>" +
                "<td style='font-size: 10px;width:50%'>" + " امتحان جزوه " + "<span style='font-weight:bold'>" + Note + "</span>" + "</td>" +
                "</tr>" +
                book +
                "</table></td><td style='text-align: center; padding-top: 5px; padding-bottom: 5px;'>" +
                "<img width='100' height='130' src='http://service.iauec.ac.ir/University/Theme/images/Azad_University_logo.png' />" +
                "<p style=' font-family: iranNastaliq; text-align: center; color: #647ED1; padding-top: -3px; margin-top: -3px;'>واحد الکترونیکی</p>" +
                "</td></tr>" +
                "</table>" +
                "<table style='border-style: solid; border-width: 1px; width:99%; margin:10px; margin-top:50px; border-spacing: 0px; border-collapse: collapse;' border='1' dir='rtl' >" +
                // "<tr><td style='width:93%; width:74vh;' ></td><td style='font-family: tahoma; font-size: 10px; text-align: center;width:7%'>بارم نمرات</td></tr>" +
                "<tr><td style='width:93%;width:74vh;font-family: 'b nazanin';font-size: 12px'><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/></td><td style='font-family: tahoma; font-size: 10px; text-align: center;width:7%'>&nbsp;</td></tr>" +
                "</table></body>";


                RadEditor1.Content = content;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Charset = "PERSIAN_CHARTSET";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-word";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                string strFileName = "ExamTemplate" + "-" + dt.Rows[0]["coursecode"].ToString() + ".doc";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
                StringBuilder strHTMLContent = new StringBuilder();
                strHTMLContent.Append(RadEditor1.Content);
                HttpContext.Current.Response.Write(strHTMLContent);
                HttpContext.Current.Response.End();
                HttpContext.Current.Response.Flush();
            }

        }



        protected void grd_Dars_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                Button btn_Option = e.Item.FindControl("btn_Option") as Button;
                Button btn_Upload = e.Item.FindControl("btn_Upload") as Button;
                Button btn_Editing = e.Item.FindControl("btn_Editing") as Button;
                Button btn_DlForm = e.Item.FindControl("btn_DlForm") as Button;

                Label lbl_Status = e.Item.FindControl("lbl_Status") as Label;
                Label lbl_Reject = e.Item.FindControl("lbl_Reject") as Label;

                var allOStadDids = eBusiness.GetDidByCodeOstad(int.Parse(Session[sessionNames.userID_StudentOstad].ToString()));
                if (allOStadDids != null && allOStadDids.Rows.Count > 0)
                {
                    var qId = allOStadDids.Rows[e.Item.ItemIndex]["examQuestionID"].ToString();
                    var did = allOStadDids.Rows[e.Item.ItemIndex]["did"].ToString();
                    var cityId = allOStadDids.Rows[e.Item.ItemIndex]["cityId"].ToString();
                    var statusQ1 = allOStadDids.Rows[e.Item.ItemIndex]["status"].ToString();
                    var statusQ2 = allOStadDids.Rows[e.Item.ItemIndex]["statusQ2"].ToString();
                    var EXAM_DATE = allOStadDids.Rows[e.Item.ItemIndex]["date"].ToString().Trim();
                    var saat = allOStadDids.Rows[e.Item.ItemIndex]["saat"].ToString();

                    int? cityIDQ2 = null;
                    if (statusQ2 != "-1" && statusQ1 == "3")
                        cityIDQ2 = int.Parse(cityId);                  

                    if (qId == "-1" )//&& statusQ1 == "-1"
                    {
                        lbl_Status.Visible = true;
                        lbl_Status.Text = "سوالات آپلود نشده است";
                        lbl_Reject.Visible = false;

                        btn_Option.Enabled = true;
                        btn_Option.Visible = true;
                        btn_Option.BackColor = Color.FromArgb(0, 200, 83);

                        btn_DlForm.Enabled = false;
                        btn_DlForm.Visible = true;

                        btn_Upload.Enabled = false;
                        btn_Upload.Visible = true;

                        btn_Editing.Enabled = false;
                        btn_Editing.Visible = false;   
                    }
                    else
                    {
                        var dtexam = eBusiness.GetExamQuestionsbyDid(did, null, cityIDQ2);
                        if (string.IsNullOrEmpty(EXAM_DATE) || EXAM_DATE.ToString().Trim().Length < 8)
                        {
                            btn_DlForm.Visible = false;
                            btn_Editing.Visible = false;
                            btn_Option.Visible = false;
                            btn_Upload.Visible = false;

                        }
                        else if (statusQ1 == "1")
                        {
                            lbl_Status.Visible = true;
                            lbl_Status.Text = "سوالات آپلود نشده است";
                            lbl_Reject.Visible = false;
                            btn_Option.Enabled = false;
                            btn_Upload.Enabled = false;
                            btn_Upload.Enabled = true;
                            btn_Upload.BackColor = Color.FromArgb(0, 200, 83);
                            btn_Editing.Enabled = false;
                            btn_Editing.Visible = false;
                            btn_Editing.BackColor = Color.FromArgb(0, 200, 83);
                            btn_DlForm.Enabled = true;
                            btn_DlForm.Visible = true;
                            btn_DlForm.BackColor = Color.FromArgb(0, 200, 83);
                        }                       
                        else if ((statusQ1 == "3" && (statusQ2 == "8" || statusQ2 == "11")))
                        {
                            lbl_Status.Visible = true;
                            lbl_Status.Text = "سوالات آپلود نشده است";

                            lbl_Reject.Visible = true;
                            lbl_Reject.Text = dtexam != null && dtexam.Rows.Count > 0 ? dtexam.Rows[0]["rejectReason"].ToString() ?? "" : "";

                            btn_Option.Visible = false;
                            btn_Upload.Visible = false;
                            btn_Upload.Visible = false;
                            btn_DlForm.Visible = false;

                            btn_Editing.Enabled = true;
                            btn_Editing.Visible = true;
                            btn_Editing.BackColor = Color.FromArgb(0, 200, 83);
                        }
                        else if ((statusQ1 == "3" && statusQ2 == "9") || statusQ1 == "2")
                        {
                            lbl_Status.Visible = true;
                            lbl_Status.Text = "در حال بررسی";
                            lbl_Reject.Visible = false;
                            btn_Option.Enabled = false;
                            btn_Option.Visible = false;
                            btn_Upload.Visible = false;
                            btn_Editing.Enabled = false;
                            btn_Editing.Visible = false;
                            btn_DlForm.Visible = false;
                        }

                        else if ((statusQ1 == "3" && statusQ2 == "10") || statusQ1 == "3")
                        {
                            lbl_Status.Visible = true;
                            lbl_Status.Text = "تایید شده است";
                            lbl_Reject.Visible = false;
                            btn_Option.Enabled = false;
                            btn_Option.Visible = false;
                            btn_Upload.Visible = false;
                            btn_Editing.Enabled = false;
                            btn_Editing.Visible = false;
                            btn_DlForm.Visible = false;
                        }

                        else if ((statusQ1 == "3" && statusQ2 == "11") || statusQ1 == "4")
                        {
                            lbl_Status.Visible = true;
                            lbl_Status.Text = "رد شده است";
                            lbl_Reject.Visible = true;
                            lbl_Reject.Text = dtexam != null && dtexam.Rows.Count > 0 ? dtexam.Rows[0]["RejectDesc"].ToString() ?? "" : "";
                            btn_Option.Visible = false;
                            btn_Upload.Visible = false;
                            btn_Editing.Enabled = true;
                            btn_Editing.Visible = true;
                            btn_Editing.BackColor = Color.FromArgb(0, 200, 83);
                            btn_DlForm.Visible = false;
                        }
                        else if (statusQ1 == "5")
                        {
                            lbl_Status.Visible = true;
                            lbl_Status.Text = "تجمیع اوراق";
                            lbl_Reject.Visible = false;
                            btn_Option.Enabled = false;
                            btn_Option.Visible = false;
                            btn_Upload.Visible = false;
                            btn_Editing.Enabled = false;
                            btn_Editing.Visible = false;
                            btn_DlForm.Visible = false;
                        }
                        else if (statusQ1 == "6")
                        {
                            lbl_Status.Visible = true;
                            lbl_Status.Text = "تحویل اوراق";
                            lbl_Reject.Visible = false;
                            btn_Option.Enabled = false;
                            btn_Option.Visible = false;
                            btn_Upload.Visible = false;
                            btn_Editing.Enabled = false;
                            btn_Editing.Visible = false;
                            btn_DlForm.Visible = false;
                        }
                        else if (statusQ1 == "7")
                        {
                            lbl_Status.Visible = true;
                            lbl_Status.Text = "دریافت اوراق";
                            lbl_Reject.Visible = false;
                            btn_Option.Enabled = false;
                            btn_Option.Visible = false;
                            btn_Upload.Visible = false;
                            btn_Editing.Enabled = false;
                            btn_Editing.Visible = false;
                            btn_DlForm.Visible = false;
                        }
                    }
                    //else
                    //{
                    //    rwm.RadAlert("هیچ درسی موجود نیست", null, 100, "پیام", "");
                    //}
                }
            }
        }
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {

                DataTable dt = new DataTable();
                dt = eBusiness.GetDidByCodeOstad(int.Parse(Session[sessionNames.userID_StudentOstad].ToString()));
                grd_Dars.DataSource = dt;
                grd_Dars.DataBind();
            }
        }

        protected void btnPostToLMS_Click(object sender, EventArgs e)
        {
            byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            var x = Encoding.ASCII.GetString(iv).Trim();

            //var profInfo = eBusiness.GetProfessorInfoByProfessorCode(Session[sessionNames.userID_StudentOstad].ToString());
            //if (profInfo.Rows.Count > 0)
            //{
            //    //string key = "b3afc5fd20e3637160da4f9cab6c8072";
            //    string key = ConfigurationManager.AppSettings["ExamLinkKey"].ToString();
            //    //string IV = "a214ee38a470c5974c10498b7152ca39";
            //    string IV = ConfigurationManager.AppSettings["ExamLinkIV"].ToString();
            //    //var url = "http://localhost:48015//University/Exam/Teacher/InsertQuestionPaper.aspx";
            //    var url = ConfigurationManager.AppSettings["ExamLink"].ToString();

            //    var token = EncryptionClass.EncryptAES256(Session[sessionNames.userID_StudentOstad].ToString() + ";" + profInfo.Rows[0]["idd_meli"].ToString(), key, IV) + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    var script = "var form = document.createElement('form'); var tokenField = document.createElement('input'); form.method = 'POST'; form.action = '" + url + "';";
            //    script += "tokenField.value='" + token + "'; tokenField.name='tokenField'; form.appendChild(tokenField); document.body.appendChild(form); form.submit();";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "submitform", script, true);
            //}

            //var token = ConfigurationManager.AppSettings["ExamGetTokenURL"].ToString() + Session[sessionNames.userID_StudentOstad].ToString();
            //Response.Redirect(ConfigurationManager.AppSettings["ExamURL"].ToString() + token);
        }



    }
}